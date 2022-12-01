using System;
using System.Collections;
using Models.Constants;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using Utilities;

namespace Infrastructure
{
    public class TimeController : Singleton<TimeController>
    {
        private float _timer;
        private float _syncTimer;

        //ProTODO: Check If GameTime Needed To Be Synced With RealTime
        public DateTime GameTime { get; set; } = DateTime.Now;
        public DateTime GamePlayTime { get; set; } = DateTime.Now;
        public UnityEvent TimeChangedEvent { get; set; } = new UnityEvent();
        private void Awake()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                StartCoroutine(GetInternetTime((isSuccess, timeString) =>
                {
                    if (isSuccess)
                    {
                        DateTime dateTime;
                        if (DateTime.TryParse(timeString, out dateTime))
                        {
                            GameTime = dateTime;
                        }
                        else
                        {
                            GameTime = DateTime.Now;
                        }
                    }
                    else
                    {
                        GameTime = DateTime.Now;
                    }
                }));
            }
            else
            {
                GameTime = DateTime.Now;
            }
            //GameTime = DateTime.Now - TimeSpan.FromDays(16);
            _timer = StaticValues.TimerUpdateInterval;
        }
        private void Update()
        {
            if ((_timer -= Time.deltaTime) <= 0)
            {
                _timer = StaticValues.TimerUpdateInterval;
                GameTime = GameTime.AddSeconds(StaticValues.TimerUpdateInterval);
                GamePlayTime = GamePlayTime.AddSeconds(GameManager.Instance.IsPaused ? 0 : StaticValues.TimerUpdateInterval);
                TimeChangedEvent?.Invoke();
            }

            if ((_syncTimer -= Time.deltaTime) <= 0)
            {
                _syncTimer = StaticValues.SyncTimerUpdateInterval;
                if (Application.internetReachability != NetworkReachability.NotReachable)
                {
                    StartCoroutine(GetInternetTime((isSuccess, timeString) =>
                    {
                        if (isSuccess)
                        {
                            DateTime dateTime;
                            if (DateTime.TryParse(timeString, out dateTime))
                            {
                                Debug.LogWarning(dateTime);
                                GameTime = dateTime;
                            }
                        }
                    }));
                }
            }
        }
        public void GetRealTimeDate(Action<DateTime?, string> callback)
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                StartCoroutine(GetInternetTime((isSuccess, timeString) =>
                {
                    if (isSuccess)
                    {
                        DateTime dateTime;
                        if (DateTime.TryParse(timeString, out dateTime))
                        {
                            //Debug.LogError(dateTime);
                            GameTime = dateTime;
                            callback(dateTime, string.Empty);
                        }
                        else
                        {
                            callback(null, StaticValues.ErrorMessage(101));
                        }
                    }
                    else
                    {
                        callback(null, StaticValues.ErrorMessage(102));
                    }
                }));
            }
            else
            {
                callback(null, StaticValues.ErrorMessage(201));
            }
        }
        private IEnumerator GetInternetTime(Action<bool, string> callback)
        {
            using (var unityWebRequest = UnityWebRequest.Get(StaticValues.TimeApiUrl))
            {
                yield return unityWebRequest.SendWebRequest();

                if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
                {
                    callback(false, unityWebRequest.error);
                }
                else
                {
                    callback(true, unityWebRequest.downloadHandler.text);
                }
            }
        }
    }
}

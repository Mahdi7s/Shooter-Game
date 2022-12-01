using System;
using DataServices;
using Hellmade.Sound;
using Infrastructure;
using Models.Constants;
using Spine.Unity;
using UnityEngine;
using System.Collections;

namespace Extensions
{
    public static class SkeletonAnimationExtensions
    {
        public static void PlayAnimation(this SkeletonAnimation animation, EnemyController character, AnimationNames state, bool hasRandomDelayOnSound = false)
        {
            animation.AnimationName = state.ToString();
            if (!character)
                return;
            if (character.IsAlreadyPlayingSound)
                return;

            //Coroutine currentCoroutine = null;
            var randomToken = new System.Random().Next(1, 100);
            Debug.LogWarning(randomToken);
            var shouldPlaySound = randomToken % 2 == 0 && randomToken > 80;
            if (state != AnimationNames.Walk && state != AnimationNames.Stand)
                shouldPlaySound = true;
            if (!shouldPlaySound || (character.IsDead && state != AnimationNames.Die))
                return;

           
            if (hasRandomDelayOnSound || state == AnimationNames.Walk || state == AnimationNames.Stand)
            {
                character.StartCoroutine(DelayedCallback(new System.Random().Next(0, 2), () =>
                {
                    PlayRandomSound(character, state);
                }, character));
            }
            else
            {
                PlayRandomSound(character, state);
            }
            //if (!shouldPlaySound || (character.IsDead && state != AnimationNames.Die))
            //{
            //    if (currentCoroutine!=null)
            //        character.StopCoroutine(currentCoroutine);
            //    return;
            //}



        }

        private static void PlayRandomSound(EnemyController character, AnimationNames state)
        {
            if (!character)
                return;
            if (character.IsDead && state != AnimationNames.Die)
            {
                character.IsAlreadyPlayingSound = false;
                Debug.LogError("non-die animation sound playback canceled because character is dead");
                return;
            }

            var animationData = GameDataService.Instance.GetAnimationData(character.CharacterType, state);
            if (animationData != null)
            {
                var randomIndex = new System.Random().Next(0, animationData.AudioClips.Count);
                character.IsAlreadyPlayingSound = true;
                //Debug.LogError($"{TrixSoundManager.Instance.ToString()}-{animationData.AudioClips[randomIndex]}-{character}-{state}-{randomIndex}-{playCallbackAction}");
                TrixSoundManager.Instance.StartPlay(animationData.AudioClips[randomIndex], Audio.AudioType.UiSound, finishedCallback:
                    () => { character.IsAlreadyPlayingSound = false; });
            }
        }

        public static void PlayAnimation(this SkeletonAnimation animation, EnemyController character, string state)
        {
            animation.AnimationName = state;
            AnimationNames animState;
            if (Enum.TryParse(state, out animState))
            {
                PlayAnimation(animation, character, animState);
            }


        }
        private static IEnumerator DelayedCallback(float timeout, Action callback, EnemyController character)
        {
            Debug.LogWarning($"character is dead: {character.IsDead}");
            if (!character || character.IsDead)
                yield break;
            yield return new WaitForSeconds(timeout);
            callback();
        }
    }
}

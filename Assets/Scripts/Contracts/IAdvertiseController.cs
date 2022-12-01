using System;
using Models;

namespace Contracts
{
    public interface IAdvertiseController
    {
        AdvertiseProvider AdvertiseProvider { get; set; }
        void ShowAdvertise(StatisticAdvertise advertise, Action<AdvertiseResult> result);
    }
}

using Models.Constants;

namespace Models
{
    public class AdvertiseResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public AdvertiseAction AdvertiseAction { get; set; }
        public int Reward { get; set; }
    }
}

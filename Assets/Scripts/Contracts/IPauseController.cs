namespace Contracts
{
    public interface IPauseController
    {
        void Pause();
        void Resume();
        bool IsPaused { get; set; }
    }
}

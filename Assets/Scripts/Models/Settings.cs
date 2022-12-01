namespace Models
{
    public class Settings
    {
        public float Sensitivity { get; set; } = 1;
        public bool Music { get; set; } = true;
        public bool Sfx { get; set; } = true;
        public bool Notification { get; set; } = true;
        
        public void SetSensitivity(float value)
        {
            if (value > 1)
                value = 1;

            if (value < 0)
                value = 0;

            Sensitivity = value;
        }
        public void ToggleMusic()
        {
            Music = !Music;
        }
        public void ToggleSfx()
        {
            Sfx = !Sfx;
        }
        public void ToggleNotification()
        {
            Notification = !Notification;
        }
    }
}

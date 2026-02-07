namespace AntiStrangulation
{
    public sealed class Config
    {
        public bool Debug { get; set; } = false;
        public bool DisableStrangulation { get; set; } = true;
        public bool DisableAutoSpawn { get; set; } = false;
        public bool RandomStopStrangulation { get; set; } = false;
        public float StaminaBalance { get; set; } = 0.03f;
    }
}

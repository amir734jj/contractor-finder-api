namespace Dal.Configs
{
    public class FtpServiceConfig
    {
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string Host { get; set; }

        public int Port { get; set; } = 21;

        public string Path { get; set; } = string.Empty;
    }
}
namespace LibraryManagement.Application.Configuration
{
    public record ApplicationConfig
    {
        public ReturnDaysConfig ReturnDaysConfig { get; set; }
        public MailServiceConfig MailServiceConfig { get; set; }
        public WorkerConfig WorkerConfig { get; set; }
    }
}

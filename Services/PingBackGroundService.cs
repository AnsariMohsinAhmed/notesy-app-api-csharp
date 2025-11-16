namespace notesy_api_c_sharp.Services
{
    public class PingBackGroundService : BackgroundService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<PingBackGroundService> _logger;

        public PingBackGroundService(IHttpClientFactory httpClientFactory, ILogger<PingBackGroundService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("https://localhost:7005/health", stoppingToken);
                    _logger.LogInformation($"Health status:- {response.StatusCode} at {DateTime.Now}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while hitting /health endpoint");
                }

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}

using BlogApplication.Common;
using BlogApplication.Common.Events.Post;
using BlogApplication.Common.Infrastructure;

namespace BlogApplication.Projections.FavoriteService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connStr = _configuration.GetConnectionString("BlogApplicationPostgresDb");

            var favoriteService = new Services.FavoriteService(connStr);


            QueueFactory.CreateBasicConsumer()
                .EnsureExchange(BlogConstants.FavoriteExchangeName)
                .EnsureQueue(BlogConstants.CreatePostFavoriteQueueName, BlogConstants.FavoriteExchangeName)
                .Receive<CreatePostFavoriteEvent>(fav =>
                {
                    favoriteService.CreatePostFavorite(fav).GetAwaiter().GetResult();
                    _logger.LogInformation($"Received PostId {fav.PostId}");
                })
                .StartConsuming(BlogConstants.CreatePostFavoriteQueueName);

        }
    }
}
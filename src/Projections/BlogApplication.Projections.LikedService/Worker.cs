using BlogApplication.Common.Events.Post;
using BlogApplication.Common.Infrastructure;
using BlogApplication.Common;

namespace BlogApplication.Projections.LikedService
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

            var likedService = new Services.LikedService(connStr);

            QueueFactory.CreateBasicConsumer()
                .EnsureExchange(BlogConstants.LikesExchangeName)
                .EnsureQueue(BlogConstants.CreatePostLikesQueueName, BlogConstants.LikesExchangeName)
                .Receive<CreatePostLikesEvent>(likes =>
                {
                    likedService.CreatePostLiked(likes).GetAwaiter().GetResult();
                    _logger.LogInformation($"Received PostId {likes.PostId}");
                })
                .StartConsuming(BlogConstants.CreatePostLikesQueueName);
        }
    }
}
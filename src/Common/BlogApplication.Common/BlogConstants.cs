using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Common
{
    public class BlogConstants
    {
        public const string RabbitMQHost = "localhost";
        public const string DefaultExchangeType = "direct";


        public const string UserExchangeName = "UserExchange";
        public const string UserEmailChangedQueueName = "UserEmailChangedQueue";
        public const string FavoriteExchangeName = "FavoriteExchange";
        public const string CreatePostFavoriteQueueName = "CreatePostFavoriteQueue";


        public const string LikesExchangeName = "LikesExchange";
        public const string CreatePostLikesQueueName = "CreatePostLikesQueue";

        public const string DeletePostFavoriteQueueName = "DeletePostFavoriteQueue";
        public const string DeletePostLikesQueueName = "DeletePostLikesQueue";

        public const string CreatePostCommentLikesQueueName = "CreatePostCommentLikesQueue";

        public const string DeletePostCommentLikesQueueName = "DeletePostCommentLikesQueue";
    }
}

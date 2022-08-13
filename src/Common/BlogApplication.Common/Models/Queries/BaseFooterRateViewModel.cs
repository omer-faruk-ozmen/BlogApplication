using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Common.Models.Queries
{
    public class BaseFooterRateViewModel
    {
        public LikedStatus LikedStatus { get; set; }

    }

    public class BaseFooterFavoriteViewModel
    {
        public bool IsFavorite { get; set; }
        public int FavoriteCount { get; set; }
    }

    public class BaseFooterRateFavoriteViewModel : BaseFooterFavoriteViewModel
    {
        public LikedStatus LikedStatus { get; set; }
    }
}

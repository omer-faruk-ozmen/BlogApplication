using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApplication.Api.Domain.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool EmailConfirmed { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<PostFavorite> PostFavorites { get; set; }
        public virtual ICollection<PostComment> PostComments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApplication.Infrastructure.Persistence.EntityConfigurations.Post;

public class PostEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.Post>
{
    public override void Configure(EntityTypeBuilder<Api.Domain.Models.Post> builder)
    {
        base.Configure(builder);
        
    }
}
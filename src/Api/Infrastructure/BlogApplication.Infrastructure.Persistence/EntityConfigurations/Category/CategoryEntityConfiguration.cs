using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApplication.Infrastructure.Persistence.EntityConfigurations.Category
{
    public class CategoryEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.Category>
    {
        public override void Configure(EntityTypeBuilder<Api.Domain.Models.Category> builder)
        {
            base.Configure(builder);
        }
    }
}

using Finance.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Services
{
    public class AmountConfiguration : IEntityTypeConfiguration<Amount>
    {

        public void Configure(EntityTypeBuilder<Amount> builder)
        {
            builder.ToTable("amounts");
        }

    }
}

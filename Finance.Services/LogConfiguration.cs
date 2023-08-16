using Finance.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Services
{
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {

        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("logs");
        }

    }
}

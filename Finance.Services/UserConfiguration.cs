using Finance.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Finance.Services
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            //builder.Property(x=>x.nick_name).HasColumnName("nickname");
            //builder.Property(x=>x.nick_name).HasColumnType("integer");
            //builder.Property(x=>x.nick_name).HasMaxLength(256);
            //builder.Property(x=>x.nick_name).IsRequired(); //null olamaz anlamına geliyor.

            //builder.Property(x=>x.email).HasColumnType("nvarchar(20)").HasColumnName("EMAIL").HasMaxLength(50);

            // FLUENT API YAPISI
            //CODE-FIRST için kulanılıyor. Yani DB'yi oluşturmadan önce DB'in yapısı kodlanıyor.

        }
    }

}




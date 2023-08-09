using Finance.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

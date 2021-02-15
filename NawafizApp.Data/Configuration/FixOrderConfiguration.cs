using NawafizApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Data.Configuration
{
    class FixOrderConfiguration : EntityTypeConfiguration<FixOrder>
    {
        internal FixOrderConfiguration()
        {
            ToTable("FixOrder");
            HasKey(x => x.Id)
               .Property(x => x.Id)
               .HasColumnName("Id")
               .HasColumnType("int")
               .IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
          
            Property(x => x.isFinished)
               .HasColumnName("isFinished")
               .HasColumnType("bit")
               ;

            Property(x => x.Creation_Time)
   .HasColumnName("Creation_Time")
   .HasColumnType("nvarchar")
   ;
            Property(x => x.Creation_Time)
.HasColumnName("isFinished")
.HasColumnType("nvarchar")
;
        }


    }
}

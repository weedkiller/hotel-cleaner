using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NawafizApp.Domain.Entities;

namespace NawafizApp.Data.Configuration
{
    class RoomTypeConfiguration : EntityTypeConfiguration<RoomType>
    {
        internal RoomTypeConfiguration()
        {
            ToTable("RoomType");


            HasKey(x => x.Id)
                    .Property(x => x.Id)
                    .HasColumnName("Id")
                    .HasColumnType("int")
                    .IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Name)
                    .HasColumnName("name")
                    .HasColumnType("nvarchar")
                    ;

            Property(x => x.Type)
                    .HasColumnName("type")
                    .HasColumnType("nvarchar")
                    ;
            HasMany(x => x.Rooms).WithRequired(x => x.RoomType);

        }
       
    }
}

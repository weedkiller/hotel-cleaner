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
   public class RoomConfiguration : EntityTypeConfiguration<Room>
    {
        internal RoomConfiguration()
        {
            ToTable("Room");


            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.RoomNum)
                .HasColumnName("RoomNum")
                .HasColumnType("nvarchar")
                ;

            Property(x => x.RoomDirection).HasColumnName("RoomDirection").HasColumnType("nvarchar")
                
                
                ;
            Property(x => x.Isbusy)
               .HasColumnName("Isbusy")
               .HasColumnType("bit")
               ;
            Property(x => x.IsInService)
   .HasColumnName("IsInService")
   .HasColumnType("bit")
   ;
            Property(x => x.isneedclean)
   .HasColumnName("isneedclean")
   .HasColumnType("bit")
   ;

            Property(x => x.IsNeedfix)
.HasColumnName("IsNeedfix")
.HasColumnType("bit")
;
            HasRequired(x => x.RoomType).WithMany(x => x.Rooms);
            HasRequired(x => x.HotelBlock).WithMany(x => x.Rooms);
            HasMany(x => x.Equipments).WithRequired(x => x.Room);


            HasMany(x => x.Orders).WithRequired(x => x.Room);
            HasMany(x => x.FixOrder).WithRequired(x => x.Room);



        }
    }
}

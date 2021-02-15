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
  partial  class EquipmentConfiguration : EntityTypeConfiguration<Equipment>
    {
        internal EquipmentConfiguration()
        {
            ToTable("Equipment");


            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);



            Property(x => x.Name)
              .HasColumnName("Name")
              .HasColumnType("nvarchar")
              ;
            Property(x => x.Description)
             .HasColumnName("Description")
             .HasColumnType("nvarchar")
             ;
            Property(x => x.Stauts)
          .HasColumnName("Stauts")
          .HasColumnType("nvarchar")
          ;
            Property(x => x.needfix)
        .HasColumnName("needfix")
        .HasColumnType("bit")
        ;
            Property(x => x.ishere) .HasColumnName("ishere").HasColumnType("bit")
   
    
    ;


            HasRequired(x => x.Room)
              .WithMany(x => x.Equipments);
       


        }
    }
}

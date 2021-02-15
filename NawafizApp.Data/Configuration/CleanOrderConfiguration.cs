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
   public class CleanOrderConfiguration : EntityTypeConfiguration<CleanOrder>
    {
        internal CleanOrderConfiguration()
        {
            ToTable("CleanOrder");


            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);



            Property(x => x.Creation_At)
              .HasColumnName("Creation_At")
              .HasColumnType("nvarchar")
              ;
            Property(x => x.Creation_Date)
             .HasColumnName("Creation_Date")
             .HasColumnType("nvarchar")
             ;
            Property(x => x.Creation_Time)
          .HasColumnName("Creation_Time")
          .HasColumnType("nvarchar")
          ;
            Property(x => x.startdate)
       .HasColumnName("startdate")
       .HasColumnType("nvarchar")
       .IsOptional()
       ;
            Property(x => x.enddate)
       .HasColumnName("enddate")
       .HasColumnType("nvarchar").IsOptional()
       ;
            Property(x => x.isFinished)
    .HasColumnName("isFinished")
    .HasColumnType("bit").IsOptional()
    ;
            Property(x => x.Description)
        .HasColumnName("Description")
        .HasColumnType("nvarchar")
        ;
            //Property(x => x.User_ID)
            //       .HasColumnName("User_ID")
            //       .HasColumnType("uniqueidentifier")
            //       .IsOptional();
            //Property(x => x.Room_ID)
            //   .HasColumnName("Room_ID")
            //   .HasColumnType("int")
            //   .IsOptional();

            //HasRequired(x => x.User)
            //       .WithMany(x => x.Orders)
            //       .HasForeignKey(x => x.User_ID).WillCascadeOnDelete(false);
            //HasRequired(x => x.Room)
            //    .WithMany(x => x.Orders)
            //    .HasForeignKey(x => x.Room_ID);

        }
    }
}

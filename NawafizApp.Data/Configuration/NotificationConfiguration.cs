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
  public  class NotificationConfiguration : EntityTypeConfiguration<Notifictation>
    {
        internal NotificationConfiguration()
        {
            ToTable("Notification");


            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasColumnName("Id")
                .HasColumnType("int")
                .IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.senderId)
                .HasColumnName("senderId")
                .HasColumnType("uniqueidentifier")
                ;

            Property(x => x.RevieverId)
                .HasColumnName("RevieverId")
                .HasColumnType("uniqueidentifier")
                ;

            Property(x => x.NotText)
              .HasColumnName("NotText")
              .HasColumnType("nvarchar")
              ;
            Property(x => x.NotDateTime)
            .HasColumnName("NotDateTime")
            .HasColumnType("nvarchar")
            ;
            Property(x => x.IsRead)
          .HasColumnName("IsRead")
          .HasColumnType("bit")
          ;
            Property(x => x.Room_ID)
                .HasColumnName("Room_ID")
                .HasColumnType("int")
                .IsOptional();
            Property(x => x.Equipment_ID)
          .HasColumnName("Equipment_ID")
          .HasColumnType("int")
          .IsOptional();

            HasRequired(x => x.User)
                .WithMany(x => x.Notifications)
                .HasForeignKey(x => x.RevieverId);
        }
    }
}

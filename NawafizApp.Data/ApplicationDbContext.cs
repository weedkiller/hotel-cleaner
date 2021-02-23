using NawafizApp.Data.Configuration;
using NawafizApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Data
{
    internal class ApplicationDbContext : DbContext
    {
        internal ApplicationDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString = "DefaultConnection")
        {
        }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            
        }



        public IDbSet<User> Users { get; set; }
        public IDbSet<Role> Roles { get; set; }
        public IDbSet<ExternalLogin> Logins { get; set; }
        public IDbSet<Language> Languages { get; set; }
        public IDbSet<HotelBlock> HotelBlocks { get; set; }
        public IDbSet<RoomType>  RoomTypes { get; set; }
        public IDbSet<RoomRec> roomRecs { get; set; }
        public IDbSet<RoomStatus> RoomStatuses { get; set; }
        public IDbSet<Room> Rooms { get; set; }
        public IDbSet<Notifictation> Notifictations { get; set; }
        public IDbSet<CleanOrder> Orders { get; set; }
        public IDbSet<FixOrder> fixOrders { get; set; }
        public IDbSet<Equipment> Equipments { get; set; }
        public IDbSet<FIxOrderEqupment> fIxOrderEqupments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new RoomTypeConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new ExternalLoginConfiguration());
            modelBuilder.Configurations.Add(new ClaimConfiguration());
            modelBuilder.Configurations.Add(new LanguageConfiguration());
            // modelBuilder.Configurations.Add(new HoteBlockConfigration());
            //  modelBuilder.Configurations.Add(new RoomConfiguration());
            //  modelBuilder.Configurations.Add(new RoomStatusConfiguration());
            //modelBuilder.Configurations.Add(new NotificationConfiguration());
            //modelBuilder.Configurations.Add(new CleanOrderConfiguration());
            //   modelBuilder.Configurations.Add(new EquipmentConfiguration());
            modelBuilder
      .Entity<Equipment>()
      .HasOptional(e => e.Room)
      .WithMany(e => e.Equipments).WillCascadeOnDelete(true)
      ;

            modelBuilder
.Entity<FIxOrderEqupment>()
.HasOptional(e => e.fixOrder)
.WithMany(e => e.FIxOrderEqupment).WillCascadeOnDelete(true)
;

            modelBuilder
 .Entity<HotelBlock>()
 .HasMany(e => e.Rooms)
 .WithRequired(e => e.HotelBlock).WillCascadeOnDelete(true)
 ;
            modelBuilder
.Entity<Room>()
.HasMany(e => e.Equipments)
.WithOptional(e => e.Room).WillCascadeOnDelete(true)
;
            modelBuilder.Entity<Room>()
.HasMany(e => e.FixOrder)
.WithOptional(e => e.Room).WillCascadeOnDelete(true);
            modelBuilder.Entity<Room>()
          .HasMany(e => e.Orders)
          .WithOptional(e => e.Room).WillCascadeOnDelete(true);
            modelBuilder.Entity<Room>()
.HasMany(e => e.RoomRec)
.WithOptional(e => e.Room).WillCascadeOnDelete(true);
            modelBuilder.Entity<FixOrder>()
.HasOptional(e => e.Room)
.WithMany(e => e.FixOrder).WillCascadeOnDelete(true);
            modelBuilder.Entity<CleanOrder>()
.HasOptional(e => e.Room)
.WithMany(e => e.Orders).WillCascadeOnDelete(true);
            modelBuilder.Entity<RoomRec>()
.HasOptional(e => e.Room)
.WithMany(e=>e.RoomRec).WillCascadeOnDelete(true);

        }
}

   

}

using NawafizApp.Domain.Entities;
using NawafizApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NawafizApp.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        #region Properties
        IExternalLoginRepository ExternalLoginRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRepository UserRepository { get; }
        IRepository<RoomType> RoomTypeRepository { get; }
        IRepository<Language> LanguageRepository { get; }
        IRepository<HotelBlock> HotelBlockRepository { get; }
        IRepository<Room> RoomRepository { get; }
        IRepository<Equipment> EquipmentRepository { get; }
        IRepository<CleanOrder> OrderRepository { get; }
        IRepository<RoomStatus> RoomStatusRepository { get; }
        IRepository<Notifictation> NotifictationRepository { get; }
        IRepository<FixOrder> FixOrderRepository { get; }
        IRepository<FIxOrderEqupment> FIxOrderEqupment { get; }
        #endregion

        #region Methods
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        #endregion
    }
}

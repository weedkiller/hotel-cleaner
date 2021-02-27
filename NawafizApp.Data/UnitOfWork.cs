using NawafizApp.Data.Repositories;
using NawafizApp.Domain;
using NawafizApp.Domain.Entities;
using NawafizApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NawafizApp.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        private readonly ApplicationDbContext _context;
        private IExternalLoginRepository _externalLoginRepository;
        private IRoleRepository _roleRepository;
        private IUserRepository _userRepository;
        private IRepository<Language> _languageRepository;
        private IRepository<HotelBlock> _HotelBlockRepository;
        private IRepository<Room> _RoomRepository;
        private IRepository<RoomStatus> _RoomStatusRepository;
        private IRepository<CleanOrder> _OrderRepository;
        private IRepository<Notifictation> _NotifictationRepository;
        private IRepository<Equipment> _EquipmentRepository;
        private IRepository<RoomType> _RoomTypeRepository;
        private IRepository<FixOrder> _fixOrderRepository;
        private IRepository<FIxOrderEqupment> _FIxOrderEqupment;
        private IRepository<RoomRec> _roomrecRepository;
        #endregion

        #region Constructors
        public UnitOfWork(string nameOrConnectionString)
        {
            _context = new ApplicationDbContext(nameOrConnectionString);
        }
        #endregion

        #region IUnitOfWork Members
        public IExternalLoginRepository ExternalLoginRepository
        {
            get { return _externalLoginRepository ?? (_externalLoginRepository = new ExternalLoginRepository(_context)); }
        }

        public IRepository<FixOrder> FixOrderRepository
        {
            get { return _fixOrderRepository ?? (_fixOrderRepository = new Repository<FixOrder>(_context)); }

        }
        public IRoleRepository RoleRepository
        {
            get { return _roleRepository ?? (_roleRepository = new RoleRepository(_context)); }
        }

        public IRepository<RoomRec> roomrecRepository
        {
            get { return _roomrecRepository ?? (_roomrecRepository = new Repository<RoomRec>(_context)); }
        }


        public IUserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_context)); }
        }

        public IRepository<Language> LanguageRepository
        {
            get { return _languageRepository ?? (_languageRepository = new Repository<Language>(_context)); }
        }
        public IRepository<HotelBlock> HotelBlockRepository
        {
            get { return _HotelBlockRepository ?? (_HotelBlockRepository = new Repository<HotelBlock>(_context)); }
        }



        public IRepository<FIxOrderEqupment> FIxOrderEqupment
        {
            get { return _FIxOrderEqupment ?? (_FIxOrderEqupment = new Repository<FIxOrderEqupment>(_context)); }
        }
        public IRepository<RoomType> RoomTypeRepository  
        {
            get { return _RoomTypeRepository ?? (_RoomTypeRepository = new Repository<RoomType>(_context)); }
        }
        public IRepository<Room> RoomRepository
        {
            get { return _RoomRepository ?? (_RoomRepository = new Repository<Room>(_context)); }
        }
        public IRepository<RoomStatus> RoomStatusRepository
        {
            get { return _RoomStatusRepository ?? (_RoomStatusRepository = new Repository<RoomStatus>(_context)); }
        }
        public IRepository<Equipment> EquipmentRepository
        {
            get { return _EquipmentRepository ?? (_EquipmentRepository = new Repository<Equipment>(_context)); }
        }
        public IRepository<CleanOrder> OrderRepository
        {
            get { return _OrderRepository ?? (_OrderRepository = new Repository<CleanOrder>(_context)); }
        }
        public IRepository<Notifictation> NotifictationRepository
        {
            get { return _NotifictationRepository ?? (_NotifictationRepository = new Repository<Notifictation>(_context)); }
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _externalLoginRepository = null;
            _roleRepository = null;
            _userRepository = null;
            _context.Dispose();
        }
        #endregion
    }
}

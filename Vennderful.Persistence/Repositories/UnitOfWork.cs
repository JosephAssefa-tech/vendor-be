
using Microsoft.AspNetCore.Identity;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VennderfulDbContext _dbContext;
        
        private ICustomerRepository _customerRepository;
        private IUserProfileRepository _userProfileRepository;
        private IOrderRepository _orderRepository;
        private IPaymentRepository _paymentRepository;
        private IUserRoleRepository _roleRepository;
        private IRateStructureRepository _rateStructureRepository;
        private IAddOnCategoryRepository _addOnsCategoryRepository;
        private IPackageCategoryRepository _packageCategoryRepository;
        private IPackageRepository _packageRepository;
        private ITypeOfBusinessRepository _typeOfBusinessRepository;
        private IVenueAccountInformationRepository _venueAccountInformationRepository;
        private IAddOnRepository _addOnRepository;
        private IDocumentRepository _NewDocumentRepository;
        private IVenuePublicProfileRepository _venuePublicProfileRespository;
        private IWorkingHoursRepository _workingHoursRepository;
        private ISocialProfileRepository _socialProfileRepository;
        private IUploadDocumentRepository _uploadDocumentRepository;
        private IClientRepository _clientRepository;
        private IEventRepository _eventRepository;
        private IRoomRepository _roomRepository;
        private IEventAndRoomRepository _eventAndRoomRepository;
        //private IEventAndClientRepository _eventAndClientRepository;
        private IEventClientRepository _eventClientRepository;
        private INotificationRepository _notificationRepository;
        private IEventFinanceRepository _eventFinanceRepository;
        private IEventFinanceAddOnRepository _eventFinanceAddOnRepository;
        private IEventPaymentRepository _eventPaymentRepository;
        private IEventFinancePaymentScheduleRepository _eventFinancePaymentScheduleRepository;
        private IMemberRepository _memberRepository;
        private IEventAndMemberRepository _eventAndMemberRepository;
        private IEventDocumentRepository _eventDocumentRepository;
        private IEventDocumentSignerRepository _eventDocumentSignerRepository;
        private IEventTimeineRepository _eventTimelineRepository;

        public UnitOfWork(VennderfulDbContext dbContext)
        {
            _dbContext = dbContext;           
        }

        public IUserProfileRepository UserProfileRepository => 
            _userProfileRepository ??= new UserProfileRepository(_dbContext);
        public IOrderRepository OrderRepository => 
            _orderRepository ??= new OrderRepository(_dbContext);
        public IPaymentRepository PaymentRepository =>
            _paymentRepository ??= new PaymentRepository(_dbContext);

        public ICustomerRepository CustomerRepository => 
            _customerRepository ??= new CustomerRepository(_dbContext);

        public IUserRoleRepository UserRoleRepository =>
            _roleRepository ??= new UserRoleRepository(_dbContext);
        public IRateStructureRepository RateStructureRepository =>
           _rateStructureRepository ??= new RateStructureRepository(_dbContext);
        public IAddOnCategoryRepository AddOnsCategoryRepository =>
            _addOnsCategoryRepository ??= new AddOnCategoryRepository(_dbContext);
        public IPackageCategoryRepository PackageCategoryRepository =>
            _packageCategoryRepository ??= new PackageCategoryRepository(_dbContext);
        public IPackageRepository PackageRepository =>
            _packageRepository ??= new PackageRepository(_dbContext);

        public ITypeOfBusinessRepository TypeOfBusinessRepository =>
           _typeOfBusinessRepository ??= new TypeOfBusinessRepository(_dbContext);

        public IVenueAccountInformationRepository VenueAccountInformationRepository =>
           _venueAccountInformationRepository ??= new VenueAccountInformationRepository(_dbContext);

        public IAddOnRepository AddOnRepository =>
           _addOnRepository ??= new AddOnRepository(_dbContext);

        public IDocumentRepository NewDocumentRepository =>
            _NewDocumentRepository ??= new DocumentRepository(_dbContext);

        public IVenuePublicProfileRepository VenuePublicProfileRepository =>
            _venuePublicProfileRespository ??= new VenuePublicProfileRepository(_dbContext);
        
        IWorkingHoursRepository IUnitOfWork.workingHoursRepository =>
            _workingHoursRepository ??= new WorkingHoursRepository(_dbContext);
        ISocialProfileRepository IUnitOfWork.socialProfileRepository =>
           _socialProfileRepository ??= new SocialProfileRepository(_dbContext);
        IUploadDocumentRepository IUnitOfWork.uploadDocumentRepository =>
            _uploadDocumentRepository ??= new UploadDocumentRepository(_dbContext);
        IClientRepository IUnitOfWork.clientRepository =>
           _clientRepository ??= new ClientRepository(_dbContext);
        IEventRepository IUnitOfWork.eventRepository =>
           _eventRepository ??= new EventRepository(_dbContext);
         IRoomRepository IUnitOfWork.RoomRepository =>
          _roomRepository ??= new RoomRepository(_dbContext);
        IEventAndRoomRepository IUnitOfWork.EventAndRoomRepository =>
          _eventAndRoomRepository ??= new EventAndRoomRepository(_dbContext);
        //IEventAndClientRepository IUnitOfWork.EventAndClientRepository =>
        // _eventAndClientRepository ??= new EventAndClientRepository(_dbContext);

        IEventClientRepository IUnitOfWork.eventClientRepository =>
           _eventClientRepository ??= new EventClientRepository(_dbContext);
        INotificationRepository IUnitOfWork.notificationRepository =>
            _notificationRepository ??= new NotificationRepository(_dbContext);
        IEventFinanceRepository IUnitOfWork.eventFinanceRepository =>
           _eventFinanceRepository ??= new EventFinanceRepository(_dbContext);
        IEventFinanceAddOnRepository IUnitOfWork.eventFinanceAddOnRepository =>
           _eventFinanceAddOnRepository ??= new EventFinanceAddOnRepository(_dbContext);
        IEventFinancePaymentScheduleRepository IUnitOfWork.eventFinancePaymentScheduleRepository =>
          _eventFinancePaymentScheduleRepository ??= new EventFinancePaymentScheduleRepository(_dbContext);

        IEventPaymentRepository IUnitOfWork.eventPaymentRepository =>
           _eventPaymentRepository ??= new EventPaymentRepository(_dbContext);
        IMemberRepository IUnitOfWork.memberRepository =>
           _memberRepository ??= new MemberRepository(_dbContext);
        IEventAndMemberRepository IUnitOfWork.eventAndMemberRepository =>
           _eventAndMemberRepository ??= new EventAndMemberRepository(_dbContext);
        IEventDocumentRepository IUnitOfWork.eventDocumentRepository =>
           _eventDocumentRepository ??= new EventDocumentRepository(_dbContext);
        IEventDocumentSignerRepository IUnitOfWork.eventDocumentSignerRepository =>
           _eventDocumentSignerRepository ??= new EventDocumentSignerRepository(_dbContext);
        IEventTimeineRepository IUnitOfWork.eventTimelineRepository =>
            _eventTimelineRepository ??= new EventTimelineRepository(_dbContext);

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

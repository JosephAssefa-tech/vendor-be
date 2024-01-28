using System;
using System.Threading.Tasks;

namespace Vennderful.Application.Contracts.Persitence
{
    public interface IUnitOfWork : IDisposable
    {
        IUserProfileRepository UserProfileRepository { get; }
        ICustomerRepository CustomerRepository { get; }  
        IOrderRepository OrderRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        IAddOnCategoryRepository AddOnsCategoryRepository { get; }
        IPackageCategoryRepository PackageCategoryRepository { get; }
        IPackageRepository PackageRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        IRateStructureRepository RateStructureRepository { get; }
        ITypeOfBusinessRepository TypeOfBusinessRepository { get; }
        IVenueAccountInformationRepository VenueAccountInformationRepository { get; }
        IAddOnRepository AddOnRepository { get; }
        IDocumentRepository NewDocumentRepository { get; }
        IVenuePublicProfileRepository VenuePublicProfileRepository { get; }
        IWorkingHoursRepository workingHoursRepository { get; }
        ISocialProfileRepository socialProfileRepository { get; }
        IUploadDocumentRepository uploadDocumentRepository { get; }
        IClientRepository clientRepository { get; }
        IEventRepository eventRepository { get; }
        IRoomRepository RoomRepository { get; }
        IEventAndRoomRepository EventAndRoomRepository { get; }
        //IEventAndClientRepository EventAndClientRepository { get; }
        IEventClientRepository eventClientRepository { get; }
        INotificationRepository notificationRepository { get; }
        IEventFinanceRepository eventFinanceRepository { get; }
        IEventFinanceAddOnRepository eventFinanceAddOnRepository { get; }
        IEventPaymentRepository eventPaymentRepository { get; }
        IEventFinancePaymentScheduleRepository eventFinancePaymentScheduleRepository { get; }
        IMemberRepository memberRepository { get; }
        IEventAndMemberRepository eventAndMemberRepository  { get; }
        IEventDocumentRepository eventDocumentRepository { get; }
        IEventDocumentSignerRepository eventDocumentSignerRepository { get; }
        IEventTimeineRepository eventTimelineRepository { get; }
        Task Save();
    }
}

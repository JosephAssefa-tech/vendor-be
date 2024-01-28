using AutoMapper;
using Vennderful.Application.Features.Customers.DTOs;
using Vennderful.Application.Features.Orders.DTOs;
using Vennderful.Application.Features.User.DTOs;
using Vennderful.Application.Features.Stripe.DTOs;
using Vennderful.Domain.Entities;
using Vennderful.Application.Features.AddOnsCategories.DTOs;
using Vennderful.Application.Features.RateStructure.DTOs;
using Vennderful.Application.Features.VenueAccount.DTOs;
using Vennderful.Application.Features.TypeOfBusiness.DTOs;
using Vennderful.Application.Features.AddOn.DTOs;
using Vennderful.Application.Features.NewDocuments.DTOs;
using Vennderful.Application.Features.PackageCategories.DTOs;
using Vennderful.Application.Features.Package.DTOs;
using Vennderful.Application.Features.VenueProfile.DTOs;
using Vennderful.Application.Features.AnotherSocialProfile.DTOs;
using Vennderful.Application.Features.WorkingHours.DTOs;
using Vennderful.Application.Features.UploadDocuments.DTOs;
using Vennderful.Application.Features.UserRoles.DTOs;
using Vennderful.Application.Features.Events.Responses;
using Vennderful.Application.Features.Client.Responses;
using Vennderful.Application.Features.Client.DTOs;
using Vennderful.Application.Features.Events.DTOs;
using Vennderful.Application.Features.EventRoom.Dto;
using Vennderful.Application.Features.EventAndRooms.Dto;
using Vennderful.Application.Features.EventAndClients.Dto;
using Vennderful.Application.Features.EventFinance.Dto;
using Vennderful.Application.Features.EventPayment.DTOs;
using Vennderful.Application.Features.Member.DTO;
using Vennderful.Application.Features.EventAndMember.DTO;
using Vennderful.Application.Features.EventDocuments.Dto;
using Vennderful.Application.Features.Notifications.Dto;
using Vennderful.Application.Features.EventDocumentSignature.Dto;
using System.Linq;
using System.Collections.Generic;
using System;
using Vennderful.Application.Features.EventTimeline.DTOs;
using Vennderful.Application.Features.Documents.DTOs;
using Vennderful.Application.Features.EventTimeline.DTOs;
namespace Vennderful.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserProfile, CreateUserInvitationDTO>().ReverseMap();


            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Customer, ListCustomerDTO>().ReverseMap();
            CreateMap<Customer, CreateCustomerDTO>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDTO>().ReverseMap();
            CreateMap<Customer, StripeCustomerDTO>().ReverseMap();
            CreateMap<Customer, AddStripeCustomerDTO>().ReverseMap();
            CreateMap<Order, ListOrderDTO>().ReverseMap();
            CreateMap<Order, CreateOrderDTO>().ReverseMap();
            CreateMap<Order, UpdateOrderDTO>().ReverseMap();
            CreateMap<UserRole, AddUserRoleDTO>().ReverseMap();
            CreateMap<UserRole, UserRoleDTO>().ReverseMap();
            CreateMap<UserProfile, UserProfileDTO>().ReverseMap();
            CreateMap<UserProfile, UserRegisterDto>().ReverseMap();
            CreateMap<Payment, StripePaymentDTO>().ReverseMap();
            CreateMap<Payment, AddStripePaymentDTO>().ReverseMap();
            CreateMap<AddOnCategory, AddOnCategoryDTo>().ReverseMap();

            CreateMap<RateStructure, GetRateStructuresDTO>().ReverseMap();
            CreateMap<VenueAccountInformation, VenueAccountInformationDto>().ReverseMap();
            CreateMap<VenueAccountInformation, CreateVenueAccountInformationDto>().ReverseMap();
            CreateMap<TypeOfBusiness, CreateTypeOfBusinessDtocs>().ReverseMap();
            CreateMap<TypeOfBusiness, ListTypeOfBusinessDto>().ReverseMap();
            CreateMap<TypeOfBusiness, TypeOfBusinessDto>().ReverseMap();

            CreateMap<AddOn, CreateAddOnDTO>().ReverseMap();
            CreateMap<AddOn, AddOnDTO>().ReverseMap();
            CreateMap<AddOn, ListAddOnDTO>().ReverseMap();

            CreateMap<Document, NewDocumentDto>().ReverseMap();
            CreateMap<Document, CreateNewDocumentDto>().ReverseMap();
            CreateMap<Document, ListNewDocumentDto>().ReverseMap();
            CreateMap<Document, EditDocumentDto>().ReverseMap();


            CreateMap<PackageCategory, PackageCategoryDTo>().ReverseMap();

            CreateMap<Package, CreatePackageDTO>().ReverseMap();
            CreateMap<Package, PackageDTO>().ReverseMap();
            CreateMap<Package, ListPackageDTO>().ReverseMap();
            CreateMap<VenuePublicProfile, CurateVenuePublicProfileDto>().ReverseMap();
            CreateMap<SocialProfile, CreateSocialProfileDto>().ReverseMap();
            CreateMap<WorkingHour, CreateWorkingHourDto>().ReverseMap();

            CreateMap<VenueAccountInformation, CompleteVenueCreationDTO>().ReverseMap();
            CreateMap<UploadDocument, UploadDocumentDto>().ReverseMap();

            CreateMap <VenuePublicProfile,UpdateVenuePublicProfileDTO>().ReverseMap();
            CreateMap<Package, CreatePackageResponseDTO>().ReverseMap();
            CreateMap<AddOn, CreateAddOnResponseDTO>().ReverseMap();
            CreateMap<Client, CreateClientResponse>().ReverseMap();
            CreateMap<Client, CreateClientDTO>().ReverseMap();
            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<Notification, ClientInvitationDTO>().ReverseMap();
            CreateMap<Client, ListClientDTO>().ReverseMap();
            CreateMap<Event, CreateEventResponse>().ReverseMap();
            CreateMap<Event, CreateEventDTO>().ReverseMap();
            CreateMap<Event, EventDTO>().ReverseMap();
            CreateMap<Event, UpdateEventDto>().ReverseMap();
            CreateMap<Event, ListEventDTO>().ReverseMap();
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<Room, ListRoomDto>().ReverseMap();
            CreateMap<Room, CreateRoomDto>().ReverseMap();
            CreateMap<EventAndRoom, EventAndRoomDto>().ReverseMap();
            CreateMap<EventAndRoom, ListEventAndRoomDto>().ReverseMap();
            CreateMap<EventAndRoom, CreateEventAndRoomDto>().ReverseMap();
            CreateMap<EventAndClient, EventAndClientDto>().ReverseMap();
            CreateMap<EventAndClient, CreateEventAndClientsDto>().ReverseMap();
            CreateMap<EventFinance, CreateEventFinanceDto>().ReverseMap();
            CreateMap<EventFinance, EventFinanceDto>().ReverseMap();
            CreateMap<EventFinanceAddOn, AddonDto>().ReverseMap();
            CreateMap<EventFinancePaymentSchedule, PaymentSchedules>().ReverseMap();

            CreateMap<Event, EditEventDTO>().ReverseMap();
            CreateMap<Event, EditEventRequestDTO>().ReverseMap();
            //CreateMap<EventClient, EventClientDto>().ReverseMap();
            //CreateMap<EventFinanceAddOn, ClientDto>().ReverseMap();

            
            CreateMap<EventPayment, EventPaymentDTO>().ReverseMap();
            CreateMap<EventPayment, CreateEventPaymentDTO>().ReverseMap();
            CreateMap<EventFinance, EventBudgetSummaryItemDto>().ReverseMap();
            CreateMap<EventFinancePaymentSchedule, ListEventPaymentScheduleDto>().ReverseMap();

            CreateMap<Member, CreateMemberDTO>().ReverseMap();
            CreateMap<Member, ListMembersDTO>().ReverseMap();
            CreateMap<EventAndMember, CreateEventAndMemberDTO>().ReverseMap();
            CreateMap<EventAndMember, ListEventAndMembersDTO>().ReverseMap();

            CreateMap<Notification, ListNotificationDTO>().ReverseMap();

            CreateMap<EventDocument, EventDocumentsDto>().ReverseMap();
            CreateMap<EventDocument, CreateEventDocumentsDto>().ReverseMap();
            CreateMap<EventDocument, ListEventDocumentDto>().ReverseMap();

            CreateMap<Notification, CreateEventDocumentSignatureNotificationDto>().ReverseMap();
           CreateMap<EventDocumentSigner, CreateEventDocumentSignatureDTO>().ReverseMap();

            CreateMap<EventTimeline, CreateEventTimelineDto>().ReverseMap();
            CreateMap<EventTimeline, EventTimelineDto>().ReverseMap();
            CreateMap<EventTimeline, UpdateEventTimelineDto>().ReverseMap();

        }
    }
}

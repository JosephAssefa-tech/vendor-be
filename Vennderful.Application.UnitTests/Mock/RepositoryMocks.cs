using Moq;
using Optivem.Framework.Core.Common.Http;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq.Expressions;
using System.Reflection.Emit;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using Vennderful.Domain.Enums;
using Vennderful.Domain.ValueObjects;
using Optivem.Framework.Core.Common.Http;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace Vennderful.Application.UnitTests.Mock
{
    public static class RepositoryMocks
    {
        public static Mock<IUnitOfWork> GetCustomerRepository()
        {
            var customers = new List<Customer>
            {
                new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Cust1 Name",
                    Address = new Domain.ValueObjects.Address
                    (
                        "street1",
                        "city1",
                        "state1",
                        "country1",
                        "zip1"
                        ),
                },
                new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Cust2 Name",
                    Address = new Domain.ValueObjects.Address
                    (
                        "street2",
                        "city2",
                        "state2",
                        "country2",
                        "zip"
                        ),
                }
            };

            var mockCustomerRepository = new Mock<IUnitOfWork>();

            mockCustomerRepository.Setup(repo => repo.CustomerRepository.GetAllAsync())
                .ReturnsAsync(customers);

            mockCustomerRepository.Setup(repo => repo.CustomerRepository.AddAsync(It.IsAny<Customer>()))
                .ReturnsAsync(
                    (Customer customer) =>
                    {
                        customers.Add(customer);
                        return customer;
                    });

            return mockCustomerRepository;
        }
        public static Mock<IUnitOfWork> GetPaymentRepository()
        {
            var payments = new List<Payment>
            {
                new Payment
                {
                    Id = Guid.NewGuid(),
                    CustomerId = "cus_NZx49i7wtwcymN",
                    ReceiptEmail = "Customer1@vennderful.com",
                    Description = "This is smaple description",
                    Currency = "USD",
                    Amount = 100
                },
                new Payment
                {
                    Id = Guid.NewGuid(),
                    CustomerId = "dfh_NZx49i7dhgfhhjN",
                    ReceiptEmail = "Customer1@vennderful.com",
                    Description = "This is smaple description",
                    Currency = "USD",
                    Amount = 100
                }
            };

            var mockPaymentRepository = new Mock<IUnitOfWork>();

            mockPaymentRepository.Setup(repo => repo.PaymentRepository.GetAllAsync())
                .ReturnsAsync(payments);

            mockPaymentRepository.Setup(repo => repo.PaymentRepository.AddAsync(It.IsAny<Payment>()))
                .ReturnsAsync(
                    (Payment payment) =>
                    {
                        payments.Add(payment);
                        return payment;
                    });

            return mockPaymentRepository;
        }
        public static Mock<IUnitOfWork> GetUserProfileRepository()
        {

            var users = new List<UserProfile>
            {
                new UserProfile
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    IsActive = true,
                    Email = "testc@mailservice.com",
                    FirstName = "First Name",
                    LastName = "Last Name",
                    Address = new Domain.ValueObjects.Address
                    (
                        "street1",
                        "city1",
                        "state1",
                        "country1",
                        "zip1"
                        ),
                    CompanyId=new Guid(),
                    Status="",
                    UserRole = "Employee",
                },
                 new UserProfile
                 {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    IsActive = false,
                    Email = "testc2@mailservice.com",
                    FirstName = "FS Name",
                    LastName = "SL Name",
                    Address = new Domain.ValueObjects.Address
                    (
                        "street1",
                        "city1",
                        "state1",
                        "country1",
                        "zip1"
                        ),
                    CompanyId=Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afb6"),
                    Status="Invited",
                    UserRole = "Administrator",
                    Created = DateTime.Now
                 }
            };

            var mockUserProfileRepository = new Mock<IUnitOfWork>();

            mockUserProfileRepository.Setup(repo => repo.UserProfileRepository.GetAllAsync())
                .ReturnsAsync(users);

            mockUserProfileRepository.Setup(repo => repo.UserProfileRepository.AddAsync(It.IsAny<UserProfile>()))
                .ReturnsAsync(
                    (UserProfile user) =>
                    {
                        users.Add(user);
                        return user;
                    });
            mockUserProfileRepository.Setup(repo => repo.UserProfileRepository.GetInvitedUsers(It.IsAny<string>())).ReturnsAsync((string id) => users.Where(u => u.CompanyId == Guid.Parse(id)));

            return mockUserProfileRepository;
        }
        public static Mock<IUnitOfWork> GetUserRoleRepository()
        {

            var usersRs = new List<UserRole>
            {
                new UserRole()
                {
                    Id = Guid.NewGuid(),
                    CompanyId = Guid.NewGuid(),
                    UserRoleType = new Domain.Enums.UserRoleType()

                },
                new UserRole()
                {
                    Id = Guid.NewGuid(),
                    CompanyId = Guid.NewGuid(),
                    UserRoleType = new Domain.Enums.UserRoleType()

                }
            };

            var mockUserRoleRepository = new Mock<IUnitOfWork>();

            mockUserRoleRepository.Setup(repo => repo.UserRoleRepository.GetAllAsync())
                .ReturnsAsync(usersRs);

            mockUserRoleRepository.Setup(repo => repo.UserRoleRepository.AddAsync(It.IsAny<UserRole>()))
                .ReturnsAsync(
                    (UserRole userR) =>
                    {
                        usersRs.Add(userR);
                        return userR;
                    });

            return mockUserRoleRepository;
        }
        public static Mock<IUnitOfWork> GetAddOnCategoryRepository()
        {

            var categories = new List<Vennderful.Domain.Entities.AddOnCategory>
            {
                new Vennderful.Domain.Entities.AddOnCategory
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Wedding"

                },
                new Vennderful.Domain.Entities.AddOnCategory
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Category2",

                }
            };

            var mockAddOnCategoryRepository = new Mock<IUnitOfWork>();

            mockAddOnCategoryRepository.Setup(repo => repo.AddOnsCategoryRepository.GetAllAsync())
                .ReturnsAsync(categories);

            mockAddOnCategoryRepository.Setup(repo => repo.AddOnsCategoryRepository.AddAsync(It.IsAny<AddOnCategory>()))
                .ReturnsAsync(
                    (AddOnCategory category) =>
                    {
                        categories.Add(category);
                        return category;
                    });

            return mockAddOnCategoryRepository;
        }
        public static Mock<IUnitOfWork> GetRateStructureRepository()
        {

            var rates = new List<Vennderful.Domain.Entities.RateStructure>
            {
                new Vennderful.Domain.Entities.RateStructure
                {
                    Id = Guid.NewGuid(),
                    Name = "Hourly",
                    Description = "Hourly",
                },
                new Vennderful.Domain.Entities.RateStructure
                {
                    Id = Guid.NewGuid(),
                    Name = "Flat",
                    Description = "Flat",
                },
            };

            var mockRateStructureRepository = new Mock<IUnitOfWork>();

            mockRateStructureRepository.Setup(repo => repo.RateStructureRepository.GetAllAsync())
                .ReturnsAsync(rates);

            mockRateStructureRepository.Setup(repo => repo.RateStructureRepository.AddAsync(It.IsAny<Vennderful.Domain.Entities.RateStructure>()))
                .ReturnsAsync(
                    (Vennderful.Domain.Entities.RateStructure rate) =>
                    {
                        rates.Add(rate);
                        return rate;
                    });

            return mockRateStructureRepository;
        }
        public static Mock<IUnitOfWork> GetVenueRepository()
        {

            var venues = new List<Vennderful.Domain.Entities.VenueAccountInformation>
            {
                new Vennderful.Domain.Entities.VenueAccountInformation
                {
                    Id = Guid.NewGuid(),
                    CompanyName = "Vennderful",
                    Website = "www.vennderful.com",
                    PhoneNumber = "123456789",
                    TypeOfBusinessId = Guid.NewGuid(),
                    Status= Domain.Enums.CompanyProfileStatus.Pending,
                    CompanyId= Guid.Parse("69398392-0279-4805-bdd8-438bbb6d6324"),
                },
                new Vennderful.Domain.Entities.VenueAccountInformation
                {
                    Id = Guid.NewGuid(),
                    CompanyName = "Excellerent",
                    Website = "www.excellerent.com",
                    PhoneNumber = "123456789",
                    TypeOfBusinessId = Guid.NewGuid(),
                    Status = Domain.Enums.CompanyProfileStatus.Pending,
                    Address = new Domain.ValueObjects.Address()
                    {
                        City="Addis Ababa",
                        Country="Ethiopia",
                        State="Addis",
                        Street="Gabon st.",
                        ZipCode="10000"
                    },
                    CompanyId= Guid.Parse("78398392-0279-4805-bdd8-438bbb6d6324"),
                },
            };

            var venueAccountInformationRepository = new Mock<IUnitOfWork>();

            venueAccountInformationRepository.Setup(repo => repo.VenueAccountInformationRepository.GetAllAsync())
                .ReturnsAsync(venues);

            venueAccountInformationRepository.Setup(repo => repo.VenueAccountInformationRepository.AddAsync(It.IsAny<Vennderful.Domain.Entities.VenueAccountInformation>()))
                .ReturnsAsync(
                    (Vennderful.Domain.Entities.VenueAccountInformation venue) =>
                    {
                        venues.Add(venue);
                        return venue;
                    });

            _ = venueAccountInformationRepository.Setup(repo => repo.VenueAccountInformationRepository.GetVenueByCompanyName(It.IsAny<string>(), It.IsAny<Guid>())).ReturnsAsync((string name, Guid companyId) => venues.FirstOrDefault(u => u.CompanyName.ToLower() == name.ToLower() && u.CompanyId == companyId));

            _ = venueAccountInformationRepository.Setup(repo => repo.VenueAccountInformationRepository.GetById(It.IsAny<Guid>())).ReturnsAsync((Guid companyId) => venues.FirstOrDefault(u => u.CompanyId == companyId));

            return venueAccountInformationRepository;
        }
        public static Mock<IUnitOfWork> GetAddOnRepository()
        {

            var addOns = new List<Vennderful.Domain.Entities.AddOn>
            {
                new Vennderful.Domain.Entities.AddOn
                {
                     Id = Guid.NewGuid(),
                AddOnName = "Addon_Test0",
                PricePerUnit = 50.40M,
                Taxable = false,
                AddOnImageUrl = "",
                AddOnDescription = "first add on",
                AddOnNote = "",
                AddOnCategory = new AddOnCategory() { CategoryName = "Birthday" },
                RateStructure = new Domain.Entities.RateStructure() { Name = "Hourly" },
                },
                new Vennderful.Domain.Entities.AddOn
                {
                     Id = Guid.NewGuid(),
                AddOnName = "Addon_Test1",
                PricePerUnit = 35.25M,
                Taxable = false,
                AddOnImageUrl = "",
                AddOnDescription = "second add on",
                AddOnNote = "",
                AddOnCategory = new AddOnCategory() { CategoryName = "Corporate" },
                RateStructure = new Domain.Entities.RateStructure() { Name = "Per Head" },
                },
            };

            var mockAddOnRepository = new Mock<IUnitOfWork>();

            mockAddOnRepository.Setup(repo => repo.AddOnRepository.GetAllAsync())
                .ReturnsAsync(addOns);

            mockAddOnRepository.Setup(repo => repo.AddOnRepository.AddAsync(It.IsAny<Domain.Entities.AddOn>()))
                .ReturnsAsync(
                    (Vennderful.Domain.Entities.AddOn addOn) =>
                    {
                        addOns.Add(addOn);
                        return addOn;
                    });

            return mockAddOnRepository;
        }
        public static Mock<IUnitOfWork> GetNewDocumentRepository()
        {
            var documents = new List<Vennderful.Domain.Entities.Document>
            {
                new Vennderful.Domain.Entities.Document
                {
                    Id = new Guid("29de9d27-c23d-4633-b654-b1e4651fa5f8"),
                    DocumentName = "doc A",
                    DocumentBody = "body of doc A",
                    DocumentUrl = "test.pdf",
                    DocumentDescription = "Document description for doc A",
                    DocumentCategory = 0,
                },
                new Vennderful.Domain.Entities.Document
                {
                    Id = new Guid("29de9d27-c23d-4633-b654-b1e4651fa5f8"),
                    DocumentName = "doc B",
                    DocumentBody = "body of doc B",
                    DocumentDescription = "Document description for doc B",
                    DocumentCategory = 0,
                },
            };

            var eventDocuments = new List<EventDocument>();

            var NewDocumentRepository = new Mock<IUnitOfWork>();

            NewDocumentRepository.Setup(repo => repo.NewDocumentRepository.GetAllAsync())
                .ReturnsAsync(documents);

            NewDocumentRepository.Setup(repo => repo.NewDocumentRepository.GetById(Guid.Parse("29de9d27-c23d-4633-b654-b1e4651fa5f8")))
                .ReturnsAsync(documents[0]);

            NewDocumentRepository.Setup(repo => repo.NewDocumentRepository.AddAsync(It.IsAny<Vennderful.Domain.Entities.Document>()))
                .ReturnsAsync((Vennderful.Domain.Entities.Document document) =>
                {
                    documents.Add(document);
                    return document;
                });

            NewDocumentRepository.Setup(repo => repo.eventDocumentRepository.AddAsync(It.IsAny<EventDocument>()))
                .ReturnsAsync((EventDocument eventDocument) =>
                {
                    eventDocuments.Add(eventDocument);
                    return eventDocument;
                });

            return NewDocumentRepository;
        }

        public static Mock<IUnitOfWork> GetPackageCategoryRepository()
        {

            var categories = new List<Vennderful.Domain.Entities.PackageCategory>
            {
                new Vennderful.Domain.Entities.PackageCategory
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Wedding"

                },
                new Vennderful.Domain.Entities.PackageCategory
                {
                    Id = Guid.NewGuid(),
                    CategoryName = "Funeral",

                }
            };

            var mockPackageCategoriesRepository = new Mock<IUnitOfWork>();

            mockPackageCategoriesRepository.Setup(repo => repo.PackageCategoryRepository.GetAllAsync())
                .ReturnsAsync(categories);

            mockPackageCategoriesRepository.Setup(repo => repo.PackageCategoryRepository.AddAsync(It.IsAny<PackageCategory>()))
                .ReturnsAsync(
                    (PackageCategory category) =>
                    {
                        categories.Add(category);
                        return category;
                    });

            return mockPackageCategoriesRepository;
        }
        public static Mock<IUnitOfWork> GetPackageRepository()
        {

            var packages = new List<Vennderful.Domain.Entities.Package>
            {
                new Vennderful.Domain.Entities.Package
                {
                     Id = Guid.NewGuid(),
                PackageName = "Premium0",
                Taxable = false,
                PackageImageUrl = "",
                PackageDescription = "first package",
                PackageNote = "",
                PackageCategory = new PackageCategory() { CategoryName = "Birthday" },
                RateStructure = new Domain.Entities.RateStructure() { Name = "Hourly" },
                },
                new Vennderful.Domain.Entities.Package
                {
                     Id = Guid.NewGuid(),
                PackageName = "Premium1",
                Taxable = false,
                PackageImageUrl = "",
                PackageDescription = "second Package",
                PackageNote = "",
                PackageCategory = new PackageCategory() { CategoryName = "Corporate" },
                RateStructure = new Domain.Entities.RateStructure() { Name = "Per Head" },
                },
            };

            var mockPackageRepository = new Mock<IUnitOfWork>();

            mockPackageRepository.Setup(repo => repo.PackageRepository.GetAllAsync())
                .ReturnsAsync(packages);

            mockPackageRepository.Setup(repo => repo.PackageRepository.AddAsync(It.IsAny<Domain.Entities.Package>()))
                .ReturnsAsync(
                    (Vennderful.Domain.Entities.Package package) =>
                    {
                        packages.Add(package);
                        return package;
                    });

            return mockPackageRepository;
        }
        public static Mock<IUnitOfWork> GetVenuePublicProfileRepository()
        {

            var venuePublicProfiles = new List<Vennderful.Domain.Entities.VenuePublicProfile>
            {
                new Vennderful.Domain.Entities.VenuePublicProfile
                {
                     Id = Guid.NewGuid(),
                ProfilePictureUrl = "",
                ProfileDescription = "Test public profile description"

                },
              new Vennderful.Domain.Entities.VenuePublicProfile
                {
                     Id = Guid.NewGuid(),

                ProfilePictureUrl = "",
                ProfileDescription = "Another Test public profile description"

                }
            };

            var mockVenuePublicProfileRepository = new Mock<IUnitOfWork>();

            mockVenuePublicProfileRepository.Setup(repo => repo.VenuePublicProfileRepository.GetAllAsync())
                .ReturnsAsync(venuePublicProfiles);

            mockVenuePublicProfileRepository.Setup(repo => repo.VenuePublicProfileRepository.AddAsync(It.IsAny<Domain.Entities.VenuePublicProfile>()))
                .ReturnsAsync(
                    (Vennderful.Domain.Entities.VenuePublicProfile venuePublicProfile) =>
                    {
                        venuePublicProfiles.Add(venuePublicProfile);
                        return venuePublicProfile;
                    }); ;

            return mockVenuePublicProfileRepository;
        }
        public static Mock<IUnitOfWork> GetRoomRepository()
        {

            var rooms = new List<Vennderful.Domain.Entities.Room>
            {
                new Vennderful.Domain.Entities.Room
                {
                    Id = Guid.NewGuid(),
                    RoomName = "roomA",

                },
                new Vennderful.Domain.Entities.Room
                {
                     Id = Guid.NewGuid(),
                    RoomName = "roomA",

                },
            };

            var RoomRepository = new Mock<IUnitOfWork>();

            RoomRepository.Setup(repo => repo.RoomRepository.GetAllAsync())
                .ReturnsAsync(rooms);

            RoomRepository.Setup(repo => repo.RoomRepository.AddAsync(It.IsAny<Vennderful.Domain.Entities.Room>()))
                .ReturnsAsync(
                    (Vennderful.Domain.Entities.Room room) =>
                    {
                        rooms.Add(room);
                        return room;
                    });

            return RoomRepository;


        }
        public static Mock<IUnitOfWork> GetEventRepository()
        {

            var events = new List<Vennderful.Domain.Entities.Event>
            {
                new Vennderful.Domain.Entities.Event
                {
                    Id = Guid.NewGuid(),
                   EventID =  "425367",
     Status =  "Booked",
     EventName =  "Mary and Alex's Wedding",
    TypeOfEvents =  0,
     NumberOfGuests =  65,
     DressCodes = 0,
     EventStartDateAndTime =  DateTime.Now,
     EventEndDateAndTime =  DateTime.Now,
     CoverPhoto =
            "https = //blog.nuvow.com/wp-content/uploads/2021/12/AdobeStock_175922155-2048x1365.jpeg",
     CompanyId= Guid.Parse("78398392-0279-4805-bdd8-438bbb6d6324"),
                },
                new Vennderful.Domain.Entities.Event
                {
                     Id = Guid.NewGuid(),
                   EventID =  "102030",
     Status =  "Booked",
     EventName =  "Joe Birthday",
     TypeOfEvents =  (TypeOfEvent)1,
     NumberOfGuests =  65,
     DressCodes =  0,
     EventStartDateAndTime =  DateTime.Now,
     EventEndDateAndTime =  DateTime.Now,

     CoverPhoto =
            "https = //blog.nuvow.com/wp-content/uploads/2021/12/AdobeStock_175922155-2048x1365.jpeg",
     CompanyId= Guid.Parse("66398392-0279-4805-bdd8-438bbb6d6324"),

                },
            };

            var EventRepository = new Mock<IUnitOfWork>();

            EventRepository.Setup(repo => repo.eventRepository.GetAllAsync())
                .ReturnsAsync(events);
            EventRepository.Setup(repo => repo.eventRepository.AddAsync(It.IsAny<Vennderful.Domain.Entities.Event>()))
               .ReturnsAsync(
                   (Vennderful.Domain.Entities.Event newEvent) =>
                   {
                       events.Add(newEvent);
                       return newEvent;
                   });
            EventRepository.Setup(repo => repo.eventRepository.GetById(It.IsAny<Guid>(), It.IsAny<Guid>())).ReturnsAsync((Guid id, Guid companyId) => events.FirstOrDefault(u => u.Id == id && u.CompanyId == companyId));

            return EventRepository;

        }
        public static Mock<IUnitOfWork> GetEventFinanceRepository()
        {
            var eventFinances = new List<Vennderful.Domain.Entities.EventFinance>
            {
                new Vennderful.Domain.Entities.EventFinance
                {
                    Id = Guid.NewGuid(),
                    PackageId = Guid.NewGuid(),
                    DepositAmount = 100,
                    TravelFees = 200
                },
                new Vennderful.Domain.Entities.EventFinance
                {
                    Id = Guid.NewGuid(),
                    PackageId = Guid.NewGuid(),
                    DepositAmount = 200,
                    TravelFees = 300
                }
            };

            var evntFinance = new Vennderful.Domain.Entities.EventFinance
            {
                EventId = Guid.Parse("820650f3-fcae-43c7-920f-1d0d2f4adc3d"),
                PackageId = Guid.NewGuid(),
                DepositAmount = 100,
                TravelFees = 200
            };

            var eventFinanceAddons = new List<EventFinanceAddOn>
            {
                new EventFinanceAddOn
                {
                    EventFinanceId = Guid.NewGuid(),
                    AddOnId = Guid.NewGuid(),
                    Quantity = 1,
                    TotalPrice = 100,
                },
                new EventFinanceAddOn
                {
                    EventFinanceId = Guid.NewGuid(),
                    AddOnId = Guid.NewGuid(),
                    Quantity = 2,
                    TotalPrice = 200,
                }
            };

            var eventFinancePayments = new List<EventFinancePaymentSchedule>
            {
                new EventFinancePaymentSchedule
                {
                    EventFinanceId = Guid.NewGuid(),
                    PaymentDate = DateTime.Now,
                    ScheduleAmount = 100,
                },
                new EventFinancePaymentSchedule
                {
                    EventFinanceId = Guid.NewGuid(),
                    PaymentDate = DateTime.Now,
                    ScheduleAmount = 200,
                }
            };

            var mockEventFinanceRepository = new Mock<IUnitOfWork>();

            mockEventFinanceRepository.Setup(repo => repo.eventFinanceRepository.GetAllAsync())
                .ReturnsAsync(eventFinances);

            mockEventFinanceRepository.Setup(repo => repo.eventFinanceRepository.GetEventFinanceByEventId(Guid.Parse("820650f3-fcae-43c7-920f-1d0d2f4adc3d")))
                .ReturnsAsync(evntFinance);

            mockEventFinanceRepository.Setup(repo => repo.eventFinanceAddOnRepository.GetEventFinanceAddOnByEventFinanceId(Guid.Parse("820650f3-fcae-43c7-920f-1d0d2f4adc3d")))
                .ReturnsAsync(eventFinanceAddons);

            mockEventFinanceRepository.Setup(repo => repo.eventFinancePaymentScheduleRepository.GetEventFinancePaymentScheduleByEventFinanceId(Guid.Parse("820650f3-fcae-43c7-920f-1d0d2f4adc3d")))
                .ReturnsAsync(eventFinancePayments);

            mockEventFinanceRepository.Setup(repo => repo.eventFinanceRepository.AddAsync(It.IsAny<Vennderful.Domain.Entities.EventFinance>()))
                .ReturnsAsync(
                    (Vennderful.Domain.Entities.EventFinance eventFinance) =>
                    {
                        eventFinances.Add(eventFinance);
                        return eventFinance;
                    });

            return mockEventFinanceRepository;
        }
        public static Mock<IUnitOfWork> GetEventAndRoomRepository()
        {

            var eventAndRooms = new List<Vennderful.Domain.Entities.EventAndRoom>
            {
              new Vennderful.Domain.Entities.EventAndRoom ()
              {
                  Id= Guid.NewGuid(),
                  EventId=Guid.Parse("094a2f64-7ccb-4048-89f0-6e427058f1cf"),
                  RoomId=new List<Guid>() { Guid.Parse("11607680-1cdf-46b0-8630-7ec0c60d2a2b") },
                  CompanyId = Guid.Parse("a8920ab0-e67c-4527-9ba3-d2862fd351da")
              },
              new Vennderful.Domain.Entities.EventAndRoom ()
              {
                  Id= Guid.NewGuid(),
                  EventId=Guid.Parse("237f3d47-76c0-4ac2-8116-535156342311"),
                  RoomId=new List<Guid>() { Guid.Parse("369e65a0-e00b-4816-89c8-ff2395543626") },
                   CompanyId = Guid.Parse("4fa85f64-5717-4562-b3fc-2c963f66afa6")
              }
            };

            var EventAndRoomRepository = new Mock<IUnitOfWork>();

            EventAndRoomRepository.Setup(repo => repo.EventAndRoomRepository.GetAllAsync())
                .ReturnsAsync(eventAndRooms);
            EventAndRoomRepository.Setup(repo => repo.EventAndRoomRepository.AddAsync(It.IsAny<Vennderful.Domain.Entities.EventAndRoom>()))
               .ReturnsAsync(
                   (Vennderful.Domain.Entities.EventAndRoom newEventRoom) =>
                   {
                       eventAndRooms.Add(newEventRoom);
                       return newEventRoom;
                   });
            EventAndRoomRepository.Setup(repo => repo.EventAndRoomRepository.GetEventAndRooms(It.IsAny<Guid>())).ReturnsAsync((Guid companyId) => eventAndRooms.Where(u => u.CompanyId == companyId).ToList());
            EventAndRoomRepository.Setup(repo => repo.EventAndRoomRepository.GetEventAndRoomsByEventId(It.IsAny<Guid>())).ReturnsAsync((Guid eventId) => eventAndRooms.Where(u => u.EventId == eventId).ToList());

            return EventAndRoomRepository;


        }
        public static Mock<IUnitOfWork> GetEventClientsRepository()
        {
            var eventClients = new List<EventClient>
    {
        new EventClient
        {
            EventId = Guid.Parse("237f3d47-76c0-4ac2-8116-535156342311"),
            ClientId = Guid.Parse("11607680-1cdf-46b0-8630-7ec0c60d2a2b"),
            Note = "Note 1",
            Status = InvitationStatus.Accepted
            // Add other properties as needed
        },
        new EventClient
        {
            EventId = Guid.Parse("237f3d47-76c0-4ac2-8116-535156342311"),
            ClientId = Guid.Parse("369e65a0-e00b-4816-89c8-ff2395543626"),
            Note = "Note 2",
            Status = InvitationStatus.Pending
            // Add other properties as needed
        }
    };

            var mockEventClientsRepository = new Mock<IUnitOfWork>();

            mockEventClientsRepository.Setup(repo => repo.eventClientRepository.GetEventClientsByEventId(It.IsAny<Guid>()))
                .ReturnsAsync((Guid eventId) => eventClients.Where(ec => ec.EventId == eventId).ToList());

            return mockEventClientsRepository;
        }
        public static Mock<IUnitOfWork> GetEventPaymentRepository()
        {

            var eventPayments = new List<Vennderful.Domain.Entities.EventPayment>
            {
              new Vennderful.Domain.Entities.EventPayment ()
              {
                  Id= Guid.NewGuid(),
                  EventId=Guid.Parse("094a2f64-7ccb-4048-89f0-6e427058f1cf"),
                  ClientId=Guid.Parse("11607680-1cdf-46b0-8630-7ec0c60d2a2b"),
                  PaymentMethod=PaymentMethod.Cash,
                  PaymentReason="Additional Payment",
                  PaymentDate= DateTime.Now,
                  PaymentAmount=150.0M,
                  PaymentNote="Additional event payment"

              },
            };

            var EventPaymentRepository = new Mock<IUnitOfWork>();
            EventPaymentRepository.Setup(repo => repo.eventPaymentRepository.GetAllAsync())
               .ReturnsAsync(eventPayments);
            EventPaymentRepository.Setup(repo => repo.eventPaymentRepository.AddAsync(It.IsAny<Vennderful.Domain.Entities.EventPayment>()))
               .ReturnsAsync(
                   (Vennderful.Domain.Entities.EventPayment payment) =>
                   {
                       eventPayments.Add(payment);
                       return payment;
                   });
            EventPaymentRepository.Setup(repo => repo.eventPaymentRepository.GetEventPaymentsByEventId(It.IsAny<Guid>()))
                .ReturnsAsync(eventPayments.OrderByDescending(x => x.Created).ToList());

            return EventPaymentRepository;

        }
        public static Mock<IUnitOfWork> GetEventFinanceBudgetSummaryItemRepository()
        {
            var package = new Package
            {
                Id = Guid.Parse("b6869874-968c-468c-a1de-2db23c9c79e2"),
                PackageName = "PackageA",
                // Populate other properties as needed
            };
            var addOn = new Vennderful.Domain.Entities.AddOn
            {
                Id = Guid.Parse("b6869874-968c-468c-a1de-2db23c9c79e2"),
                AddOnName = "addonA",


            };
            var eventFinance = new Vennderful.Domain.Entities.EventFinance
            {
                Id = Guid.Parse("b6869874-968c-468c-a1de-2db23c9c79e2"),
                EventId = Guid.Parse("b6869874-968c-468c-a1de-2db23c9c79e2"),
                PackageId = Guid.Parse("b6869874-968c-468c-a1de-2db23c9c79e2"),
                DepositAmount = 100,
                PackagePrice = 23,
                TravelFees = 200,
                Package = package // Set the Package property with a valid package object
            };


            var eventFinanceAddOn = new EventFinanceAddOn
            {
                EventFinanceId = Guid.Parse("b6869874-968c-468c-a1de-2db23c9c79e2"),
                AddOnId = Guid.Parse("b6869874-968c-468c-a1de-2db23c9c79e2"),
                Quantity = 1,
                TotalPrice = 100,
                AddOn = addOn
            };


            var mockEventFinanceBudgetSummaryRepository = new Mock<IUnitOfWork>();

            mockEventFinanceBudgetSummaryRepository.Setup(repo =>
                repo.eventFinanceRepository.GetEventFinanceBudgetSummaryItemByEventId(eventFinance.EventId, eventFinance.PackageId))
                .ReturnsAsync(eventFinance);

            mockEventFinanceBudgetSummaryRepository.Setup(repo =>
                repo.eventFinanceAddOnRepository.GetEVentFinanceAddonsByEventFinanceIdAndAddonId(eventFinanceAddOn.EventFinanceId, eventFinanceAddOn.AddOnId))
                .ReturnsAsync(eventFinanceAddOn);


            return mockEventFinanceBudgetSummaryRepository;
        }
        public static Mock<IUnitOfWork> GetEventFinancePaymentScheduleRepository()
        {

            var eventfinancePayments = new List<Vennderful.Domain.Entities.EventFinancePaymentSchedule>
            {
              new Vennderful.Domain.Entities.EventFinancePaymentSchedule ()
              {
                  Id= 1,
                  EventFinanceId=Guid.Parse("094a2f64-7ccb-4048-89f0-6e427058f1cf"),
                  PaymentDate= DateTime.Now,
                  ScheduleAmount=150.0M,
                  Status=0

              },
            };

            var EventFinancePaymentScheduleRepository = new Mock<IUnitOfWork>();
            EventFinancePaymentScheduleRepository.Setup(repo => repo.eventFinancePaymentScheduleRepository.GetAllAsync())
               .ReturnsAsync(eventfinancePayments);
            EventFinancePaymentScheduleRepository.Setup(repo => repo.eventFinancePaymentScheduleRepository.AddAsync(It.IsAny<Vennderful.Domain.Entities.EventFinancePaymentSchedule>()))
               .ReturnsAsync(
                   (Vennderful.Domain.Entities.EventFinancePaymentSchedule payment) =>
                   {
                       eventfinancePayments.Add(payment);
                       return payment;
                   });

            EventFinancePaymentScheduleRepository.Setup(repo => repo.eventFinancePaymentScheduleRepository.GetEventFinancePaymentScheduleByEventFinanceId(It.IsAny<Guid>())).ReturnsAsync((Guid id) => eventfinancePayments.Where(u => u.EventFinanceId == id).ToList());
            EventFinancePaymentScheduleRepository.Setup(repo => repo.eventFinancePaymentScheduleRepository.GetEventFinancePaymentScheduleByEventFinanceIdAndStatus(It.IsAny<Guid>())).ReturnsAsync((Guid id) => eventfinancePayments.Where(u => u.EventFinanceId == id && u.Status == Domain.Enums.PaymentStatus.Pending).ToList());
            EventFinancePaymentScheduleRepository.Setup(repo => repo.eventFinancePaymentScheduleRepository.GetEventFinancePaymentScheduleById(It.IsAny<int>())).ReturnsAsync((int id) => eventfinancePayments.Where(u => u.Id == id).FirstOrDefault());

            return EventFinancePaymentScheduleRepository;

        }
        public static Mock<IUnitOfWork> GetMembersRepository()
        {

            var members = new List<Vennderful.Domain.Entities.Member>
            {
              new Vennderful.Domain.Entities.Member ()
              {
                  Id= Guid.NewGuid(),
                   Email = "user.member@yahoo.com",
                    FirstName = "",
                    LastName="",
                    Gender =Gender.Female,
                    JobTitle = "Bartender",
                    ProfilePicture ="",
                    UserRole = "Admin",
                    ProfileId=Guid.Parse("f47ebfd2-d6b1-4556-831b-9f939a59f747")

              },
            };

            var MemberRepository = new Mock<IUnitOfWork>();
            MemberRepository.Setup(repo => repo.memberRepository.GetAllAsync())
               .ReturnsAsync(members);
            MemberRepository.Setup(repo => repo.memberRepository.AddAsync(It.IsAny<Vennderful.Domain.Entities.Member>()))
               .ReturnsAsync(
                   (Vennderful.Domain.Entities.Member member) =>
                   {
                       members.Add(member);
                       return member;
                   });
            MemberRepository.Setup(repo => repo.memberRepository.GetMembersByCompanyId(It.IsAny<Guid>())).ReturnsAsync((Guid companyId) => members.Where(u => u.Profile.CompanyId == companyId).ToList());

            return MemberRepository;

        }
        public static Mock<IUnitOfWork> GetEventAndMembersRepository()
        {

            var eventAndMembers = new List<Vennderful.Domain.Entities.EventAndMember>
            {
              new Vennderful.Domain.Entities.EventAndMember ()
              {
                  Id= Guid.NewGuid(),
                  EventId=Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                  MemberId=Guid.Parse("8f7ea48a-2d6c-4a22-a411-b27698ac3a2d"),
                  IsActive=true
              },
            };

            var EventAndMemberRepository = new Mock<IUnitOfWork>();
            EventAndMemberRepository.Setup(repo => repo.eventAndMemberRepository.GetAllAsync())
               .ReturnsAsync(eventAndMembers);
            EventAndMemberRepository.Setup(repo => repo.eventAndMemberRepository.AddAsync(It.IsAny<Vennderful.Domain.Entities.EventAndMember>()))
               .ReturnsAsync(
                   (Vennderful.Domain.Entities.EventAndMember member) =>
                   {
                       eventAndMembers.Add(member);
                       return member;
                   });
            EventAndMemberRepository.Setup(repo => repo.eventAndMemberRepository.GetMembersByEventId(It.IsAny<Guid>())).ReturnsAsync((Guid id) => eventAndMembers.Where(u => u.EventId == id).ToList());

            return EventAndMemberRepository;

        }
        public static Mock<IUnitOfWork> GetClientRepository()
        {
            var client = new Vennderful.Domain.Entities.Client
            {
                Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Created = DateTime.Now,
                CreatedBy = "",
                LastModified = DateTime.Now,
                LastModifiedBy = "",
                IsActive = true,
                Email = "client@client.com",
                FirstName = "First",
                LastName = "Name",
                Gender = Gender.Female,
                AccountType = AccountType.Personal,
                CompanyName = "Vender",
                Phone = null,
                Address = new Address
                {
                    Street = "stree",
                    City = "city",
                    State = "state",
                    Country = "country",
                    ZipCode = "zip"
                },
            };

            var mockClientsRepo = new Mock<IUnitOfWork>();

            mockClientsRepo.Setup(repo => repo.clientRepository.GetClientById(It.IsAny<string>()))
                .ReturnsAsync(client);

            return mockClientsRepo;
        }
        public static Mock<IUnitOfWork> UpdateNotificationRepository()
        {
            var mockNotificationRepository = new Mock<IUnitOfWork>();
            mockNotificationRepository.Setup(repo => repo.notificationRepository.UpdateAsync(It.IsAny<Vennderful.Domain.Entities.Notification>()))
                .Returns(Task.FromResult(
                    (Vennderful.Domain.Entities.Notification notification) =>
                    {
                        notification.HasBeenRead = true;
                        return notification;
                    }));

            return mockNotificationRepository;
        }
        public static Mock<IUnitOfWork> GetEventDocumentSignerRepository()
        {
            var documentId = Guid.Parse("ce52f6dc-a4f2-4dfe-be18-62926ca12a7f");
            var eventDocumentId = Guid.Parse("609462fb-9f1b-4e83-842f-003051110d6b");
            var signerId = Guid.Parse("a8ca876b-585e-475a-9caf-ab06c33da91e");
            var senderId = Guid.Parse("6cef10c7-dfdc-49b6-955a-1d8d5a774dbd");

            var evntDocumentSigner = new Vennderful.Domain.Entities.EventDocumentSigner
            {
                Id = Guid.NewGuid(),
                SignatureRequestSender = senderId,
                Created = DateTime.Now
            };

            var sender = new Vennderful.Domain.Entities.UserProfile
            {
                Id = Guid.NewGuid(),
                FirstName = "Sender Fn",
                LastName = "Sender Ln"
            };

            var signer = new Vennderful.Domain.Entities.UserProfile
            {
                Id = Guid.NewGuid(),
                FirstName = "Signer Fn",
                LastName = "Signer Ln"
            };

            var document = new Vennderful.Domain.Entities.Document
            {
                Id = Guid.NewGuid(),
                LastModified = DateTime.Now,
            };

            var mockEventDocumentSignerRepository = new Mock<IUnitOfWork>();

            mockEventDocumentSignerRepository.Setup(repo => repo.eventDocumentSignerRepository.GetEventDocumentSignerByEventDocumentIdAndSignerId(eventDocumentId, signerId))
                .ReturnsAsync(evntDocumentSigner);

            mockEventDocumentSignerRepository.Setup(repo => repo.UserProfileRepository.GetUserProfileByUserId(senderId))
                .ReturnsAsync(sender);

            mockEventDocumentSignerRepository.Setup(repo => repo.UserProfileRepository.GetUserProfileByUserId(signerId))
                .ReturnsAsync(signer);

            mockEventDocumentSignerRepository.Setup(repo => repo.NewDocumentRepository.GetById(documentId))
                .ReturnsAsync(document);

            mockEventDocumentSignerRepository.Setup(repo => repo.eventDocumentSignerRepository.UpdateAsync(It.IsAny<Vennderful.Domain.Entities.EventDocumentSigner>()))
                .Returns(Task.FromResult(
                    (Vennderful.Domain.Entities.EventDocumentSigner eventDocumentSigner) =>
                    {
                        eventDocumentSigner.DocumentStatus = DocumentStatus.Completed;
                        return eventDocumentSigner;
                    }));

            return mockEventDocumentSignerRepository;
        }
        public static Mock<IUnitOfWork> GetEventDocumentRepository()
        {
            var eventt = new Vennderful.Domain.Entities.Event
            {
                Id = Guid.Parse("b6869874-968c-468c-a1de-2db23c9c79e2"),
                EventName = "eventA",
            };

            var document = new Vennderful.Domain.Entities.Document
            {
                Id = Guid.Parse("b6869874-968c-468c-a1de-2db23c9c79e2"),
                DocumentName = "docA",
            };

            var eventDocument = new Vennderful.Domain.Entities.EventDocument
            {
                Id = Guid.Parse("b6869874-968c-468c-a1de-2db23c9c79e2"),
                EventId = Guid.Parse("b6869874-968c-468c-a1de-2db23c9c79e2"),
                DocumentId = Guid.Parse("b6869874-968c-468c-a1de-2db23c9c79e2"),
                DocumentSignerType = (DocumentSignerType)1,
                Event = eventt,
                Document = document,

            };

            var mockEventFDocumentRepository = new Mock<IUnitOfWork>();
            mockEventFDocumentRepository.Setup(repo => repo.eventDocumentRepository.AddAsync(It.IsAny<Vennderful.Domain.Entities.EventDocument>()))
       .ReturnsAsync(eventDocument
          );
            mockEventFDocumentRepository.Setup(repo => repo.eventDocumentRepository.GetAllAsync())
     .ReturnsAsync(new List<EventDocument> { eventDocument });

            return mockEventFDocumentRepository;
        }
        public static Mock<IUnitOfWork> GetNotificationRepository()
        {
            var notifications = new List<Vennderful.Domain.Entities.Notification>
            {
                new Vennderful.Domain.Entities.Notification
                {
                    Id = Guid.Empty,
                    UserId = Guid.Empty,
                    HasBeenRead = false
                },
                new Vennderful.Domain.Entities.Notification
                {
                    Id = Guid.Empty,
                    UserId = Guid.Empty,
                    HasBeenRead = false
                },
            };

            var mockNotificationRepository = new Mock<IUnitOfWork>();

            mockNotificationRepository.Setup(repo => repo.notificationRepository.GetNotificationsByUserId(Guid.Empty))
                .ReturnsAsync(notifications);

            return mockNotificationRepository;
        }
        public static Mock<IUnitOfWork> CreateEventDocumentSignerNotificationRepository()
        {
            var notifications = new List<Vennderful.Domain.Entities.Notification>
            {
                new Vennderful.Domain.Entities.Notification
                {
        UserId= Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
        NotificationType=(NotificationType)1 ,
        NotificationMethod= (NotificationMethod)1 ,
        Content= "mock content",
        ClientId=Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
        EventId= Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
        EventDocumentId=Guid.Parse("375baaf8-c774-4739-b175-a15a2fe9f1ce"),
        DocumentId= Guid.Parse("03f35fc3-8065-41cc-a832-086808f8e2f1"),
        SenderId= Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
        HasBeenRead= true

        },

            };

            var createEventDocumentSignerRepository = new Mock<IUnitOfWork>();

            createEventDocumentSignerRepository.Setup(repo => repo.notificationRepository.GetAllAsync())
                .ReturnsAsync(notifications);

            createEventDocumentSignerRepository.Setup(repo => repo.notificationRepository.AddAsync(It.IsAny<Vennderful.Domain.Entities.Notification>()))
                .ReturnsAsync(
                    (Vennderful.Domain.Entities.Notification notification) =>
                    {
                        notifications.Add(notification);
                        return notification;
                    });

            return createEventDocumentSignerRepository;

        }
        public static Mock<IUnitOfWork> UpdateDocumentRepository()
        {
            var mockDocumentRepository = new Mock<IUnitOfWork>();
            mockDocumentRepository.Setup(repo => repo.NewDocumentRepository.UpdateAsync(It.IsAny<Vennderful.Domain.Entities.Document>()))
                .Returns(Task.FromResult(
                    (Vennderful.Domain.Entities.Document document) =>
                    {
                        return document;
                    }));

            return mockDocumentRepository;
        }
        public static Mock<IUnitOfWork> GetEventTimelineRepository()
        {

            var eventTimelines = new List<Vennderful.Domain.Entities.EventTimeline>
            {
              new Vennderful.Domain.Entities.EventTimeline ()
              {
                  Id = Guid.NewGuid(),
                  EventId = Guid.Parse("094a2f64-7ccb-4048-89f0-6e427058f1cf"),
                  SlotTitle = "Slot 1",
                  StartDate = DateTime.Now,
                  EndDate = DateTime.Now,
                  StartTime = "01:00",
                  EndTime = "12:00",
                  Comment = "",
                  ResponsiblePersons = new List<ResponsiblePerson>
                  {
                      new ResponsiblePerson { Id = Guid.Parse("094a2f64-7ccb-4048-89f0-6e427058f1ce"), Type = "Client" },
                      new ResponsiblePerson { Id = Guid.Parse("094a2f64-7ccb-4048-89f0-6e427058aace"), Type = "Music" }
                  }
              },
            };

            var EventTimelineRepository = new Mock<IUnitOfWork>();
            EventTimelineRepository.Setup(repo => repo.eventTimelineRepository.AddAsync(It.IsAny<Vennderful.Domain.Entities.EventTimeline>()))
               .ReturnsAsync(
                   (Vennderful.Domain.Entities.EventTimeline timeline) =>
                   {
                       eventTimelines.Add(timeline);
                       return timeline;
                   });
            EventTimelineRepository.Setup(repo => repo.eventTimelineRepository.GetEventTimelineByEventId(It.IsAny<Guid>()))
                .ReturnsAsync(eventTimelines.OrderByDescending(x => x.Created).ToList());

            return EventTimelineRepository;

        }
        public static Mock<IUnitOfWork> CreateEventDocumentSignerRepository()
        {
            var eventDocumentSigners = new List<Vennderful.Domain.Entities.EventDocumentSigner>
            {
                new Vennderful.Domain.Entities.EventDocumentSigner
                {
                     EventDocumentId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                SignerId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                SignatureRequestSender = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
         },

            };

            var createEventDocumentSignerRepository = new Mock<IUnitOfWork>();

            createEventDocumentSignerRepository.Setup(repo => repo.eventDocumentSignerRepository.GetAllAsync())
                .ReturnsAsync(eventDocumentSigners);

            createEventDocumentSignerRepository.Setup(repo => repo.eventDocumentSignerRepository.AddAsync(It.IsAny<Vennderful.Domain.Entities.EventDocumentSigner>()));

            return createEventDocumentSignerRepository;

        }
    }
}


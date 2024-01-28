using AutoMapper;
using Moq;
using Shouldly;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.AddOn.DTOs;
using Vennderful.Application.Features.AddOn.Handlers.Commands;
using Vennderful.Application.Features.AddOn.Requests;
using Vennderful.Application.Features.AddOn.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;
using Vennderful.Domain.Entities;

namespace Vennderful.Application.UnitTests.AddOn.Commands
{
    public class CreateAddOnCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockAddOnRepository;
        private readonly CreateAddOnDTO _CreateAddOnDTO;
        private readonly CreateAddOnCommandHandler _CreateAddOnCommandHandler;

        public CreateAddOnCommandHandlerTests()
        {
            _mockAddOnRepository = RepositoryMocks.GetAddOnRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _CreateAddOnCommandHandler = new CreateAddOnCommandHandler(_mockAddOnRepository.Object, _mapper);

            _CreateAddOnDTO = new CreateAddOnDTO
            {
                Id = Guid.NewGuid(),
                AddOnName = "Addon_Test2",
                PricePerUnit = 15.0M,
                Taxable = false,
                AddOnImageUrl = "",
                AddOnDescription = "third add on",
                AddOnNote = "",
                AddOnCategoryId = Guid.NewGuid(),
                RateStructureId = Guid.NewGuid(),
            };
        }

        [Fact]
        public async Task Should_Create_AddOn()
        {
            var result = await _CreateAddOnCommandHandler.Handle(new CreateAddOnCommand() { CreateAddOnDTO = _CreateAddOnDTO }
                , CancellationToken.None);

            var addons = await _mockAddOnRepository.Object.AddOnRepository.GetAllAsync();

            result.ShouldBeOfType<CreateAddOnResponse>();
            result.Success.ShouldBeTrue();
            addons.Count.ShouldBe(3);
        }

        [Fact]
        public async Task Should_Not_Create_AddOn()
        {
            _CreateAddOnDTO.AddOnName = null;
            var result = await _CreateAddOnCommandHandler.Handle(new CreateAddOnCommand() { CreateAddOnDTO = _CreateAddOnDTO }
                , CancellationToken.None);

            var addons = await _mockAddOnRepository.Object.AddOnRepository.GetAllAsync();

            result.ShouldBeOfType<CreateAddOnResponse>();
            result.Success.ShouldBeFalse();
            addons.Count.ShouldBe(2);
        }

        [Fact]
        public async Task Should_Show_Error_Message()
        {
            _CreateAddOnDTO.AddOnName = string.Empty;
            var result = await _CreateAddOnCommandHandler.Handle(new CreateAddOnCommand() { CreateAddOnDTO = _CreateAddOnDTO }
                , CancellationToken.None);

            result.Errors[0].ShouldBe("Add On Name is required.");
        }
    }
}

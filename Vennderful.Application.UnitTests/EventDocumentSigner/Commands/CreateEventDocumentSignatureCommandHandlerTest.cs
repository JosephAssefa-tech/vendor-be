using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.EventDocumentSignature.Dto;
using Vennderful.Application.Features.EventDocumentSignature.Handlers.Commands;
using Vennderful.Application.Features.EventDocumentSignature.Requests;
using Vennderful.Application.Features.EventDocumentSignature.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.EventDocumentSigner.Commands
{
    public class CreateEventDocumentSignatureCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockEventDocumentSignatureRepository;
        CreateEventDocumentSignatureDTO _createEventDocumentSignatureDTO;
        CreateEventDocumentSignatureDTO _createEventDocumentSignatureFailDTO;
        CreateEventDocumentSignatureCommandHandler _createEventDocumentSignatureCommandHandler;

        public CreateEventDocumentSignatureCommandHandlerTest()
        {
            _mockEventDocumentSignatureRepository = RepositoryMocks.CreateEventDocumentSignerRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _createEventDocumentSignatureCommandHandler = new CreateEventDocumentSignatureCommandHandler(_mockEventDocumentSignatureRepository.Object, _mapper);

            _createEventDocumentSignatureDTO = new CreateEventDocumentSignatureDTO
            {
                EventDocumentId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                SignerId = new List<Guid>
                 {
              Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                 Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
                 },
                SignatureRequestSender = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            };

            _createEventDocumentSignatureFailDTO = new CreateEventDocumentSignatureDTO
            {
                SignerId = new List<Guid>
                 {
              Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                 Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
                 },
                SignatureRequestSender = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6")
            };

        }
        [Fact]
        public async Task Should_Create_Event_Document_Signature_Request()
        {

            var result = await _createEventDocumentSignatureCommandHandler.Handle(new CreateEventDocumentSignerCommand() { CreateEventDocumentSignatureDto = _createEventDocumentSignatureDTO }
                , CancellationToken.None);

            var customers = await _mockEventDocumentSignatureRepository.Object.eventDocumentSignerRepository.GetAllAsync();

            result.ShouldBeOfType<CreateEventDocumentSignatureResponse>();
            result.Success.ShouldBeTrue();
            customers.Count.ShouldBe(1);
        }
        [Fact]
        public async Task Should_Not_Create_Event_Document_Signature_Request()
        {

            var result = await _createEventDocumentSignatureCommandHandler.Handle(new CreateEventDocumentSignerCommand() { CreateEventDocumentSignatureDto = _createEventDocumentSignatureFailDTO }
                , CancellationToken.None);

            var customers = await _mockEventDocumentSignatureRepository.Object.eventDocumentSignerRepository.GetAllAsync();

            result.ShouldBeOfType<CreateEventDocumentSignatureResponse>();
            result.Success.ShouldBeFalse();

        }
    }
}

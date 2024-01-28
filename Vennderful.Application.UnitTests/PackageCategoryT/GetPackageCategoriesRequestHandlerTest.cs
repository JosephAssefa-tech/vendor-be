using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Application.Features.Customers.Handlers.Queries;
using Vennderful.Application.Features.Customers.Requests;
using Vennderful.Application.Features.Customers.Responses;
using Vennderful.Application.Features.PackageCategories.DTOs;
using Vennderful.Application.Features.PackageCategories.Handlers.Queries;
using Vennderful.Application.Features.PackageCategories.Requests;
using Vennderful.Application.Features.PackageCategories.Responses;
using Vennderful.Application.Profiles;
using Vennderful.Application.UnitTests.Mock;

namespace Vennderful.Application.UnitTests.PackageCategoryT
{
  
    public class GetPackageCategoriesRequestHandlerTest
    {

        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockRateStructuresRepository;

        public GetPackageCategoriesRequestHandlerTest()
        {
            _mockRateStructuresRepository = RepositoryMocks.GetPackageCategoryRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetRateStructuresTest()
        {
            var handler = new GetPackageCategoriesRequestHandler(_mockRateStructuresRepository.Object, _mapper);
            var result = await handler.Handle(new GetPackageCategoriesRequest(), CancellationToken.None);

            result.ShouldBeOfType<GetPackageCategoriesResponse>();
            result.Data.Count.ShouldBe(2);
        }
    }
}

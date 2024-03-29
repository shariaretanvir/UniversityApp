using Castle.Core.Logging;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Moq;
using StudentApp.Core.Common;
using StudentApp.Core.Feature.Student.Get;
using StudentApp.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudentApp.Tests.Handlers.Student
{
    public class GetStudentHandlerTests
    {
        private readonly Mock<ILogger<GetStudentHandler>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IRepository> _repository;

        public GetStudentHandlerTests()
        {
            _logger = new Mock<ILogger<GetStudentHandler>>();
            _mapper = new Mock<IMapper>();
            _repository = new Mock<IRepository>();
        }
        [Fact]
        public async void GetStudentHandler_Handle_GetStudentResponse()
        {
            //arrange
            GetStudentHandler handler = new GetStudentHandler(_logger.Object, _repository.Object, _mapper.Object);
            var resourceParamMock = new ResourceParameters { PageNumber = 1, PageSize = 5 };
            var request = new GetStudentRequest { ResourceParameters = resourceParamMock };

            var response = new GetStudentResponse(new List<GetStudentModel>(), new PaginationMetaData());
            var studentObject = new List<Domain.Entities.Student>
            {
                new Domain.Entities.Student(Guid.NewGuid(), "Akash",  30, true),
            };
            var pagedList = PagedList<Entities.Student>.Create(studentObject, 1, 5);
            var mapedData = _repository.Setup(x => x.GetAll(request.ResourceParameters)).ReturnsAsync(pagedList);

            var mocStudentModel = new List<GetStudentModel>();
            var studentModel = _mapper.Setup(x => x.Map<List<GetStudentModel>>(pagedList.Items)).Returns(new List<GetStudentModel>());
            var paginMetaDataMock = _mapper.Setup(c => c.Map<PaginationMetaData>(pagedList)).Returns(new PaginationMetaData { CurrentPage = 1, PageSize = 5 });

            CancellationToken token = CancellationToken.None;
            //act
            var result = await handler.Handle(request, token);

            //arrange

            Assert.NotNull(result);
            Assert.Equal(5, result.PaginationMetaData.PageSize = 5);
        }
    }
}

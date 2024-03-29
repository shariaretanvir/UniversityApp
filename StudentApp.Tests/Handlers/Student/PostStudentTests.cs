using MapsterMapper;
using Microsoft.Extensions.Logging;
using Moq;
using StudentApp.Core.Feature.Student.Get;
using StudentApp.Core.Feature.Student.Post;
using StudentApp.Core.Infra;
using StudentApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Tests.Handlers.Student
{
    public class PostStudentTests
    {
        private readonly Mock<ILogger<PostStudentHandler>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public PostStudentTests()
        {
            _logger = new Mock<ILogger<PostStudentHandler>>();
            _mapper = new Mock<IMapper>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }
        [Fact]
        public async void PostStudentHandler_Handle_PostStudentResponse()
        {
            //arrange
            var studentId = Guid.NewGuid();
            var postHandler = new PostStudentHandler(_mapper.Object, _unitOfWork.Object);
            var studentDtoMock = new PostStudentCommand { StudentDetailsModels = new List<StudentAddressModel> { new StudentAddressModel { Id = Guid.NewGuid(), AddressType = AddressType.Permanent, FullAddress = "Birmingham", StudentId = studentId } } };
            var studentMock = new Entities.Student(studentId, "Akash", 30, true);

            _mapper.Setup(x => x.Map<Entities.Student>(studentDtoMock)).Returns(studentMock);

            var studentRepositoryMock = new Mock<IGenericRepository<Entities.Student, Guid>>();
            _unitOfWork.Setup(x => x.GetRepository<Entities.Student, Guid>()).Returns(studentRepositoryMock.Object);
            studentRepositoryMock.Setup(x => x.AddAsync(studentMock)).Returns(Task.CompletedTask);

            if (studentDtoMock.StudentDetailsModels.Count > 0)
            {
                //var studentAddressDtoMock = new Mock<List<StudentAddressModel>>();
                var studentAddressMock = new Mock<List<Entities.StudentAddress>>();
                studentAddressMock.Object.Add(new Domain.Entities.StudentAddress(Guid.NewGuid(), AddressType.Permanent, "Akash"));
                                
                _mapper.Setup(x => x.Map<List<Entities.StudentAddress>>(studentDtoMock.StudentDetailsModels)).Returns(studentAddressMock.Object);
                studentAddressMock.Object.ForEach(x => x.SetStudent(studentMock));

                var addressRepository = new Mock<IGenericRepository<Entities.StudentAddress, Guid>>();
                _unitOfWork.Setup(x => x.GetRepository<Entities.StudentAddress, Guid>()).Returns(addressRepository.Object);
                addressRepository.Setup(x => x.AddRangeAsync(studentAddressMock.Object)).Returns(Task.CompletedTask);
            }
            CancellationToken token = CancellationToken.None;
            _unitOfWork.Setup(x => x.CommitAsync(token)).ReturnsAsync(1);

            //var addStudent = _unitOfWork.Setup(x => x.GetRepository<Entities.Student, Guid>().AddAsync(studentMock.Object)).Returns();

            //act
            var response = await postHandler.Handle(studentDtoMock, token);

            //assert
            Assert.NotNull(response);

        }
    }
}

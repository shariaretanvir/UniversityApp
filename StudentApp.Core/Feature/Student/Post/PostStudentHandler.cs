using MapsterMapper;
using MediatR;
using StudentApp.Core.Infra;
using StudentApp.Domain.Entities;

namespace StudentApp.Core.Feature.Student.Post
{
    public class PostStudentHandler : IRequestHandler<PostStudentCommand, PostStudentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PostStudentHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<PostStudentResponse> Handle(PostStudentCommand command, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Entities.Student>(command);
            await _unitOfWork.GetRepository<Entities.Student, Guid>().AddAsync(student);
            
            if(command.StudentDetailsModels.Count > 0)
            {
                var studentAddress = _mapper.Map<List<StudentAddress>>(command.StudentDetailsModels);
                studentAddress.ForEach(x => x.SetStudent(student));
                await _unitOfWork.GetRepository<Entities.StudentAddress, Guid>().AddRangeAsync(studentAddress);
            }

            if(await _unitOfWork.CommitAsync() > 0)
            {
                return new PostStudentResponse { IsAdded = true };
            }
            else
            {
                return new PostStudentResponse { IsAdded = false };
            }
            
        }
    }
}

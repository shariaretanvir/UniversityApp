using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using StudentApp.Core.Common;
using StudentApp.Core.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Feature.Student.Get
{
    public class GetStudentHandler : IRequestHandler<GetStudentRequest, GetStudentResponse>
    {
        private readonly ILogger<GetStudentHandler> _logger;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public GetStudentHandler(ILogger<GetStudentHandler> logger, IRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<GetStudentResponse> Handle(GetStudentRequest request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetAll(request.ResourceParameters);
            return new GetStudentResponse(_mapper.Map<List<GetStudentModel>>(data.Items), _mapper.Map<PaginationMetaData>(data));
        }
    }
}

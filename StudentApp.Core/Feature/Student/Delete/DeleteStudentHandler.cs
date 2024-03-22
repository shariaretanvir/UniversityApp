using MediatR;
using Microsoft.Extensions.Logging;
using StudentApp.Core.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Feature.Student.Delete
{
    public class DeleteStudentHandler(ILogger<DeleteStudentHandler> _logger, IUnitOfWork _unitOfWork) : IRequestHandler<DeleteStudentCommand, DeleteStudentResponse>
    {
        public async Task<DeleteStudentResponse> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _unitOfWork.GetRepository<Entities.Student, Guid>().GetByIdAsync(request.Id);
            if (student != null)
            {
                _unitOfWork.GetRepository<Entities.Student, Guid>().Delete(student);
                _logger.LogInformation("Calling log from handlers");
                if (await _unitOfWork.CommitAsync() > 0)
                {
                    return new DeleteStudentResponse { IsDeleted = true, IsExists = false };
                }
                else
                {
                    return new DeleteStudentResponse { IsDeleted = false, IsExists = false };
                }
            }
            else
            {
                return new DeleteStudentResponse { IsDeleted = false, IsExists = true };
            }
        }
    }
}

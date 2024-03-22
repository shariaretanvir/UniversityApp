using MapsterMapper;
using MediatR;
using StudentApp.Core.Infra;
using Entities = StudentApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentApp.Domain.Entities;
using Mapster;

namespace StudentApp.Core.Feature.Student.Put
{
    public class PutStudentHandler : IRequestHandler<PutStudentCommand, PutStudentResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PutStudentHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PutStudentResponse> Handle(PutStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _unitOfWork.GetRepository<Entities.Student, Guid>().GetByIdAsync(request.Id);
            
            if(student != null)
            {
                _mapper.Map(request, student);
                var addresses = _unitOfWork.GetRepository<Entities.StudentAddress, Guid>().Query(x => x.Student).Where(x => x.Student.Id == student.Id).ToList();

                var updatedEntities = addresses.Where(x => request.PutStudentDetailsModels.Any(c => c.AddressId == x.Id)).ToList();
                var incomingAddresses = request.PutStudentDetailsModels.Where(x => updatedEntities.Select(c => c.Id).Contains(x.AddressId)).ToList();

                var deletedEntities = addresses.Where(x => !request.PutStudentDetailsModels.Select(c => c.AddressId).Contains(x.Id)).ToList();
                

                var newEntities = request.PutStudentDetailsModels.Where(x => !addresses.Select(c => c.Id).Contains(x.AddressId)).ToList();
                List<Entities.StudentAddress> addedAddress = new();
                newEntities.ForEach(x =>
                {
                    var address = new Entities.StudentAddress(x.AddressId, x.AddressType, x.FullAddress);
                    address.SetStudent(student);
                    addedAddress.Add(address);
                });
                //var shouldBeAddedAddress = _mapper.Map<List<Entities.StudentAddress>>(newEntities);                
                //shouldBeAddedAddress.ForEach(x => x.SetStudent(student));

                //update
                _unitOfWork.GetRepository<Entities.Student, Guid>().Update(student);
                foreach (var item in updatedEntities)
                {
                    var incomingData = incomingAddresses.Where(x => x.AddressId == item.Id).FirstOrDefault();
                    if (incomingData is not null)
                    {   
                        _mapper.Map(incomingData, item);                        
                        _unitOfWork.GetRepository<Entities.StudentAddress, Guid>().Update(item);
                    }
                }

                //add
                await _unitOfWork.GetRepository<Entities.StudentAddress, Guid>().AddRangeAsync(addedAddress);
                //delete
                _unitOfWork.GetRepository<Entities.StudentAddress, Guid>().DeleteRange(deletedEntities);

                if (await _unitOfWork.CommitAsync() > 0)
                {
                    return new PutStudentResponse { IsUpdated = true, IsExists = false };
                }
                else
                {
                    return new PutStudentResponse { IsUpdated = true, IsExists = false };
                }
            }
            else
            {
                return new PutStudentResponse { IsUpdated = false, IsExists = true };
            }
        }
    }
}

using Mapster;
using Entities = StudentApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentApp.Domain.Entities;

namespace StudentApp.Core.Feature.Student.Post
{
    public class MappingProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PostStudentCommand, Entities.Student>()
                .IgnoreNonMapped(true)
                .ConstructUsing(src => new Entities.Student(
                        src.Id,
                        src.Name,
                        src.Age,
                        src.IsActive
                    ));

            config.NewConfig<StudentAddressModel, StudentAddress>()
                .IgnoreNonMapped(true)
                .ConstructUsing(src => new StudentAddress(
                    src.Id,
                    src.AddressType,
                    src.FullAddress
                    ));
        }
    }
}

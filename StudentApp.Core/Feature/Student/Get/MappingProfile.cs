using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Feature.Student.Get
{
    public class MappingProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Entities.Student, GetStudentModel>()
                .IgnoreNonMapped(true)
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Age, src => src.Age)
                .Map(dest => dest.IsActive, src => src.IsActive);
        }
    }
}

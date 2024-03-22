using Mapster;
using Entities = StudentApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Feature.Student.Put
{
    public class MappingProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PutStudentCommand, Entities.Student>()
                .IgnoreNonMapped(true)
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Age, src => src.Age)
                .Map(dest => dest.IsActive, src => src.IsActive);

            config.NewConfig<PutStudentDetailsModel, Entities.StudentAddress>()
                .Map(dest => dest.Id, src => src.AddressId)
                .Map(dest => dest.FullAddress, src => src.FullAddress)
                .Map(dest => dest.AddressType, src => src.AddressType)
                .IgnoreNonMapped(true);

            //config.NewConfig<PutStudentDetailsModel, Entities.StudentAddress>()
            //.IgnoreNonMapped(true)
            //.ConstructUsing(src => new Entities.StudentAddress(
            //        src.AddressId,
            //        src.AddressType,
            //        src.FullAddress
            //    ));
        }
    }
}

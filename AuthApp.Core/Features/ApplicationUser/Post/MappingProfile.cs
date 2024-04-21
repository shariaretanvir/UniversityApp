using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Core.Features.ApplicationUser.Post
{
    public class MappingProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PostApplicationUserCommand, Entities.ApplicationUser>()
                .IgnoreNonMapped(true)
                .ConstructUsing(src => new Entities.ApplicationUser(
                        src.Id,
                        src.UserName,
                        src.Email,
                        src.Password,                        
                        src.IsActive
                    ));
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Core.Services
{
    public interface ITokenService
    {
        string CreateToken(Entities.ApplicationUser applicationUser);
        string CreateRefreshToken();
    }
}

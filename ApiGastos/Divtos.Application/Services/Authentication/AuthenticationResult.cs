using Divtos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Divtos.Application.Services.Authentication
{
    public record AuthenticationResult(
       User User,
       string Token);
}

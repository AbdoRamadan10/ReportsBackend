using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ReportsBackend.Domain.Entities;

namespace ReportsBackend.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        ClaimsPrincipal? GetPrincipalFromToken(string token);
        bool ValidateToken(string token);
    }
}

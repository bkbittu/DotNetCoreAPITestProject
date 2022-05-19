using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI
{
    public class JWTAuthentication : IJWTAuthenticationmanager
    {
        private readonly IDictionary<string, string> users = new Dictionary<string, string>
        { { "test1","pass1"},{"test2","pass2" } };
        private readonly string key;
        public JWTAuthentication(string key)
        {
            this.key = key;
        }
        public string Authenticate(string userName, string password)
        {
            if (!users.Any(u => u.Key == userName && u.Value == password))
            {
                return null;
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userName),
            };
            var tokenOptions = new JwtSecurityToken
            (
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        }

        
    }
}

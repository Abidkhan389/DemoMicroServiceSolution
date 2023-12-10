using Identity.Domain.Entities;
using JwtAuthenticationManager.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "57b6c8d7-473e-4b3d-b415-f1164e2354ad9&$u@vY8!AutHENTICATIONKEy";
        private const int JWT_TOKEN_VALIDITY_MINS = 40;
        private readonly List<UserAccount> _UserAccountList;
        public JwtTokenHandler()
        {
            _UserAccountList = new List<UserAccount>
            {
                new UserAccount{ Email="Admin", Password="admin1234", Role="Administrator"},
                new UserAccount{ Email="Abid", Password="1234", Role="User"}
            }; 
        }
        public AuthenticationResponse? GenerateJwtToken(ApplicationUser model,List<string> userRoles)
        {
            //if (string.IsNullOrWhiteSpace(authenticationRequest.Email) || string.IsNullOrWhiteSpace(authenticationRequest.Password))
             //   return null;
            /* Validation */
            //var userAccount= _UserAccountList.Where(x=> x.Email== authenticationRequest.Email && x.Password==authenticationRequest.pass).FirstOrDefault();
            //if (userAccount==null) return null;
            var tokenExpiryTimeStamp= DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenkey= Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            //var claimsIdentity = new ClaimsIdentity(new List<Claim>
            //{
            //    new Claim(JwtRegisteredClaimNames.Name,authenticationRequest.Email),
            //    new Claim("Role",userAccount.Role)
            //});
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                // Now, if you want to create a ClaimsIdentity object, you can do this:
            var claimsIdentity = new ClaimsIdentity(authClaims);
            var signinCrendentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenkey),
                SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject= claimsIdentity,
                Expires=tokenExpiryTimeStamp,
                SigningCredentials=signinCrendentials,
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);
            return new AuthenticationResponse
            {
                Email = model.Email,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds,
                JwtToken= token,
            };
        }
    }
}

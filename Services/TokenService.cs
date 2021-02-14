using appAngularWeb.Entities;
using appAngularWeb.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace appAngularWeb.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey  _key;

        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        

        public string CreateToken(AppUser user)
        {
            //var claimes = new List<Claim>()
            //{
            //    new Claim(JwtRegisteredClaimNames.NameId,user.UserName)

            //};
            //var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            //var tokenDescriptor = new SecurityTokenDescriptor() { 
            //    Subject=new ClaimsIdentity(claimes),
            //    Expires=DateTime.Now.AddDays(7),
            //    SigningCredentials=creds
            //};
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //return tokenHandler.WriteToken(token);

            //1-claimes
            var claimes = new List<Claim>() { new Claim(JwtRegisteredClaimNames.NameId, user.UserName) };
            //2-creds
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            //3-tokenDescriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject=new ClaimsIdentity(claimes),
                Expires= DateTime.Now.AddDays(7),
                SigningCredentials=creds
            };
            //4-tokenHandler
            var tokenHandler = new JwtSecurityTokenHandler();
            //5-token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //6-writeToken
            return tokenHandler.WriteToken(token);


        }
    }
}

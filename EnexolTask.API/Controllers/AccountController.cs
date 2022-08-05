using EnexolTask.Application.DTO.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EnexolTask.Application.Wrappers;

namespace EnexolTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private  IConfiguration _config;
        public AccountController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost(nameof(Login))]
        public async Task<Response<AuthenticationResponse>> Login(AuthenticationRequest req)
        {
           
            AuthenticationResponse response = new AuthenticationResponse();
            response.ErrorMsg = "";
            try
            {
                if (!(req.UserID == "admin" && req.Password == "admin"))
                {
                    response.ErrorMsg = $"Invalid UserID or Password";
            }
                else
                {
                    string jwtToken = GenerateToken(req);
                    response.JWToken = jwtToken;
                }
            }
            catch (Exception ee)
            {
                response.ErrorMsg = $"Someting Went Wrong";
            }
            if (response.ErrorMsg != null && response.ErrorMsg != "")
                return new Response<AuthenticationResponse>(response, $"Error in Authenticated {req.UserID}");
            else
                return new Response<AuthenticationResponse>(response, $"Authenticated {req.UserID}");

        }

        private string GenerateToken(AuthenticationRequest userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            string issuer = _config.GetValue<string>("Jwt:Issuer");
            //Claims 
            var premClaims = new List<Claim>();
            premClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            premClaims.Add(new Claim("userid", userInfo.UserID));
            premClaims.Add(new Claim("name", userInfo.Name));

            var token = new JwtSecurityToken(issuer, _config["Jwt:Issuer"], premClaims,
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

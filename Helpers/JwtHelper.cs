using Microsoft.IdentityModel.Tokens;
using MovieWeb.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieWeb.Helpers
{
    public class JwtHelper
    {
        private IConfiguration _config;

        public JwtHelper(IConfiguration configuration)
        {
            _config = configuration;
        }

        public string GenerateJsonWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, userInfo.RoleName),
                new Claim("UserId", userInfo.Id.ToString()),
                new Claim("Username", userInfo.Username),
                new Claim("Email", userInfo.Email),
                new Claim("Password", userInfo.Password)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

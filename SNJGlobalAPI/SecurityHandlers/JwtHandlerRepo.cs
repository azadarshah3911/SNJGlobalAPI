using Microsoft.IdentityModel.Tokens;
using SNJGlobalAPI.DtoModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SNJGlobalAPI.SecurityHandlers
{
    public class JwtHandlerRepo : IJwtHandler
    {
        private readonly IConfiguration _config;

        public JwtHandlerRepo(IConfiguration config) =>
            _config = config;

        public string CreateToken(UserAuthDto user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Actor, user.Image),
                new Claim(ClaimTypes.Surname, user.Branch),
            };

            if (user.Roles is not null)
                user.Roles?.ForEach(role =>
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                });

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            int tokenExpireTime = Convert.ToInt32(_config["JwtSettings:ExpTime"]);
            var jwt = new JwtSecurityToken
            (   
                claims: claims,
                expires: DateTime.Now.AddHours(tokenExpireTime),
                signingCredentials: creds,
                issuer: _config.GetValue<string>("JwtSettings:Issuer")
            //audience: HttpContent.
            );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }//Create Token 

        public static int? GetCrntUserId(HttpContext httpContext)
        {
            string userid = httpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            return String.IsNullOrEmpty(userid) ? null : Convert.ToInt32(userid);
        }//Get current user id
    }//class
}

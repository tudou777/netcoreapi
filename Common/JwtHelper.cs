using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TD.Common
{
    public class JwtHelper
    {
        private static JwtSettings settings;
        public static JwtSettings Settings { set { settings = value; } }
        public static string create_Token(string user_id, string user_name, string user_role)
        {
            //这里就是声明我们的claim
            var claims = new Claim[] {
                        new Claim(ClaimTypes.Name,user_id),
                        new Claim(ClaimTypes.Role,user_name),
                        new Claim(ClaimTypes.Sid,user_role)
                    };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims: claims,
                expires: DateTime.Now.AddYears(1),//token有效期1年，（设置20年及以上提示WWW-Authenticate: Bearer error="invalid_token", error_description="The token has no expiration"）
                signingCredentials: creds);
            var Token = new JwtSecurityTokenHandler().WriteToken(token);
            return Token;
        }
    }
}

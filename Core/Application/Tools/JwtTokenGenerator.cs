﻿using Application.Dtos;
using Application.Features.Mediator.Results.AppUserResults;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tools
{
    public class JwtTokenGenerator
    {
        public static TokenResponseDto GenerateToken(GetCheckAppUserQueryResult result)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()),
            };




            if (!string.IsNullOrWhiteSpace(result.UserName))
                claims.Add(new Claim("Username", result.UserName));

            if (result.Roles != null)
            {
                foreach (var role in result.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role)); // veya "role"
                }
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));

            var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefaults.Expire);

            JwtSecurityToken token = new JwtSecurityToken(issuer: JwtTokenDefaults.ValidIssuer, audience: JwtTokenDefaults.ValidAudience, claims: claims, notBefore: DateTime.UtcNow, expires: expireDate, signingCredentials: signinCredentials);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            return new TokenResponseDto(tokenHandler.WriteToken(token), expireDate);
        }
    }
}

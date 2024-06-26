﻿using BlogApp.Core.Intefaces;
using BlogApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogApp.Core;

public class TokenService : ITokenService
{
    private readonly ILogger<TokenService> _logger;
    private readonly IConfiguration _configuration;

    public TokenService(ILogger<TokenService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public string GetToken(UserCredentials user)
    {
		try
		{
            var secretKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Authentication:SecretKey")));

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new()
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("userRoleId", user.Role.Id.ToString()),
                new Claim("Username", user.Username)
            };

            var token = new JwtSecurityToken(
                _configuration.GetValue<string>("Authentication:Issuer"),
                _configuration.GetValue<string>("Authentication:Audience"),
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Authentication:TokenExpirationTimeMinutes")),
                signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
		catch (Exception ex)
		{
            _logger.LogError(ex, ex.Message);
			throw;
		}
    }
}
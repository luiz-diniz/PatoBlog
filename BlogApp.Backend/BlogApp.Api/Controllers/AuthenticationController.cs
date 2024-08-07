﻿using BlogApp.Core.Exceptions;
using BlogApp.Core.Intefaces;
using BlogApp.Models.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogApp.Api.Controllers;

[Route("api/v1/[controller]")]
[AllowAnonymous]
public class AuthenticationController : ApiControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authenticationService)
	{
        _logger = logger;
        _authenticationService = authenticationService;
    }

    [HttpPost]
    public IActionResult Authenticate([FromBody] Login loginModel)
    {
		try
		{
            var result = _authenticationService.Authenticate(loginModel);

            return Ok(result);
        }
        catch (InvalidUserCredentialsException ex)
        {
            return ReturnError(HttpStatusCode.Unauthorized, ex, "Invalid Username or Password", _logger);
        }
        catch (Exception ex)
		{
            return InternalServerError(ex, _logger);	
		}
    }
}
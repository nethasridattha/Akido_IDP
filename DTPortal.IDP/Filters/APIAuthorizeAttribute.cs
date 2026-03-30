//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Security.Principal;
using static DTPortal.IDP.DTOs.ResponseDTO;
using DTPortal.Core.Utilities;
using static DTPortal.Common.CommonResponse;
using DTPortal.Core.Domain.Services.Communication;
using Microsoft.Extensions.Configuration;

namespace AppShieldRestAPICore.Filters
{
    public class APIAuthorizeAttribute : TypeFilterAttribute
    {
        public APIAuthorizeAttribute() :
            base(typeof(APIAuthorizeFilter))
        {
            Arguments = new object[] {};
        }
    }

    public class APIAuthorizeFilter : ControllerBase, IAsyncAuthorizationFilter
    {
        // Initialize logger
        private readonly ILogger<APIAuthorizeFilter> _logger;
        // Initialize Cache Client
        private readonly ICacheClient _cacheClient;
        private readonly IConfiguration Configuration;

        public APIAuthorizeFilter(ILogger<APIAuthorizeFilter> logger,
            ICacheClient cacheClient, IConfiguration configuration)
        {
            _logger = logger;
            _cacheClient = cacheClient;
            Configuration = configuration;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!ModelState.IsValid)
            {
                return;
            }
            _logger.LogDebug("-->GetUserInfo");
            Response response = new Response();

            // Check the value of authorization header
            var authHeader = context.HttpContext.Request.Headers[
                Configuration["AccessTokenHeaderName"]];
            if (string.IsNullOrEmpty(authHeader))
            {
                _logger.LogError("Authorization header not found in request");
                response.Success = false;
                response.Message = "Invalid Authorization header";
                context.Result = Ok(response);
                return;
            }

            _logger.LogDebug("Authorization header recieved");

            // Parse the authorization header
            var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);
            if (null == authHeaderVal.Scheme || null == authHeaderVal.Parameter)
            {
                _logger.LogError("Invalid scheme or parameter in Authorization header");
                response.Success = false;
                response.Message = "Invalid Authorization header";
                context.Result = Unauthorized(response);
                return;
            }

            // Check the authorization is of Bearer type
            if (!authHeaderVal.Scheme.Equals("bearer",
                 StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogError($"Token is not Bearer token type.Recieved {0} type",
                    authHeaderVal.Scheme);
                response.Success = false;
                response.Message = "unsupportedAuthorizationScheme";
                context.Result = Unauthorized(response);
                return;
            }

            // Get the access token record
            var accessToken = await _cacheClient.Get<Accesstoken>("AccessToken",
                authHeaderVal.Parameter);
            if (null == accessToken)
            {
                _logger.LogError("Access token not recieved from cache." +
                    "Expired or Invalid access token");
                response.Success = false;
                response.Message = "The access token is invalid";
                context.Result = Unauthorized(response);
                return;
            }

            // Get the Global Session record
            //var globalSession = await _cacheClient.Get<GlobalSession>("GlobalSession",
            //    accessToken.GlobalSessionId);
            //if (null != globalSession)
            //{
                //_logger.LogError("Global session not recieved from cache." +
                //    "Expired or Invalid access token");
                //response.Success = false;
                //response.Message = "The access token is invalid";
                //context.Result = Unauthorized(response);
                //return;
                //}

                //////////////globalSession.LastAccessTime = DateTime.Now.ToString();

                //////////////var retValue = await _cacheClient.Add("GlobalSession", globalSession.GlobalSessionId,
                //////////////    globalSession);
                //////////////if (0 != retValue.retValue)
                //////////////{
                //////////////    response.Success = false;
                //////////////    response.Message = "Internal Error";
                //////////////    return;
                //////////////}
            //}

            // Send valid authorization response
            return;
        }
    }

}

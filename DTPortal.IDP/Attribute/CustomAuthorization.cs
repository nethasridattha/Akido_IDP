using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DTPortal.Core.Utilities;
using DTPortal.IDP.ViewModel.Saml2;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Request.Body.Peeker;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DTPortal.IDP.Attribute
{
    public class CustomAuthorizationAttribute : System.Attribute, IAuthorizationFilter
    {
        private readonly DTPortal.Core.Domain.Services.IAuthenticationService
                                                  _authenticationService;
        private readonly ILogger<CustomAuthorizationAttribute> _logger;
        public IConfiguration Configuration { get; }
        public CustomAuthorizationAttribute(ILogger<CustomAuthorizationAttribute> logger,
            DTPortal.Core.Domain.Services.IAuthenticationService authenticationService,
            IConfiguration configuration)
        {
            _authenticationService = authenticationService;
            Configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> validateSession(string session)
        {
            try
            {
                _logger.LogDebug("-->validateSession");
                var response = await _authenticationService.CustomValidateSession(session);
                _logger.LogDebug("<--validateSession");
                return response.Success;
            }
            catch (Exception e)
            {
                _logger.LogError("validateSession :{0}", e.Message);
                return false;
            }
        }

        private void ExpireCookie(string name, AuthorizationFilterContext filterContext)
        {
            _logger.LogDebug("-->ExpireCookie");
            if (filterContext.HttpContext.Request.Cookies[name] != null)
            {
                filterContext.HttpContext.Response.Cookies.Delete(name);
            }
            _logger.LogDebug("<--ExpireCookie");
        }

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            try
            {
                _logger.LogDebug(" OnAuthorization");

                var loginUrl = "/Login?METHOD={0}&TARGET={1}";
                var SiteAliasName = Configuration["SiteAliasName"];
                var RequestPath = filterContext.HttpContext.Request.Path.Value;
                var RequestMethod = filterContext.HttpContext.Request.Method.ToLower();

                if (String.IsNullOrEmpty(filterContext.HttpContext.Request.QueryString.Value))
                {
                    _logger.LogInformation("RequestUrl : {url}" +
                        filterContext.HttpContext.Request.Path.Value?.SanitizeForLogging());
                }
                else
                {
                    _logger.LogInformation("RequestUrl : {Path}{Query}",
                  filterContext.HttpContext.Request.Path.Value?.SanitizeForLogging(),
                  filterContext.HttpContext.Request.QueryString.Value?.SanitizeForLogging());
                        }

                var query=filterContext.HttpContext.Request.Query;

                _logger.LogDebug("Domain : " + filterContext.HttpContext.Request.Host.Value);

                if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                {

                    _logger.LogDebug("User is authenticated");
                    if (RequestPath == "/authorization" || 
                        RequestPath == SiteAliasName+"/authorization" ||
                        RequestPath == "/authorization/" ||
                        RequestPath == SiteAliasName + "/authorization/" ||
                        RequestPath.StartsWith("/Saml2/SingleSignOnService") ||
                        RequestPath.StartsWith(SiteAliasName+"/Saml2/SingleSignOnService")
                        )
                    {
                        var GlobalSession = filterContext.HttpContext.User.Claims
                            .FirstOrDefault(c => c.Type == "Session").Value;
                        if (!string.IsNullOrEmpty(GlobalSession))
                        {
                            _logger.LogDebug("GlobalSession Found");
                            var response = validateSession(GlobalSession).Result;
                            if (response)
                            {
                                _logger.LogDebug("GlobalSession valid ");
                                _logger.LogDebug("<--OnAuthorization");
                                return;
                            }
                            else
                            {
                                _logger.LogInformation("GlobalSession Invalid");
                                var SessionName = Configuration["IDPSessionName"].ToString();
                                if (string.IsNullOrEmpty(SessionName))
                                    SessionName = "IDPSession";
                                ExpireCookie(SessionName, filterContext);
                            }
                        }
                        else
                        {
                            _logger.LogDebug("GlobalSession NotFound");
                        }

                        filterContext.HttpContext.SignOutAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme);
                    }
                    else
                    {
                        _logger.LogDebug("<--OnAuthorization");
                        return;
                    }
                }

                _logger.LogInformation("User is not authenticated");

                if (!string.IsNullOrEmpty(SiteAliasName))
                {
                    loginUrl = SiteAliasName + loginUrl;
                    RequestPath = SiteAliasName + RequestPath;
                }

                KeyValuePair<string, string> cookies = new KeyValuePair<string, string>();

                if (filterContext.HttpContext.Request.Cookies.ContainsKey("IDPCookieConsent"))
                    cookies = filterContext.HttpContext.Request.Cookies
                        .FirstOrDefault(x => x.Key == "IDPCookieConsent");

                if (RequestMethod == "get")
                {
                    if (RequestPath.StartsWith("/Saml2/SingleLogoutService/") ||
                        RequestPath.StartsWith(SiteAliasName+"/Saml2/SingleLogoutService/"))
                    {
                        _logger.LogDebug("<--OnAuthorization");
                        return;
                    }
                    else
                    {
                        var Method = RequestMethod;
                        var url = RequestPath
                            + filterContext.HttpContext.Request.QueryString.Value;
                        var Target = Convert.ToBase64String(Encoding.UTF8.GetBytes(url));
                        filterContext.Result = new RedirectResult(string
                            .Format(loginUrl, Method, Target));
                    }
                }
                else
                {
                    var TargetString = "";
                    if (RequestPath.StartsWith("/Saml2/SingleSignOnService/") ||
                        RequestPath.StartsWith(SiteAliasName + "/Saml2/SingleSignOnService/"))
                    {
                        var Bodystring = filterContext.HttpContext.Request
                            .PeekBodyAsync().Result;
                        var body = HttpUtility.ParseQueryString(Bodystring);
                        var TargetObj = new
                        {
                            entityEndpoint = RequestPath,
                            type = "SAMLRequest",
                            context = (body["SAMLRequest"] != null ?
                                        body["SAMLRequest"].ToString() : ""),
                            relayState = (body["relayState"] != null ? 
                                        body["relayState"].ToString() : "")
                        };
                        TargetString = JsonConvert.SerializeObject(TargetObj);

                        var Method = RequestMethod;
                        var Target = Convert.ToBase64String(Encoding.UTF8
                                            .GetBytes(TargetString));

                        filterContext.Result = new RedirectResult(string.Format(loginUrl,
                            Method, Target));
                    }
                    else
                    {
                        _logger.LogDebug("<--OnAuthorization");
                        return;
                        //var Bodystring = filterContext.HttpContext.Request.PeekBodyAsync().Result;
                        //var body = HttpUtility.ParseQueryString(Bodystring);
                        //var TargetObj = new
                        //{
                        //    entityEndpoint = filterContext.HttpContext.Request.Path.Value,
                        //    type = "SAMLRequest",
                        //    context = (body["SAMLRequest"] != null ?
                        //    body["SAMLRequest"].ToString() : ""),
                        //    relayState = (body["relayState"] != null ?
                        //    body["relayState"].ToString() : "")
                        //};
                        //TargetString = JsonConvert.SerializeObject(TargetObj);
                        //var Method = filterContext.HttpContext.Request.Method.ToLower();
                        //var Target = Convert.ToBase64String(Encoding.UTF8.GetBytes(TargetString));
                        //filterContext.Result = new RedirectResult(string.Format("/Login?
                        //METHOD={0}&TARGET={1}", Method, Target));

                    }


                }

                if (!string.IsNullOrEmpty(cookies.Key))
                {
                    filterContext.HttpContext.Response.Cookies.Append(
                        cookies.Key,
                        cookies.Value,
                        new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Lax
                        });
                }

                _logger.LogDebug("<--OnAuthorization");
                return;
            }
            catch (Exception e)
            {
                _logger.LogError("OnAuthorization :{0}", e.Message);
                return;
            }
        }

    }
}

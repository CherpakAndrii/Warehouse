// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Http;
// using Models.Api.Common.Response;
// using Newtonsoft.Json;
// using System.Text;
//
// namespace WebApi.Services
// {
//     public class Authentication : AuthorizationHandler<AuthenticateRequirement>
//     {
//         private readonly IHttpContextAccessor _accessor;
//
//         public Authentication(IHttpContextAccessor accessor)
//         {
//             _accessor = accessor;
//         }
//
//         protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthenticateRequirement requirement)
//         {
//             var httpContext = _accessor.HttpContext;
//
//             var authHeader = httpContext.Request.Headers["Authorization"].ToString();
//
//             var authenticationRequirements = (AuthenticateRequirement)context.Requirements.FirstOrDefault();
//
//             if (authenticationRequirements != null && authHeader != null && authHeader.StartsWith("Basic"))
//             {
//                 var encodedUsernamePassword = authHeader["Basic ".Length..].Trim();
//                 var splitUsernamePassword = Encoding.GetEncoding("iso-8859-1").GetString(Convert.FromBase64String(encodedUsernamePassword)).Split(':');
//
//                 if (splitUsernamePassword[0] == authenticationRequirements.User
//                     && splitUsernamePassword[1] == authenticationRequirements.Password)
//                 {
//                     context.Succeed(requirement);
//                 }
//                 else
//                 {
//                     SetFailResult();
//                 }
//             }
//             else if (authenticationRequirements != null && authHeader != null && authHeader.StartsWith("App"))
//             {
//                 var app = authHeader["App ".Length..].Trim();
//                 if (authenticationRequirements.ApiKey.Equals(app))
//                 {
//                     context.Succeed(requirement);
//                 }
//                 else
//                 {
//                     SetFailResult();
//                 }
//             }
//             else
//             {
//                 SetFailResult();
//             }
//
//             return Task.FromResult(0);
//         }
//
//         private void SetFailResult()
//         {
//             _accessor.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
//             _accessor.HttpContext.Response.ContentType = "application/json";
//
//             _accessor.HttpContext.Response.Headers.Add("error_code", "Unauthorized");
//
//             _accessor.HttpContext.Response.WriteAsync(CreateErrorResponse("Invalid login details"));
//
//             _accessor.HttpContext.Response.CompleteAsync().GetAwaiter();
//         }
//
//         public string CreateErrorResponse(string text)
//             => JsonConvert.SerializeObject(new ErrorResponseModel
//             {
//                 ErrorMessage = text
//             });
//     }
//
//     public class AuthenticateRequirement : IAuthorizationRequirement
//     {
//         public AuthenticateRequirement(string user, string password, string apiKey)
//         {
//             User = user;
//             Password = password;
//
//             ApiKey = apiKey;
//         }
//
//         public string User { get; }
//         public string Password { get; }
//         public string ApiKey { get; }
//     }
// }

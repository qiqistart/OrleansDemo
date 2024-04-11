using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Security.Claims;

namespace Orleans.WebAPI.Identity
{
    public class SystemUserGrantValidator : IExtensionGrantValidator
    {
        public string GrantType => "SystemUser";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var clientId = context.Request.Raw.Get("client_id");
            var userId = context.Request.Raw.Get("userId");
            if (!clientId.Equals("Admin"))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "错误的客户端id");
                return;
            }
            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "用户信息丢失！");
                return;
            }
            var claim = new Claim[]
            {
                new Claim("UserId", userId)
             };
            context.Result = new GrantValidationResult(userId, GrantType, claim);
            return;
        }
    }
}

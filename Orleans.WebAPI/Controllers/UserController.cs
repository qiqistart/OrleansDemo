using IdentityModel.Client;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Orleans.Application.Dto.RequestDto.User;
using OrleansDemo.Common.ApiResultModel;
using Ubiety.Dns.Core;

namespace Orleans.WebAPI.Controllers;

/// <summary>
/// 
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IConfiguration _cfg;

    public UserController(IConfiguration _cfg)
    {
        this._cfg = _cfg;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="loginDto"></param>
    /// <returns></returns>
    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<ApiResult> LoginAsync([FromBody] LoginDto loginDto)
    {

        var tokenClient = new TokenClient(new HttpClient { BaseAddress = new Uri($"{_cfg.GetValue<string>("IdentityConfig:Authority")}{_cfg.GetValue<string>("IdentityConfig:GetTokenUrl")}") },
        new TokenClientOptions
        {
            ClientId = _cfg.GetValue<string>("IdentityConfig:ClientId"),
            ClientSecret = _cfg.GetValue<string>("IdentityConfig:ClientSecret")
        });
        var granType = _cfg.GetValue<string>("IdentityConfig:GranType");
        IDictionary<string, string> parameters = new Dictionary<string, string>()
        {
            { "userId","1"}
        };
        var response = await tokenClient.RequestTokenAsync(granType, parameters);
        if (response == null)
        {
            throw new UnauthorizedAccessException();
        }
        SetResponseHeaderToken(response.AccessToken!, response.RefreshToken!);

        return ApiResult.OkMsg("登录成功");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="refreshTokenDto"></param>
    /// <returns></returns>
    [HttpPost("UserRefreshToken")]
    [AllowAnonymous]
    public async Task<ApiResult> UserRefreshTokenAsync([FromBody] RefreshTokenDto refreshTokenDto)
    {
        var tokenClient = new TokenClient(new HttpClient { BaseAddress = new Uri($"{_cfg.GetValue<string>("IdentityConfig:Authority")}{_cfg.GetValue<string>("IdentityConfig:GetTokenUrl")}") },
     new TokenClientOptions
     {
         ClientId = _cfg.GetValue<string>("IdentityConfig:ClientId"),
         ClientSecret = _cfg.GetValue<string>("IdentityConfig:ClientSecret")
     });
        var granType = _cfg.GetValue<string>("IdentityConfig:GranType");
        IDictionary<string, string> parameters = new Dictionary<string, string>()
        {
            { IdentityServerConstants.PersistedGrantTypes.RefreshToken,refreshTokenDto.RefreshToken},
            {"granType", granType}
        };
        var response = await tokenClient.RequestRefreshTokenAsync(refreshTokenDto.RefreshToken);
        if (response == null)
        {
            throw new UnauthorizedAccessException();
        }
        if (response.IsError)
        {
            throw new Exception("无效的refresh_token");
        }
        SetResponseHeaderToken(response.AccessToken!, response.RefreshToken!);
        return ApiResult.OkMsg("刷新成功");
    }
    /// <summary>
    /// todo
    /// </summary>
    /// <returns></returns>
    [HttpPost("Logout")]
    public async Task<ApiResult> LogoutAsync()
    {
        var token = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
        if (string.IsNullOrEmpty(token)) throw new ArgumentNullException(nameof(token));
        HttpClient client = new HttpClient();
        IDictionary<string, string> parameters = new Dictionary<string, string>()
        {
            {"granType", "SystemUser"}
        };
        var response = await client.RevokeTokenAsync(new TokenRevocationRequest
        {
            Address = "http://localhost:7085/connect/revocation",
            ClientId = "Admin",
            ClientSecret = "secret",
            Parameters = parameters,
            Token = token,
            TokenTypeHint = "access_token"
        });
        if (response.IsError)
        {
            throw new Exception("退出失败!");
        }
        return ApiResult.OkMsg("退出成功");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpPost("Test")]
    public async Task Test()
    {
        await Console.Out.WriteLineAsync("111");
    }
    #region
    /// <summary>
    /// 设置http返回头
    /// </summary>
    /// <param name="accessToken"></param>
    /// <param name="refreshToken"></param>
    private void SetResponseHeaderToken(string accessToken, string refreshToken)
    {
        HttpContext.Response.Headers.Add("access-token", accessToken);
        HttpContext.Response.Headers.Add("x-access-token", refreshToken);
    }
    #endregion



}

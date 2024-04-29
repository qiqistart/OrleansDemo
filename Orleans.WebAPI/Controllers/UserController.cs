using IdentityModel.Client;
using IdentityServer4;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Orleans.Application.Command.User;
using Orleans.Application.Dto.RequestDto.User;
using Orleans.Application.Queries.User;
using OrleansDemo.Common.ApiResultModel;
using OrleansDemo.Common.ErorrException;
using OrleansDemo.Common.MD5;
namespace Orleans.WebAPI.Controllers;

/// <summary>
/// 
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    private readonly IConfiguration _cfg;

    /// <summary>
    /// 
    /// </summary>
    private readonly IMediator _mediator;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_cfg"></param>
    /// <param name="_mediator"></param>
    public UserController(IConfiguration _cfg, IMediator _mediator)
    {
        this._cfg = _cfg;
        this._mediator = _mediator;
    }


    /// <summary>
    ///   登录
    /// </summary>
    /// <param name="loginDto"></param>
    /// <returns></returns>
    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<ApiResult> LoginAsync([FromBody] LoginDto loginDto)
    {
        var userData = await _mediator
                   .Send(new GetUserByAccountQueries(loginDto.Account));
        if (userData == null)
        {
            throw new HintException("用户不存在!");
        }
        if (userData.PassWord != MD5Helper.MD5Hash(loginDto.PassWord))
        {
            throw new HintException("密码错误!");
        }
        if (userData.IsEnable == false)
        {
            throw new HintException("用户已被禁用!");
        }
        #region 登录调用id4
        var tokenClient = new TokenClient(new HttpClient { BaseAddress = new Uri($"{_cfg.GetValue<string>("IdentityConfig:Authority")}{_cfg.GetValue<string>("IdentityConfig:GetTokenUrl")}") },
        new TokenClientOptions
        {
            ClientId = _cfg.GetValue<string>("IdentityConfig:ClientId"),
            ClientSecret = _cfg.GetValue<string>("IdentityConfig:ClientSecret")
        });
        var granType = _cfg.GetValue<string>("IdentityConfig:GranType");
        IDictionary<string, string> parameters = new Dictionary<string, string>()
        {
            { "userId",$"{userData.Id}" }
        };
        var response = await tokenClient.RequestTokenAsync(granType, parameters);
        if (response == null)
        {
            throw new UnauthorizedAccessException();
        }
        SetResponseHeaderToken(response.AccessToken!, response.RefreshToken!);
        #endregion

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
    /// <param name="createUserDto"></param>
    /// <returns></returns>
    [HttpPost("CreateUser"),AllowAnonymous]
    public async Task<ApiResult> CreateUserAsync([FromBody] CreateUserDto createUserDto)
    {

        if (createUserDto == null)
        {
            throw new Exception("参数丢失!");
        }
        if (!createUserDto.PassWord.Equals(createUserDto.SurePassWord))
        {
            throw new Exception("两次密码不一致!");
        }
        try
        {
            var adduUserOk = await _mediator.Send(new AddUserCommand()
            {
                Account = createUserDto.Account,
                PassWord = MD5Helper.MD5Hash(createUserDto.PassWord),
                Avatar = createUserDto.Avatar,
                UserName = createUserDto.UserName
            });
            if (!adduUserOk)
                throw new Exception("创建失败!");

        }
        catch (Exception ex)
        {
            throw new Exception("创建失败!");
        }
        return ApiResult.OkMsg("创建成功!");
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orleans.Grains.User;

namespace Orleans.WebAPI.Controllers;

/// <summary>
/// 
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IClusterClient _client;
    public UserController(IClusterClient _client)
    {
        this._client = _client;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task Test()
    {
         await _client.GetGrain<IUserGrains>(Guid.NewGuid().ToString()).SayHalo();
    }



}

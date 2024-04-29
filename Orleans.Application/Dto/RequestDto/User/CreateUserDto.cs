using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.Application.Dto.RequestDto.User;

public class CreateUserDto
{

    /// <summary>
    ///     
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    ///     
    /// </summary>
    public string Account { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string PassWord { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string SurePassWord { get; set; }
}

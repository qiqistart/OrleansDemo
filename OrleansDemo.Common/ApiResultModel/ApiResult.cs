namespace OrleansDemo.Common.ApiResultModel;

/// <summary>
/// 返回结果的规范
/// </summary>
public class ApiResult
{
    public ApiResult()
    {
    }

    public ApiResult(int code, string msg)
    {
        Code = code;
        Msg = msg;
    }

    /// <summary>
    /// 错误代码
    /// </summary>

    public int Code { get; set; } = -1;

    /// <summary>
    /// 错误描述
    /// </summary>
    public string Msg { get; set; } = "发生未知错误";

    public static ApiResult Fail(string msg)
    {
        return new ApiResult(-1, msg);
    }

    public static ApiResult Ok()
    {
        return new ApiResult(0, "成功");
    }

    public static ApiResult OkMsg(string msg)
    {
        return new ApiResult(0, msg);
    }

    public static ApiResult<T> Ok<T>(T data)
    {
        return new ApiResult<T>(data, 0, "获取数据成功");
    }

    public static ApiResult<T> Fail<T>(string msg)
    {
        return new ApiResult<T>(default, -1, msg);
    }


    public static PageResult<T> OkPage<T>(List<T>? data, PagerModel pager)
    {
        return new PageResult<T>(data, pager, 0, "获取数据成功");
    }

    public static PageResult<T> FailPage<T>(string msg)
    {
        return new PageResult<T>(default, default, -1, msg);
    }
}

/// <summary>
/// 返回结果的规范
/// </summary>
/// <typeparam name="T">结果的泛型</typeparam>
public class ApiResult<T> : ApiResult
{
    /// <summary>
    /// 结果
    /// </summary>
    public T Data { get; set; }

    protected internal ApiResult(T data, int code, string msg)
        : base(code, msg)
    {
        Data = data;
    }
}

/// <summary>
/// 
/// </summary>
public class PagerModel
{
    /// <summary>
    /// 计算分页信息
    /// </summary>
    /// <param name="pageIndex">当前页码</param>
    /// <param name="pageSize">页的大小</param>
    /// <param name="totalCount">记录总数</param>
    [Newtonsoft.Json.JsonConstructor]
    public PagerModel(int pageIndex, int pageSize, int totalCount)
    {
        PageIndex = pageIndex;
        PageSize = pageSize == 0 ? 1 : pageSize;
        TotalCount = totalCount;
        PageCount = Convert.ToInt32(Math.Ceiling(TotalCount * 1.0 / PageSize));
    }

    /// <summary>
    /// 当前页码
    /// </summary>
    public int PageIndex { get; set; }

    /// <summary>
    /// 页的大小
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// 页的总数
    /// </summary>
    public int PageCount { get; set; }

    /// <summary>
    /// 记录总数
    /// </summary>
    public int TotalCount { get; set; }
}

public class PageResult<T> : ApiResult
{
    public PageResult()
    {
    }

    /// <summary>
    /// 结果
    /// </summary>
    public List<T>? Data { get; set; }

    public PagerModel Pager { get; set; }


    protected internal PageResult(List<T> data, PagerModel pager, int code, string msg)
        : base(code, msg)
    {
        Data = data;
        Pager = pager;
    }
}

public static class ResultExtensions
{
    public static ApiResult<T> Ok<T>(this T data, string msg = "获取数据成功", int code = 0) where T : new()
    {
        return new ApiResult<T>(data, code, msg);
    }
}
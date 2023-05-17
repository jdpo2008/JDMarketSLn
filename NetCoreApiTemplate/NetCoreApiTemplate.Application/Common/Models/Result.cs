namespace JDMarketSLn.Application.Common.Models;

public class Result
{
    internal Result(bool succeeded, IEnumerable<string> errors, int statusCode = 200)
    {
        Succeeded = succeeded;
        StatusCode = statusCode;
        Errors = errors.ToArray();
    }

    public bool Succeeded { get; init; }

    public string[] Errors { get; init; }

    public int StatusCode { get; set; }

    public static Result Success(int statusCode)
    {
        return new Result(true, Array.Empty<string>(), statusCode);
    }

    public static Result Failure(IEnumerable<string> errors, int statusCode)
    {
        return new Result(false, errors, statusCode);
    }
}

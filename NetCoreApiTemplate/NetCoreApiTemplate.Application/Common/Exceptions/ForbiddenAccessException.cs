namespace JDMarketSLn.Application.Common.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException() : base("You don´t have access this page o resource.") { }

}

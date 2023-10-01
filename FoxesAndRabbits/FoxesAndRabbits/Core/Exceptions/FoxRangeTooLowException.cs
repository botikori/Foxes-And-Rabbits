namespace FoxesAndRabbits.Core.Exceptions;

public class FoxRangeTooLowException : Exception
{
    public FoxRangeTooLowException() : base("Fox detection range must be higher than rabbit's!")
    {
    }

}
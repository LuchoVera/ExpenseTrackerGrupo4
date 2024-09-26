namespace ExpenseTrackerGrupo4.src.Utils;

public class TokenValidatorConstants
{
    public static readonly string _secretKey = 
        Environment.GetEnvironmentVariable("TOKEN_SECRET_KEY") 
        ?? throw new InvalidOperationException("The environment variable TOKEN_SECRET_KEY is not set.");
}

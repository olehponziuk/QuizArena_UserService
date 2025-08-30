namespace UsersService.Application.Exeptions;

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string messege) : base(messege)
    {
        
    }
}
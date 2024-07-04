public class UserNotFoundException : Exception
{
    public UserNotFoundException(string message) : base(message) { }
}

public class AuthorNotFoundException : Exception
{
    public AuthorNotFoundException(string message) : base(message) { }
}

public class UserAlreadyFollowingException : Exception
{
    public UserAlreadyFollowingException(string message) : base(message) { }
}
public class UserNotFollowingException : Exception
{
    public UserNotFollowingException(string message) : base(message) { }
}
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

public class ConflictException : Exception
{
    public ConflictException(string message) : base(message) { }
}

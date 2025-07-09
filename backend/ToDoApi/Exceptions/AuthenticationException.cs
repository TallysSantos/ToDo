namespace ToDoApi.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException(string message) : base (message) { }
    }
}

namespace ToDoApi.Exceptions
{
    public class TaskValidationException : Exception
    {
        public TaskValidationException(string message) : base (message) { }
    }
}

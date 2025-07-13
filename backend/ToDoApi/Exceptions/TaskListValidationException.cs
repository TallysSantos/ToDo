namespace ToDoApi.Exceptions
{
    public class TaskListValidationException : Exception
    {
        public TaskListValidationException(string message) : base (message) { }
    }
}

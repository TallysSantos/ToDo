namespace ToDoApi.Exceptions
{
    public class TaskItemValidationException : Exception
    {
        public TaskItemValidationException(string message) : base (message) { }
    }
}

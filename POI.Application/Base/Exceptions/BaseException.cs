namespace POI.Application.Base.Exceptions
{
    public abstract class BaseException : Exception
    {
        public string Title { get; }
        public BaseException(string title, string message) : base(message)
        {
            Title = title;
        }

        protected BaseException(string title,string message, Exception innerException)
            : base(message, innerException)
        {
            Title = title;
        }
    }
}

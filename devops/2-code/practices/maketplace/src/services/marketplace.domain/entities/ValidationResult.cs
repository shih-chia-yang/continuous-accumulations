namespace marketplace.domain.entities
{
    public class ValidationResult
    {
        public string Name { get; private set; }

        public string Message { get; private set; }

        public ValidationResult(string name ,string message)
        {
            Name = name;
            Message = message;
        }
    }
}
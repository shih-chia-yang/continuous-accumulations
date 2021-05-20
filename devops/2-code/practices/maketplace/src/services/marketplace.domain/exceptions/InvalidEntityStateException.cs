using System;
namespace marketplace.domain.exceptions
{
    public class InvalidEntityStateException:Exception
    {
        public InvalidEntityStateException(object entity,string message)
        :base($"Entity {entity.GetType().Name} state change rejected , {message}")
        {
            
        }
    }
}
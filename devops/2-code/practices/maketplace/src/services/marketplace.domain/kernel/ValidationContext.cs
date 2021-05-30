using System;
using System.Collections.Generic;
using System.Linq;
using marketplace.domain.entities;

namespace marketplace.domain.kernel
{
    public abstract class VaildationContext<T>
    where T:class
    {
        public IEnumerable<ValidationResult> Errors => _errors.AsReadOnly();
        private List<ValidationResult> _errors;

        protected T Entity;
        public VaildationContext(T entity)
        {
            _errors = new List<ValidationResult>();
            Entity = entity;
        }
        public void AddError(string entity,string message)
        {
            if(_errors.Any(x=>x.Name.Equals(entity)))
            {
                throw new ArgumentException($"{entity} error message is already existed");
            }
            _errors.Add(new ValidationResult(entity, message));
        }

        public abstract bool Validate();
    }
}
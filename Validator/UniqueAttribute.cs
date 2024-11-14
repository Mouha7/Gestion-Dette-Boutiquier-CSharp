using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace gestion_dette_web.Validator
{
    public class UniqueAttribute : ValidationAttribute
    {
        private readonly string _propertyName;

        public UniqueAttribute(string propertyName)
        {
            _propertyName = propertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = (DbContext)validationContext.GetService(typeof(DbContext))!;
            var entity = validationContext.ObjectInstance;
            var entityType = entity.GetType();

            var property = entityType.GetProperty(_propertyName);
            if (property == null)
            {
                throw new ArgumentException($"Property '{_propertyName}' not found on type '{entityType.Name}'.");
            }

            var primaryKeyProperty = entityType.GetProperty("Id");
            if (primaryKeyProperty == null)
            {
                throw new ArgumentException($"Primary key property 'Id' not found on type '{entityType.Name}'.");
            }

            var primaryKeyValue = primaryKeyProperty.GetValue(entity);

            // Utilisez la méthode générique pour spécifier explicitement le type d'entité
            var query = context.Set<object>()
                .Where(x => property.GetValue(x).ToString() == value.ToString())
                .Where(x => primaryKeyProperty.GetValue(x).ToString() != primaryKeyValue.ToString());

            if (query.Any())
            {
                return new ValidationResult($"The value '{value}' for '{_propertyName}' is already in use.");
            }

            return ValidationResult.Success;
        }
    }
}
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Storyteller.Helpers
{
    public static class PartialUpdateHelper
    {
        public static void ApplyPatch<T>(T existingEntity, T updatedEntity)
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                // Skip key properties and properties with a value of null
                if (string.Equals(property.Name, "Id", StringComparison.OrdinalIgnoreCase) ||
                    property.GetCustomAttributes(typeof(KeyAttribute), false).Length > 0)
                {
                    continue;
                }

                // Get the current value of the property
                var newValue = property.GetValue(updatedEntity);

                // Check if the property is a class and not a string (for handling nested objects)
                if (newValue != null && property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    // Get the current value of the property
                    var existingValue = property.GetValue(existingEntity);

                    // Apply patch recursively for nested objects
                    if (existingValue != null)
                    {
                        // Recursively apply the patch to the nested object
                        ApplyPatch(existingValue, newValue);
                    }
                }
                else if (newValue != null)
                {
                    // Update the property value
                    property.SetValue(existingEntity, newValue);
                }
            }
        }
    }

}
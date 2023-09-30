namespace POI.Api.Utils;

public class Validation
{
    //need more work
    public bool ValidateRequest(object obj)
    {
        var type = obj.GetType();
        foreach (var property in type.GetProperties())
        {
            if (property.PropertyType.IsValueType && !property.PropertyType.IsGenericType)
            {
                var value = property.GetValue(obj);
                if (value.Equals(Activator.CreateInstance(property.PropertyType)))
                {
                    throw new ArgumentException($"{property.Name} cannot be default value.");
                }
            }

            if (property.PropertyType == typeof(string))
            {
                var value = property.GetValue(obj) as string;
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException($"{property.Name} cannot be null or empty.");
                }
            }
        }
        return true;
    }

}
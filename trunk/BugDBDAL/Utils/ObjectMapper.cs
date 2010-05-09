using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BugDB.DataAccessLayer.Utils
{
  /// <summary>
  /// Utility class for copying values of properties 
  /// with the same name from one object to another.
  /// </summary>
  public class ObjectMapper<T1, T2>
    where T1 : new()
    where T2 : new()
  {
    private PropertyDescriptorCollection m_t1Props = TypeDescriptor.GetProperties(typeof(T1));
    private PropertyDescriptorCollection m_t2Props = TypeDescriptor.GetProperties(typeof(T2));

    /// <summary>
    /// Copies object.
    /// </summary>
    public T1 Copy(T2 source)
    {
      T1 dest = new T1();
      Copy(m_t2Props, source, m_t1Props, dest);
      return dest;
    }

    /// <summary>
    /// Copies object.
    /// </summary>
    public T2 Copy(T1 source)
    {
      T2 dest = new T2();
      Copy(m_t1Props, source, m_t2Props, dest);
      return dest;
    }

    /// <summary>
    /// Copies public properties from source to destination.
    /// </summary>
    private static void Copy(PropertyDescriptorCollection srcProps, object source,
      PropertyDescriptorCollection destProps, object dest)
    {
      if (source == null)
      {
        throw new ArgumentException("source");
      }
      if (dest == null)
      {
        throw new ArgumentException("dest");
      }
      // Enumerate all properties in source type
      foreach (PropertyDescriptor srcProp in srcProps)
      {
        // Try to find correspondent property in target type (don't ignore case)
        PropertyDescriptor destProp = destProps.Find(srcProp.Name, false);
        if (destProp != null)
        {
          // Get source value
          object value = srcProp.GetValue(source);
          // Try to convert value if compatible
          value = ChangeType(value, destProp.PropertyType);
          // Set converted value to destination
          destProp.SetValue(dest, value);
        }
      }
    }

    /// <summary>
    /// Converts value to desired type if possible.
    /// </summary>
    /// <remarks>
    /// Solution of Nullable enumerations.
    /// Originally from http://weblogs.asp.net/pjohnson/archive/2006/02/07/437631.aspx
    /// </remarks>
    static public object ChangeType(object value, Type type)
    {
      if (value == null && type.IsGenericType)
        return Activator.CreateInstance(type);
      if (value == null)
        return null;
      if (type == value.GetType())
        return value;
      if (type.IsEnum)
      {
        if (value is string)
          return Enum.Parse(type, value as string);
        else
          return Enum.ToObject(type, value);
      }
      if (!type.IsInterface && type.IsGenericType)
      {
        Type innerType = type.GetGenericArguments()[0];
        object innerValue = ChangeType(value, innerType);
        return Activator.CreateInstance(type, new object[] { innerValue });
      }
      if (value is string && type == typeof(Guid))
        return new Guid(value as string);
      if (value is string && type == typeof(Version))
        return new Version(value as string);
      if (!(value is IConvertible))
        return value;
      return Convert.ChangeType(value, type);
    }
  }
}

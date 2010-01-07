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
  public class ObjectCopier<T1, T2>
    where T1 : new()
    where T2 : new()
  {
    private PropertyDescriptorCollection m_t1Props = TypeDescriptor.GetProperties(typeof(T1));
    private PropertyDescriptorCollection m_t2Props = TypeDescriptor.GetProperties(typeof(T2));

    public T1 Copy(T2 source)
    {
      T1 dest = new T1();
      Copy(m_t2Props, source, m_t1Props, dest);
      return dest;
    }

    public T2 Copy(T1 source)
    {
      T2 dest = new T2();
      Copy(m_t1Props, source, m_t2Props, dest);
      return dest;
    }

    private static void Copy(PropertyDescriptorCollection srcProps, object source, 
      PropertyDescriptorCollection destProps, object dest)
    {
      if( source == null )
      {
        throw new ArgumentException("source");
      }
      if( dest == null )
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
          // Handle special case with Nullable types
          if (srcProp.PropertyType.IsGenericType &&
            srcProp.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
            Nullable.GetUnderlyingType(srcProp.PropertyType).IsEnum)
          {
            destProp.SetValue(dest, ChangeType(srcProp.GetValue(source), 
              destProp.PropertyType));
          }
          else
          {
            //object value = srcProp.GetValue(source);
            //object res = TypeDescriptor.GetConverter(destProp.PropertyType).ConvertFrom(value);
            // Copy value
            destProp.SetValue(dest, srcProp.GetValue(source));
          }
        }
      }
    }

    /// <summary>
    /// </summary>
    /// <remarks>
    /// Solution of Nullable enumerations.
    /// Originally from http://weblogs.asp.net/pjohnson/archive/2006/02/07/437631.aspx
    /// </remarks>
    static public object ChangeType(object value, Type type)
    {
      if (value == null && type.IsGenericType) return Activator.CreateInstance(type);
      if (value == null) return null;
      if (type == value.GetType()) return value;
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
      if (value is string && type == typeof(Guid)) return new Guid(value as string);
      if (value is string && type == typeof(Version)) return new Version(value as string);
      if (!(value is IConvertible)) return value;
      return Convert.ChangeType(value, type);
    } 
  }


  /// <summary>
  /// Utility class for copying values of properties 
  /// with the same name from one object to another.
  /// </summary>
  public class ObjectMapper
  {
    /// <summary>
    /// Creates new object of Target type and 
    /// copy values of all properties with the same 
    /// names as in in Source type.
    /// </summary>
    public static T Map<S, T>(S source) where T : new()
    {
      T target = new T();
      Copy(source, target);
      return target;
    }

    /// <summary>
    /// Copy values of corresponding properties
    /// from source to target.
    /// </summary>
    private static void Copy<S, T>(S source, T target)
    {
      if( typeof(S).IsClass && source == null )
      {
        throw new ArgumentException("source");
      }
      if( typeof(T).IsClass && target == null )
      {
        throw new ArgumentException("target");
      }

      PropertyDescriptorCollection srcProps = TypeDescriptor.GetProperties(typeof (S));
      PropertyDescriptorCollection destProps = TypeDescriptor.GetProperties(typeof (T));
      // Enumerate all properties in source type
      foreach( PropertyDescriptor srcProp in srcProps )
      {
        // Try to find correspondent property in target type (don't ignore case)
        PropertyDescriptor destProp = destProps.Find(srcProp.Name, false);
        if( destProp != null )
        {
          // Copy value
          destProp.SetValue(target, srcProp.GetValue(source));
        }
      }
    }
  }
}


  /*
   * 
/// <summary>
/// Converts any compatible object to an instance of T.
/// </summary>
/// <param name="value">The value to convert.</param>
/// <returns>The converted value.</returns>
public static T Convert(object value)
{
    if (value is T)
    {
        return (T)value;
    }

    Type t = typeof(T);

    if (t == typeof(string))
    {
        if (value is DBNull || value == null)
        {
            return (T)(object)null;
        }
        else
        {
            return (T)(object)(value.ToString());
        }
    }
    else
    {
        if (value is DBNull || value == null)
        {
            return default(T);
        }

        if (value is string && string.IsNullOrEmpty((string)value))
        {
            return default(T);
        }

        try
        {
            return (T)value;
        }
        catch (InvalidCastException)
        {
        }

        if (Nullable.GetUnderlyingType(t) != null)
        {
            t = Nullable.GetUnderlyingType(t);
        }

        MethodInfo parse = t.GetMethod("Parse", new Type[] { typeof(string) });

        if (parse != null)
        {
            object parsed = parse.Invoke(null, new object[] { value.ToString() });
            return (T)parsed;
        }
        else
        {
            throw new InvalidOperationException("The value you specified is not a valid " + typeof(T).ToString());
        }
    }
}
 
   */

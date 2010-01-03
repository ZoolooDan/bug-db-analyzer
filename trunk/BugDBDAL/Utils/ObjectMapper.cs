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
          // Copy value
          destProp.SetValue(dest, srcProp.GetValue(source));
        }
      }
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

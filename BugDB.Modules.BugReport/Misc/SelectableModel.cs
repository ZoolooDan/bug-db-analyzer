namespace BugDB.Modules.BugReport.Misc
{
  /// <summary>
  /// Generic wrap model for UI elements which has selected state.
  /// </summary>
  public class SelectableModel<T> : SimpleModel
  {
    private T m_data;
    private bool? m_isSelected = false;

    /// <summary>
    /// Wrapped data.
    /// </summary>
    public T Data
    {
      get
      {
        return m_data;
      }
      set
      {
        if( !ReferenceEquals(m_data, value) )
        {
          m_data = value;
          FireProperyChanged("Data");
        }
      }
    }

    /// <summary>
    /// Checked state.
    /// </summary>
    public bool? IsSelected
    {
      get
      {
        return m_isSelected;
      }
      set
      {
        if( m_isSelected != value )
        {
          m_isSelected = value;
          FireProperyChanged("IsSelected");
        }
      }
    }
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BugDB.DataAccessLayer.DataTransferObjects
{
  /// <summary>
  /// Sub module represents distinct sub parts of 
  /// the module of the application for which bug 
  /// records can be created.
  /// </summary>
  /// <remarks>
  /// Sub module has title. And it is associated
  /// with particular module.
  /// </remarks>
  public class SubModule
  {
    /// <summary>
    /// Unique ID of the module.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID of parent module.
    /// </summary>
    public int ModuleId { get; set; }

    /// <summary>
    /// Title of the module.
    /// </summary>
    public string Title { get; set; }
  }
}

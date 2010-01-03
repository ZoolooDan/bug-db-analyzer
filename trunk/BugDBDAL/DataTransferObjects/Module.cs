using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BugDB.DataAccessLayer.DataTransferObjects
{
  /// <summary>
  /// Module represents distinct parts of 
  /// application for which bug records can be 
  /// created.
  /// </summary>
  /// <remarks>
  /// Module has title and associated with particular
  /// application. It also has subordinate collection
  /// of submodules (which actually isn't represented
  /// in this DTO).
  /// </remarks>
  public class Module
  {
    /// <summary>
    /// Unique ID of the module.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ID of parent application.
    /// </summary>
    public int ApplicationId { get; set; }

    /// <summary>
    /// Title of the module.
    /// </summary>
    public string Title { get; set; }
  }
}

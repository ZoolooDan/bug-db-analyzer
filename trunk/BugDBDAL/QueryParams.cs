using System;

using BugDB.DataAccessLayer.DataTransferObjects;


namespace BugDB.DataAccessLayer
{
  /// <summary>
  /// Paramters for queries in data provider.
  /// </summary>
  public class QueryParams
  {
    /// <summary>
    /// Search in history or in recent revision only.
    /// </summary>
    /// <remarks>
    /// True to search in all revisions. False
    /// to apply query only to recent revisions.
    /// </remarks>
    public bool IncludeHistory { get; set; }

    /// <summary>
    /// Minimum bug number to process.
    /// </summary>
    public int? BugNumberMin { get; set;}

    /// <summary>
    /// Maximum bug number to process.
    /// </summary>
    public int? BugNumberMax { get; set; }

    /// <summary>
    /// Bug types to process.
    /// </summary>
    public BugType[] BugTypes { get; set; }

    /// <summary>
    /// Bug statuses to process.
    /// </summary>
    public BugStatus[] BugStatuses { get; set; }

    /// <summary>
    /// Start date of period to process.
    /// </summary>
    public DateTime? DateFrom { get; set; }

    /// <summary>
    /// End date of period to process.
    /// </summary>
    public DateTime? DateTo { get; set; }

    /// <summary>
    /// Application IDs to process.
    /// </summary>
    public int[] Apps { get; set; }

    /// <summary>
    /// Module IDs in application to process.
    /// </summary>
    /// <remarks>
    /// Only one application shall be specified.
    /// </remarks>
    public int[] Modules { get; set; }

    /// <summary>
    /// Sub modeule IDs in module to process.
    /// </summary>
    /// <remarks>
    /// Only one module shall be specified.
    /// </remarks>
    public int[] SubModules { get; set; }

    /// <summary>
    /// Found release IDs to process.
    /// </summary>
    public int[] FoundReleases { get; set; }

    /// <summary>
    /// Terget release IDs to process
    /// </summary>
    public int[] TargetReleases { get; set; }

    /// <summary>
    /// Severities to process.
    /// </summary>
    public BugSeverity[] Severities { get; set; }

    /// <summary>
    /// Priorities to process.
    /// </summary>
    public int[] Priorities { get; set; } 

    /// <summary>
    /// Contributor IDs to process.
    /// </summary>
    public int[] Contributors { get; set; }

    /// <summary>
    /// Team leader IDs to process.
    /// </summary>
    public int[] TeamLeaders { get; set; }

    /// <summary>
    /// Developer IDs to process.
    /// </summary>
    public int[] Developers { get; set; }

    /// <summary>
    /// Testers (QA/QM) IDs to process.
    /// </summary>
    public int[] Testers { get; set; }

    /// <summary>
    /// String to search in summary.
    /// </summary>
    public string Summary { get; set; }
  }
}
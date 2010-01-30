using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics;

using BugDB.DataAccessLayer.DataTransferObjects;


namespace BugDB.Reporter.RevisionTransition
{
  /// <summary>
  /// </summary>
  /// <remarks>
  /// Some class of reports is based on transition of bug revision status
  /// from one value to another. It's reasonable to define those transition
  /// through configuration files and create class (probably enumerator) 
  /// which can filter out revisions which actually do not initiate transition 
  /// and allow to concentrate on those which do.
  /// </remarks>
  internal class RevisionTransitionFilter
  {
    #region Private Fields
    private Configuration.ReportConfig m_reportConfig;
    #endregion Private Fields

    #region Constructors
    /// <summary>
    /// Constructs filter based on report configuration file.
    /// </summary>
    public RevisionTransitionFilter()
    {
      XmlSerializer serializer = new XmlSerializer(typeof(Configuration.ReportConfig));

      string configPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                       @"Config\ReporterConfig.xml");
      using( Stream stream = new FileStream(configPath, FileMode.Open, FileAccess.Read) )
      {
        m_reportConfig = (Configuration.ReportConfig)serializer.Deserialize(stream);
      }
    }
    #endregion Constructors

    #region Public Methods
    //public IEnumerable<RevisionStatusTransition> GetTransitions(Revision[] revisions)
    //{
    //  return new TransitionsEnumerable(this, revisions);
    //}
    /// <summary>
    /// Returns transition enumerator for revisions.
    /// </summary>
    /// <remarks>
    /// Enumerates revisions and returns status transition
    /// object if transition was detected.
    /// First implementation is based on the fact that 
    /// all revisions belong to one bug.
    /// </remarks>
    public IEnumerable<RevisionStatusTransition> GetTransitions(Revision[] revisions)
    {
      Revision prevRevision = null;

      // Enumerate revisions
      foreach( Revision curRevision in revisions )
      {
        RevisionStatusTransition transition; // = StatusTransition.None;

        // Determine revisions status groups
        RevisionStatusGroup prevGroup = GetStatusGroup(prevRevision);
        RevisionStatusGroup curGroup = GetStatusGroup(curRevision);

        // First try to find in allowed transitions
        // one which is from prev group to the cur one
        var queryA = from t in m_reportConfig.AllowedTransitions
                     from p in t.Passage ?? new Configuration.Passage[0]
                     where p.From == prevGroup.Name &&
                           p.To == curGroup.Name
                     select new RevisionStatusTransition
                            {
                              Name = t.Name,
                              PreviousRevision = prevRevision,
                              CurrentRevision = curRevision,
                              PreviousGroup = prevGroup,
                              CurrentGroup = curGroup,
                              IsForbidden = false
                           };

        // Get first returned if any
        transition = queryA.Any() ? queryA.First() : null;

        // Couldn't find in allowed
        if( transition == null )
        {
          var queryF = from t in m_reportConfig.ForbiddenTransitions
                       from p in t.Passage
                       where p.From == prevGroup.Name &&
                             p.To == curGroup.Name
                       select new RevisionStatusTransition
                              {
                                Name = t.Name,
                                PreviousRevision = prevRevision,
                                CurrentRevision = curRevision,
                                PreviousGroup = prevGroup,
                                CurrentGroup = curGroup,
                                IsForbidden = true
                             };

          // Get first returned if any
          transition = queryF.Any() ? queryF.First() : null;
        }

        // Return found transition
        if( transition != null )
        {
          yield return transition;
        }

        // Remember previous revision
        prevRevision = curRevision;
      }
    }
    #endregion Public Methods

    #region Helper Methods
    /// <summary>
    /// Returns status group for the revision.
    /// </summary>
    /// <remarks>
    /// Status group is defined via report configuration file.
    /// </remarks>
    private RevisionStatusGroup GetStatusGroup(Revision revision)
    {
      RevisionStatusGroup statusGroup;

      if( revision != null )
      {
        // treat empty status as if but is just open
        string statusStr = (revision.Status ?? BugStatus.Open).ToString();

        var query = from g in m_reportConfig.StatusGroups
                    where Array.IndexOf(g.Status ?? new string[0], statusStr) != -1
                    select new RevisionStatusGroup
                           {
                             Name = g.Name,
                             Statuses = (from s in g.Status 
                                         select (BugStatus)Enum.Parse(typeof(BugStatus), s)).
                                         ToArray()
                           };

        // Get first in query
        Debug.Assert(query.Any());
        statusGroup = query.First();
      }
      else
      {
        // If revision didn't exist - then group is DontExist
        // Find specific group without statuses
        var query = from g in m_reportConfig.StatusGroups
                    where g.Status == null || g.Status.Length == 0
                    select new RevisionStatusGroup
                           {
                             Name = g.Name,
                             Statuses = new BugStatus[0]
                           };

        // Get first in query
        statusGroup = query.First();
      }
      return statusGroup;
    }
    #endregion Helper Methods

    #region Inner Types
    /// <summary>
    /// Just didn't know that yield can be both IEnumerable and IEnumerator.
    /// </summary>
    internal class TransitionsEnumerable : IEnumerable<RevisionStatusTransition>
    {
      #region Private Fileds
      private RevisionTransitionFilter m_filter;
      private Revision[] m_revisions;
      #endregion Private Fileds

      #region Constructors
      public TransitionsEnumerable(RevisionTransitionFilter filter, Revision[] revisions)
      {
        m_filter = filter;
        m_revisions = revisions;
      }
      #endregion Constructors

      #region Implementation of IEnumerable
      /// <summary>
      /// Returns an enumerator that iterates through the collection.
      /// </summary>
      public IEnumerator<RevisionStatusTransition> GetEnumerator()
      {
        return m_filter.GetTransitions(m_revisions).GetEnumerator();
      }

      /// <summary>
      /// Returns an enumerator that iterates through a collection.
      /// </summary>
      IEnumerator IEnumerable.GetEnumerator()
      {
        return GetEnumerator();
      }
      #endregion
    }
    #endregion Inner Types

    }
}



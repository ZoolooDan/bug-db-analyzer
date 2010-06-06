using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using BugDB.Common;
using BugDB.DataAccessLayer;
using BugDB.DataAccessLayer.DataTransferObjects;
using System.Linq;
using BugDB.Modules.BugReport.Misc;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;

namespace BugDB.Modules.BugReport.Views
{
  public class AppModel : SelectableModel<Application>
  {
    public string Title
    {
      get { return Data.Title; }
    }
  }

  public class ModuleModel : SelectableModel<Module>
  {
    public string Title
    {
      get { return Data.Title; }
    }
  }

  public class SubModuleModel : SelectableModel<SubModule>
  {
    public string Title
    {
      get { return Data.Title; }
    }
  }

  public class StatusModel : SelectableModel<BugStatus>
  {
    public string Title
    {
      get { return Data.ToString(); }
    }
  }

  public class SeverityModel : SelectableModel<BugSeverity>
  {
    public string Title
    {
      get { return Data.ToString(); }
    }
  }

  public class PriorityModel : SelectableModel<int>
  {
    public string Title
    {
      get { return Data.ToString(); }
    }
  }

  public class PersonModel : SelectableModel<Person>
  {
    public string Title
    {
      get { return Data.Login; }
    }
  }

  public class TypeModel : SelectableModel<BugType>
  {
    public string Title
    {
      get { return Data.ToString(); }
    }
  }

  public class ReleaseModel : SelectableModel<Release>
  {
    public string Title
    {
      get { return Data.Title; }
    }
  }

  public class QueryDefinitionPresentationModel : SimpleModel
  {
    private IDataProvider m_provider;
    private IEventAggregator m_eventAggregator;

    private IList<AppModel> m_apps = new List<AppModel>();
    private IList<ModuleModel> m_modules = new List<ModuleModel>();
    private IList<SubModuleModel> m_subModules = new List<SubModuleModel>();
    private IList<PersonModel> m_contributors;
    private IList<PersonModel> m_teamLeaders;
    private IList<PersonModel> m_developsers;
    private IList<PersonModel> m_testers;
    private IList<SeverityModel> m_severities;
    private IList<StatusModel> m_statuses;
    private IList<PriorityModel> m_priorites;
    private IList<TypeModel> m_types;
    private IList<ReleaseModel> m_foundReleases = new List<ReleaseModel>();
    private IList<ReleaseModel> m_targetReleases = new List<ReleaseModel>();

    private ICommand m_runReportCommand;

    public QueryDefinitionPresentationModel(IDataProvider provider,
                                            IEventAggregator eventAggregator, QueryDefinitionView view)
    {
      m_provider = provider;
      m_eventAggregator = eventAggregator;

      this.View = view;
      this.View.Model = this;

      InitApps();
    }

    private void InitApps()
    {
      var query = from a in m_provider.GetApplications()
                  select new AppModel { Data = a };

      var list = new List<AppModel>(query);
      // Subscribe changes
      list.ForEach(app => app.PropertyChanged += OnAppChanged);

      this.Applications = list;
    }

    private void OnAppChanged(object sender, PropertyChangedEventArgs args)
    {
      InitModules();
      InitTargetReleases();
      InitFoundReleases();
    }

    private void InitFoundReleases()
    {
      this.FoundReleases = GetReleases();
    }

    private void InitTargetReleases()
    {
      this.TargetReleases = GetReleases();
    }


    private void InitModules()
    {
      IList<ModuleModel> modules;

      // Return non empty modules list only if only one app is selected
      var queryApps = from a in this.Applications
                      where a.IsSelected ?? false
                      select a;
      if( queryApps.Count() == 1 )
      {
        var queryMods = from m in m_provider.GetModules(queryApps.Single().Data.Id)
                        select new ModuleModel { Data = m, IsSelected = false };
        var list = new List<ModuleModel>(queryMods);
        modules = list;
        // Subscribe changes
        list.ForEach(module => module.PropertyChanged +=
                            (o, a) =>
                            {
                              InitSubModules();
                            });
      }
      else
      {
        modules = new List<ModuleModel>();
      }

      this.Modules = modules;
    }

    private void InitSubModules()
    {
      IList<SubModuleModel> subModules;

      // Return non empty sub modules list only if only one module is selected
      var queryModules = from a in this.Modules
                         where a.IsSelected ?? false
                         select a;
      if( queryModules.Count() == 1 )
      {
        Module module = queryModules.Single().Data;
        var querySubModules = from sm in m_provider.GetSubModules(module.Id)
                              select new SubModuleModel { Data = sm, IsSelected = false };
        subModules = querySubModules.ToList();
      }
      else
      {
        subModules = new List<SubModuleModel>();
      }

      this.SubModules = subModules;
    }

    public QueryDefinitionView View { get; set; }


    public IList<AppModel> Applications
    {
      get
      {
        return m_apps;
      }
      set
      {
        if (m_apps != value)
        {
          m_apps = value;
          FirePropertyChanged("Applications");
        }
      }
    }

    public IList<ModuleModel> Modules
    {
      get
      {
        return m_modules;
      }
      set
      {
        if (m_modules != value)
        {
          m_modules = value;
          FirePropertyChanged("Modules");
        }
      }
    }

    public IList<SubModuleModel> SubModules
    {
      get
      {
        return m_subModules;
      }
      set
      {
        if (m_subModules != value)
        {
          m_subModules = value;
          FirePropertyChanged("SubModules");
        }
      }
    }

    public IList<ReleaseModel> FoundReleases
    {
      get
      {
        return m_foundReleases;
      }
      set
      {
        if (m_foundReleases != value)
        {
          m_foundReleases = value;
          FirePropertyChanged("FoundReleases");
        }
      }
    }

    private IList<ReleaseModel> GetReleases()
    {
      IList<ReleaseModel> releases;

      // Return non empty modules list only if only one app is selected
      var queryApps = from a in this.Applications
                      where a.IsSelected ?? false
                      select a;
      if (queryApps.Count() == 1)
      {
        Application app = queryApps.Single().Data;
        var queryReleases = from m in m_provider.GetReleases(app.Id)
                            select new ReleaseModel {Data = m, IsSelected = false};
        releases = queryReleases.ToList();
      }
      else
      {
        releases = new List<ReleaseModel>();
      }
      return releases;
    }

    public IList<ReleaseModel> TargetReleases
    {
      get { return m_targetReleases; }
      set
      {
        if( m_targetReleases != value )
        {
          m_targetReleases = value;
          FirePropertyChanged("TargetReleases");
        }
      }
    }

    public IList<StatusModel> Statuses
    {
      get
      {
        if (m_statuses == null)
        {
          var query = from s in m_provider.GetStatuses()
                      select new StatusModel {Data = s, IsSelected = false};
          m_statuses = query.ToList();
        }
        return m_statuses;
      }
    }

    public IList<SeverityModel> Severities
    {
      get
      {
        if (m_severities == null)
        {
          var query = from s in m_provider.GetSeverities()
                      select new SeverityModel {Data = s, IsSelected = false};
          m_severities = query.ToList();
        }
        return m_severities;
      }
    }

    public IList<PriorityModel> Priorities
    {
      get
      {
        if (m_priorites == null)
        {
          var query = from p in m_provider.GetPriorities()
                      select new PriorityModel {Data = p, IsSelected = false};
          m_priorites = query.ToList();
        }
        return m_priorites;
      }
    }

    public IList<PersonModel> Contributors
    {
      get
      {
        if (m_contributors == null)
        {
          m_contributors = CreateStaffModels();
        }
        return m_contributors;
      }
    }

    public IList<PersonModel> TeamLeaders
    {
      get
      {
        if (m_teamLeaders == null)
        {
          m_teamLeaders = CreateStaffModels();
        }
        return m_teamLeaders;
      }
    }

    public IList<PersonModel> Developers
    {
      get
      {
        if (m_developsers == null)
        {
          m_developsers = CreateStaffModels();
        }
        return m_developsers;
      }
    }

    public IList<PersonModel> Testers
    {
      get
      {
        if (m_testers == null)
        {
          m_testers = CreateStaffModels();
        }
        return m_testers;
      }
    }

    public IList<TypeModel> Types
    {
      get
      {
        if (m_types == null)
        {
          var query = from t in m_provider.GetTypes()
                      select new TypeModel {Data = t, IsSelected = false};
          m_types = query.ToList();
        }
        return m_types;
      }
    }

    private IList<PersonModel> CreateStaffModels()
    {
      IList<PersonModel> staff;

      var query = from p in m_provider.GetStaff()
                  select new PersonModel {Data = p, IsSelected = false};
      staff = query.ToList();

      return staff;
    }

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string Summary { get; set; }

    public void NotifyAppsChanged()
    {
      FirePropertyChanged("Modules");
      FirePropertyChanged("TargetReleases");
      FirePropertyChanged("FoundReleases");
    }


    public ICommand RunReportCommand
    {
      get
      {
        if (m_runReportCommand == null)
        {
          m_runReportCommand = new DelegateCommand<object>(OnRunReportCommand);
        }
        return m_runReportCommand;
      }
    }

    private void OnRunReportCommand(object obj)
    {
      var evt = m_eventAggregator.GetEvent<QueryParamsChangedEvent>();

      var queryApps = from a in Applications
                      where a.IsSelected ?? false
                      select a.Data.Id;

      var queryModules = from m in Modules
                         where m.IsSelected ?? false
                         select m.Data.Id;

      var querySubModules = from sm in SubModules
                            where sm.IsSelected ?? false
                            select sm.Data.Id;

      var queryStatuses = from s in Statuses
                          where s.IsSelected ?? false
                          select s.Data;

      var querySevers = from s in Severities
                        where s.IsSelected ?? false
                        select s.Data;

      var queryPriors = from p in Priorities
                        where p.IsSelected ?? false
                        select p.Data;

      var queryTypes = from t in Types
                       where t.IsSelected ?? false
                       select t.Data;

      var queryContrs = from c in Contributors
                        where c.IsSelected ?? false
                        select c.Data.Id;

      var queryLeads = from l in TeamLeaders
                       where l.IsSelected ?? false
                       select l.Data.Id;

      var queryDevs = from d in Developers
                      where d.IsSelected ?? false
                      select d.Data.Id;

      var queryTesters = from t in Testers
                         where t.IsSelected ?? false
                         select t.Data.Id;

      var queryFRels = from r in FoundReleases
                       where r.IsSelected ?? false
                       select r.Data.Id;

      var queryTRels = from r in FoundReleases
                       where r.IsSelected ?? false
                       select r.Data.Id;
      ;

      // Create query parameters object
      QueryParams queryParams = new QueryParams
                                  {
                                    Apps = queryApps.ToArray(),
                                    Modules = queryModules.ToArray(),
                                    SubModules = querySubModules.ToArray(),
                                    FoundReleases = queryFRels.ToArray(),
                                    TargetReleases = queryTRels.ToArray(),
                                    BugTypes = queryTypes.ToArray(),
                                    BugStatuses = queryStatuses.ToArray(),
                                    Severities = querySevers.ToArray(),
                                    Priorities = queryPriors.ToArray(),
                                    Contributors = queryContrs.ToArray(),
                                    TeamLeaders = queryLeads.ToArray(),
                                    Developers = queryDevs.ToArray(),
                                    Testers = queryTesters.ToArray(),
                                    DateFrom = FromDate,
                                    DateTo = ToDate,
                                    Summary = Summary
                                  };

      // Publish event
      evt.Publish(queryParams);
    }
  }
}
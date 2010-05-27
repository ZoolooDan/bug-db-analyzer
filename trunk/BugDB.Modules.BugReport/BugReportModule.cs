using System.IO;
using System.Reflection;
using BugDB.Aggregator;
using BugDB.Common;
using BugDB.DataAccessLayer;
using BugDB.DataAccessLayer.BLToolkitProvider;
using BugDB.Modules.BugReport.Views;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;

namespace BugDB.Modules.BugReport
{
  public class BugReportModule : IModule
  {
    private readonly IUnityContainer m_container;
    private readonly IRegionManager m_regionManager;

    public BugReportModule(IUnityContainer container, IRegionManager regionManager)
    {
      m_container = container;
      m_regionManager = regionManager;
    }

    #region IModule Members

    public void Initialize()
    {
      RegisterViewsAndServices();

      //m_regionManager.RegisterViewWithRegion(RegionNames.MainRegion, 
      //  () => m_container.Resolve<IBugReportPresentationModel>().View);
      m_regionManager.RegisterViewWithRegion(RegionNames.Sidebar,
        () => m_container.Resolve<QueryDefinitionPresentationModel>().View);
    }

    protected void RegisterViewsAndServices()
    {
      m_container.RegisterType<IBugReportView, BugReportView>();
      m_container.RegisterType<IBugReportPresentationModel, BugReportPresentationModel>();

      m_container.RegisterInstance(GetDataProvider());
    }

    private static IDataProvider GetDataProvider()
    {
      IDataProvider provider = new BLToolkitDataProvider(null);
      provider.CleanStorage();

      string dataPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                                     @"Data\data.txt");

      StorageAggregator aggregator = new StorageAggregator(provider);
      aggregator.FillStorage(dataPath);

      return provider;
    }

    #endregion
  }
}

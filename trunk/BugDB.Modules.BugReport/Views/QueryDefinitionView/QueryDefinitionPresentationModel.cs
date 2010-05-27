using System.Collections.Generic;
using BugDB.DataAccessLayer;
using BugDB.DataAccessLayer.DataTransferObjects;

namespace BugDB.Modules.BugReport.Views
{
  public class QueryDefinitionPresentationModel
  {
    private IDataProvider m_provider;

    public QueryDefinitionPresentationModel(IDataProvider provider,
      QueryDefinitionView view)
    {
      m_provider = provider;
      this.View = view;
      this.View.Model = this;
    }

    public QueryDefinitionView View { get; set; }


    public IList<Application> Applications
    {
      get
      {
        return m_provider.GetApplications();
      }
    }
  }
}
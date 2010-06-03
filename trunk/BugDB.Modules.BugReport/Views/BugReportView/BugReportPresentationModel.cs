using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using BugDB.Common;
using BugDB.DataAccessLayer;
using BugDB.DataAccessLayer.DataTransferObjects;
using Microsoft.Practices.Composite.Events;

namespace BugDB.Modules.BugReport
{
  class BugReportPresentationModel : IBugReportPresentationModel,
    INotifyPropertyChanged
  {
    private IList<BugModel> m_items;
    private IDataProvider m_provider;

    public BugReportPresentationModel(IEventAggregator eventAggregator,
      IDataProvider provider, IBugReportView view)
    {
      this.View = view;
      this.View.Model = this;

      var evt = eventAggregator.GetEvent<QueryParamsChangedEvent>();
      evt.Subscribe(OnQueryParamsChanged);

      m_provider = provider;
    }

    private void OnQueryParamsChanged(QueryParams queryParams)
    {
      Revision[] revisions = m_provider.GetRevisions(queryParams);

      // Get most recent revision
      var query = from r in revisions
                  group r by r.BugNumber into g
                  select new BugModel
                           {
                             BugNumber = g.Last().BugNumber, 
                             Summary = g.Last().Summary
                           };
      


      this.Items = query.ToList();
    }

    public IBugReportView View { get; set; }

    public IList<BugModel> Items
    {
      get
      {
        return m_items;
      }
      set
      {
        m_items = value;
        if( PropertyChanged != null )
        {
          PropertyChanged(this, new PropertyChangedEventArgs("Items"));
        }
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }

}

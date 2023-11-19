using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Generic;
using Ext.Net;
using Ext.Net.Utilities;
using CoreNET.Common.Base;
using CoreNET.Common.BO;

namespace Usadi.Valid49.BO
{
  #region Usadi.Valid49.BO.DaskrControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class DaskrControl : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript
  {
    #region Properties 
    public long Id { get; set; }
    public decimal Nilai { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Kdper { get; set; }
    public string Nmper { get; set; }
    public string Kdtahap { get; set; }
    public string Idprgrm { get; set; }
    public string Thang { get; set; }
    public string Mtgkey { get; set; }
    public string Kdkegunit { get; set; }
    public string Noba { get; set; }
    public string Nobarenov { get; set; }
    public string Idxdask { get; set; }
    public int Idxkode { get; set; }
    public string Unitkey { get; set; }
    public string Mtgunit
    {
      get
      {
        return Mtgkey + Unitkey;
      }
    }
    
    #endregion Properties 

    #region Methods 
    public DaskrControl()
    {
      XMLName = ConstantTablesAsetDM.XMLDASKR;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Mtgkey","Kdkegunit","Idxdask","Idxkode","Unitkey" };
      cViewListProperties.IDKey = "Mtgunit";//IDKey for ID Notes
      cViewListProperties.IDProperty = "Mtgunit";//UniqueKey in gridview
      cViewListProperties.ReadOnlyFields = new String[] {  };//Key in GetFilters should put here
      cViewListProperties.SortFields = new String[] { "Kdper" };//
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      cViewListProperties.PageSize = 30;

      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdper=Kode Rekening"), typeof(string), 25, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmper=Uraian"), typeof(string), 75, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 30, HorizontalAlign.Right).SetEditable(false));

      return columns;
    }
    public new void SetPageKey()
    {
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(SkdaskControl).IsInstanceOfType(bo))
      {
        Unitkey = ((SkdaskControl)bo).Unitkey;
        Idxdask = ((SkdaskControl)bo).Idxdask;
        Idxkode = ((SkdaskControl)bo).Idxkode;
        Kdkegunit = ((SkdaskControl)bo).Kdkegunit;
        Kdtahap = ((SkdaskControl)bo).Kdtahap;
      }
    }
    public new void SetPrimaryKey()
    {
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();

      return hpars;
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<DaskrControl> ListData = new List<DaskrControl>();
      foreach (DaskrControl dc in list)
      {
        ListData.Add(dc);
      }
      //Update(ListData);
      return ListData;
    }
    //Unuk ParameterLookup2, pastikan parameter entry is true
    public new int Delete()
    {
      Status = -1;
      int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
      return n;
    }
    public override HashTableofParameterRow GetEntries()
    {
      //bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      return hpars;
    }

    #endregion Methods 
  }
  #endregion Daskr
}


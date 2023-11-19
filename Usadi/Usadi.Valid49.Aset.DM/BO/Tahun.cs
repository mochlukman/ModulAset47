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
  #region Usadi.Valid49.BO.TahunControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class TahunControl : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Nmtahun { get; set; }
    public string Kdtahun { get; set; }
    #endregion Properties 

    #region Methods 
    public TahunControl()
    {
      XMLName = ConstantTablesAsetDM.XMLTAHUN;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Kdtahun" };
      cViewListProperties.IDKey = "Kdtahun";
      cViewListProperties.IDProperty = "Kdtahun";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD;
      cViewListProperties.PageSize = 30;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdtahun=Kode"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmtahun=Tahun"), typeof(string), 50, HorizontalAlign.Left));

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
      List<TahunControl> ListData = new List<TahunControl>();
      foreach (TahunControl dc in list)
      {
        ListData.Add(dc);
      }

      return ListData;
    }
    public new int Delete()
    {
      Status = -1;
      int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
      return n;
    }
    public override HashTableofParameterRow GetEntries()
    {
      bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdtahun=Kode"), true, 30).SetEnable(enable));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nmtahun=Tahun"), true, 30).SetEnable(enable));

      return hpars;
    }

    #endregion Methods 
  }
  #endregion Tahun
}


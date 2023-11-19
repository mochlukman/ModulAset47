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
  #region Usadi.Valid49.BO.StruruangControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class StruruangControl : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public string Nmlevel { get; set; }
    public string Level { get; set; }
    #endregion Properties 

    #region Methods 
    public StruruangControl()
    {
      XMLName = ConstantTablesAsetDM.XMLSTRURUANG;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Level" };
      cViewListProperties.IDKey = "Level";
      cViewListProperties.IDProperty = "Level";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
      cViewListProperties.AllowMultiDelete = true;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Level=Kode"), typeof(string), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmlevel=Uraian"), typeof(string), 50, HorizontalAlign.Left));

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
      Level = Guid.NewGuid().ToString();
      UtilityUI.GetNoUrut(this, "Level", 1, "Level", string.Empty, string.Empty);
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
      List<StruruangControl> ListData = new List<StruruangControl>();
      foreach (StruruangControl dc in list)
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
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Level=Kode"), false, 10).SetEnable(enable));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nmlevel=Uraian"), true, 50).SetEnable(enable));

      return hpars;
    }

    #endregion Methods 
  }
  #endregion Struruang
}


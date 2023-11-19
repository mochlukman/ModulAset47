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
  #region Usadi.Valid49.BO.WebgroupControl, Usadi.Valid49.Aset.Sys
  [Serializable]
  public class WebgroupControl : BaseDataControlAsetSys, IDataControlUIEntry, IHasJSScript
  {
    #region Properties 
    public string Groupid { get; set; }
    public new string Ket { get; set; }
    public int Jmlotor { get; set; }
    public int Nmgroup { get; set; }

    #endregion Properties 

    #region Methods 
    public WebgroupControl()
    {
      XMLName = ConstantTablesAsetSys.XMLWEBRGROUP;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Groupid","Nmgroup" };
      cViewListProperties.IDKey = "Groupid";//IDKey for ID Notes
      cViewListProperties.IDProperty = "Groupid";//UniqueKey in gridview
      cViewListProperties.ReadOnlyFields = new String[] {};//Key in GetFilters should put here
      cViewListProperties.SortFields = new String[] { "Groupid" };//
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.AllowMultiDelete = true;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Groupid=Group Id"), typeof(string), 12, HorizontalAlign.Left).SetEditable(true));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmgroup=Nama Group"), typeof(string), 50, HorizontalAlign.Left).SetEditable(true));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 50, HorizontalAlign.Left).SetEditable(true));
      return columns;
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
      Groupid = Guid.NewGuid().ToString();
      UtilityUI.GetNoUrut(this, "Groupid", 2, "Groupid", string.Empty, string.Empty);
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
      List<WebgroupControl> ListData = new List<WebgroupControl>();
      foreach(WebgroupControl dc in list)
      {
        ListData.Add(dc);
      }
      return ListData;
    }

    public new int Delete()
    {
      WebgroupControl cWebgroupotor = new WebgroupControl();
      cWebgroupotor.Groupid = Groupid;
      cWebgroupotor.Load("Cekotor");

      if (cWebgroupotor.Jmlotor != 0)
      {
        throw new Exception("Gagal menghapus data : Group user masih digunakan di hak akses user");
      }

      Status = -1;
      int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
      return n;
    }

    public new void Update()
    {
      base.Update();
    }

    public override HashTableofParameterRow GetEntries()
    {
      bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Groupid=Group Id"), true, 10).SetEnable(false));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nmgroup=Group User"), true, 50).SetEnable(enable));
      hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 50).SetEnable(enable));
      return hpars;
    }
    #endregion Methods 
  }
  #endregion Webgroup
}


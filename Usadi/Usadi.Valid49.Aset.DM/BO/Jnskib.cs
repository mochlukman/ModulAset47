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
  #region Usadi.Valid49.BO.JnskibControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class JnskibControl : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript
  {
    #region Properties 
    public string Nmkib { get; set; }
    public string Golkib { get; set; }
    public string Kdkib { get; set; }
    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewJnskibaset",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Mapping Golongan Aset";
        return new ImageCommand[] { cmd1 };
      }
    }
    public string ViewJnskibaset
    {
      get
      {
        string app = GlobalAsp.GetRequestApp();
        string id = GlobalAsp.GetRequestId();
        string idprev = GlobalAsp.GetRequestId();
        string kode = GlobalAsp.GetRequestKode();
        string idx = GlobalAsp.GetRequestIndex();
        string strenable = "&enable=" + ((Status == 0) ? 1 : 0);
        string url = string.Format("PageTabular.aspx?passdc=1&app={0}&i={1}&id={2}&idprev={3}&kode={4}&idx={5}" + strenable, app, 11, id, idprev, kode, idx);
        return "Mapping Golongan Aset; "+ Kdkib + " - " + Nmkib + ":" + url;
      }
    }
    #endregion Properties 

    #region Methods 
    public JnskibControl()
    {
      XMLName = ConstantTablesAsetDM.XMLJNSKIB;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Kdkib" };
      cViewListProperties.IDKey = "Kdkib";
      cViewListProperties.IDProperty = "Kdkib";
      cViewListProperties.ReadOnlyFields = new String[] {};
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdkib=Kode KIB"), typeof(string), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkib=Uraian"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Golkib=Golongan"), typeof(string), 15, HorizontalAlign.Center));

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
      Kdkib = Guid.NewGuid().ToString();
      UtilityUI.GetNoUrut(this, "Kdkib", 2, "Kdkib", string.Empty, string.Empty);
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
      List<JnskibControl> ListData = new List<JnskibControl>();
      foreach(JnskibControl dc in list)
      {
        ListData.Add(dc);
      }
      //Update(ListData);
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
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdkib=Kode KIB"), false, 10).SetEnable(enable));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nmkib=Uraian"), true, 70).SetEnable(enable));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Golkib=Golongan"), true, 10).SetEnable(enable));

      return hpars;
    }
    #endregion Methods 
  }
  #endregion Jnskib
}


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
  #region Usadi.Valid49.BO.StdrenovControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class StdrenovControl : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public decimal Prosen2 { get; set; }
    public decimal Umekotbh { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public decimal Umeko { get; set; }
    public decimal Prosen { get; set; }
    public decimal Prosen1 { get; set; }
    public string Asetkey { get; set; }
    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewStdrenovdet",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Penambahan Masa Manfaat";
        return new ImageCommand[] { cmd1 };
      }
    }
    public string ViewStdrenovdet
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
        return "Penambahan Masa Manfaat; " + Kdaset + " - " + Nmaset +":" + url;
      }
    }
    #endregion Properties 

    #region Methods 
    public StdrenovControl()
    {
      XMLName = ConstantTablesAsetDM.XMLSTDRENOV;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Asetkey" };
      cViewListProperties.IDKey = "Asetkey";
      cViewListProperties.IDProperty = "Asetkey";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      cViewListProperties.PageSize = 30;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Uraian"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Umeko=Umur Ekonomis Standar"), typeof(decimal), 30, HorizontalAlign.Center));

      return columns;
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();

      return hpars;
    }
    public new IList View()
    {
      IList list = this.View("Aset");
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<StdrenovControl> ListData = new List<StdrenovControl>();
      foreach (StdrenovControl dc in list)
      {
        ListData.Add(dc);
      }
      return ListData;
    }

    #endregion Methods 
  }
  #endregion Stdrenov


  #region Stdrenovdet
  [Serializable]
  public class StdrenovdetControl : StdrenovControl, IDataControlUIEntry
  {
    public StdrenovdetControl()
    {
      XMLName = ConstantTablesAsetDM.XMLSTDRENOV;
    }
    public new IList View()
    {
      IList list = this.View("All");
      return list;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Prosen1", "Asetkey" };
      cViewListProperties.IDKey = "Asetkey";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { "Asetkey" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;
      cViewListProperties.PageSize = 15;
      cViewListProperties.AllowMultiDelete = true;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Prosen1=Prosentase Awal (%)"), typeof(decimal), 30, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Prosen2=Prosentase Akhir (%)"), typeof(decimal), 30, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Umekotbh=Penambahan Masa Manfaat"), typeof(decimal), 30, HorizontalAlign.Center));

      return columns;
    }
    public new void SetPrimaryKey()
    {
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(StdrenovControl).IsInstanceOfType(bo))
      {
        Asetkey = ((StdrenovControl)bo).Asetkey;
      }
    }
    public override HashTableofParameterRow GetEntries()
    {
      bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Prosen1=Prosentase Awal (%)"), true, 30).SetEnable(enable));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Prosen2=Prosentase Akhir (%)"), true, 30).SetEnable(enable));
      hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Umekotbh=Penambahan Masa Manfaat"), true, 30).SetEnable(enable));

      return hpars;
    }
    public new void Insert()
    {
      try
      {
        ((BaseDataControlUI)this).Insert(BaseDataControl.DEFAULT);
      }
      catch (Exception ex)
      {
        if (ex.Message.Contains("Violation of PRIMARY KEY"))
        {
          string msg = "Gagal menyimpan data : Prosentase awal {0} sudah ada";
          msg = string.Format(msg, Prosen1);
          throw new Exception(msg);
        }
      }
    }
    public new int Delete()
    {
      Status = -1;
      int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
      return n;
    }
  }
  #endregion Stdrenovdet
}


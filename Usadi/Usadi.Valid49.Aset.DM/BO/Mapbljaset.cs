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
  #region Usadi.Valid49.BO.MapbljasetControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class MapbljasetControl : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Kdnmaset { get; set; }
    public string Kdper { get; set; }
    public string Nmper { get; set; }
    public string Nmdana { get; set; }
    public int Jmlrek { get; set; }
    public int Jmlaset { get; set; }
    public string Kdkib { get; set; }
    public string Nmkib { get; set; }
    public string Thang { get; set; }
    public string Kddana { get; set; }
    public string Mtgkey { get; set; }
    public string Asetkey { get; set; }
    
    #endregion Properties 

    #region Methods 
    public MapbljasetControl()
    {
      XMLName = ConstantTablesAsetDM.XMLMAPBLJASET;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Thang", "Kddana", "Mtgkey", "Asetkey" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.SortFields = new String[] { "Kdaset" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
      cViewListProperties.LookupDC = " Usadi.Valid49.BO.DaftasetKibfilterLookupControl, Usadi.Valid49.Aset.DM";
      cViewListProperties.LookupLabelQuery = "Mapbljaset";
      cViewListProperties.PageSize = 30;
      cViewListProperties.AllowMultiDelete = true;

      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 100, HorizontalAlign.Left));

      return columns;
    }
    public new IList View()
    {
      IList list = this.View("All");
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<MapbljasetControl> ListData = new List<MapbljasetControl>();
      foreach (MapbljasetControl dc in list)
      {
        ListData.Add(dc);
      }

      return ListData;
    }
    public override HashTableofParameterRow GetEntries()
    {
      //bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();

      return hpars;
    }
    #endregion Methods 
  }
  #endregion Mapbljaset

  #region Mapbljasetrek
  [Serializable]
  public class MapbljasetrekControl : MapbljasetControl, IDataControlUIEntry
  {
    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewMapbljasetkey",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Mapping Golongan Aset";
        return new ImageCommand[] { cmd1 };
      }
    }
    public string ViewMapbljasetkey
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
        return "Mapping Kode Barang Ke Rekening; " + Nmper + ":" + url;
      }
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Mtgkey" };
      cViewListProperties.IDKey = "Mtgkey";
      cViewListProperties.IDProperty = "Mtgkey";
      cViewListProperties.ReadOnlyFields = new String[] { "Kdana" };
      cViewListProperties.SortFields = new String[] { "Kdper" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
      cViewListProperties.LookupDC = " Usadi.Valid49.BO.MatangrLookupControl, Usadi.Valid49.Aset.DM";
      cViewListProperties.LookupLabelQuery = "Lookup";
      cViewListProperties.PageSize = 30;
      cViewListProperties.AllowMultiDelete = true;

      return cViewListProperties;
    }
    public new IList View()
    {
      IList list = this.View("Rek");
      return list;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(MatangrControl).IsInstanceOfType(bo))
      {
        Mtgkey = ((MatangrControl)bo).Mtgkey;
      }
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kddana=Sumber Dana"),
      GetList(new JdanaLookupControl()), "Kddana=Nmdana", 50).SetAllowRefresh(true).SetEnable(enableFilter));
      return hpars;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdper=Kode Rekening"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmper=Nama Rekening"), typeof(string), 100, HorizontalAlign.Left));
      return columns;
    }
    public new void Insert()
    {
      base.Update("Kddana");
    }
    public new int Delete()
    {
      int n = 0;

      MapbljasetControl cMapbljasetCekjmlrek = new MapbljasetControl();
      cMapbljasetCekjmlrek.Mtgkey = Mtgkey;
      cMapbljasetCekjmlrek.Kddana = Kddana;
      cMapbljasetCekjmlrek.Load("Jmlrek");

      if(cMapbljasetCekjmlrek.Jmlrek != 0 )
      {
        throw new Exception("Gagal menghapus data : kode barang yang di mapping ke rekening ini sudah digunakan di transaksi berita");
      }
      else
      {
        Status = -1;
        base.Delete("Kddana");
        base.Update("Hapusdana");
      }
      return n;
    }
  }
  #endregion Mapbljasetrek

  #region Mapbljasetkey
  [Serializable]
  public class MapbljasetkeyControl : MapbljasetControl, IDataControlUIEntry
  {
    public new void SetPrimaryKey()
    {
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      Thang = cPemda.Configval.Trim();
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(MapbljasetControl).IsInstanceOfType(bo))
      {
        Mtgkey = ((MapbljasetControl)bo).Mtgkey;
        Kddana = ((MapbljasetControl)bo).Kddana;
      }
      else if (typeof(DaftasetControl).IsInstanceOfType(bo))
      {
        Asetkey = ((DaftasetControl)bo).Asetkey;
      }
    }
    public new void Insert()
    {
      base.Insert();
    }
    public new int Delete()
    {
      MapbljasetControl cMapbljasetCekjmlaset = new MapbljasetControl();
      cMapbljasetCekjmlaset.Asetkey = Asetkey;
      cMapbljasetCekjmlaset.Mtgkey = Mtgkey;
      cMapbljasetCekjmlaset.Kddana = Kddana;
      cMapbljasetCekjmlaset.Load("Jmlaset");

      if (cMapbljasetCekjmlaset.Jmlaset != 0)
      {
        throw new Exception("Gagal menghapus data : kode barang ini sudah digunakan di transaksi berita");
      }
      else
      {
        Status = -1;
        int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
        return n;
      }
    }
  }
  #endregion Mapbljasetkey
}



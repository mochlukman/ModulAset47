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
  #region Usadi.Valid49.BO.MapnrcasetControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class MapnrcasetControl : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript
  {
    #region Properties
    public long Id { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Kdnmaset { get { return Kdaset + " " + Nmaset; } }
    public string Kdper { get; set; }
    public string Nmper { get; set; }
    public string Nmdana { get; set; }
    public string Thang { get; set; }
    public string Kddana { get; set; }
    public string Mtgkey { get; set; }
    public string Asetkey { get; set; }
    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewMapnrcasetkey",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Mapping Golongan Aset";
        return new ImageCommand[] { cmd1 };
      }
    }
    public string ViewMapnrcasetkey
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
        return "Mapping Kode Barang Ke Rekening; " +Nmper+ ":" + url;
      }
    }
    #endregion Properties 

    #region Methods 
    public MapnrcasetControl()
    {
      XMLName = ConstantTablesAsetDM.XMLMAPNRCASET;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Mtgkey" };
      cViewListProperties.IDKey = "Mtgkey";
      cViewListProperties.IDProperty = "Mtgkey";
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdper=Kode Rekening"), typeof(string), 30, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmper=Nama Rekening"), typeof(string), 100, HorizontalAlign.Left));

      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
    }
    public new IList View()
    {
      IList list = this.View("Rek");
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<MapnrcasetControl> ListData = new List<MapnrcasetControl>();
      foreach (MapnrcasetControl dc in list)
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
  #endregion Mapnrcaset
  #region Mapnrcasetkey
  [Serializable]
  public class MapnrcasetkeyControl : MapnrcasetControl, IDataControlUIEntry
  {
    public MapnrcasetkeyControl()
    {
      XMLName = ConstantTablesAsetDM.XMLMAPNRCASET;
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
      cViewListProperties.PrimaryKeys = new String[] { "Thang", "Kddana", "Mtgkey", "Asetkey" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
      cViewListProperties.LookupDC = " Usadi.Valid49.BO.DaftasetLookupControl, Usadi.Valid49.Aset.DM";
      cViewListProperties.LookupLabelQuery = "Mapnrcaset";
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
    public new void SetPrimaryKey()
    {
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      Thang = cPemda.Configval.Trim();
      Kddana = "301";
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
      else if (typeof(MapnrcasetControl).IsInstanceOfType(bo))
      {
        Mtgkey = ((MapnrcasetControl)bo).Mtgkey;
      }
      else if (typeof(DaftasetControl).IsInstanceOfType(bo))
      {
        Asetkey = ((DaftasetControl)bo).Asetkey;
      }
    }
    public new int Delete()
    {
      Status = -1;
      int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
      return n;
    }
  }
  #endregion Mapnrcasetkey
}



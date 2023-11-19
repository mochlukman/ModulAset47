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
  #region Usadi.Valid49.BO.JnskibasetControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class JnskibasetControl : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript
  {
    #region Properties 
    public string Asetkey { get; set; }
    public string Kdaset { get; set; }
    public string Nmaset { get; set; }
    public string Kdkib { get; set; }
    public string Nmkib { get; set; }
    public string Kddana { get; set; }
    public string K_brg { get; set; }
    #endregion Properties 

    #region Methods 
    public JnskibasetControl()
    {
      XMLName = ConstantTablesAsetDM.XMLJNSKIBASET;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "K_brg" };
      cViewListProperties.IDKey = "K_brg";
      cViewListProperties.IDProperty = "K_brg";
      cViewListProperties.ReadOnlyFields = new String[] { };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
      cViewListProperties.LookupDC = " Usadi.Valid49.BO.DaftasetLookupControl, Usadi.Valid49.Aset.DM";
      cViewListProperties.LookupLabelQuery = "Jnskibaset";
      cViewListProperties.AllowMultiDelete = true;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 15, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkib=Golongan"), typeof(string), 30, HorizontalAlign.Left));

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
      else if (typeof(JnskibControl).IsInstanceOfType(bo))
      {
        Kdkib = ((JnskibControl)bo).Kdkib;
      }
      else if (typeof(DaftasetControl).IsInstanceOfType(bo))
      {
        Asetkey = ((DaftasetControl)bo).Asetkey;
        K_brg = ((DaftasetControl)bo).Kdaset;
      }
    }
    public new void SetPrimaryKey()
    {
      Kdkib = Kdkib;
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
      List<JnskibasetControl> ListData = new List<JnskibasetControl>();
      foreach (JnskibasetControl dc in list)
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
      //bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();

      return hpars;
    }
    #endregion Methods 
  }
  #endregion Jnskibaset
}


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
  #region Usadi.Valid49.BO.TahapControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class TahapControl : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript
  {
    #region Properties 
    public long Id { get; set; }
    public string Uraian { get; set; }
    public string Kdtahap { get; set; }
    #endregion Properties 

    #region Methods 
    public TahapControl()
    {
      XMLName = ConstantTablesAsetDM.XMLTAHAP;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Kdtahap" };
      cViewListProperties.IDKey = "Kdtahap";
      cViewListProperties.IDProperty = "Kdtahap";
      cViewListProperties.ReadOnlyFields = new String[] {};
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();

      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdtahap=Kode Tahap"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraian"), typeof(string), 50, HorizontalAlign.Left));

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
      //Id = Guid.NewGuid();
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
      List<TahapControl> ListData = new List<TahapControl>();
      foreach(TahapControl dc in list)
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
    //public static string GetTahapan(HttpContext page)
    //{
    //  return (string)page.Session["KDTAHAP"];
    //}
    #endregion Methods 
  }
  #endregion Tahap
}


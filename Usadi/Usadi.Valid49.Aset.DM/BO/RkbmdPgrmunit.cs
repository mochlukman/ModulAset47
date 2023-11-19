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
  #region Usadi.Valid49.BO.RkbmdPgrmunitControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class RkbmdPgrmunitControl : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript
  {
    #region Properties 
    public long Id { get; set; }
    public string Target { get; set; }
    public string Sasaran { get; set; }
    public int Noprio { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Nuprgrm { get; set; }
    public string Nmprgrm { get; set; }
    public string Thang { get; set; }
    public string Unitkey { get; set; }
    public string Idprgrm { get; set; }

    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewRkbmdKegunit",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Daftar kegiatan";
        return new ImageCommand[] { cmd1 };
      }
    }
    public string ViewRkbmdKegunit
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
        return "Daftar Kegiatan :" + url;
      }
    }
    #endregion Properties 

    #region Methods 
    public RkbmdPgrmunitControl()
    {
      XMLName = ConstantTablesAsetDM.XMLRKBMD_PGRMUNIT;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Thang", "Idprgrm" };
      cViewListProperties.IDKey = "Id";//IDKey for ID Notes
      cViewListProperties.IDProperty = "Id";//UniqueKey in gridview
      cViewListProperties.ReadOnlyFields = new String[] { "Unitkey" };//Key in GetFilters should put here
      cViewListProperties.SortFields = new String[] { "Nuprgrm" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      cViewListProperties.PageSize = 30;

      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nuprgrm=No Program"), typeof(string), 30, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmprgrm=Uraian Program"), typeof(string), 100, HorizontalAlign.Left).SetEditable(false));

      return columns;
    }
    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Unitkey = (string)GlobalAsp.GetSessionUser().GetValue("Unitkey");
        Kdunit = (string)GlobalAsp.GetSessionUser().GetValue("Kdunit");
        Nmunit = (string)GlobalAsp.GetSessionUser().GetValue("Nmunit");
      }
      else if (typeof(RkbmdMpgrmControl).IsInstanceOfType(bo))
      {
        Idprgrm = ((RkbmdMpgrmControl)bo).Idprgrm;
        Nuprgrm = ((RkbmdMpgrmControl)bo).Nuprgrm;
        Nmprgrm = ((RkbmdMpgrmControl)bo).Nmprgrm;
      }
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true));
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
      List<RkbmdPgrmunitControl> ListData = new List<RkbmdPgrmunitControl>();
      foreach (RkbmdPgrmunitControl dc in list)
      {
        ListData.Add(dc);
      }
      //Update(ListData);
      return ListData;
    }
    public override HashTableofParameterRow GetEntries()
    {
      //bool enable = true;
      HashTableofParameterRow hpars = new HashTableofParameterRow();

      return hpars;
    }
    public new int Delete()
    {
      Status = -1;
      int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
      return n;
    }
    #endregion Methods 
    
  }
  #endregion RkbmdPgrmunit
}


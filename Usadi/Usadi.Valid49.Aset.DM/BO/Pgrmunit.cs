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
  #region Usadi.Valid49.BO.PgrmunitControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class PgrmunitControl : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript
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
    public string Kdtahap { get; set; }
    public string Idprgrm { get; set; }
    public string Kdgroup { get; set; }
    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewKegunit",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Daftar kegiatan";
        return new ImageCommand[] { cmd1 };
      }
    }
    public string ViewKegunit
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
    public PgrmunitControl()
    {
      XMLName = ConstantTablesAsetDM.XMLPGRMUNIT;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Kdtahap", "Thang", "Idprgrm" };
      cViewListProperties.IDKey = "Idprgrm";//IDKey for ID Notes
      cViewListProperties.IDProperty = "Idprgrm";//UniqueKey in gridview
      cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdtahap" };//Key in GetFilters should put here
      cViewListProperties.SortFields = new String[] { "Nuprgrm" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      cViewListProperties.PageSize = 30;

      //PgrmunitControl cPgrmunitGetkdgroup = new PgrmunitControl();
      //cPgrmunitGetkdgroup.Userid = GlobalAsp.GetSessionUser().GetUserID();
      //cPgrmunitGetkdgroup.Load("Getkdgroup");

      //if (cPgrmunitGetkdgroup.Kdgroup == "01")
      //{
      //  cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD;
      //}
      //else
      //{
      //  cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
      //}

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
      else if (typeof(MpgrmControl).IsInstanceOfType(bo))
      {
        Idprgrm = ((MpgrmControl)bo).Idprgrm;
        Nuprgrm = ((MpgrmControl)bo).Nuprgrm;
        Nmprgrm = ((MpgrmControl)bo).Nmprgrm;
      }
    }
    public new void SetPrimaryKey()
    {
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      Thang = cPemda.Configval;
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdtahap=Tahap"),
      GetList(new TahapLookupControl()), "Kdtahap=Uraian", 41).SetAllowRefresh(true).SetEnable(enableFilter).SetAllowEmpty(true));
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
      List<PgrmunitControl> ListData = new List<PgrmunitControl>();
      foreach (PgrmunitControl dc in list)
      {
        ListData.Add(dc);
      }
      //Update(ListData);
      return ListData;
    }
    public override HashTableofParameterRow GetEntries()
    {
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Thang=Tahun Anggaran"), false, 15).SetEnable(false));
      return hpars;
    }
    public new void Insert()
    {

      string sql = @"
            exec [dbo].[WSP_GETMASTER_KUA]
            @THANG = N'{0}'
            ";
      sql = string.Format(sql, Thang);
      BaseDataAdapter.ExecuteCmd(this, sql);
    }
    public new int Delete()
    {
      Status = -1;
      int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
      return n;
    }
    #endregion Methods 
    
  }
  #endregion Pgrmunit
}


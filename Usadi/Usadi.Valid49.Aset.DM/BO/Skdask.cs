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
  #region Usadi.Valid49.BO.SkdaskControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class SkdaskControl : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript
  {
    #region Properties 
    public long Id { get; set; }
    public string Kdtahap { get; set; }
    public string Uraitahap { get; set; }
    public int Idxkode { get; set; }
    public string Uraian { get; set; }
    public string Idxttd1 { get; set; }
    public string Idxttd2 { get; set; }
    public string Nodask { get; set; }
    public DateTime Tgldask { get; set; }
    public string Nosah { get; set; }
    public string Ketdask { get; set; }
    public DateTime Tglvalid { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public string Kdkegunit { get; set; }
    public string Nukeg { get; set; }
    public string Nmkegunit { get; set; }
    public string Thang { get; set; }
    public string Idxdask { get; set; }
    public string Unitkey { get; set; }
    public string Kdgroup { get; set; }
    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          CommandName = "ViewDaskr",
          Icon = Icon.PageCopy
        };
        cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rekening Belanja";
        return new ImageCommand[] { cmd1 };
      }
    }

    public string ViewDaskr
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
        return "Rekening Belanja :" + url;
      }
    }
    #endregion Properties 

    #region Methods 
    public SkdaskControl()
    {
      XMLName = ConstantTablesAsetDM.XMLSKDASK;
    }
    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Idxdask","Unitkey" };
      cViewListProperties.IDKey = "Idxdask";//IDKey for ID Notes
      cViewListProperties.IDProperty = "Idxdask";//UniqueKey in gridview
      cViewListProperties.ReadOnlyFields = new String[] { "Kdunit","Nmunit","Unitkey","Kdtahap","Kdkegunit" };//Key in GetFilters should put here
      cViewListProperties.SortFields = new String[] { "Nodask" };//
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;

      //SkdaskControl cSkdaskGetkdgroup = new SkdaskControl();
      //cSkdaskGetkdgroup.Userid = GlobalAsp.GetSessionUser().GetUserID();
      //cSkdaskGetkdgroup.Load("Getkdgroup");

      //if (cSkdaskGetkdgroup.Kdgroup == "01")
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
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nodask=Kode DPA"), typeof(string), 25, HorizontalAlign.Left).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tgldask=Tanggal DPA"), typeof(DateTime), 30, HorizontalAlign.Center).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglvalid=Tanggal Valid"), typeof(DateTime), 30, HorizontalAlign.Center).SetEditable(false));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ketdask=Keterangan"), typeof(string), 75, HorizontalAlign.Left).SetEditable(false));

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
      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(true));
      hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdtahap=Tahap"),
      GetList(new TahapLookupControl()), "Kdtahap=Uraian", 41).SetAllowRefresh(true).SetEnable(enableFilter).SetAllowEmpty(true));
      hpars.Add(KegunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(true));

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
      List<SkdaskControl> ListData = new List<SkdaskControl>();
      foreach (SkdaskControl dc in list)
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
    //Unuk ParameterLookup2, pastikan parameter entry is true
    public override HashTableofParameterRow GetEntries()
    {
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Thang=Tahun Anggaran"), false, 15).SetEnable(false));
      return hpars;
    }
    public new void Insert()
    {

      string sql = @"
            exec [dbo].[WSP_GETMASTER_DPA]
            @THANG = N'{0}'
            ";
      sql = string.Format(sql, Thang);
      BaseDataAdapter.ExecuteCmd(this, sql);
    }
    #endregion Methods 
  }
  #endregion Skdask
}


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
    #region Usadi.Valid49.BO.KiblainnyadetControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class KiblainnyadetControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript, IExtDataControl
    {
        #region Properties
        public long Id { get; set; }
        public string Unitkey { get; set; }
        public string Kdunit { get; set; }
        public string Nmunit { get; set; }
        public string Asetkey { get; set; }
        public string Noreg { get; set; }
        public string Nodokumen { get; set; }
        public DateTime Tgldokumen { get; set; }
        public decimal Nilai { get; set; }
        public decimal Umeko { get; set; }
        public string Kdtans { get; set; }
        public string Nmtrans { get; set; }
        public string Kdkib { get; set; }
        public new string Ket { get; set; }
        public string Kdkon { get; set; }
        public string Kdklas { get; set; }
        public string Kdsensus { get; set; }
        public string Kdstatusaset { get; set; }
        public string Kdlokpo { get; set; }
        public string Kdbrgpo { get; set; }
        public string Thang { get; set; }
        public string Uraitrans { get; set; }
        public decimal Nilaitrans { get; set; }
        public int Uruttrans { get; set; }
        public string Idbrg { get; set; }
        public string Blokid
        {
            get
            {
                WebuserControl cWebuserGetid = new WebuserControl();
                cWebuserGetid.Userid = GlobalAsp.GetSessionUser().GetUserID();
                cWebuserGetid.Load("PK");

                return cWebuserGetid.Blokid;
            }
        }
        #endregion Properties 

        #region Methods 
        public KiblainnyadetControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLKIBLAINNYADET;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Idbrg", "Uruttrans" };
            cViewListProperties.IDKey = "Id";
            cViewListProperties.IDProperty = "Id";
            cViewListProperties.ReadOnlyFields = new String[] { };
            cViewListProperties.SortFields = new String[] { "Uruttrans" };
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
            cViewListProperties.AllowMultiDelete = false;

            return cViewListProperties;
        }
        public new void SetFilterKey(BaseBO bo)
        {
            if (typeof(KiblainnyaControl).IsInstanceOfType(bo))
            {
                Idbrg = ((KiblainnyaControl)bo).Idbrg;
                Unitkey = ((KiblainnyaControl)bo).Unitkey;
                Asetkey = ((KiblainnyaControl)bo).Asetkey;
            }
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nodokumen=Nomor Dokumen"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tgldokumen=Tanggal Dokumen"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraitrans=Jenis Transaksi"), typeof(string), 50, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilaitrans=Nilai"), typeof(decimal), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Umeko=Masa Pakai"), typeof(decimal), 20, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket"), typeof(string), 100, HorizontalAlign.Left));

            return columns;
        }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    //  public new IList View()
    //  {
    //      string sql = @"
    //  exec [dbo].[REGISTER_KIBLAINNYA]
    //@UNITKEY = N'{0}',
    //@ASETKEY = N'{1}'
    //";

    //      sql = string.Format(sql, Unitkey, Asetkey);
    //      string[] fields = new string[] { "Id", "Idbrg", "Unitkey", "Kdunit", "Nmunit", "Asetkey", "Kdaset", "Nmaset", "Tglperolehan"
    //  , "Tahun", "Noreg", "Nilai", "Umeko", "Kdpemilik", "Nmpemilik", "Kdkon", "Nmkon", "Asalusul", "Pengguna", "Kdsatuan", "Nmsatuan"
    //  , "Spesifikasi", "Ukuran", "Bahan", "Nosertifikat", "Alamat", "Ket", "Kdklas", "Uraiklas", "Kdstatusaset", "Kdkib", "Nmkib"  };
    //      List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
    //      List<KiblainnyaControl> ListData = new List<KiblainnyaControl>();

    //      foreach (KiblainnyaControl dc in list)
    //      {
    //          ListData.Add(dc);
    //      }
    //      return ListData;
    //  }

    public new IList View(string label)
        {
            IList list = ((BaseDataControl)this).View(label);
            List<KiblainnyadetControl> ListData = new List<KiblainnyadetControl>();
            foreach (KiblainnyadetControl dc in list)
            {
                ListData.Add(dc);
            }

            return ListData;
        }
        public Icon GetIcon()
        {
            return Icon.Table;
        }
        public string GetIconText()
        {
            return string.Empty;
        }
        public void SetTotalFields(Toolbar tbbtm)
        {
            if (true)
            {
                tbbtm.Add(new ToolbarFill());
                //tbbtm.Add(new DisplayField() { ID = "DfSubTotal", Text = "0" });
                tbbtm.Add(new ToolbarSeparator());
                tbbtm.Add(new DisplayField() { ID = "DfTotal", Text = "0" });
            }
        }
        public void SetTotal(Control seed)
        {
            if (true)
            {
                PagingToolbar toolbar = ControlUtils.FindControl<PagingToolbar>(seed, "TopBar1");
                int idx = 0;
                int pagesize = 0;
                if (toolbar != null)
                {
                    idx = toolbar.PageIndex;
                    pagesize = toolbar.PageSize;
                }

                int id = GlobalAsp.GetRequestI();
                IList list = GlobalAsp.GetSessionListRows();

                decimal subtotal = 0;
                decimal total = 0;
                if (list != null && list.Count > 0)
                {
                    int start = (idx * pagesize);
                    int finish = ((idx + 1) * pagesize);
                    for (int i = 0; i < list.Count; i++)
                    {
                        KiblainnyadetControl ctrl = (KiblainnyadetControl)list[i];
                        if ((i >= start) && (i <= finish))
                        {
                            subtotal += ctrl.Nilaitrans;
                        }
                        total += ctrl.Nilaitrans;
                    }
                }
                //DisplayField DfSubTotal = ControlUtils.FindControl<DisplayField>(seed, "DfSubTotal");
                DisplayField DfTotal = ControlUtils.FindControl<DisplayField>(seed, "DfTotal");
                //DfSubTotal.Text = "Subtotal = " + subtotal.ToString("#,##0");
                DfTotal.Text = "Total = " + total.ToString("#,##0");
            }
        }
        #endregion Methods 
    }
    #endregion Kiblainnyadet
}


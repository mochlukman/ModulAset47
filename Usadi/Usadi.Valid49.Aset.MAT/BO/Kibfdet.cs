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
    #region Usadi.Valid49.BO.KibfdetControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class KibfdetControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript, IExtDataControl
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
        public int Idtermin { get; set; }
        public int Jumlah { get; set; }
        public decimal Nilai { get; set; }
        public decimal Nilaibakf { get; set; }
        public decimal Nilaitrans { get; set; }
        public decimal Prosenfisik { get; set; }
        public decimal Prosenbiaya { get; set; }
        public string Kdtans { get; set; }
        public string Nmtrans { get; set; }
        public string Uraitrans { get; set; }
        public string Kdlokpo { get; set; }
        public string Kdbrgpo { get; set; }
        public new string Ket { get; set; }
        public string Thang { get; set; }
        public string Noba { get; set; }
        public DateTime Tglba { get; set; }
        public string Tglbalookup { get { return Tgldokumen.ToString("dd/MM/yyyy"); } }
        public int Maxuruttrans { get; set; }
        public int Uruttrans { get; set; }
        public string Idbrg { get; set; }
        public string Blokid { get; set; }
        #endregion Properties 

        #region Methods 
        public KibfdetControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLKIBFDET;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Idbrg", "Uruttrans" };
            cViewListProperties.IDKey = "Id";
            cViewListProperties.IDProperty = "Id";
            cViewListProperties.ReadOnlyFields = new String[] { "Idbrg", "Unitkey", "Kdunit", "Nmunit", "Asetkey", "Tahun", "Noreg", "Jumlah", "Kdlokpo", "Kdbrgpo" };
            cViewListProperties.SortFields = new String[] { "Uruttrans" };
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;

            WebsetControl cWebset = new WebsetControl();
            cWebset.Kdset = "saldoawal";
            cWebset.Load("PK");
            string Entrysa = cWebset.Valset.ToUpper();

            if (Blokid == "1")
            {
                cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
            }
            else
            {
                if (Entrysa == "Y")
                {
                    cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_EDIT_DEL;
                }
                else
                {
                    cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
                }
                cViewListProperties.AllowMultiDelete = true;
            }

            return cViewListProperties;
        }
        public override DataControlFieldCollection GetColumns()
        {
            bool enable = true;

            if (Blokid == "1")
            {
                enable = false;
            }

            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nodokumen=Nomor Dokumen"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tgldokumen=Tanggal Dokumen"), typeof(DateTime), 20, HorizontalAlign.Center).SetEditable(false));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraitrans=Jenis Transaksi"), typeof(string), 50, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilaitrans=Nilai"), typeof(decimal), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Idtermin=Termin"), typeof(decimal), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Prosenfisik=Prosentase Fisik"), typeof(decimal), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Prosenbiaya=Prosentase Biaya"), typeof(decimal), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket"), typeof(string), 100, HorizontalAlign.Left));

            return columns;
        }
        public override HashTableofParameterRow GetEntries()
        {
            bool enable = true;

            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(BeritaBakfLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetEditable(false).SetAllowRefresh(true)
              .SetAllowEmpty(false).SetEditable(false));
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Idtermin=Termin"), true, 28).SetEnable(enable).SetEditable(true)
              .SetAllowEmpty(false));
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Prosenfisik=Prosentase Fisik"), true, 28).SetEnable(enable).SetEditable(true)
              .SetAllowEmpty(false));
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Prosenbiaya=Prosentase Biaya"), true, 28).SetEnable(enable).SetEditable(true)
              .SetAllowEmpty(false));
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Nilai"), true, 28).SetEnable(false).SetEditable(false).SetAllowRefresh(true)
              .SetAllowEmpty(false));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true));
            return hpars;
        }
        public new void SetFilterKey(BaseBO bo)
        {
            if (typeof(IDataControlMenu).IsInstanceOfType(bo))
            {
                Last_by = ((BaseBO)bo).Userid;
            }
            else if (typeof(KibfControl).IsInstanceOfType(bo))
            {
                Idbrg = ((KibfControl)bo).Idbrg;
                Unitkey = ((KibfControl)bo).Unitkey;
                Asetkey = ((KibfControl)bo).Asetkey;
                Tahun = ((KibfControl)bo).Tahun;
                Noreg = ((KibfControl)bo).Noreg;
                Jumlah = ((KibfControl)bo).Jumlah;
                Kdlokpo = ((KibfControl)bo).Kdlokpo;
                Kdbrgpo = ((KibfControl)bo).Kdbrgpo;
                Blokid = ((KibfControl)bo).Blokid;
                Kdunit = ((KibfControl)bo).Kdunit;
                Nmunit = ((KibfControl)bo).Nmunit;
            }
        }
        public new IList View()
        {
            IList list = this.View(BaseDataControl.ALL);
            return list;
        }
        public new IList View(string label)
        {
            IList list = ((BaseDataControl)this).View(label);
            List<KibfdetControl> ListData = new List<KibfdetControl>();
            foreach (KibfdetControl dc in list)
            {
                ListData.Add(dc);
            }
            return ListData;
        }
        public new void Insert()
        {
            PemdaControl cPemda = new PemdaControl();
            cPemda.Configid = "cur_thang";
            cPemda.Load("PK");

            Thang = cPemda.Configval.Trim();
            //Nodokumen = Noba;
            Tgldokumen = Tglba;

            KibfdetControl cKibfdetGetmaxuruttrans = new KibfdetControl();
            cKibfdetGetmaxuruttrans.Idbrg = Idbrg;
            cKibfdetGetmaxuruttrans.Load("Maxuruttrans");
            Uruttrans = (cKibfdetGetmaxuruttrans.Maxuruttrans) + 1;

            base.Insert();
        }
        public new int Delete()
        {
            Status = -1;
            int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
            return n;
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
                        KibfdetControl ctrl = (KibfdetControl)list[i];
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
    #endregion Kibfdet
}


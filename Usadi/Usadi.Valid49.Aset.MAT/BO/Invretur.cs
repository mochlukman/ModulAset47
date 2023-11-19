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
    #region Usadi.Valid49.BO.InvreturControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class InvreturControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
    {
        #region Properties 
        public DateTime Datecreate { get; set; }
        public DateTime Dateupdate { get; set; }
        public string Kdgudang { get; set; }
        public string Nmgudang { get; set; }
        public long Id { get; set; }
        public string Idxttd { get; set; }
        public string Kdtans { get; set; }
        public string Ket { get; set; }
        public string Noretur { get; set; }
        public string Nobast { get; set; }
        public DateTime Tglretur { get; set; }
        public DateTime Tglvalid { get; set; }
        public DateTime Tglbast { get; set; }
        public string Tglbastlookup { get { return Tglbast.ToString("dd/MM/yyyy"); } }
        public string Unitkey { get; set; }
        public string Kdunit { get; set; }
        public string Nmunit { get; set; }
        public string Unitkey2 { get; set; }
        public string Kdunit2 { get; set; }
        public string Nmunit2 { get; set; }
        public string Unitkeyuk { get; set; }
        public string Urairetur { get; set; }
        #endregion Properties 

        #region Methods 
        public InvreturControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLINVRETUR;
        }
        public new void SetPageKey()
        {
            Kdtans = "118";
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Unitkey2", "Noretur" };
            cViewListProperties.IDKey = "Id";//IDKey for ID Notes
            cViewListProperties.IDProperty = "Id";//UniqueKey in gridview
            cViewListProperties.ReadOnlyFields = new String[] { "Unitkey2", "Kdunit2", "Nmunit2" };//Key in GetFilters should put here
            cViewListProperties.SortFields = new String[] { "Noretur" };//
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
            //cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
            //cViewListProperties.LookupDC = "CoreNET.Common.BO.DmstatusLookupControl, CoreNET.Common.Sys";
            //cViewListProperties.LookupLabelQuery = BaseDataControl.LOOKUP;
            return cViewListProperties;
        }
        public new void SetFilterKey(BaseBO bo)
        {
            if (typeof(IDataControlMenu).IsInstanceOfType(bo))
            {
                //This Is Sample.For more than 2 filter, must checking if property have had value
                Last_by = !string.IsNullOrEmpty(Last_by) ? Last_by : GlobalAsp.GetSessionUser().GetUserID();
            }
        }
        public ImageCommand[] Cmds
        {
            get
            {
                ImageCommand cmd1 = new ImageCommand()
                {
                    CommandName = "ViewInvreturdet",
                    Icon = Icon.PageCopy
                };
                cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Transaksi";
                return new ImageCommand[] { cmd1 };
            }
        }
        public string ViewInvreturdet
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
                return "No. Retur; " + Noretur + ":" + url;
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
            List<InvreturControl> ListData = new List<InvreturControl>();
            foreach (InvreturControl dc in list)
            {
                dc.Valid = (dc.Tglvalid != new DateTime());
                ListData.Add(dc);
            }

            return ListData;
        }
        public new void Insert()
        {
            base.Insert();
        }
        public new int Update()
        {
            int n = 0;

            if (Valid)
            {
                return ((BaseDataControlUI)this).Update("Valid");
            }
            return n;
        }
        public new int Delete()
        {


            int n = 0;
            if (Valid)
            {
                return ((BaseDataControlUI)this).Update("Draft");
            }
            else
            {
                try
                {
                    Status = -1;
                    base.Delete();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("REFERENCE"))
                    {
                        string msg = "Hapus rincian terlebih dahulu";
                        msg = string.Format(msg);
                        throw new Exception(msg);
                    }
                }
            }
            return n;
        }
        public new HashTableofParameterRow GetFilters()
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(DaftunitUkLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
            return hpars;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noretur=No Pengembalian"), typeof(string), 25, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nobast=No BAST Penyaluran"), typeof(string), 25, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglretur=Tanggal Pengembalian"), typeof(DateTime), 20, HorizontalAlign.Center));
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Idxttd"), typeof(string), 10, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglvalid=Tanggal Pengesahan"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmgudang=Gudang"), typeof(string), 40, HorizontalAlign.Left).SetEditable(true));
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdtans"), typeof(string), 30, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Urairetur=Keterangan"), typeof(string), 40, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 40, HorizontalAlign.Left).SetEditable(true));
            return columns;
        }
        //Unuk ParameterLookup2, pastikan parameter entry is true
        public override HashTableofParameterRow GetEntries()
        {

           
            bool enable = true;
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Noretur=No Pengembalian"), true, 100).SetEnable(enable));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglretur=Tanggal Pengembalian"), true).SetEnable(enable));
            hpars.Add(DaftunitPenyediaLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetEditable(false));
            hpars.Add(InvbastLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetEditable(false));
            hpars.Add(DaftgudangLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetEditable(false));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Urairetur=Uraian"), true, 3).SetEnable(enable).SetAllowEmpty(true));
            return hpars;
        }
       
        #endregion Methods 
    }
    #endregion Invretur
}

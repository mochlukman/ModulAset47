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
    #region Usadi.Valid49.BO.InvspbControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class InvspbControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
    {
        #region Properties 
        public DateTime Datecreate { get; set; }
        public DateTime Dateupdate { get; set; }
        public long Id { get; set; }
        public string Idxttd { get; set; }
        public string Kdtans { get; set; }
        public string Ket { get; set; }
        public string Nospb { get; set; }
        public string Nonpb { get; set; }
        public DateTime Tglspb { get; set; }
        public string Tglspblookup { get { return Tglspb.ToString("dd/MM/yyyy"); } }
        public DateTime Tglvalid { get; set; }
        public string Unitkey { get; set; }
        public string Kdunit { get; set; }
        public string Nmunit { get; set; }
        public string Unitkey2 { get; set; }
        public string Kdunit2 { get; set; }
        public string Nmunit2 { get; set; }
        public string Untuk { get; set; }
        public string Untuk2 { get; set; }
        public int Jmldata { get; set; }
        #endregion Properties 

        #region Methods 
        public InvspbControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLINVSPB;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nospb" };
            cViewListProperties.IDKey = "Nospb";//IDKey for ID Notes
            cViewListProperties.IDProperty = "Nospb";//UniqueKey in gridview
            cViewListProperties.ReadOnlyFields = new String[] { "Unitkey2", "Kdunit2", "Nmunit2" };//Key in GetFilters should put here
            cViewListProperties.SortFields = new String[] { "Nospb" };//
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;

            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;
            cViewListProperties.AllowMultiDelete = true;
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
        public new IList View()
        {
            IList list = this.View(BaseDataControl.ALL);
            return list;
        }
        public new IList View(string label)
        {
            IList list = ((BaseDataControl)this).View(label);
            List<InvspbControl> ListData = new List<InvspbControl>();
            foreach (InvspbControl dc in list)
            {
                dc.Valid = (dc.Tglvalid != new DateTime());
                ListData.Add(dc);
            }
            //Update(ListData);
            return ListData;

        }
        public new int Update()
        {
            int n = 0;

            if (Valid)
            {
                return ((BaseDataControlUI)this).Update("ValidRev");
            }
            return n;
        }
        public new void SetPageKey()
        {
            Kdtans = "229";
        }

        public new HashTableofParameterRow GetFilters()
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(DaftunitUkLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
            return hpars;
        }
        public ImageCommand[] Cmds
        {
            get
            {
                ImageCommand cmd1 = new ImageCommand()
                {
                    CommandName = "ViewInvspbdet",
                    Icon = Icon.PageCopy
                };
                cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Permintaan Barang";
                return new ImageCommand[] { cmd1 };
            }
        }
        public string ViewInvspbdet
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
                return "No. SPB; " + Nospb + ":" + url;
            }
        }
        public override DataControlFieldCollection GetColumns()
        {
            bool enable = true;

            DataControlFieldCollection columns = new DataControlFieldCollection();
            //columns.Add(ExtFields.GetStatusField());
            //columns.Add(ExtFields.GetRatingField());
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Id"), typeof(long), 10, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nospb=No Surat Permintaan Barang"), typeof(string), 30, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nonpb=Nota Permintaan Barang"), typeof(string), 25, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglspb=Tanggal Dokumen"), typeof(DateTime), 20, HorizontalAlign.Center));
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Untuk"), typeof(string), 10, HorizontalAlign.Left).SetEditable(true));
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Idxttd"), typeof(string), 10, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglvalid=Tanggal Pengesahan"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 30, HorizontalAlign.Left).SetEditable(true));
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Datecreate"), typeof(DateTime), 13, HorizontalAlign.Center));
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Dateupdate"), typeof(DateTime), 13, HorizontalAlign.Center));
            return columns;
        }
        //Unuk ParameterLookup2, pastikan parameter entry is true
        public override HashTableofParameterRow GetEntries()
        {
            bool enable = true;
            bool enableValid = false;

            InvspbControl cInvspbGetjmldata = new InvspbControl();
            cInvspbGetjmldata.Unitkey = Unitkey;
            cInvspbGetjmldata.Nospb = Nospb;
            cInvspbGetjmldata.Load("Jmldata");

            if (cInvspbGetjmldata.Jmldata != 0)
            {
                enableValid = true;
            }
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            //hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Id"), false, 30).SetEnable(enable));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nospb=Nomor SPB"), true, 100).SetEnable(enable));
            hpars.Add(InvnotaLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetEditable(false));
            //hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Unitkey2"), true, 100).SetEnable(enable));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglspb=Tanggal Dokumen"), true).SetEnable(enable));
            //hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Idxttd"), false, 30).SetEnable(enable));
            //hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglvalid"), true).SetEnable(enable));
            hpars.Add(DaftunitPenyediaLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetEditable(false));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Untuk=Keperluan"), true, 3).SetEnable(enable).SetAllowEmpty(true));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglvalid"), true).SetEnable(enableValid));
            hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));
            //hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Datecreate"), true).SetEnable(enable));
            //hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Dateupdate"), true).SetEnable(enable));
            //hpars.Add(DmtahunLookupControl.Instance.GetLookupParameterRow(this, true).SetEnable(enable));
            //hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdlevel"),
            //  DmlevelLookupControl.GetListDataSingleton(), "Kdlevel=Nmlevel", 30).SetAllowRefresh(false).SetEnable(enable));
            //hpars.Add(new ParameterRowType(this, true));
            //hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitleEntry("Status"),
            //  DmstatusLookupControl.GetListDataSingleton(), "Idstatus=Nmstatus", 30).SetAllowRefresh(false).SetEnable(enable));
            //hpars.Add(new ParameterRowCek(this, true));
            hpars.Add(new ParameterRowUploadFile(this, true));
            //hpars.Add(new ParameterRowHelp(this, true));
            //hpars.Add(new ParameterRowForum(this, true));
            return hpars;
        }
       
        #endregion Methods 
    }
    #endregion Invspb
}

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
    #region Usadi.Valid49.BO.InvrealControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class InvrealControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
    {
        #region Properties 
        public DateTime Datecreate { get; set; }
        public DateTime Dateupdate { get; set; }
        public long Id { get; set; }
        public string Idxttd { get; set; }
        public string Kdgudang { get; set; }
        public string Nmgudang { get; set; }
        public string Kdtans { get; set; }
        public string Ket { get; set; }
        public string Nobhp { get; set; }
        public DateTime Tglbhp { get; set; }
        public string Nobast { get; set; }
        public DateTime Tglbast { get; set; }
        public DateTime Tglvalid { get; set; }
        public string Tglbastlookup { get { return Tglbast.ToString("dd/MM/yyyy"); } }
        public string Unitkey { get; set; }
        public string Kdunit { get; set; }
        public string Nmunit { get; set; }
        public string Untuk { get; set; }
        #endregion Properties 

        #region Methods 
        public InvrealControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLINVREAL;
        }
        public new void SetPageKey()
        {
            Kdtans = "232";
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nobhp" };
            cViewListProperties.IDKey = "Id";//IDKey for ID Notes
            cViewListProperties.IDProperty = "Id";//UniqueKey in gridview
            cViewListProperties.ReadOnlyFields = new String[] { "Unitkey" };//Key in GetFilters should put here
            cViewListProperties.SortFields = new String[] { "Nobhp" };//
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;
            cViewListProperties.AllowMultiDelete = true;
            //cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
            //cViewListProperties.LookupDC = "CoreNET.Common.BO.DmstatusLookupControl, CoreNET.Common.Sys";
            //cViewListProperties.LookupLabelQuery = BaseDataControl.LOOKUP;
            return cViewListProperties;
        }

        public ImageCommand[] Cmds
        {
            get
            {
                ImageCommand cmd1 = new ImageCommand()
                {
                    CommandName = "ViewInvrealdet",
                    Icon = Icon.PageCopy
                };
                cmd1.ToolTip.Text = "Klik Untuk Menampilkan Rincian Transaksi";
                return new ImageCommand[] { cmd1 };
            }
        }
        public string ViewInvrealdet
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
                return "No. Penggunaan; " + Nobhp + ":" + url;
            }
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
            List<InvrealControl> ListData = new List<InvrealControl>();
            foreach (InvrealControl dc in list)
            {
                DmstatusLookupControl.FindAndSetValuesInto(dc);
                ListData.Add(dc);
            }
            //Update(ListData);
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
            hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
            return hpars;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nobhp=No Penggunaan"), typeof(string), 25, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nobast=No BAST Penyaluran"), typeof(string), 25, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglbhp=Tanggal Penggunaan"), typeof(DateTime), 20, HorizontalAlign.Center));
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Idxttd"), typeof(string), 10, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglvalid=Tanggal Pengesahan"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmgudang=Gudang"), typeof(string), 40, HorizontalAlign.Left).SetEditable(true));
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdtans"), typeof(string), 30, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Untuk=Keperluan"), typeof(string), 40, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 40, HorizontalAlign.Left).SetEditable(true));
            return columns;
        }
        //Unuk ParameterLookup2, pastikan parameter entry is true
        public override HashTableofParameterRow GetEntries()
        {
            bool enable = true;
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nobhp=Nomor Penggunaan"), true, 80).SetEnable(enable));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglbhp=Tanggal Penggunaan"), true).SetEnable(enable));
            hpars.Add(InvbastLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetEditable(false));
            hpars.Add(DaftgudangLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetEditable(false));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Untuk=Keperluan"), true, 3).SetEnable(enable).SetAllowEmpty(true));
            //hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true));
            return hpars;
        }
     
        #endregion Methods 
    }
    #endregion Invreal
}
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
    #region Usadi.Valid49.BO.InvbastControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class InvbastControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript
    {
        #region Properties 
        public DateTime Datecreate { get; set; }
        public DateTime Dateupdate { get; set; }
        public long Id { get; set; }
        public string Nobast { get; set; }
        public string Nosppb { get; set; }
        public DateTime Tglbast { get; set; }
        public DateTime Tglsppb { get; set; }
        public string Tglbastlookup { get { return Tglbast.ToString("dd/MM/yyyy"); } }
        public string Tglsppblookup { get { return Tglsppb.ToString("dd/MM/yyyy"); } }
        public string Nospb { get; set; }
        public DateTime Tglspb { get; set; }
        public string Unitkey { get; set; }
        public string Kdunit { get; set; }
        public string Nmunit { get; set; }
        public string Unitkeyuk { get; set; }
        public string Ket { get; set; }
        #endregion Properties 

        #region Methods 
        public InvbastControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLINVBAST;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Nobast" };
            cViewListProperties.IDKey = "Id";//IDKey for ID Notes
            cViewListProperties.IDProperty = "Id";//UniqueKey in gridview
            cViewListProperties.ReadOnlyFields = new String[] { "Unitkey" };//Key in GetFilters should put here
            cViewListProperties.SortFields = new String[] { "Nobast" };//
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;
            cViewListProperties.AllowMultiDelete = true;
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
                Unitkey = (string)GlobalAsp.GetSessionUser().GetValue("Unitkey");
                Kdunit = (string)GlobalAsp.GetSessionUser().GetValue("Kdunit");
                Nmunit = (string)GlobalAsp.GetSessionUser().GetValue("Nmunit");
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
            List<InvbastControl> ListData = new List<InvbastControl>();
            foreach (InvbastControl dc in list)
            {
                DmstatusLookupControl.FindAndSetValuesInto(dc);
                ListData.Add(dc);
            }

            return ListData;
        }

        public new HashTableofParameterRow GetFilters()
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
            return hpars;
        }
        public new void Insert()
        {
            base.Insert();
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Id"), typeof(long), 10, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nobast=No Bast"), typeof(string), 30, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglbast=Tanggal Bast"), typeof(DateTime), 20, HorizontalAlign.Center));
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Unitkeyuk"), typeof(string), 10, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nosppb=No Surat Permintaan Barang"), typeof(string), 30, HorizontalAlign.Left).SetEditable(true));
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglsppb"), typeof(DateTime), 13, HorizontalAlign.Center));
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Datecreate"), typeof(DateTime), 13, HorizontalAlign.Center));
            //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Dateupdate"), typeof(DateTime), 13, HorizontalAlign.Center));
            return columns;


        }
        //Unuk ParameterLookup2, pastikan parameter entry is true
        public override HashTableofParameterRow GetEntries()
        {
            bool enable = true;
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nobast=No Bast"), true, 100).SetEnable(enable));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglbast=Tgl Bast"), true).SetEnable(enable));
            hpars.Add(InvpenyaluranLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetEditable(enable));
            //hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Unitkeyuk"), true, 100).SetEnable(enable));
            //hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nosppb"), true, 100).SetEnable(enable));
            //hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglsppb"), true).SetEnable(enable));
            //hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Datecreate"), true).SetEnable(enable));
            //hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Dateupdate"), true).SetEnable(enable));
            //hpars.Add(DmtahunLookupControl.Instance.GetLookupParameterRow(this, true).SetEnable(enable));
            return hpars;


        }
        #endregion Methods 
    }
    #endregion Invbast
}

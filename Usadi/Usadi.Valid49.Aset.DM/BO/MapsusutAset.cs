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
    #region Usadi.Valid49.BO.MapsusutAsetControl, Usadi.Valid49.Aset.DM
    [Serializable]
    public class MapsusutAsetControl : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript
    {
        #region Properties
        public long Id { get; set; }
        public string Kdaset { get; set; }
        public string Nmaset { get; set; }
        public string Kdasetsusut { get; set; }
        public string Nmasetsusut { get; set; }
        public string Kdnmaset { get; set; }
        public string Kdper { get; set; }
        public string Nmper { get; set; }
        public string Nmdana { get; set; }
        public int Jmlrek { get; set; }
        public int Jmlaset { get; set; }
        public string Kdkib { get; set; }
        public string Nmkib { get; set; }
        public string Asetkeysusut { get; set; }
        public string Asetkey { get; set; }
        public string Kddana { get; set; }
        public string Thang { get; set; }

        #endregion Properties 

        #region Methods 
        public MapsusutAsetControl()
        {
            XMLName = ConstantTablesAsetDM.XMLMAPSUSUTASET;
        }

        public new void SetPageKey()
        {
            Kddana = "00";
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Asetkeysusut", "Asetkey" };
            cViewListProperties.IDKey = "Id";
            cViewListProperties.IDProperty = "Id";
            cViewListProperties.ReadOnlyFields = new String[] { };
            cViewListProperties.SortFields = new String[] { "Kdaset" };
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
            cViewListProperties.LookupDC = " Usadi.Valid49.BO.DaftasetKibfilterLookupControl, Usadi.Valid49.Aset.DM";
            cViewListProperties.LookupLabelQuery = "MapsusutAset";
            cViewListProperties.PageSize = 30;
            cViewListProperties.AllowMultiDelete = true;

            return cViewListProperties;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Aset"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Aset"), typeof(string), 100, HorizontalAlign.Left));

            return columns;
        }
        public new IList View()
        {
            IList list = this.View("All");
            return list;
        }
        public new IList View(string label)
        {
            IList list = ((BaseDataControl)this).View(label);
            List<MapsusutAsetControl> ListData = new List<MapsusutAsetControl>();
            foreach (MapsusutAsetControl dc in list)
            {
                ListData.Add(dc);
            }

            return ListData;
        }
        public override HashTableofParameterRow GetEntries()
        {
            //bool enable = true;
            HashTableofParameterRow hpars = new HashTableofParameterRow();

            return hpars;
        }
        #endregion Methods 
    }
    #endregion MapsusutAset

    #region MapsusutAsetrek
    [Serializable]
    public class MapsusutAsetkeysusutControl : MapsusutAsetControl, IDataControlUIEntry
    {
        public ImageCommand[] Cmds
        {
            get
            {
                ImageCommand cmd1 = new ImageCommand()
                {
                    CommandName = "ViewMapsusutAsetkey",
                    Icon = Icon.PageCopy
                };
                cmd1.ToolTip.Text = "Klik Untuk Menampilkan Mapping Golongan Aset";
                return new ImageCommand[] { cmd1 };
            }
        }
        public string ViewMapsusutAsetkey
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
                return "Mapping Akumulasi Penyusutan Aset " + Nmasetsusut + ":" + url;
            }
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Asetkey" };
            cViewListProperties.IDKey = "Asetkey";
            cViewListProperties.IDProperty = "Asetkey";
            cViewListProperties.ReadOnlyFields = new String[] {  };
            cViewListProperties.SortFields = new String[] { "Kdaset" };
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
            cViewListProperties.LookupDC = " Usadi.Valid49.BO.DaftasetLookupControl, Usadi.Valid49.Aset.DM";
            cViewListProperties.LookupLabelQuery = "Lookup";
            cViewListProperties.PageSize = 30;
            cViewListProperties.AllowMultiDelete = true;

            return cViewListProperties;
        }
        public new IList View()
        {
            IList list = this.View("Penyusutan");
            return list;
        }
        public new void SetFilterKey(BaseBO bo)
        {
            if (typeof(IDataControlMenu).IsInstanceOfType(bo))
            {
                Last_by = ((BaseBO)bo).Userid;
            }
            else if (typeof(DaftasetControl).IsInstanceOfType(bo))
            {
                Asetkey = ((DaftasetControl)bo).Asetkey;
            }
        }
        public new HashTableofParameterRow GetFilters()
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            return hpars;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdasetsusut=Kode Aset"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmasetsusut=Nama Aset"), typeof(string), 100, HorizontalAlign.Left));
            return columns;
        }
        public new void Insert()
        {
            base.Update("Kddana");
        }
        public new int Delete()
        {
            int n = 0;

            MapsusutAsetControl cMapsusutAsetCekjmlrek = new MapsusutAsetControl();
            cMapsusutAsetCekjmlrek.Asetkey = Asetkey;
            cMapsusutAsetCekjmlrek.Load("Jmlrek");

            if (cMapsusutAsetCekjmlrek.Jmlrek != 0)
            {
                throw new Exception("Gagal menghapus data : kode barang yang di mapping ke rekening ini sudah digunakan di transaksi berita");
            }
            else
            {
                Status = -1;
                base.Delete("Kddana");
                base.Update("Hapusdana");
            }
            return n;
        }
    }
    #endregion MapsusutAsetrek

    #region MapsusutAsetkey
    [Serializable]
    public class MapsusutAsetkeyControl : MapsusutAsetControl, IDataControlUIEntry
    {
        public new void SetPrimaryKey()
        {
            PemdaControl cPemda = new PemdaControl();
            cPemda.Configid = "cur_thang";
            cPemda.Load("PK");

            Thang = cPemda.Configval.Trim();
        }
        public new void SetFilterKey(BaseBO bo)
        {
            if (typeof(IDataControlMenu).IsInstanceOfType(bo))
            {
                Last_by = ((BaseBO)bo).Userid;
            }
            else if (typeof(MapsusutAsetControl).IsInstanceOfType(bo))
            {
                Asetkeysusut = ((MapsusutAsetControl)bo).Asetkeysusut;
            }
            else if (typeof(DaftasetControl).IsInstanceOfType(bo))
            {
                Asetkey = ((DaftasetControl)bo).Asetkey;
            }
        }
        public new void Insert()
        {
            base.Insert();
        }
        public new int Delete()
        {
            Status = -1;
            int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
            return n;
        }
    }
    #endregion MapsusutAsetkey
}



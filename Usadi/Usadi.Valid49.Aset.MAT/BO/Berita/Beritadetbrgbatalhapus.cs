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
    #region Usadi.Valid49.BO.BeritadetbrgbatalhapusControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class BeritadetbrgbatalhapusControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript, IExtDataControl
    {
        #region Properties
        public long Id { get; set; }
        public int Jumlah { get; set; }
        public decimal Nilai { get; set; }
        public decimal Umeko { get; set; }
        public bool Generated { get; set; }
        public string Kdpemilik { get; set; }
        public string Asalusul { get; set; }
        public string Pengguna { get; set; }
        public new string Ket { get; set; }
        public string Kdhak { get; set; }
        public decimal Luastnh { get; set; }
        public string Alamat { get; set; }
        public string Nofikat { get; set; }
        public DateTime Tgfikat { get; set; }
        public string Kdsatuan { get; set; }
        public string Kdkon { get; set; }
        public string Merktype { get; set; }
        public string Ukuran { get; set; }
        public string Bahan { get; set; }
        public string Kdwarna { get; set; }
        public string Nopabrik { get; set; }
        public string Norangka { get; set; }
        public string Nopolisi { get; set; }
        public string Nobpkb { get; set; }
        public string Nomesin { get; set; }
        public string Bertingkat { get; set; }
        public string Beton { get; set; }
        public decimal Luaslt { get; set; }
        public string Nodokgdg { get; set; }
        public DateTime Tgdokgdg { get; set; }
        public string Konstruksi { get; set; }
        public decimal Panjang { get; set; }
        public decimal Lebar { get; set; }
        public decimal Luas { get; set; }
        public string Nodokjij { get; set; }
        public DateTime Tgdokjij { get; set; }
        public string Jdlpenerbit { get; set; }
        public string Bkpencipta { get; set; }
        public string Spesifikasi { get; set; }
        public string Asaldaerah { get; set; }
        public string Pencipta { get; set; }
        public string Jenis { get; set; }
        public string Kdfisik { get; set; }
        public string Nodokkdp { get; set; }
        public DateTime Tgdokkdp { get; set; }
        public DateTime Tgmulai { get; set; }
        public decimal Prosenfisik { get; set; }
        public decimal Prosenbiaya { get; set; }
        public string Nokdtanah { get; set; }
        public string Nmkdtanah { get; set; }
        public string Judul { get; set; }
        public string Kdunit { get; set; }
        public string Nmunit { get; set; }
        public string Kdaset { get; set; }
        public string Nmaset { get; set; }
        public string Nmpemilik { get; set; }
        public string Nmhak { get; set; }
        public string Nmsatuan { get; set; }
        public string Nmkon { get; set; }
        public string Nmwarna { get; set; }
        public string Nmfisik { get; set; }
        public string Kddana { get; set; }
        public string Kdtans { get; set; }
        public DateTime Tglba { get; set; }
        public DateTime Tglvalid { get; set; }
        public string Statusentry { get; set; }
        public string Nokontrak { get; set; }
        public string Kdtahap { get; set; }
        public string Kdkegunit { get; set; }
        public string Kdkib { get; set; }
        public string Kdtahun { get; set; }
        public string Nmtahun { get; set; }
        public string Unitkey { get; set; }
        public string Noba { get; set; }
        public string Mtgkey { get; set; }
        public string Asetkey { get; set; }
        public int Urutbrg { get; set; }
        public string Blokid { get; set; }
        public string Noskhapus { get; set; }
        public string Entryhibahnol { get; set; }
        public ImageCommand[] Cmds
        {
            get
            {
                ImageCommand cmd1 = new ImageCommand()
                {
                    CommandName = "ViewBeritadetbrg",
                    Icon = Icon.PageCopy
                };
                cmd1.ToolTip.Text = "Klik Untuk Menampilkan Spesifikasi Barang";
                return new ImageCommand[] { cmd1 };
            }
        }
        public string ViewBeritadetbrg
        {
            get
            {
                string app = GlobalAsp.GetRequestApp();
                string id = GlobalAsp.GetRequestId();
                string idprev = GlobalAsp.GetRequestId();
                string kode = GlobalAsp.GetRequestKode();
                string idx = GlobalAsp.GetRequestIndex();
                string strenable = "&enable=" + ((Status == 0) ? 1 : 0);
                string url = string.Format("PageTabular.aspx?passdc=1&app={0}&i=13&iprev=12&id={1}&idprev={2}&kode={3}&idx={4}" + strenable, app, id, idprev, kode, idx);
                return "Spesifikasi Barang; " + Kdaset + " - " + Nmaset + ":" + url;
            }
        }
        #endregion Properties 

        #region Methods 
        public BeritadetbrgbatalhapusControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLBERITADETBRG;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Unitkey", "Noba", "Asetkey", "Urutbrg" };
            cViewListProperties.IDKey = "Id";
            cViewListProperties.IDProperty = "Id";
            cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Noba", "Mtgkey", "Kdtahap", "Kdkegunit", "Nokontrak", "Kdtans", "Kddana", "Tglba" };
            cViewListProperties.SortFields = new String[] { "Kdaset", "Nmaset", "Tahun", "Noreg" };
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
            cViewListProperties.LookupDC = " Usadi.Valid49.BO.ViewasetHapusbatalControl, Usadi.Valid49.Aset.MAT";
            cViewListProperties.LookupLabelQuery = "";
            cViewListProperties.PageSize = 20;
            cViewListProperties.RefreshFilter = true;

            if (Tglvalid != new DateTime() || Blokid == "1")
            {
                cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
            }
            else
            {
                cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
                cViewListProperties.AllowMultiDelete = true;
            }
            return cViewListProperties;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmtrans=Jenis Penghapusan"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noreg=No Register"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 20, HorizontalAlign.Right));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noskhapus=No. Penghapusan"), typeof(string), 30, HorizontalAlign.Left));
            return columns;
        }
        public new void SetFilterKey(BaseBO bo)
        {
            if (typeof(IDataControlMenu).IsInstanceOfType(bo))
            {
                Last_by = ((BaseBO)bo).Userid;
            }
            else if (typeof(BeritaControl).IsInstanceOfType(bo))
            {
                Unitkey = ((BeritaControl)bo).Unitkey;
                Noba = ((BeritaControl)bo).Noba;
                Kdtans = ((BeritaControl)bo).Kdtans;
                Tglba = ((BeritaControl)bo).Tglba;
                Tglvalid = ((BeritaControl)bo).Tglvalid;
                Blokid = ((BeritaControl)bo).Blokid;
            }
            else if (typeof(DaftasetControl).IsInstanceOfType(bo))
            {
                Asetkey = ((DaftasetControl)bo).Asetkey;
            }
            else if (typeof(ViewasetHapusbatalControl).IsInstanceOfType(bo))
            {
                Unitkey = ((ViewasetHapusbatalControl)bo).Unitkey;
                Tahun = ((ViewasetHapusbatalControl)bo).Tahun;
                Kdtans = ((ViewasetHapusbatalControl)bo).Kdtans;
                Asetkey = ((ViewasetHapusbatalControl)bo).Asetkey;

            }
        }
        public new void Insert()
        {
            base.Insert("Hapusbatal");
        }
        public new IList View()
        {
            IList list = this.View(BaseDataControl.ALL);
            return list;
        }
        public new IList View(string label)
        {
            IList list = ((BaseDataControl)this).View(label);
            List<BeritadetbrgControl> ListData = new List<BeritadetbrgControl>();
            foreach (BeritadetbrgControl dc in list)
            {
                ListData.Add(dc);
            }
            return ListData;
        }
        public new int Delete()
        {
            Status = -1;
            int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
            return n;
        }
        public override HashTableofParameterRow GetEntries()
        {
            //bool enable = true;
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            return hpars;
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
                        BeritadetbrgControl ctrl = (BeritadetbrgControl)list[i];
                        if ((i >= start) && (i <= finish))
                        {
                            subtotal += ctrl.Nilai;
                        }
                        total += ctrl.Nilai;
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
    #endregion Beritadetbrgbatalhapus

}


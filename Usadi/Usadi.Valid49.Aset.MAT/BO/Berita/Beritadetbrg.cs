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
    #region Usadi.Valid49.BO.BeritadetbrgControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class BeritadetbrgControl : BaseDataControlAsetMAT, IDataControlUIEntry, IHasJSScript, IExtDataControl
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
        public BeritadetbrgControl()
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
            cViewListProperties.SortFields = new String[] { "Kdaset", "Jumlah", "Nilai" };
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
            cViewListProperties.PageSize = 20;

            //if (Tglvalid != new DateTime() || Blokid == "1")
            //{
            //  cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
            //}
            //else
            //{
            //  cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
            //  cViewListProperties.AllowMultiDelete = true;
            //}
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
            cViewListProperties.AllowMultiDelete = true;
            return cViewListProperties;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 50, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jumlah=Jumlah Barang"), typeof(int), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai"), typeof(decimal), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Statusentry"), typeof(string), 20, HorizontalAlign.Center));
            return columns;
        }
        public new void SetFilterKey(BaseBO bo)
        {
            if (typeof(IDataControlMenu).IsInstanceOfType(bo))
            {
                Last_by = ((BaseBO)bo).Userid;
            }
            else if (typeof(BeritadetrControl).IsInstanceOfType(bo))
            {
                Unitkey = ((BeritadetrControl)bo).Unitkey;
                Noba = ((BeritadetrControl)bo).Noba;
                Mtgkey = ((BeritadetrControl)bo).Mtgkey;
                Nokontrak = ((BeritadetrControl)bo).Nokontrak;
                Kdtahap = ((BeritadetrControl)bo).Kdtahap;
                Kdkegunit = ((BeritadetrControl)bo).Kdkegunit;
                Kdtans = ((BeritadetrControl)bo).Kdtans;
                Kddana = ((BeritadetrControl)bo).Kddana;
                Tglba = ((BeritadetrControl)bo).Tglba;
                Tglvalid = ((BeritadetrControl)bo).Tglvalid;
                Blokid = ((BeritadetrControl)bo).Blokid;
            }
            else if (typeof(DaftasetControl).IsInstanceOfType(bo))
            {
                Asetkey = ((DaftasetControl)bo).Asetkey;
            }
        }
        public new void SetPrimaryKey()
        {
            Jumlah = 1;
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
        public new void Insert()
        {
            if (Jumlah == 0)
            {
                throw new Exception("Jumlah aset tidak boleh 0");
            }

            WebsetControl cWebset = new WebsetControl();
            cWebset.Kdset = "nilbasthibah";
            cWebset.Load("PK");
            Entryhibahnol = cWebset.Valset.ToUpper();

            BeritadetbrgControl cBeritadetbrgGetKddana = new BeritadetbrgControl();
            cBeritadetbrgGetKddana.Unitkey = Unitkey;
            cBeritadetbrgGetKddana.Noba = Noba;
            cBeritadetbrgGetKddana.Kdtans = Kdtans;
            cBeritadetbrgGetKddana.Load("Kddana");
            Kddana = cBeritadetbrgGetKddana.Kddana;


            if (Nilai == 0)
            {
                if (Kdtans != "102")
                {
                    throw new Exception("Nilai aset tidak boleh 0");
                }
                else if (Kdtans == "102" && Entryhibahnol == "T")
                {
                    throw new Exception("Nilai aset tidak boleh 0");
                }
            }


            // if (Kdtans == "101" || Kdtans == "111")  //pengadaan baru dan bakf
            if ((Kdtans == "101" && Kddana != "02") && (Kdtans == "101" && Kddana != "03"))
            {
                string sql = @"
        exec [dbo].[WSP_VALBERITADETBRG]
        @UNITKEY = N'{0}',
        @NOBA = N'{1}',
        @TGLBA = N'{2}',
        @NOKONTRAK = N'{3}',
        @MTGKEY = N'{4}',
        @KDTAHAP = N'{5}',
        @KDKEGUNIT = N'{6}',
        @ASETKEY = N'{7}',
        @JUMLAH = N'{8}',
        @NILAI = N'{9}'
        ";

                sql = string.Format(sql, Unitkey, Noba, Tglba.ToString("yyyy-MM-dd"), Nokontrak, Mtgkey, Kdtahap, Kdkegunit, Asetkey, Jumlah, Nilai);
                BaseDataAdapter.ExecuteCmd(this, sql);
            }
            else if ((Kdtans == "111" && Kddana != "02") && (Kdtans == "111" && Kddana != "03"))
            {
                string sql = @"
        exec [dbo].[WSP_VALBERITADETBRG]
        @UNITKEY = N'{0}',
        @NOBA = N'{1}',
        @TGLBA = N'{2}',
        @NOKONTRAK = N'{3}',
        @MTGKEY = N'{4}',
        @KDTAHAP = N'{5}',
        @KDKEGUNIT = N'{6}',
        @ASETKEY = N'{7}',
        @JUMLAH = N'{8}',
        @NILAI = N'{9}'
        ";

                sql = string.Format(sql, Unitkey, Noba, Tglba.ToString("yyyy-MM-dd"), Nokontrak, Mtgkey, Kdtahap, Kdkegunit, Asetkey, Jumlah, Nilai);
                BaseDataAdapter.ExecuteCmd(this, sql);
            }
            else // Hibah dan lainnya
            {
                base.Insert("Lainnya");
            }
        }
        public new int Delete()
        {
            int n = 0;
            Status = -1;
            base.Delete();
            base.Update("Hapus");
            return n;
        }
        public override HashTableofParameterRow GetEntries()
        {
            bool enable = true;

            HashTableofParameterRow hpars = new HashTableofParameterRow();

            //if(Kddana == "01")
            //{
            //  hpars.Add(DaftasetBastLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetEditable(false));
            //}
            //else
            //{
            //  hpars.Add(DaftasetBosbludLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetEditable(false));
            //}
            hpars.Add(DaftasetBastLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetEditable(false));
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Jumlah=Jumlah Barang"), true, 20).SetEnable(enable).SetEditable(false)
              .SetAllowEmpty(false));
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Nilai=Nilai Total"), true, 50).SetEnable(enable).SetEditable(false)
              .SetAllowEmpty(false));
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
    #endregion Beritadetbrg

    #region BeritadetbrgLainnya
    [Serializable]
    public class BeritadetbrgLainnyaControl : BeritadetbrgControl, IDataControlUIEntry, IHasJSScript
    {
        #region Properties
        public new ImageCommand[] Cmds
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
        public new string ViewBeritadetbrg
        {
            get
            {
                string app = GlobalAsp.GetRequestApp();
                string id = GlobalAsp.GetRequestId();
                string idprev = GlobalAsp.GetRequestId();
                string kode = GlobalAsp.GetRequestKode();
                string idx = GlobalAsp.GetRequestIndex();
                string strenable = "&enable=" + ((Status == 0) ? 1 : 0);
                string url = string.Format("PageTabular.aspx?passdc=1&app={0}&i=12&iprev=11&id={1}&idprev={2}&kode={3}&idx={4}" + strenable, app, id, idprev, kode, idx);
                return "Spesifikasi Barang; " + Kdaset + " - " + Nmaset + ":" + url;
            }
        }
        #endregion Properties

        #region Methods 
        public new void SetFilterKey(BaseBO bo)
        {
            if (typeof(BeritaControl).IsInstanceOfType(bo))
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
        }
        public new void SetPrimaryKey()
        {
            BeritadetbrgLainnyaControl cBeritadetbrgGeturutbrg = new BeritadetbrgLainnyaControl();
            cBeritadetbrgGeturutbrg.Unitkey = Unitkey;
            cBeritadetbrgGeturutbrg.Noba = Noba;
            cBeritadetbrgGeturutbrg.Load("Geturutbrg");

            Urutbrg = cBeritadetbrgGeturutbrg.Urutbrg;
            Mtgkey = null;
            Jumlah = 1;
            Tahun = Tglba.Year;
        }
        public override HashTableofParameterRow GetEntries()
        {
            bool enable = true;
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(DaftasetKibfilterLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetEditable(false));
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Jumlah=Jumlah Barang"), true, 20).SetEnable(enable).SetEditable(false)
              .SetAllowEmpty(false));
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Nilai=Nilai Total"), true, 50).SetEnable(enable).SetEditable(false)
              .SetAllowEmpty(false));
            return hpars;
        }
        #endregion Methods 
    }
    #endregion BeritadetbrgLainnya

    #region BeritadetbrgBakf
    [Serializable]
    public class BeritadetbrgBakfControl : BeritadetbrgControl, IDataControlUIEntry, IHasJSScript
    {
        #region Methods 
        public override HashTableofParameterRow GetEntries()
        {
            bool enable = true;
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(DaftasetKibfilterLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetEditable(false));
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Jumlah=Jumlah Barang"), true, 20).SetEnable(enable).SetEditable(false)
              .SetAllowEmpty(false));
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Nilai=Nilai Total"), true, 50).SetEnable(enable).SetEditable(false)
              .SetAllowEmpty(false));
            return hpars;
        }
        #endregion Methods 
    }
    #endregion BeritadetbrgBakf

    #region BeritadetbrgEdit
    [Serializable]
    public class BeritadetbrgEditControl : BeritadetbrgControl, IDataControlUIEntry
    {
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
            cViewListProperties.PageSize = 20;

            if (Tglvalid != new DateTime() || Blokid == "1")
            {
                cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
            }
            else
            {
                cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_EDIT;
                cViewListProperties.AllowMultiDelete = false;
            }
            return cViewListProperties;
        }
        public new IList View()
        {
            IList list = this.View("Edit");
            return list;
        }
        public new void SetFilterKey(BaseBO bo)
        {
            if (typeof(IDataControlMenu).IsInstanceOfType(bo))
            {
                Last_by = ((BaseBO)bo).Userid;
            }
            else if (typeof(BeritadetbrgControl).IsInstanceOfType(bo))
            {
                Unitkey = ((BeritadetbrgControl)bo).Unitkey;
                Noba = ((BeritadetbrgControl)bo).Noba;
                Mtgkey = ((BeritadetbrgControl)bo).Mtgkey;
                Asetkey = ((BeritadetbrgControl)bo).Asetkey;
                Urutbrg = ((BeritadetbrgControl)bo).Urutbrg;
                Kdkib = ((BeritadetbrgControl)bo).Kdkib;
                Kdtans = ((BeritadetbrgControl)bo).Kdtans;
                Tglba = ((BeritadetbrgControl)bo).Tglba;
                Tglvalid = ((BeritadetbrgControl)bo).Tglvalid;
                Blokid = ((BeritadetbrgControl)bo).Blokid;
            }
        }
        public new void SetPrimaryKey()
        {
            Bertingkat = "0";
            Beton = "0";
        }
        public override DataControlFieldCollection GetColumns()
        {
            bool enable = true;

            if (Tglvalid != new DateTime() || Blokid == "1")
            {
                enable = false;
            }

            DataControlFieldCollection columns = new DataControlFieldCollection();
            if (Kdkib == "00")
            {
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 40, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Center));
                //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pengguna"), typeof(string), 25, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi"), typeof(string), 20, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
                //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Judul"), typeof(string), 30, HorizontalAlign.Left));
                //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pencipta"), typeof(string), 30, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Spesifikasi"), typeof(string), 15, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jenis"), typeof(string), 15, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ukuran"), typeof(string), 15, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));
            }
            else if (Kdkib == "01")
            {
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 40, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Left));
                //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pengguna"), typeof(string), 25, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmhak=Hak"), typeof(string), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nofikat=Nomor Sertifikat"), typeof(string), 20, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tgfikat=Tanggal Sertifikat"), typeof(DateTime), 20, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Luastnh=Luas Tanah"), typeof(decimal), 15, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));
            }
            else if (Kdkib == "02")
            {
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 40, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Left));
                //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pengguna"), typeof(string), 25, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi"), typeof(string), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmwarna=Warna"), typeof(string), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Merktype=Merk/Type"), typeof(string), 25, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ukuran"), typeof(string), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Bahan"), typeof(string), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nopabrik=No Pabrik"), typeof(string), 20, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Norangka=No Rangka"), typeof(string), 25, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nopolisi=No Polisi"), typeof(string), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nobpkb=No BPKB"), typeof(string), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nomesin=No Mesin"), typeof(string), 20, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));
            }
            else if (Kdkib == "03")
            {
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 40, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Center));
                //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pengguna"), typeof(string), 25, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi"), typeof(string), 20, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmhak=Hak"), typeof(string), 25, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Luaslt=Luas Lantai"), typeof(decimal), 15, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Luastnh=Luas Tanah"), typeof(decimal), 15, HorizontalAlign.Left));
                //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Bertingkat=Bertingkat!"), typeof(string), 15, HorizontalAlign.Center));
                //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Beton=Beton!"), typeof(string), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nodokgdg=Nomor Dokumen"), typeof(string), 20, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tgdokgdg=Tanggal Dokumen"), typeof(DateTime), 20, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));
            }
            else if (Kdkib == "04")
            {
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 40, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi"), typeof(string), 20, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmhak=Hak"), typeof(string), 25, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nodokjij=Nomor Dokumen"), typeof(string), 20, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tgdokjij=Tanggal Dokumen"), typeof(DateTime), 20, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Konstruksi"), typeof(string), 30, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Panjang"), typeof(decimal), 15, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Lebar"), typeof(decimal), 15, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Luas"), typeof(decimal), 15, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));
            }
            else if (Kdkib == "05")
            {
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 40, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi"), typeof(string), 20, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jdlpenerbit=Buku Judul/Penerbit"), typeof(string), 30, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Bkpencipta=Buku Pencipta"), typeof(string), 30, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Spesifikasi"), typeof(string), 15, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asaldaerah=Asal Daerah"), typeof(string), 15, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pencipta"), typeof(string), 15, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Bahan"), typeof(string), 15, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jenis"), typeof(string), 15, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ukuran"), typeof(string), 15, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));
            }
            else if (Kdkib == "06")
            {
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 40, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmhak=Hak"), typeof(string), 25, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmfisik=Fisik"), typeof(string), 20, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Luaslt=Luas Lantai"), typeof(decimal), 15, HorizontalAlign.Left));
                //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Bertingkat=Bertingkat!"), typeof(string), 15, HorizontalAlign.Center));
                //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Beton=Beton!"), typeof(string), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nodokkdp=No. Dok KDP"), typeof(string), 20, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tgdokkdp=Tgl Dok KDP"), typeof(DateTime), 20, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tgmulai=Tgl Mulai"), typeof(DateTime), 20, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));
            }
            else if (Kdkib == "07")
            {
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center).SetVisible(enable));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 40, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tahun"), typeof(int), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpemilik=Pemilik"), typeof(string), 30, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), typeof(string), 25, HorizontalAlign.Center));
                //columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pengguna"), typeof(string), 25, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmkon=Kondisi"), typeof(string), 20, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Judul"), typeof(string), 30, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pencipta"), typeof(string), 30, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Spesifikasi"), typeof(string), 15, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jenis"), typeof(string), 15, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ukuran"), typeof(string), 15, HorizontalAlign.Left));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 100, HorizontalAlign.Left));
            }
            return columns;
        }

        public const string GROUP_1 = "A. Spesifikasi 1";
        public const string GROUP_2 = "B. Spesifikasi 2";
        public const string GROUP_3 = "C. Spesifikasi 3";
        public override HashTableofParameterRow GetEntries()
        {
            HashTableofParameterRow hpars = new HashTableofParameterRow();

            bool enable = true, tahun_e = false, kdpemilik_e = false, alamat_e = false, asaldaerah_e = false, asalusul_e = false, bahan_e = false, bertingkat_e = false, beton_e = false
              , bkpencipta_e = false, jdlpenerbit_e = false, jenis_e = false, kdfisik_e = false, kdhak_e = false, kdkon_e = false, kdsatuan_e = false
              , kdwarna_e = false, ket_e = false, konstruksi_e = false, lebar_e = false, luas_e = false, luaslt_e = false, luastnh_e = false
              , merktype_e = false, nobpkb_e = false, nodokgdg_e = false, nodokjij_e = false, nodokkdp_e = false, nofikat_e = false, nomesin_e = false
              , nopabrik_e = false, nopolisi_e = false, norangka_e = false, panjang_e = false, pencipta_e = false, spesifikasi_e = false
              , tgdokgdg_e = false, tgdokjij_e = false, tgdokkdp_e = false, tgfikat_e = false, tgmulai_e = false, ukuran_e = false, judul_e = false
              , pengguna_e = false, prosenfisik_e = false, prosenbiaya_e = false
              , kdtanah_e = false
               , spesifikasi_o = false
               , jenis_o = false
               , ukuran_o = false
               , ket_o = false; 

            if (Kdtans == "102" || Kdtans == "103" || Kdtans == "107")
            {
                tahun_e = true;
            }

            if (Kdkib == "00")
            {
                kdpemilik_e = true;
                asalusul_e = true;
                ket_o = true;
                kdkon_e = true;
                //pengguna_e = true;
                //judul_e = true;
                //pencipta_e = true;
                spesifikasi_o = true;
                jenis_o = true;
                ukuran_o = true;
                kdsatuan_e = true;

                Pengguna = null;
                Judul = null;
                Pencipta = null;
                Nofikat = null;
                Kdfisik = null;
                Kdhak = null;
                Kdwarna = null;
                Merktype = null;
                Bahan = null;
                Nomesin = null;
                Nopabrik = null;
                Nopolisi = null;
                Norangka = null;
                Nobpkb = null;
                Nodokgdg = null;
                Nodokjij = null;
                Konstruksi = null;
                Bertingkat = null;
                Beton = null;
                Nodokkdp = null;
                Bkpencipta = null;
                Jdlpenerbit = null;
                Asaldaerah = null;
                Nokdtanah = null;
                Alamat = null;
            }
            if (Kdkib == "01")
            {
                kdpemilik_e = true;
                asalusul_e = true;
                //pengguna_e = true;
                ket_e = true;
                kdhak_e = true;
                luastnh_e = true;
                alamat_e = true;
                nofikat_e = true;
                tgfikat_e = true;
                kdsatuan_e = true;

                Pengguna = null;
                Kdkon = null;
                Merktype = null;
                Ukuran = null;
                Bahan = null;
                Nomesin = null;
                Nopabrik = null;
                Nopolisi = null;
                Norangka = null;
                Nobpkb = null;
                Kdwarna = null;
                Nodokgdg = null;
                Nodokjij = null;
                Nodokkdp = null;
                Konstruksi = null;
                Bertingkat = null;
                Beton = null;
                Jdlpenerbit = null;
                Bkpencipta = null;
                Pencipta = null;
                Spesifikasi = null;
                Asaldaerah = null;
                Jenis = null;
                Judul = null;
                Kdfisik = null;
                Nokdtanah = null;
            }
            if (Kdkib == "02")
            {
                kdpemilik_e = true;
                asalusul_e = true;
                //pengguna_e = true;
                ket_e = true;
                kdkon_e = true;
                merktype_e = true;
                ukuran_e = true;
                bahan_e = true;
                kdwarna_e = true;
                nopabrik_e = true;
                norangka_e = true;
                nopolisi_e = true;
                nobpkb_e = true;
                nomesin_e = true;
                kdsatuan_e = true;

                Pengguna = null;
                Kdhak = null;
                Nofikat = null;
                Nodokgdg = null;
                Nodokjij = null;
                Nodokkdp = null;
                Konstruksi = null;
                Bertingkat = null;
                Beton = null;
                Jdlpenerbit = null;
                Bkpencipta = null;
                Pencipta = null;
                Spesifikasi = null;
                Asaldaerah = null;
                Jenis = null;
                Judul = null;
                Kdfisik = null;
                Nokdtanah = null;
                Alamat = null;
            }
            if (Kdkib == "03")
            {
                kdpemilik_e = true;
                asalusul_e = true;
                //pengguna_e = true;
                ket_e = true;
                kdkon_e = true;
                kdhak_e = true;
                bertingkat_e = true;
                beton_e = true;
                luaslt_e = true;
                alamat_e = true;
                nodokgdg_e = true;
                tgdokgdg_e = true;
                luastnh_e = true;
                kdtanah_e = true;
                kdsatuan_e = true;

                Pengguna = null;
                Nofikat = null;
                Kdfisik = null;
                Kdwarna = null;
                Merktype = null;
                Ukuran = null;
                Bahan = null;
                Nomesin = null;
                Nopabrik = null;
                Nopolisi = null;
                Norangka = null;
                Nobpkb = null;
                Nodokjij = null;
                Konstruksi = null;
                Nodokkdp = null;
                Bkpencipta = null;
                Jdlpenerbit = null;
                Jenis = null;
                Spesifikasi = null;
                Pencipta = null;
                Asaldaerah = null;
                Judul = null;
            }
            if (Kdkib == "04")
            {
                kdpemilik_e = true;
                asalusul_e = true;
                ket_e = true;
                kdkon_e = true;
                kdhak_e = true;
                konstruksi_e = true;
                panjang_e = true;
                lebar_e = true;
                luas_e = true;
                alamat_e = true;
                nodokjij_e = true;
                tgdokjij_e = true;
                kdtanah_e = true;
                kdsatuan_e = true;

                Pengguna = null;
                Nofikat = null;
                Kdfisik = null;
                Kdwarna = null;
                Merktype = null;
                Ukuran = null;
                Bahan = null;
                Nomesin = null;
                Nopabrik = null;
                Nopolisi = null;
                Norangka = null;
                Nobpkb = null;
                Nodokgdg = null;
                Bertingkat = null;
                Beton = null;
                Nodokkdp = null;
                Bkpencipta = null;
                Jdlpenerbit = null;
                Jenis = null;
                Spesifikasi = null;
                Pencipta = null;
                Asaldaerah = null;
                Judul = null;
            }
            if (Kdkib == "05")
            {
                kdpemilik_e = true;
                asalusul_e = true;
                ket_e = true;
                kdkon_e = true;
                jdlpenerbit_e = true;
                bkpencipta_e = true;
                spesifikasi_e = true;
                asaldaerah_e = true;
                pencipta_e = true;
                bahan_e = true;
                jenis_e = true;
                ukuran_e = true;
                kdsatuan_e = true;

                Pengguna = null;
                Nofikat = null;
                Kdfisik = null;
                Kdhak = null;
                Kdwarna = null;
                Merktype = null;
                Nomesin = null;
                Nopabrik = null;
                Nopolisi = null;
                Norangka = null;
                Nobpkb = null;
                Nodokgdg = null;
                Nodokjij = null;
                Konstruksi = null;
                Bertingkat = null;
                Beton = null;
                Nodokkdp = null;
                Judul = null;
                Nokdtanah = null;
                Alamat = null;
            }
            if (Kdkib == "06")
            {
                kdpemilik_e = true;
                asalusul_e = true;
                ket_e = true;
                kdhak_e = true;
                kdfisik_e = true;
                bertingkat_e = true;
                beton_e = true;
                luaslt_e = true;
                alamat_e = true;
                nodokkdp_e = true;
                tgdokkdp_e = true;
                tgmulai_e = true;
                kdtanah_e = true;
                kdsatuan_e = true;
                prosenfisik_e = true;
                prosenbiaya_e = true;

                Pengguna = null;
                Nofikat = null;
                Kdkon = null;
                Kdwarna = null;
                Merktype = null;
                Ukuran = null;
                Bahan = null;
                Nomesin = null;
                Nopabrik = null;
                Nopolisi = null;
                Norangka = null;
                Nobpkb = null;
                Nodokgdg = null;
                Nodokjij = null;
                Konstruksi = null;
                Bkpencipta = null;
                Jdlpenerbit = null;
                Jenis = null;
                Spesifikasi = null;
                Pencipta = null;
                Asaldaerah = null;
                Judul = null;
            }
            if (Kdkib == "07")
            {
                kdpemilik_e = true;
                asalusul_e = true;
                ket_e = true;
                kdkon_e = true;
                //pengguna_e = true;
                judul_e = true;
                pencipta_e = true;
                spesifikasi_e = true;
                jenis_e = true;
                ukuran_e = true;
                kdsatuan_e = true;

                Pengguna = null;
                Nofikat = null;
                Kdfisik = null;
                Kdhak = null;
                Kdwarna = null;
                Merktype = null;
                Bahan = null;
                Nomesin = null;
                Nopabrik = null;
                Nopolisi = null;
                Norangka = null;
                Nobpkb = null;
                Nodokgdg = null;
                Nodokjij = null;
                Konstruksi = null;
                Bertingkat = null;
                Beton = null;
                Nodokkdp = null;
                Bkpencipta = null;
                Jdlpenerbit = null;
                Asaldaerah = null;
                Nokdtanah = null;
                Alamat = null;
            }

            BeritadetbrgControl cBeritadetbrgGetkdtanah = new BeritadetbrgControl();
            cBeritadetbrgGetkdtanah.Nokdtanah = Nokdtanah;
            cBeritadetbrgGetkdtanah.Load("Kdtanah");
            Nmkdtanah = cBeritadetbrgGetkdtanah.Nmkdtanah;

            //kib all group 1
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Tahun=Tahun Perolehan"),
            GetList(new TahunLookupControl()), "Kdtahun=Nmtahun", 50).SetEnable(tahun_e).SetAllowEmpty(false).SetGroup(GROUP_1));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdpemilik=Pemilik"),
            GetList(new JmilikLookupControl()), "Kdpemilik=Nmpemilik", 50).SetEnable(enable).SetAllowEmpty(false)
              .SetVisible(kdpemilik_e).SetGroup(GROUP_1));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdhak=Hak"),
            GetList(new JhakLookupControl()), "Kdhak=Nmhak", 50).SetEnable(enable).SetAllowEmpty(false)
              .SetVisible(kdhak_e).SetGroup(GROUP_1));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdsatuan=Satuan"),
            GetList(new SatuanLookupControl()), "Kdsatuan=Nmsatuan", 50).SetEnable(enable).SetAllowEmpty(false)
              .SetVisible(kdsatuan_e).SetGroup(GROUP_1));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdkon=Kondisi"),
            GetList(new KonasetLookupControl()), "Kdkon=Nmkon", 50).SetEnable(enable).SetAllowEmpty(false)
              .SetVisible(kdkon_e).SetGroup(GROUP_1));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdwarna=Warna"),
            GetList(new WarnaLookupControl()), "Kdwarna=Nmwarna", 50).SetEnable(enable).SetAllowEmpty(false)
              .SetVisible(kdwarna_e).SetGroup(GROUP_1));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdfisik=Fisik"),
            GetList(new JfisikLookupControl()), "Kdfisik=Nmfisik", 50).SetEnable(enable).SetAllowEmpty(false)
              .SetVisible(kdfisik_e).SetGroup(GROUP_1));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Asalusul=Asal Usul"), true, 90).SetEnable(enable)
              .SetVisible(asalusul_e).SetGroup(GROUP_1));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Pengguna"), true, 90).SetEnable(enable)
              .SetVisible(pengguna_e).SetGroup(GROUP_1));

            //kiba
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nofikat=Nomor Sertifikat"), true, 50).SetEnable(enable)
              .SetVisible(nofikat_e).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tgfikat=Tanggal Sertifikat"), true).SetEnable(enable)
              .SetVisible(tgfikat_e).SetGroup(GROUP_2));

            //kibb
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Merktype=Merk/Type"), true, 50).SetEnable(enable)
              .SetVisible(merktype_e).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nopabrik=No. Pabrik"), true, 50).SetEnable(enable)
              .SetVisible(nopabrik_e).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nomesin=No. Mesin"), true, 50).SetEnable(enable)
              .SetVisible(nomesin_e).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Norangka=No. Rangka"), true, 50).SetEnable(enable)
              .SetVisible(norangka_e).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nopolisi=No. Polisi"), true, 50).SetEnable(enable)
              .SetVisible(nopolisi_e).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nobpkb=No. BPKB"), true, 50).SetEnable(enable)
              .SetVisible(nobpkb_e).SetGroup(GROUP_2));

            //kibc
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nodokgdg=Nomor Dokumen"), true, 50).SetEnable(enable)
              .SetVisible(nodokgdg_e).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tgdokgdg=Tanggal Dokumen"), true).SetEnable(enable)
              .SetVisible(tgdokgdg_e).SetGroup(GROUP_2));

            //kibd
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nodokjij=Nomor Dokumen"), true, 50).SetEnable(enable)
              .SetVisible(nodokjij_e).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tgdokjij=Tanggal Dokumen"), true).SetEnable(enable)
              .SetVisible(tgdokjij_e).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Konstruksi"), true, 50).SetEnable(enable)
              .SetVisible(konstruksi_e).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Panjang"), true, 50).SetEnable(enable)
              .SetVisible(panjang_e).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Lebar"), true, 50).SetEnable(enable)
              .SetVisible(lebar_e).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Luas"), true, 50).SetEnable(enable)
              .SetVisible(luas_e).SetGroup(GROUP_2));

            //kibe
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Jdlpenerbit=Buku Judul/Penerbit"), true, 90).SetEnable(enable)
              .SetVisible(jdlpenerbit_e).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Bkpencipta=Buku Pencipta"), true, 90).SetEnable(enable)
              .SetVisible(bkpencipta_e).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Asaldaerah=Asal Daerah"), true, 90).SetEnable(enable)
              .SetVisible(asaldaerah_e).SetGroup(GROUP_2));

            //kibf
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nodokkdp=Nomor Dok KDP"), true, 50).SetEnable(enable)
              .SetVisible(nodokkdp_e).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tgdokkdp=Tanggal Dok KDP"), true).SetEnable(enable)
              .SetVisible(tgdokkdp_e).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tgmulai=Tanggal Mulai"), true).SetEnable(enable)
              .SetVisible(tgmulai_e).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Prosenfisik=Prosentase Fisik %"), true, 20).SetEnable(enable)
              .SetVisible(prosenfisik_e).SetGroup(GROUP_3));
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Prosenbiaya=Prosentase Biaya %"), true, 20).SetEnable(enable)
              .SetVisible(prosenbiaya_e).SetGroup(GROUP_3));

            //kibg
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Judul"), true, 90).SetEnable(enable)
              .SetVisible(judul_e).SetGroup(GROUP_2));

            // kib all group 2
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Luastnh=Luas Tanah"), true, 50).SetEnable(enable)
              .SetVisible(luastnh_e).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowNumeric(this, ConstantDict.GetColumnTitle("Luaslt=Luas Lantai"), true, 50).SetEnable(enable)
              .SetVisible(luaslt_e).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Pencipta"), true, 90).SetEnable(enable)
              .SetVisible(pencipta_e).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Spesifikasi"), true, 90).SetEnable(enable)
              .SetVisible(spesifikasi_e).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Spesifikasi"), true, 90).SetEnable(enable)
           .SetVisible(spesifikasi_o).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Jenis"), true, 50).SetEnable(enable)
             .SetVisible(jenis_o).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Ukuran"), true, 50).SetEnable(enable)
              .SetVisible(ukuran_o).SetGroup(GROUP_2));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true)
              .SetVisible(ket_o).SetGroup(GROUP_2));

            hpars.Add(KdtanahLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetAllowEmpty(true)
              .SetVisible(kdtanah_e).SetGroup(GROUP_2));
            ArrayList listBertingkat = new ArrayList(new ParamControl[] {
        new ParamControl() {  Kdpar="1",Nmpar="Bertingkat "}
        ,new ParamControl() { Kdpar="0",Nmpar="Tidak "}
      });
            hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Bertingkat=Bertingkat"), ParameterRow.MODE_TYPE,
              listBertingkat, "Kdpar=Nmpar", 70).SetEnable(enable).SetEditable(enable).SetVisible(bertingkat_e).SetGroup(GROUP_2));

            ArrayList listBeton = new ArrayList(new ParamControl[] {
        new ParamControl() {  Kdpar="1",Nmpar="Beton "}
        ,new ParamControl() { Kdpar="0",Nmpar="Tidak "}
      });
            hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Beton=Beton"), ParameterRow.MODE_TYPE,
              listBeton, "Kdpar=Nmpar", 70).SetEnable(enable).SetEditable(enable).SetVisible(beton_e).SetGroup(GROUP_2));

            //kib all group 3
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Bahan"), true, 50).SetEnable(enable)
              .SetVisible(bahan_e).SetGroup(GROUP_3));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Jenis"), true, 50).SetEnable(enable)
              .SetVisible(jenis_e).SetGroup(GROUP_3));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Ukuran"), true, 50).SetEnable(enable)
              .SetVisible(ukuran_e).SetGroup(GROUP_3));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Alamat"), true, 3).SetEnable(enable).SetAllowEmpty(true)
              .SetVisible(alamat_e).SetGroup(GROUP_3));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 3).SetEnable(enable).SetAllowEmpty(true)
              .SetVisible(ket_e).SetGroup(GROUP_3));
            hpars.Add(new ParameterRowUploadFile(this, true));
            return hpars;

        }
        public new int Update()
        {
            PemdaControl cPemda = new PemdaControl();
            cPemda.Configid = "cur_thang";
            cPemda.Load("PK");

            if (Tahun > Int32.Parse(cPemda.Configval.Trim()))
            {
                throw new Exception("Gagal menyimpan data: tahun perolehan tidak boleh melebihi tahun anggaran berjalan");
            }
            if (Prosenfisik > 100)
            {
                throw new Exception("Gagal menyimpan data : Prosentase fisik tidak boleh melebihi 100%");
            }
            if (Prosenbiaya > 100)
            {
                throw new Exception("Gagal menyimpan data : Prosentase biaya tidak boleh melebihi 100%");
            }

            int n = 0;
            try
            {
                n = ((BaseDataControlUI)this).Update("Edit");
            }
            catch (Exception ex)
            {

                if (ex.Message.Contains("FK_ASET_BERITADETBRG_JMILIK"))
                {
                    string msg = "Gagal menyimpan data : kolom PEMILIK tidak boleh kosong";
                    msg = string.Format(msg);
                    throw new Exception(msg);
                }
                else if (ex.Message.Contains("FK_ASET_BERITADETBRG_JHAK"))
                {
                    string msg = "Gagal menyimpan data : kolom HAK tidak boleh kosong";
                    msg = string.Format(msg);
                    throw new Exception(msg);
                }
                else if (ex.Message.Contains("FK_ASET_BERITADETBRG_SATUAN"))
                {
                    string msg = "Gagal menyimpan data : kolom SATUAN tidak boleh kosong";
                    msg = string.Format(msg);
                    throw new Exception(msg);
                }
                else if (ex.Message.Contains("FK_ASET_BERITADETBRG_KONASET"))
                {
                    string msg = "Gagal menyimpan data : kolom KONDISI tidak boleh kosong";
                    msg = string.Format(msg);
                    throw new Exception(msg);
                }
                else if (ex.Message.Contains("FK_ASET_BERITADETBRG_JFISIK"))
                {
                    string msg = "Gagal menyimpan data : kolom FISIK tidak boleh kosong";
                    msg = string.Format(msg);
                    throw new Exception(msg);
                }
                else if (ex.Message.Contains("FK_ASET_BERITADETBRG_WARNA"))
                {
                    string msg = "Gagal menyimpan data : kolom WARNA tidak boleh kosong";
                    msg = string.Format(msg);
                    throw new Exception(msg);
                }
            }
            return n;
        }
    }
    #endregion BeritadetbrgEdit

    #region BeritadetbrgEditlainnya
    [Serializable]
    public class BeritadetbrgEditlainnyaControl : BeritadetbrgEditControl, IDataControlUIEntry
    {
        public new void SetFilterKey(BaseBO bo)
        {
            if (typeof(IDataControlMenu).IsInstanceOfType(bo))
            {
                Last_by = ((BaseBO)bo).Userid;
            }
            else if (typeof(BeritadetbrgLainnyaControl).IsInstanceOfType(bo))
            {
                Unitkey = ((BeritadetbrgLainnyaControl)bo).Unitkey;
                Noba = ((BeritadetbrgLainnyaControl)bo).Noba;
                Asetkey = ((BeritadetbrgLainnyaControl)bo).Asetkey;
                Urutbrg = ((BeritadetbrgLainnyaControl)bo).Urutbrg;
                Kdkib = ((BeritadetbrgLainnyaControl)bo).Kdkib;
                Kdtans = ((BeritadetbrgLainnyaControl)bo).Kdtans;
                Tglba = ((BeritadetbrgLainnyaControl)bo).Tglba;
                Tglvalid = ((BeritadetbrgLainnyaControl)bo).Tglvalid;
                Blokid = ((BeritadetbrgLainnyaControl)bo).Blokid;
            }
        }
    }
    #endregion BeritadetbrglainnyaEdit
}

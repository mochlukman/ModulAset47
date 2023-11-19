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

    #region BeritaBakf
    [Serializable]
    public class BeritaBakfControl : BeritaControl, IDataControlUIEntry, IHasJSScript
    {
        #region Methods 
        public new IList View()
        {
            IList list = this.View("Bakf");
            return list;
        }

        //public new void SetPageKey()
        //{
        //    Kdtans = "101";
        //}
        public new void SetPrimaryKey()
        {
            PemdaControl cPemda = new PemdaControl();
            cPemda.Configid = "cur_thang";
            cPemda.Load("PK");

            KegunitControl cKegunit = new KegunitControl();
            cKegunit.Unitkey = Unitkey;
            cKegunit.Kdkegunit = Kdkegunit;
            cKegunit.Kdtahap = Kdtahap;
            cKegunit.Thang = cPemda.Configval.Trim();
            cKegunit.Load("PK");

            Idprgrm = cKegunit.Idprgrm.Trim();
            Tahunsa = (Int32.Parse(cPemda.Configval));
            Tglsa = new DateTime(Tahunsa, DateTime.Today.Month, DateTime.Today.Day);
            Tglba = Tglsa;
            Tglbap = Tglsa;
            Kdbukti = "03";
        }
        public new HashTableofParameterRow GetFilters()
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdtahap=Tahap"),
            GetList(new TahapLookupControl()), "Kdtahap=Uraian", 41).SetAllowRefresh(true).SetEnable(enableFilter).SetAllowEmpty(false));
            hpars.Add(KegunitBastLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(false).SetAllowEmpty(false));
            return hpars;
        }
        public override HashTableofParameterRow GetEntries()
        {
            bool enable = !Valid, enableValid = false;

            BeritaControl cBeritaCekjmlrek = new BeritaControl();
            cBeritaCekjmlrek.Unitkey = Unitkey;
            cBeritaCekjmlrek.Noba = Noba;
            cBeritaCekjmlrek.Load("Jmlrek");
            Jmlrek = cBeritaCekjmlrek.Jmlrek;

            BeritaControl cBeritaCekjmlbrg = new BeritaControl();
            cBeritaCekjmlbrg.Unitkey = Unitkey;
            cBeritaCekjmlbrg.Noba = Noba;
            cBeritaCekjmlbrg.Load("Jmlbrg");
            Jmlbrg = cBeritaCekjmlbrg.Jmlbrg;

            BeritaControl cBeritaCekjmlgenerated = new BeritaControl();
            cBeritaCekjmlgenerated.Unitkey = Unitkey;
            cBeritaCekjmlgenerated.Noba = Noba;
            cBeritaCekjmlgenerated.Load("Jmlgenerated");
            Jmlgenerated = cBeritaCekjmlgenerated.Jmlgenerated;

            if (Kdtans == "111") //transaksi pengadaan BAKF KDP cek jumlah barang
            {
                if (Jmlbrg != 0 && (Jmlbrg - Jmlgenerated) == 0)
                {
                    enableValid = true;
                }
            }
            else //penambahan termin hanya cek rekening
            {
                if (Jmlrek != 0)
                {
                    enableValid = true;
                }
            }

            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdtans=Jenis Transaksi"),
              GetList(new JtransBakfLookupControl()), "Kdtans=Nmtrans", 50).SetEnable(enable).SetEditable(false).SetAllowEmpty(false));
            hpars.Add(KontrakLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enable).SetEditable(false).SetAllowRefresh(true).SetAllowEmpty(false));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Noba=No BAKF"), false, 50).SetEnable(enable).SetAllowEmpty(false));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglba=Tanggal BAKF"), true).SetEnable(enable).SetAllowEmpty(false));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nobap=No BAP"), true, 50).SetEnable(enable));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglbap=Tanggal BAP"), true).SetEnable(enable));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kddana=Sumber Dana"),
              GetList(new JdanaLookupControl()), "Kddana=Nmdana", 50).SetEnable(enable).SetEditable(false).SetAllowEmpty(false));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdbukti=Jenis Bukti"),
              GetList(new JbuktiLookupControl()), "Kdbukti=Nmbukti", 50).SetEnable(false).SetEditable(false).SetAllowEmpty(false));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Uraiba=Uraian BAKF"), true, 3).SetEnable(enable).SetAllowEmpty(true));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglvalid=Tanggal Valid"), true).SetEnable(enableValid).SetAllowEmpty(false).SetEditable(enable));
            hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));
            return hpars;
        }
        #endregion Methods 
    }
    #endregion BeritaBakf


}


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

    #region BeritaJanjikontrak
    [Serializable]
    public class BeritaJanjikontrakControl : BeritaControl, IDataControlUIEntry, IHasJSScript
    {
        #region Methods 
        public new IList View()
        {
            IList list = this.View("Janjikontrak");
            return list;
        }

        public new void SetPageKey()
        {
            Kdtans = "116";
            Kddana = null;
        }
        public new void SetPrimaryKey()
        {
            PemdaControl cPemda = new PemdaControl();
            cPemda.Configid = "cur_thang";
            cPemda.Load("PK");
        }
        public new HashTableofParameterRow GetFilters()
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetAllowEmpty(false));
            return hpars;
        }
        //public override HashTableofParameterRow GetEntries()
        //{
        //    bool enable = !Valid, enableValid = false;

        //    BeritaControl cBeritaCekjmlrek = new BeritaControl();
        //    cBeritaCekjmlrek.Unitkey = Unitkey;
        //    cBeritaCekjmlrek.Noba = Noba;
        //    cBeritaCekjmlrek.Load("Jmlrek");
        //    Jmlrek = cBeritaCekjmlrek.Jmlrek;

        //    BeritaControl cBeritaCekjmlbrg = new BeritaControl();
        //    cBeritaCekjmlbrg.Unitkey = Unitkey;
        //    cBeritaCekjmlbrg.Noba = Noba;
        //    cBeritaCekjmlbrg.Load("Jmlbrg");
        //    Jmlbrg = cBeritaCekjmlbrg.Jmlbrg;

        //    BeritaControl cBeritaCekjmlgenerated = new BeritaControl();
        //    cBeritaCekjmlgenerated.Unitkey = Unitkey;
        //    cBeritaCekjmlgenerated.Noba = Noba;
        //    cBeritaCekjmlgenerated.Load("Jmlgenerated");
        //    Jmlgenerated = cBeritaCekjmlgenerated.Jmlgenerated;

        //    if (Jmlrek != 0)
        //    {
        //        enableValid = true;
        //    }

        //    HashTableofParameterRow hpars = new HashTableofParameterRow();
        //    hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdtans=Jenis Transaksi"),
        //      GetList(new JtransJanjikontrakLookupControl()), "Kdtans=Nmtrans", 50).SetEnable(enable).SetEditable(false).SetAllowEmpty(false));
        //    hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Noba=No BAST"), false, 50).SetEnable(enable).SetAllowEmpty(false));
        //    hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglba=Tanggal BAST"), true).SetEnable(enable).SetAllowEmpty(false));
        //    hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nobap=No BAP"), true, 50).SetEnable(enable));
        //    hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglbap=Tanggal BAP"), true).SetEnable(enable));
        //    hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kddana=Sumber Dana"),
        //      GetList(new JdanaLookupControl()), "Kddana=Nmdana", 50).SetEnable(enable).SetEditable(false)
        //      .SetAllowEmpty(false).SetVisible(false));
        //    hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdbukti=Jenis Bukti"),
        //       GetList(new JbuktiJanjikontrakLookupControl()), "Kdbukti=Nmbukti", 50).SetEnable(true).SetEditable(false).SetAllowEmpty(false));

        //    hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Uraiba=Uraian BAST"), true, 3).SetEnable(enable).SetAllowEmpty(true));
        //    hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglvalid=Tanggal Valid"), true).SetEnable(enableValid).SetAllowEmpty(false).SetEditable(enable));
        //    hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));
        //    return hpars;
        //}
        #endregion Methods 
    }
    #endregion BeritaJanjikontrak


}


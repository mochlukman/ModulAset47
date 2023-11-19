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

    #region BeritaBlud
    [Serializable]
    public class BeritaBludControl : BeritaControl, IDataControlUIEntry, IHasJSScript
    {
        #region Methods
        public new IList View()
        {
            IList list = this.View("Blud");
            return list;
        }
        public new void SetPageKey()
        {
            Kdtans = "101";
        }
        public new void SetPrimaryKey()
        {
            PemdaControl cPemda = new PemdaControl();
            cPemda.Configid = "cur_thang";
            cPemda.Load("PK");

            Tahunsa = (Int32.Parse(cPemda.Configval));
            Tglsa = new DateTime(Tahunsa, DateTime.Today.Month, DateTime.Today.Day);
            Tglba = Tglsa;
            Tglbap = Tglsa;

            Kdtahap = null;
            Idprgrm = null;
            Kdkegunit = null;
            Nokontrak = null;
            Kddana = "03";
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
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Stricon"), typeof(CommandColumn), Cmds, 5, HorizontalAlign.Center));

            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noba=No BAST"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglba=Tanggal BAST"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglvalid=Tanggal Pengesahan"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmdana=Sumber Dana"), typeof(string), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmbukti=Jenis Bukti"), typeof(string), 25, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraiba=Uraian BAST"), typeof(string), 50, HorizontalAlign.Left));
            return columns;
        }
        public override HashTableofParameterRow GetEntries()
        {
            bool enable = !Valid, enableValid = false;//, enableEdit = true;

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

            if (Jmlbrg != 0 && (Jmlbrg - Jmlgenerated) == 0) //cek jumlah barang untuk validasi
            {
                enableValid = true;
            }

            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Noba=No BAST"), false, 50).SetEnable(enable).SetAllowEmpty(false));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglba=Tanggal BAST"), true).SetEnable(enable).SetAllowEmpty(false));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Nobap=No BAP"), true, 50).SetEnable(enable));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglbap=Tanggal BAP"), true).SetEnable(enable));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kddana=Sumber Dana"),
              GetList(new JdanaLookupControl()), "Kddana=Nmdana", 50).SetEnable(false).SetEditable(false).SetAllowEmpty(false));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitle("Kdbukti=Jenis Bukti"),
              GetList(new JbuktiBastLookupControl()), "Kdbukti=Nmbukti", 50).SetEnable(enable).SetEditable(false).SetAllowEmpty(false));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Uraiba=Uraian BAST"), true, 3).SetEnable(enable).SetAllowEmpty(true));
            hpars.Add(new ParameterRowDate(this, ConstantDict.GetColumnTitle("Tglvalid=Tanggal Valid"), true).SetEnable(enableValid).SetAllowEmpty(false).SetEditable(enable));
            hpars.Add(new ParameterRowCek(this, true).SetEnable(enableValid).SetEditable(enable));
            return hpars;
        }
        #endregion Methods
    }
    #endregion BeritaBlud
}


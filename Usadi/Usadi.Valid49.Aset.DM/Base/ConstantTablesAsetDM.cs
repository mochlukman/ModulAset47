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
    #region ConstantTablesAsetDM
    [Serializable]
    public class ConstantTablesAsetDM
    {
        public const string XMLBULAN = "Bulan";
        public const string XMLDAFTASET = "Daftaset";
        public const string XMLDAFTLOKASI = "Daftlokasi";
        public const string XMLDAFTGUDANG = "Daftgudang";
        public const string XMLDAFTDOK = "Daftdok";
        public const string XMLDAFTPHK3 = "Daftphk3";
        public const string XMLDAFTREKENING = "Daftrekening";
        public const string XMLDAFTRUANG = "Daftruang";
        public const string XMLDAFTUNIT = "Daftunit";
        public const string XMLDAFTUNITSK = "Daftunitsk";
        public const string XMLDAFTUNITSKHISTORY = "Daftunitskhistory";
        public const string XMLDAFTPENGGUNA = "Daftpengguna";
        public const string XMLDASKR = "Daskr";
        public const string XMLGOLONGAN = "Golongan";
        public const string XMLJABTTD = "Jabttd";
        public const string XMLJBANK = "Jbank";
        public const string XMLJBUKTI = "Jbukti";
        public const string XMLJLOKASI = "Jlokasi";
        public const string XMLJDANA = "Jdana";
        public const string XMLJFISIK = "Jfisik";
        public const string XMLJHAK = "Jhak";
        public const string XMLJKLAS = "Jklas";
        public const string XMLJKONTRAK = "Jkontrak";
        public const string XMLJMILIK = "Jmilik";
        public const string XMLJNSKIB = "Jnskib";
        public const string XMLJNSKIBASET = "Jnskibaset";
        public const string XMLJPOSTING = "Jposting";
        public const string XMLJTRANS = "Jtrans";
        public const string XMLJUSAHA = "Jusaha";
        public const string XMLKEGUNIT = "Kegunit";
        public const string XMLKONASET = "Konaset";
        public const string XMLMAPBLJASET = "Mapbljaset";
        public const string XMLMAPNRCASET = "Mapnrcaset";
        public const string XMLMAPSUSUTASET = "MapsusutAset";
        public const string XMLMATANGNRC = "Matangnrc";
        public const string XMLMATANGR = "Matangr";
        public const string XMLMKEGIATAN = "Mkegiatan";
        public const string XMLMPGRM = "Mpgrm";
        public const string XMLPEGAWAI = "Pegawai";
        public const string XMLPEMDA = "Pemda";
        public const string XMLPGRMUNIT = "Pgrmunit";
        public const string XMLRKBMD_MPGRM = "RkbmdMpgrm";
        public const string XMLRKBMD_MKEGIATAN = "RkbmdMkegiatan";
        public const string XMLRKBMD_PGRMUNIT = "RkbmdPgrmunit";
        public const string XMLRKBMD_KEGUNIT = "RkbmdKegunit";
        public const string XMLSATUAN = "Satuan";
        public const string XMLSATWAKTU = "Satwaktu";
        public const string XMLSKDASK = "Skdask";
        public const string XMLSTATUSMITRA = "Statusmitra";
        public const string XMLSTDRENOV = "Stdrenov";
        public const string XMLSTRUASET = "Struaset";
        public const string XMLSTRUREK = "Strurek";
        public const string XMLSTRURUANG = "Struruang";
        public const string XMLSTRUUNIT = "Struunit";
        public const string XMLSTSKIR = "Stskir";
        public const string XMLTAHAP = "Tahap";
        public const string XMLTAHUN = "Tahun";
        public const string XMLWARNA = "Warna";
        public const string XMLWEBSET = "Webset";
        public const string XMLDAFTJABATAN = "Daftjabatan";
    }
    #endregion ConstantTablesAsetDM
}
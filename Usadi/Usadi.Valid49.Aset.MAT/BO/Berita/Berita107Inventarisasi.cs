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
    #region BeritaInventarisasi
    [Serializable]
    public class BeritaInventarisasiControl : BeritaControl, IDataControlUIEntry, IHasJSScript
    {
        #region Methods 
        public new IList View()
        {
            IList list = this.View("Inventarisasi");
            return list;
        }
        public new void SetPageKey()
        {
            Kdtans = "107";
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
            Kddana = null;
            Kdbukti = "08";
        }
        #endregion Methods 
    }
    #endregion BeritaInventarisasi


}


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

  #region BeritaLainnya
  [Serializable]
  public class BeritaLainnyaControl : BeritaControl, IDataControlUIEntry, IHasJSScript
  {
        #region Methods 
        public new void SetPageKey()
        {
            Kdtans = "125"; //Perolehan/Penerimaan Lainnya
            Kdbukti = "13";
        }
        public new IList View()
        {
            IList list = this.View("Kdtans");
            return list;
        }
        public new void SetPrimaryKey()
        {
            base.SetPrimaryKey();

            Kdtahap = null;
            Idprgrm = null;
            Kdkegunit = null;
            Nokontrak = null;
            Kddana = null;
        }
        #endregion Methods 
    }
    #endregion BeritaLainnya


}


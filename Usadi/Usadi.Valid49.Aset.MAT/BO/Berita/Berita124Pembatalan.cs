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

  #region BeritaPembatalan
  [Serializable]
  public class BeritaPembatalanControl : BeritaControl, IDataControlUIEntry, IHasJSScript
  {
        #region Methods 
        public new void SetPageKey()
        {
            Kdtans = "124"; //Pembatalan Penghapusan
            Kdbukti = "12";
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
    #endregion BeritaPembatalan


}


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
 
  #region BeritaPengadaan
  [Serializable]
  public class BeritaPengadaanControl : BeritaControl, IDataControlUIEntry, IHasJSScript
  {
    #region Methods 
    public new IList View()
    {
      IList list = this.View("Pengadaan");
      return list;
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
    public new void SetPageKey()
    {
      Kdtans = "101";
    }
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
    }
    #endregion Methods 
  }
  #endregion BeritaPengadaan

  
}


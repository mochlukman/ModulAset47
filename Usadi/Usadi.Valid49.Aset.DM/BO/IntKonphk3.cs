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
  #region Usadi.Valid49.BO.IntKonphk3Control, Usadi.Valid49.Aset.DM
  [Serializable]
  public class IntKonphk3Control : BaseDataControlAsetDM, IDataControlUIEntry
  {
    #region Properties
    public long Id { get; set; }
    public string Nmtahun { get; set; }
    public string Kdtahun { get; set; }
    public string Thang { get; set; }
    #endregion Properties 

    #region Methods 
    public IntKonphk3Control()
    {
      XMLName = ConstantTablesAsetDM.XMLTAHUN;

      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      Thang = cPemda.Configval;
    }

    ViewListProperties cViewListProperties = null;
    public new IProperties GetProperties()
    {
      if (cViewListProperties == null)
      {
        cViewListProperties = (ViewListProperties)base.GetProperties();
        cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD;
        cViewListProperties.ReadOnlyFields = new string[] { };
        cViewListProperties.ModeToolbar = ViewListProperties.MODE_TOOLBAR_NORMAL;
      }
      return cViewListProperties;
    }

    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();

      return hpars;
    }
    public new void SetPrimaryKey()
    {
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = "cur_thang";
      cPemda.Load("PK");

      Thang = cPemda.Configval;
    }
    public override HashTableofParameterRow GetEntries()
    {
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Thang=Tahun Anggaran"), false, 20).SetEnable(false));
      return hpars;
    }
    public new void Insert()
    {

      string sql = @"
            exec [dbo].[WSP_GETMASTER_PHK3KONTRAK]
            @THANG = N'{0}'
            ";
      sql = string.Format(sql, Thang);
      BaseDataAdapter.ExecuteCmd(this, sql);
    }
    #endregion Methods 
  }
  #endregion IntKonphk3
}


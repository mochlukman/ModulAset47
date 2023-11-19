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
  #region Usadi.Valid49.BO.PemdaControl, Usadi.Valid49.Aset.DM
  [Serializable]
  public class PemdaControl : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript
  {
    #region Properties 
    public long Id { get; set; }
    public string Configval { get; set; }
    public string Configdes { get; set; }
    public string Configid { get; set; }
    public string Unitkey { get; set; }
    public string Asetkey { get; set; }
    public string Kdunit { get; set; }
    public string Nmunit { get; set; }
    public int Tahunsa { get; set; }
    #endregion Properties 

    #region Methods
    public const string CUR_USKPKD = "cur_uskpkd";
    public const string CUR_SKPKD = "cur_skpkd";
    public PemdaControl()
    {
      XMLName = ConstantTablesAsetDM.XMLPEMDA;
      #region COR-49 Refactor Get Tahun dari Setting DB - STEP 2
      Ss10userControl dcUser = (Ss10userControl)GlobalAsp.GetSessionUser();
      Tahun = dcUser.Tahun;
      Tahunsa = dcUser.Tahun;
      #endregion
    }
    public ImageCommand[] Cmds
    {
      get
      {
        ImageCommand cmd1 = new ImageCommand()
        {
          //CommandName = "ViewTransaksi",
          Icon = Icon.Pencil
        };
        cmd1.ToolTip.Text = "Klik Untuk Edit";
        return new ImageCommand[] { cmd1 };
      }
    }


    public new IProperties GetProperties()
    {
      ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
      cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
      cViewListProperties.PrimaryKeys = new String[] { "Configid" };
      cViewListProperties.IDKey = "Id";
      cViewListProperties.IDProperty = "Id";
      cViewListProperties.SortFields = new String[] { "Configid" };
      cViewListProperties.ReadOnlyFields = new String[] { "Tahunsa", "Tahun" };
      cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
      cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_EDIT;

      return cViewListProperties;
    }
    public override DataControlFieldCollection GetColumns()
    {
      DataControlFieldCollection columns = new DataControlFieldCollection();
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Configdes=Uraian"), typeof(string), 50, HorizontalAlign.Left));
      columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Configval=Nilai"), typeof(string), 70, HorizontalAlign.Left));

      return columns;
    }

    public new void SetFilterKey(BaseBO bo)
    {
      if (typeof(IDataControlMenu).IsInstanceOfType(bo))
      {
        Last_by = ((BaseBO)bo).Userid;
      }
    }

    public static string GetConfigVal(string configid)
    {
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = configid;
      cPemda.Load(BaseDataControl.PK);
      return cPemda.Configval;
    }
    public static string GetConfigDes(string configid)
    {
      PemdaControl cPemda = new PemdaControl();
      cPemda.Configid = configid;
      cPemda.Load(BaseDataControl.PK);
      return cPemda.Configdes;
    }
    public new HashTableofParameterRow GetFilters()
    {
      bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      return hpars;
    }
    public new IList View()
    {
      IList list = this.View(BaseDataControl.ALL);
      return list;
    }
    public new IList View(string label)
    {
      IList list = ((BaseDataControl)this).View(label);
      List<PemdaControl> ListData = new List<PemdaControl>();
      foreach (PemdaControl dc in list)
      {
        ListData.Add(dc);
      }
      //Update(ListData);
      return ListData;
    }
    public override HashTableofParameterRow GetEntries()
    {
      HashTableofParameterRow hpars = new HashTableofParameterRow();
      bool enable = true, cur_skpd_e = false, configid_e = false, configval = false, configdes_e = false;

      GetConfigVal(Configid);
      {        
        configid_e = true;
        cur_skpd_e = true;
        configdes_e = true;        
      }

      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Configid"), true, 50).SetEnable(false).SetVisible(configid_e));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Configdes"), true, 50).SetEnable(enable).SetVisible(configdes_e));
      hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Configval"), true, 70).SetEnable(enable).SetVisible(configval));

      hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetAllowEmpty(true)
        .SetVisible(cur_skpd_e));
      hpars.Add(DaftasetLookupControl.Instance.GetLookupParameterRow(this, false).SetAllowRefresh(true).SetEnable(enable).SetAllowEmpty(true)
        );

      return hpars;
    }
    #endregion Methods 
  }
  #endregion Pemda
}


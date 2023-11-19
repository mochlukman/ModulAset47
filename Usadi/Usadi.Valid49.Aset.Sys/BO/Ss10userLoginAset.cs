using CoreNET.Common.Base;
using CoreNET.Common.BO;
using System;
using System.Collections.Generic;
using System.Configuration;


namespace Usadi.Valid49.BO
{
    #region Usadi.Valid49.BO.Ss10userLoginAsetControl, Usadi.Valid49.Aset.Sys;
    [Serializable]
  public class Ss10userLoginAsetControl : Ss10userLoginControl, IDataControlAppuserUI
  {
    private static Ss10userLoginAsetControl _Instance = null;
    public static Ss10userLoginAsetControl Instance
    {
      get
      {
        if (_Instance == null)
        {
          _Instance = new Ss10userLoginAsetControl();
        }
        return _Instance;
      }
    }
    //public string Unitkey { get; set; }
    public string Kdtahap { get; set; }
    public string SuffixDB { get; set; }
    public Ss10userLoginAsetControl()
    {
      XMLName = ConstantTablesAsetSys.XMLUSERUNIT;
    }

    public new bool IsAdmin()
    {
      return (Usertype.Equals(MasterAppConstants.STR_ADMIN));
    }

    public new string GetUserDebugInfo()
    {
      //string kdmenu = GlobalExt.GetSessionCurrentRoleid();
      //string strModeEditableFromGroup = string.Empty;
      //if (GlobalExt.GetSessionGroup() != null)
      //{
      //  strModeEditableFromGroup = ((SswebgroupControl)GlobalExt.GetSessionGroup()).GetGroupConfigs("MODE_EDITABLE");
      //}
      //int modeEditablefromGroup = -1;
      //if (!string.IsNullOrEmpty(strModeEditableFromGroup))
      //{
      //  modeEditablefromGroup = int.Parse(strModeEditableFromGroup);
      //}
      //int modeEditablefromUser = ((Ss10userControl)GlobalExt.GetSessionUser()).ModeEditable;

      //int modeEditablefromMenu = -1;
      //if (GlobalExt.GetSessionMenu() != null)
      //{
      //  modeEditablefromMenu = ((SswebmenuControl)GlobalExt.GetSessionMenu()).Modeeditable;
      //}

      //string info = @"
      //  Kdpemda = " + Kdpemda + @"<br/>
      //  Tahun = " + AppUtils.GetCurrentYear() + @"<br/>
      //  Kdunit = " + Kdunit + @"<br/>
      //  Nip = " + Nip + @"<br/>
      //  /* ModeEditable, urutan prioritas ValidUnit,User,Group,Menu (klo tidak -1) */<br/>
      //  modeEditablefromMenu = " + modeEditablefromMenu + @"<br/>
      //  modeEditablefromGroup = " + modeEditablefromGroup + @"<br/>
      //  modeEditablefromUser = " + modeEditablefromUser + @"<br/>
      //  FILTER_DOK = " + AppUtils.GetFilterKddok() + @"<br/>
      //  DEFAULT_DOK = " + Nodok + @"<br/>
      //";

      return string.Empty;
    }

    public new string GetUserID()
    {
      return Userid;
    }
    public new string GetGroupingDataID()
    {
      return Kdpemda;
    }
    public new string GetUserPwd()
    {
      return Userpwd;
    }
    public new string GetUserType()
    {
      return Usertype;
    }

    public new string GetLabelUser() { return "User ID = " + Userid; }
    public new string GetLabelNip() { return "NIP = " + Usernip; }
    public new string GetLabelNama() { return "Nama = " + Usernama; }
    public new string GetLabelEmail() { return "Email = " + Useremail; }
    public new string GetLabelNomor()
    {
      return null; // "Nomor =" + Nodok;
    }
    public new string GetLabelUraian() { return "Uraian = " + Userket; }
    public new string GetLabelRole()
    {
      return null;// "Role=" + Uturaian; 
    }
    public new string GetLabelIP() { return "IP = " + UtilityUI.GetIPAddress(); }

    public new object GetValueProperty(string par)
    {
      //throw new NotImplementedException();
      return null;
    }

    public int GetTahun()
    {
      return Tahun;
    }
    public string GetPemda()
    {
      return Kdpemda;
    }
    public string GetDBName()
    {
      return DBName;
    }
    public string GetKdtahap()
    {
      return Kdtahap;
    }

    public new bool IsEnableLookup()
    {
      return true;
    }
    public new bool IsVisibleLookup()
    {
      return true;
    }
    public new string GetAppName(string idapp)
    {
      Ss00appControl dc = new Ss00appControl
      {
        Idapp = idapp
      };
      return dc.Nmapp;
    }



    public new void SetUserID(string appID, string userID)
    {
      Idapp = appID;
      Userid = userID;
    }

    public new void SetUserOL(string userID, string userkey)
    {
      Ss11userolControl dc = new Ss11userolControl
      {
        Idol = Guid.NewGuid().ToString(),
        Userid = userID,
        Userkey = userkey,
        Last_by = userID,
        Last_date = DateTime.Now
      };
      ((BaseDataControl)dc).Insert();
    }

    public new void UpdatePwd(string pwd)
    {
      Ss10userControl dc = new Ss10userControl();
      dc.Userid = Userid;
      dc.UpdatePwd(pwd);
    }


    public new BaseBO Load()
    {
      DBName = ConfigurationManager.AppSettings["DataSourceAset"];
      Idapp = GlobalAsp.GetSessionApp();

      string sql = @"
      select
      rtrim(A.USERID) as USERID, rtrim(A.KDPEMDA) as KDPEMDA,E.NMPEMDA
      , USERTYPE,USERNAMA,USERPWD,USERBLOCK,USERNIP,USERHP,USEREMAIL,USERNO,USERURAIAN,USERSTATUS,USERKET,IDROLE
      , rtrim(W.UNITKEY) as UNITKEY
      , rtrim(W.KDTAHAP) as KDTAHAP
      , rtrim(C.KDUNIT) as KDUNIT
      , rtrim(C.NMUNIT) as NMUNIT
      , rtrim(D.URAIAN) as URAIAN
      from SS10USER A
      left join {0}.dbo.WEBUSER W on A.USERID=W.USERID COLLATE DATABASE_DEFAULT
      left join SS10USERAPP UA on A.USERID=UA.USERID          
      left join SS00APP S on UA.IDAPP=S.IDAPP 
      left join {0}.dbo.DAFTUNIT C on W.UNITKEY=C.UNITKEY COLLATE DATABASE_DEFAULT
      left join {0}.dbo.TAHAP D on W.KDTAHAP=D.KDTAHAP COLLATE DATABASE_DEFAULT
      left join DMPEMDA E on A.KDPEMDA=E.KDPEMDA
      where rtrim(A.USERID)='{1}' and UA.IDAPP='{2}'
      ";
      sql = string.Format(sql, DBName, Userid, Idapp);
      string[] fields = new string[] { "Userid","Kdpemda","Nmpemda","Usertype", "Usernama", "Userpwd"
        , "Userblock", "Usernip", "Userhp", "Useremail", "Userno", "Useruraian"
        , "Userstatus", "Userket", "Idrole","Unitkey","Kdtahap" ,"Kdunit","Nmunit", "Uraian"};
      List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
      List<Ss10userLoginControl> ListData = list.ConvertAll(new Converter<IDataControl, Ss10userLoginControl>(delegate (IDataControl par) { return (Ss10userLoginControl)par; }));


      if (ListData.Count > 0)
      {
        CopyPropertyBOFrom(ListData[0]);
        return this;
      }
      else
      {
        return null;
      }
    }

  }
  #endregion SsuserPmuserLogin
}


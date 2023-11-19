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
using System.Configuration;

namespace Usadi.Valid49.BO
{
    #region Usadi.Valid49.BO.WebuserControl, Usadi.Valid49.Aset.Sys
    [Serializable]
    public class WebuserControl : BaseDataControlAsetSys, IDataControlUIEntry, IHasJSScript
    {
        #region Properties 
        public new string Idapp { get; set; }
        public string Uraian { get; set; }
        public string Unitkey { get; set; }
        public string Nmunit { get; set; }
        public string Kdunit { get; set; }
        public string Kdnmunit { get; set; }
        public string Kdtahap { get; set; }
        public string Nip { get; set; }
        public string Pwd { get; set; }
        public string Nama { get; set; }
        public string Email { get; set; }
        public string Blokid { get; set; }
        public string Userblok { get; set; }
        public bool Transecure { get; set; }
        public bool Stinsert { get; set; }
        public bool Stupdate { get; set; }
        public bool Stdelete { get; set; }
        public string Kdpemda { get; set; }
        public new string Ket { get; set; }
        public new string Userid { get; set; }
        public string Nmpemda { get; set; }
        public string Namauser { get; set; }
        public string Usernip { get; set; }
        public string Usertype { get; set; }
        public string Uturaian { get; set; }
        public string Kdgroup { get; set; }
        public string Nmgroup { get; set; }
        #endregion Properties 

        #region Methods 
        public WebuserControl()
        {
            XMLName = ConstantTablesAsetSys.XMLWEBUSER;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = "Userid";//ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Userid" };
            cViewListProperties.IDKey = "Userid";//IDKey for ID Notes
            cViewListProperties.IDProperty = "Userid";//UniqueKey in gridview
            cViewListProperties.ReadOnlyFields = new String[] { "Idapp" };
            cViewListProperties.SortFields = new String[] { "Userid" };//
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_EDIT_DEL;
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
            cViewListProperties.AllowMultiDelete = true;
            return cViewListProperties;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle(""), typeof(string), EditCmd, 5, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Userid=UserID"), typeof(string), 20, HorizontalAlign.Left).SetEditable(false));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Pwd=Password"), typeof(string), 30, HorizontalAlign.Left).SetEditable(false));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdnmunit=SKPD"), typeof(string), 50, HorizontalAlign.Left).SetEditable(false));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmgroup=Kelompok"), typeof(string), 30, HorizontalAlign.Left).SetEditable(false));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uturaian=Tipe User"), typeof(string), 30, HorizontalAlign.Left).SetEditable(false));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nama"), typeof(string), 30, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nip=NIP"), typeof(string), 30, HorizontalAlign.Left).SetEditable(true));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Email"), typeof(string), 30, HorizontalAlign.Left).SetEditable(false));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 50, HorizontalAlign.Left).SetEditable(true));
            return columns;
        }
        public HttpContext GetAdapter()
        {
            return HttpContext.Current;
        }
        public void Insertss01user()
        {
            try
            {
                Ss10userControl user = new Ss10userControl();
                user.Userid = Userid;


                var returnuser = user.Load(BaseDataControl.PK);

                if (returnuser == null)
                {
                    user.Kdpemda = Kdpemda;
                    user.Usertype = Usertype;
                    user.Usernama = Namauser;
                    user.Userblock = Blokid;
                    user.Usernip = Nip;
                    user.Useremail = Email;
                    user.Useruraian = UtilityBO.GetRandomPassword(user.Userid);
                    ((BaseDataControl)user).Insert();
                    user.Userpwd = UtilityBO.GetMD5HexStringForStorePwd(user.Useruraian);
                    user.Userket = GlobalAsp.GetHashData(user);
                    user.UpdatePwd(user.Userpwd);
                }
                else
                {
                    user.Kdpemda = Kdpemda;
                    user.Usertype = Usertype;
                    user.Usernama = Namauser;
                    user.Userblock = Blokid;
                    user.Usernip = Nip;
                    user.Useremail = Email;
                    user.Update();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Insertuserapp()
        {
            try
            {
                Ss10userappControl userapp = new Ss10userappControl();
                userapp.Userid = Userid;
                userapp.Idapp = Idapp;

                userapp.Insert();
            }
            catch { }
        }

        public void Insertusergroup()
        {
            try
            {
                Ss20groupuserControl usergroup = new Ss20groupuserControl();
                usergroup.Userid = Userid;
                usergroup.Kdgroup = Kdgroup;

                usergroup.Insert();
            }
            catch { }
        }

        public void Delss01user()
        {
            Ss10userControl user = new Ss10userControl();
            user.Userid = Userid;

            string sql = @"
      delete from SMARTSYS..SS10USER where userid='" + Userid + "'";
            sql = string.Format(sql, XMLName, ID);
            BaseDataAdapter.ExecuteCmd(this, sql);
        }
        public void Delss01userapp()
        {
            Ss10userappControl userapp = new Ss10userappControl();
            userapp.Userid = Userid;

            string sql = @"
      delete from SMARTSYS..SS10USERAPP where userid='" + Userid + "'";
            sql = string.Format(sql, XMLName, ID);
            BaseDataAdapter.ExecuteCmd(this, sql);
        }
        public void Delss20groupuser()
        {
            Ss20groupuserControl usergroup = new Ss20groupuserControl();
            usergroup.Userid = Userid;

            string sql = @"
      delete from SMARTSYS..SS20GROUPUSER where userid='" + Userid + "'";
            sql = string.Format(sql, XMLName, ID);
            BaseDataAdapter.ExecuteCmd(this, sql);
        }
        private bool IsValid()
        {
            bool valid = true;
            return valid;
        }
        public new void Insert()
        {
            if (IsValid())
            {
                Insertss01user();
                Insertuserapp();
                Insertusergroup();
                base.Insert();
            }
            else
            {
                throw new Exception(ConstantDict.Translate("LBL_INVALID_INSERT"));
            }
        }
        public new int Update()
        {
            int n = 0;
            base.Update();
            return n;
        }
        public new int Delete()
        {
            Delss01userapp();
            Delss01user();
            Delss20groupuser();

            Status = -1;
            int n = 0;
            base.Delete();
            return n;
        }

        public new HashTableofParameterRow GetFilters()
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            return hpars;
        }
        public new IList View()
        {
            string dbsmart = ConfigurationManager.AppSettings["Database"];
            Idapp = GlobalAsp.GetSessionApp();

            string sql = $@"
                select W.USERID, GU.KDGROUP, G.NMGROUP, S.USERTYPE, UT.UTURAIAN, W.UNITKEY, D.KDUNIT, D.NMUNIT, W.KDTAHAP
                , W.NIP, S.USERURAIAN as PWD, W.NAMA, W.EMAIL, W.BLOKID, case W.BLOKID when '0' then 'Tidak' else 'Ya' end as USERBLOK
                , W.TRANSECURE, W.STINSERT, W.STUPDATE, W.STDELETE, W.KDPEMDA, W.KET, A.IDAPP, A.KDAPP, A.NMAPP
                , ltrim(rtrim(D.KDUNIT))+' - '+ltrim(rtrim(D.NMUNIT)) KDNMUNIT
                from {dbsmart}..SS10USER S
                inner join WEBUSER W on S.USERID=W.USERID COLLATE DATABASE_DEFAULT
                left join DAFTUNIT D on D.UNITKEY=W.UNITKEY
                inner join {dbsmart}..SS10USERAPP UA on S.USERID=UA.USERID
                inner join {dbsmart}..SS00APP A on UA.IDAPP=A.IDAPP
                inner join {dbsmart}..SS10USERTYPES UT on S.USERTYPE=UT.USERTYPE
                inner join {dbsmart}..SS20GROUPUSER GU on S.USERID=GU.USERID
                inner join {dbsmart}..SS20GROUP G on G.KDGROUP=GU.KDGROUP
                where 1=1
                and UA.IDAPP='{Idapp}'
                order by W.USERID
            ";
            string[] fields = new string[] { "Userid", "Pwd", "Kdnmunit", "Nmgroup", "Uturaian", "Nama", "Nip", "Email", "Ket" };
            List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
            List<WebuserControl> ListData = list.ConvertAll(new Converter<IDataControl, WebuserControl>(delegate (IDataControl par) { return (WebuserControl)par; }));
            return ListData;
        }
        public new IList View(string label)
        {
            IList list = ((BaseDataControl)this).View(label);
            List<WebuserControl> ListData = new List<WebuserControl>();
            foreach (WebuserControl dc in list)
            {
                ListData.Add(dc);
            }
            //Update(ListData);
            return ListData;
        }
        //Unuk ParameterLookup2, pastikan parameter entry is true
        public new void SetPrimaryKey()
        {
            //Namauser = Nama;
            Kdtahap = "321";
            Blokid = "0";

            PemdaControl cPemda = new PemdaControl();
            cPemda.Configid = "sikdpemda";
            cPemda.Load("PK");

            Kdpemda = cPemda.Configval.Trim();
        }
        public override HashTableofParameterRow GetEntries()
        {
            bool tidak = false;
            bool ya = true;

            string Tidak = tidak.ToString();
            string Ya = ya.ToString();

            bool enable = true;
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Userid=User ID"), true, 50).SetEnable(enable).SetEditable(false));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitleEntry("Kdgroup=Kelompok"),
            Ss20groupLookupControl.GetListDataSingleton(), "Kdgroup=Nmgroup", 50).SetAllowRefresh(false).SetEnable(enable).SetEditable(false));
            hpars.Add(new ParameterRowSelect(ConstantDict.GetColumnTitleEntry("Usertype=Jenis User"),
            Ss10usertypesLookupControl.GetListDataSingleton(), "Usertype=Uturaian", 50).SetAllowRefresh(false).SetEnable(enable).SetEditable(false));
            hpars.Add(DaftunitLookupControl.Instance.GetLookupParameterRow(this, true).SetAllowRefresh(false).SetAllowEmpty(true).SetEnable(enable).SetEditable(true));
            hpars.Add(PegawaiLookupControl.Instance.GetLookupParameterRow(this, true).SetAllowRefresh(false).SetAllowEmpty(true).SetEnable(enable).SetEditable(true));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Namauser=Nama"), true, 95).SetEnable(enable));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Email"), true, 95).SetEnable(enable));
            //hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Blokid"), true, 30).SetEnable(enable));
            ArrayList listBertingkat = new ArrayList(new ParamControl[] {
        new ParamControl() {  Kdpar="1",Nmpar="Ya "}
        ,new ParamControl() { Kdpar="0",Nmpar="Tidak "}
      });
            hpars.Add(new ParameterRow(ConstantDict.GetColumnTitle("Blokid"), ParameterRow.MODE_TYPE,
              listBertingkat, "Kdpar=Nmpar", 70).SetEnable(enable).SetEditable(enable));

            ArrayList list = new ArrayList(new ParamControl[] {
        new ParamControl() {  Kdpar = Ya,Nmpar="Ya"}
        ,new ParamControl() { Kdpar=Tidak,Nmpar="Tidak"}
      });

            hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Transecure=Transfer"), ParameterRow.MODE_SELECT,
              list, "Kdpar=Nmpar", 30).SetAllowRefresh(false).SetEnable(enable).SetEditable(false));
            hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Stinsert=Insert"), ParameterRow.MODE_SELECT,
              list, "Kdpar=Nmpar", 30).SetAllowRefresh(false).SetEnable(enable).SetEditable(false));
            hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Stupdate=Update"), ParameterRow.MODE_SELECT,
              list, "Kdpar=Nmpar", 30).SetAllowRefresh(false).SetEnable(enable).SetEditable(false));
            hpars.Add(new ParameterRow(ConstantDict.GetColumnTitleEntry("Stdelete=Delete"), ParameterRow.MODE_SELECT,
              list, "Kdpar=Nmpar", 30).SetAllowRefresh(false).SetEnable(enable).SetEditable(false));
            hpars.Add(new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Ket=Keterangan"), true, 0).SetEnable(enable));
            return hpars;
        }
        #endregion Methods 
    }
    #endregion Webuser

    #region WebuserBlock
    [Serializable]
    public class WebuserBlockControl : WebuserControl, IDataControlUIEntry
    {
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
            cViewListProperties.LookupDC = "Usadi.Valid49.BO.WebuserLookupControl, Usadi.Valid49.Aset.Sys";
            cViewListProperties.LookupLabelQuery = "";
            cViewListProperties.AllowMultiDelete = true;
            return cViewListProperties;
        }
        public new IList View()
        {
            Idapp = GlobalAsp.GetSessionApp();
            IList list = this.View("Block");
            return list;
        }
        public new void SetFilterKey(BaseBO bo)
        {
            if (typeof(WebuserControl).IsInstanceOfType(bo))
            {
                Userid = ((WebuserControl)bo).Userid;
            }
        }
        public new void Insert()
        {
            base.Update("Blokid");
        }
        public new int Delete()
        {
            int n = 0;
            Status = -1;
            base.Update("Hapusblok");
            return n;
        }
    }
    #endregion WebuserBlock

}


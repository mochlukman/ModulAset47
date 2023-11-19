using CoreNET.Common.Base;
using CoreNET.Common.BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Usadi.Valid49.BO
{
    #region Usadi.Valid49.BO.Ss01appmenuAsetGroupControl, Usadi.Valid49.Aset.Sys
    [Serializable]
    public class Ss01appmenuAsetGroupControl : Ss01appmenuAsetControl, IDataControlMenu
    {
        public Ss01appmenuAsetGroupControl()
        {
            XMLName = ConstantTablesSys.XMLSS01APPMENU;
        }

        private ViewListProperties cViewListProperties = null;
        public new IProperties GetProperties()
        {
            if (cViewListProperties == null)
            {
                cViewListProperties = (ViewListProperties)base.GetProperties();
            }
            return cViewListProperties;
        }

        public new IList View()
        {
            Userid = GlobalAsp.GetSessionUser().GetUserID();
            string sql = $@"
      select rtrim(S.KDMENU) as KDMENU,rtrim(S.NMMENU) as NMMENU,
          rtrim(S.IDMENU) as IDMENU,rtrim(S.KDDOK) as KDDOK,rtrim(S.IDXDOK) as IDXDOK,
          rtrim(S.URL) as URL,
          case S.URL when '' then '' else rtrim(S.URL)+'app='+S.IDAPP+'&amp;id='+S.IDMENU+'&amp;kode='+rtrim(isnull(S.KDDOK,''))+'&amp;idx='+rtrim(isnull(S.IDXDOK,''))+'&amp;root=1' end as URLFULL,
          rtrim(S.OLIST1) as OLIST1,rtrim(S.OLISTDETIL1) as OLISTDETIL1,rtrim(S.OLISTDETIL2) as OLISTDETIL2,
          rtrim(S.OLISTDETIL3) as OLISTDETIL3,rtrim(S.OLISTDETIL4) as OLISTDETIL4,
          S.KDLEVEL,rtrim(S.TYPE) as TYPE
          from SS01APPMENU S
          inner join SS00APP A on S.IDAPP=A.IDAPP
          where 1=1
          and IDMENU in (select IDMENU from SS20GROUPMENU 
            where 1=1
            and KDGROUP in (select KDGROUP from SS20GROUPUSER where USERID='{Userid}')
            and STATUS <> -1)
          order by S.KDMENU
      ";
            string[] fields = GetKeys();
            List<IDataControl> list = BaseDataAdapter.GetListDC(this, sql, fields);
            List<Ss01appmenuControl> ListData = list.ConvertAll(new Converter<IDataControl, Ss01appmenuControl>(delegate (IDataControl par) { return (Ss01appmenuControl)par; }));
            return list;
        }
    }
    #endregion Ss01appmenuAset
}


using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using CoreNET.Common.BO;
using CoreNET.Common.Base;

namespace Usadi.Valid49.BO
{
  public class BaseDataControlAsetSys : BaseDataControlExt, IDataControlUIEntry, IExtLoadCsv, IHasJSScript
  {
    public BaseDataControlAsetSys()
    {
      ConnectionString = GetConnectionString();
      ModeDB = SQLDataSource.MODE_DB_OPERATIONAL;
    }

    public static string GetConnectionString()
    {
      string SQLInstance = SQLDataSource.GetSQLInstance();
      string db = ConfigurationManager.AppSettings["DataSourceAset"];
      string user = SQLDataSource.GetUserDB();
      string pwd = SQLDataSource.GetPwdDB();
      string ConnectionString = string.Format("data source={0};initial catalog={1};user id={2};password={3};Asynchronous Processing=true",
        SQLInstance, db, user, pwd);
      return ConnectionString;
    }
  }
}

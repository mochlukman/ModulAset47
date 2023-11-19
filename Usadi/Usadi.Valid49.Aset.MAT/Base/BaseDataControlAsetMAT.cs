using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreNET.Common.BO;
using CoreNET.Common.Base;

namespace Usadi.Valid49.BO
{
    public class BaseDataControlAsetMAT : BaseDataControlAsetDM, IDataControlUIEntry, IExtLoadCsv, IHasJSScript
    {
        public BaseDataControlAsetMAT()
        {
            ConnectionString = GetConnectionString();
            ModeDB = SQLDataSource.MODE_DB_OPERATIONAL;

            #region COR-49 Refactor Get Tahun dari Setting DB - STEP 1
            Ss10userControl dcUser = (Ss10userControl)GlobalAsp.GetSessionUser();
            if (dcUser.Status == 0)
            {
                PemdaControl cPemda = new PemdaControl();
                cPemda.Configid = "cur_thang";
                cPemda.Load("PK");
                dcUser.Tahun = int.Parse(cPemda.Configval ?? DateTime.Now.Year.ToString());
                dcUser.Status = 1;
            }
            #endregion
        }
    }
}

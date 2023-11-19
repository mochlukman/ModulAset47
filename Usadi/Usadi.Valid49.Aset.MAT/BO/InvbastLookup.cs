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
    #region Usadi.Valid49.BO.InvbastLookupControl, CoreNET.Common.BO
    [Serializable]
    public class InvbastLookupControl : InvbastControl, IDataControlLookup, IHasJSScript
    {
        #region Singleton
        private static InvbastLookupControl _Instance = null;
        public static InvbastLookupControl Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new InvbastLookupControl();
                }
                return _Instance;
            }
        }
        //public static void SetSessionListDataNull()
        //{
        //  string session_name = (new InvbastControl()).XMLName; ;
        //  HttpContext.Current.Session[session_name] = null;
        //}
        //public static List<InvbastControl> GetSessionListDataSingleton()
        //{
        //  string session_name = (new InvbastControl()).XMLName; ;
        //  List<InvbastControl> _ListData = (List<InvbastControl>)HttpContext.Current.Session[session_name];
        //  if (_ListData == null)
        //  {
        //    try
        //    {
        //      InvbastLookupControl dc = new InvbastLookupControl();
        //      dc.SetPageKey();
        //      _ListData = (List<InvbastControl>)dc.View(BaseDataControl.ALL);
        //    }
        //    catch(Exception){}
        //    HttpContext.Current.Session[session_name] = _ListData;
        //  }
        //  return _ListData;
        //}
        private static List<InvbastControl> _ListData = null;
        public static void SetListDataNull()
        {
            _ListData = null;
        }
        public static List<InvbastControl> GetListDataSingleton()
        {
            if (_ListData == null)
            {
                InvbastLookupControl dc = new InvbastLookupControl();
                dc.SetPageKey();
                _ListData = (List<InvbastControl>)dc.View(BaseDataControl.LOOKUP);
            }
            return _ListData;
        }
        #endregion
        public InvbastLookupControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLINVBAST;
        }
        public new void SetFilterKey(BaseBO bo)
        {
            Unitkey = (string)bo.GetValue("Unitkey");
            Kdunit = (string)bo.GetValue("Kdunit");
            Nmunit = (string)bo.GetValue("Nmunit");
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.ReadOnlyFields = new String[] { "Unitkey", "Kdunit", "Nmunit" };//Key in GetFilters should put here
            return cViewListProperties;
        }
        public new IList View()
        {
            IList list = this.View(BaseDataControl.LOOKUP);
            return list;
        }
        public new HashTableofParameterRow GetFilters()
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            return hpars;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nobast=No Bast"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglbast=Tanggal Bast"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 50, HorizontalAlign.Left));
            return columns;
        }
        public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
              && string.IsNullOrEmpty((string)callerCtr.GetValue("Nobast"));

            InvbastLookupControl dclookup = new InvbastLookupControl();
            string title = ConstantDict.Translate(dclookup.XMLName);
            string[] keys = new String[] { "Tglbastlookup", "Nobast",  "Ket" };
            string[] targets = new String[] { "Tglbastlookup", "Nobast",  "Ket" };
            ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
            {
                Label = "No Bast",
                VisibleControls = new bool[] { true, true, !entry },
                AllowRefresh = !entry,
                DCLookup = dclookup,
                IsTree = false,
                SelectionCriteria = ParameterRow.SELECTION_CRITERIA_TYPE,
                //SelectionType = "D"
            };
            return par;
        }
        public string GetFieldValueMap()
        {
            return "Tglbastlookup=Nobast";
        }
    }
    #endregion InvbastLookup
}


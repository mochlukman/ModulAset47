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
    #region Usadi.Valid49.BO.DaftpenggunaLookupControl, CoreNET.Common.BO
    [Serializable]
    public class DaftpenggunaLookupControl : DaftpenggunaControl, IDataControlLookup, IHasJSScript
    {
        #region Singleton
        private static DaftpenggunaLookupControl _Instance = null;
        public static DaftpenggunaLookupControl Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DaftpenggunaLookupControl();
                }
                return _Instance;
            }
        }
        //public static void SetSessionListDataNull()
        //{
        //  string session_name = (new TahunControl()).XMLName; ;
        //  HttpContext.Current.Session[session_name] = null;
        //}
        //public static List<TahunControl> GetSessionListDataSingleton()
        //{
        //  string session_name = (new TahunControl()).XMLName; ;
        //  List<TahunControl> _ListData = (List<TahunControl>)HttpContext.Current.Session[session_name];
        //  if (_ListData == null)
        //  {
        //    try
        //    {
        //      DaftpenggunaLookupControl dc = new DaftpenggunaLookupControl();
        //      dc.SetPageKey();
        //      _ListData = (List<TahunControl>)dc.View(BaseDataControl.ALL);
        //    }
        //    catch(Exception){}
        //    HttpContext.Current.Session[session_name] = _ListData;
        //  }
        //  return _ListData;
        //}
        private static List<TahunControl> _ListData = null;
        public static void SetListDataNull()
        {
            _ListData = null;
        }
        public static List<TahunControl> GetListDataSingleton()
        {
            if (_ListData == null)
            {
                DaftpenggunaLookupControl dc = new DaftpenggunaLookupControl();
                dc.SetPageKey();
                _ListData = (List<TahunControl>)dc.View(BaseDataControl.LOOKUP);
            }
            return _ListData;
        }
        #endregion
        public DaftpenggunaLookupControl()
        {
            XMLName = ConstantTablesAsetDM.XMLDAFTPENGGUNA;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
            return cViewListProperties;
        }
        public new IList View()
        {
            IList list = this.View(BaseDataControl.LOOKUP);
            return list;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdpengguna=Kode"), typeof(string), 15, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmpengguna=Nama Pengguna"), typeof(string), 35, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nminst=Instansi"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Telepon"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Npwp=NPWP"), typeof(string), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Alamat"), typeof(string), 50, HorizontalAlign.Left));
            return columns;
        }
        public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
              && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdpengguna"));

            DaftpenggunaLookupControl dclookup = new DaftpenggunaLookupControl();
            string title = ConstantDict.Translate(dclookup.XMLName);
            string[] keys = new String[] { "Kdpengguna", "Nmpengguna" };
            string[] targets = new String[] { "Kdpengguna", "Nmpengguna" };
            ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 26, 63, 0 }, targets)
            {
                Label = "Mitra Pemanfaatan",
                VisibleControls = new bool[] { true, true, !entry },
                AllowRefresh = !entry,
                DCLookup = dclookup,
                IsTree = false,
                SelectionCriteria = ParameterRow.SELECTION_CRITERIA_TYPE,
                SelectionType = "D"
            };
            return par;
        }
        public string GetFieldValueMap()
        {
            return "Kdpengguna=Nmpengguna";
        }
    }
    #endregion DaftpenggunaLookup
}


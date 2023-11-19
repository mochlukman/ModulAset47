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
    #region Usadi.Valid49.BO.BeritaBakfLookupControl, CoreNET.Common.BO
    [Serializable]
    public class BeritaBakfLookupControl : BeritaControl, IDataControlLookup, IHasJSScript
    {
        #region Singleton
        private static BeritaBakfLookupControl _Instance = null;
        public static BeritaBakfLookupControl Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BeritaBakfLookupControl();
                }
                return _Instance;
            }
        }
        //public static void SetSessionListDataNull()
        //{
        //  string session_name = (new BeritaControl()).XMLName; ;
        //  HttpContext.Current.Session[session_name] = null;
        //}
        //public static List<BeritaControl> GetSessionListDataSingleton()
        //{
        //  string session_name = (new BeritaControl()).XMLName; ;
        //  List<BeritaControl> _ListData = (List<BeritaControl>)HttpContext.Current.Session[session_name];
        //  if (_ListData == null)
        //  {
        //    try
        //    {
        //      BeritaBakfLookupControl dc = new BeritaBakfLookupControl();
        //      dc.SetPageKey();
        //      _ListData = (List<BeritaControl>)dc.View(BaseDataControl.ALL);
        //    }
        //    catch(Exception){}
        //    HttpContext.Current.Session[session_name] = _ListData;
        //  }
        //  return _ListData;
        //}
        private static List<BeritaControl> _ListData = null;
        public static void SetListDataNull()
        {
            _ListData = null;
        }
        public static List<BeritaControl> GetListDataSingleton()
        {
            if (_ListData == null)
            {
                BeritaBakfLookupControl dc = new BeritaBakfLookupControl();
                dc.SetPageKey();
                _ListData = (List<BeritaControl>)dc.View(BaseDataControl.LOOKUP);
            }
            return _ListData;
        }
        #endregion
        public BeritaBakfLookupControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLBERITA;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
            return cViewListProperties;
        }
        public new IList View()
        {
            IList list = this.View("BakfLookup");
            return list;
        }
        public new void SetFilterKey(BaseBO bo)
        {
            Unitkey = (string)bo.GetValue("Unitkey");
            Kdunit = (string)bo.GetValue("Kdunit");
            Nmunit = (string)bo.GetValue("Nmunit");
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Noba=No BAST"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglba=Tanggal BAST"), typeof(DateTime), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmtrans=Jenis Transaksi"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilaibakf=Nilai BAKF"), typeof(decimal), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nokontrak=No Kontrak"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nilai=Nilai Kontrak"), typeof(decimal), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraiba=Uraian BAST"), typeof(string), 50, HorizontalAlign.Left));
            return columns;
        }
        public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
              && string.IsNullOrEmpty((string)callerCtr.GetValue("Noba"));

            BeritaBakfLookupControl dclookup = new BeritaBakfLookupControl();
            string title = ConstantDict.Translate(dclookup.XMLName);
            string[] keys = new String[] { "Tglbalookup", "Nodokumen" };
            string[] targets = new String[] { "Tglbalookup", "Nodokumen=Noba", "Nilai=Nilaibakf", "Ket=Uraiba", "Kdtans", "Tglba" };
            ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 22, 65, 0 }, targets)
            {
                Label = "Nomor BAKF",
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
            return "Tglbalookup=Noba";
        }
    }
    #endregion BeritaBakfLookup
}


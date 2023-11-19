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
    #region Usadi.Valid49.BO.PegawaiLookupControl, CoreNET.Common.BO
    [Serializable]
    public class PegawaiLookupControl : PegawaiControl, IDataControlLookup, IHasJSScript
    {
        #region Singleton
        private static PegawaiLookupControl _Instance = null;
        public static PegawaiLookupControl Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new PegawaiLookupControl();
                }
                return _Instance;
            }
        }
        //public static void SetSessionListDataNull()
        //{
        //  string session_name = (new PegawaiControl()).XMLName; ;
        //  HttpContext.Current.Session[session_name] = null;
        //}
        //public static List<PegawaiControl> GetSessionListDataSingleton()
        //{
        //  string session_name = (new PegawaiControl()).XMLName; ;
        //  List<PegawaiControl> _ListData = (List<PegawaiControl>)HttpContext.Current.Session[session_name];
        //  if (_ListData == null)
        //  {
        //    try
        //    {
        //      PegawaiLookupControl dc = new PegawaiLookupControl();
        //      dc.SetPageKey();
        //      _ListData = (List<PegawaiControl>)dc.View(BaseDataControl.ALL);
        //    }
        //    catch(Exception){}
        //    HttpContext.Current.Session[session_name] = _ListData;
        //  }
        //  return _ListData;
        //}
        private static List<PegawaiControl> _ListData = null;
        public static void SetListDataNull()
        {
            _ListData = null;
        }
        public static List<PegawaiControl> GetListDataSingleton()
        {
            if (_ListData == null)
            {
                PegawaiLookupControl dc = new PegawaiLookupControl();
                dc.SetPageKey();
                _ListData = (List<PegawaiControl>)dc.View(BaseDataControl.LOOKUP);
            }
            return _ListData;
        }

        public new HashTableofParameterRow GetFilters()
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            return hpars;
        }
        #endregion
        public PegawaiLookupControl()
        {
            XMLName = ConstantTablesAsetDM.XMLPEGAWAI;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
            return cViewListProperties;
        }
        public new void SetFilterKey(BaseBO bo)
        {
            if (typeof(IDataControlMenu).IsInstanceOfType(bo))
            {
                Last_by = ((BaseBO)bo).Userid;
            }
            else if (bo.GetProperty("Unitkey") != null)
            {
                Unitkey = bo.GetValue("Unitkey").ToString();
                Kdunit = bo.GetValue("Kdunit").ToString();
                Nmunit = bo.GetValue("Nmunit").ToString();
            }
        }
        public new IList View()
        {
            IList list = this.View(BaseDataControl.LOOKUP);
            return list;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nip"), typeof(int), 35, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nama"), typeof(string), 50, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jabatan"), typeof(string), 30, HorizontalAlign.Left));
            return columns;
        }
        public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
        {
            PegawaiLookupControl dclookup = new PegawaiLookupControl();
            string title = ConstantDict.Translate(dclookup.XMLName);
            string[] keys = new String[] { "Nip", "Nama" };
            string[] targets = new String[] { "Nip=Nip", "Nama=Nama", "Namauser=Nama" };
            ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
            {
                Label = title,
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
            return "Nip=Nama";
        }
    }
    #endregion PegawaiLookup
}


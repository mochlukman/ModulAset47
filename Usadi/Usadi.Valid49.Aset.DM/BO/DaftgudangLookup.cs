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
    #region Usadi.Valid49.BO.DaftgudangLookupControl, Usadi.Valid49.Aset.DM
    [Serializable]
    public class DaftgudangLookupControl : DaftgudangControl, IDataControlLookup, IHasJSScript
    {
        #region Singleton
        private static DaftgudangLookupControl _Instance = null;
        public static DaftgudangLookupControl Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DaftgudangLookupControl();
                }
                return _Instance;
            }
        }
       
        private static List<DaftgudangControl> _ListData = null;
        public static void SetListDataNull()
        {
            _ListData = null;
        }
        public static List<DaftgudangControl> GetListDataSingleton()
        {
            if (_ListData == null)
            {
                DaftgudangLookupControl dc = new DaftgudangLookupControl();
                dc.SetPageKey();
                _ListData = (List<DaftgudangControl>)dc.View(BaseDataControl.LOOKUP);
            }
            return _ListData;
        }
        #endregion
        public DaftgudangLookupControl()
        {
            XMLName = ConstantTablesAsetDM.XMLDAFTGUDANG;
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
        public new void SetFilterKey(BaseBO bo)
        {
            Unitkey = (string)bo.GetValue("Unitkey");
            Kdunit = (string)bo.GetValue("Kdunit");
            Nmunit = (string)bo.GetValue("Nmunit");
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
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdgudang=Kode"), typeof(string), 20, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmgudang=Uraian"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nama=Nama Penanggung Jawab"), typeof(string), 40, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket"), typeof(string), 30, HorizontalAlign.Left));

            return columns;
        }
        public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
        {
            //bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
            //  && string.IsNullOrEmpty((string)callerCtr.GetValue("Noba"));

            DaftgudangLookupControl dclookup = new DaftgudangLookupControl();
            string title = ConstantDict.Translate(dclookup.XMLName);
            string[] keys = new String[] { "Kdgudang", "Nmgudang" };
            string[] targets = new String[] { "Kdgudang", "Nmgudang" };
            ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 22, 65, 0 }, targets)
            {
                Label = "Gudang",
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
            return "Kdgudang=Nmgudang";
        }
    }
    #endregion DaftgudangLookup
}


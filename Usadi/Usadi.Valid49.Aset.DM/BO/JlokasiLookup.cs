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
    #region Usadi.Valid49.BO.JlokasiLookupControl, CoreNET.Common.BO
    [Serializable]
    public class JlokasiLookupControl : JlokasiControl, IDataControlLookup, IHasJSScript
    {
        #region Singleton
        private static JlokasiLookupControl _Instance = null;
        public static JlokasiLookupControl Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new JlokasiLookupControl();
                }
                return _Instance;
            }
        }
        
        private static List<JlokasiControl> _ListData = null;
        public static void SetListDataNull()
        {
            _ListData = null;
        }
        public static List<JlokasiControl> GetListDataSingleton()
        {
            if (_ListData == null)
            {
                JlokasiLookupControl dc = new JlokasiLookupControl();
                dc.SetPageKey();
                _ListData = (List<JlokasiControl>)dc.View(BaseDataControl.LOOKUP);
            }
            return _ListData;
        }
        #endregion
        public JlokasiLookupControl()
        {
            XMLName = ConstantTablesAsetDM.XMLJLOKASI;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
            return cViewListProperties;
        }
        public new IList View()
        {
            IList list = this.View("All");
            return list;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Jenis=Kode"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraijlok=Jenis Lokasi"), typeof(string), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 30, HorizontalAlign.Left));
            return columns;
        }
        public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
              && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdjlok"));

            JlokasiLookupControl dclookup = new JlokasiLookupControl();
            string title = ConstantDict.Translate(dclookup.XMLName);
            string[] keys = new String[] { "Jenis", "Jenis" };
            string[] targets = new String[] { "Jenis=Jenis" };
            ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
            {
                Label = "Jenis Lokasi",
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
            return "Jenis=Jenis";
        }
    }
    #endregion JlokasiLookup
}


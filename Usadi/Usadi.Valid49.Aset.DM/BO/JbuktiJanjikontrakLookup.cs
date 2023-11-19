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
    #region Usadi.Valid49.BO.JbuktiJanjikontrakLookupControl, CoreNET.Common.BO
    [Serializable]
    public class JbuktiJanjikontrakLookupControl : JbuktiControl, IDataControlLookup, IHasJSScript
    {
        #region Singleton
        private static JbuktiJanjikontrakLookupControl _Instance = null;
        public static JbuktiJanjikontrakLookupControl Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new JbuktiJanjikontrakLookupControl();
                }
                return _Instance;
            }
        }

        private static List<JbuktiControl> _ListData = null;
        public static void SetListDataNull()
        {
            _ListData = null;
        }
        public static List<JbuktiControl> GetListDataSingleton()
        {
            if (_ListData == null)
            {
                JbuktiJanjikontrakLookupControl dc = new JbuktiJanjikontrakLookupControl();
                dc.SetPageKey();
                _ListData = (List<JbuktiControl>)dc.View(BaseDataControl.LOOKUP);
            }
            return _ListData;
        }
        #endregion
        public JbuktiJanjikontrakLookupControl()
        {
            XMLName = ConstantTablesAsetDM.XMLJBUKTI;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
            return cViewListProperties;
        }
        public new void SetFilterKey(BaseBO bo)
        {
        }
        public new IList View()
        {
            IList list = this.View("Janjikontrak");
            return list;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdbukti=Kode"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmbukti=Jenis Bukti"), typeof(string), 20, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 30, HorizontalAlign.Left));
            return columns;
        }
        public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
              && string.IsNullOrEmpty((string)callerCtr.GetValue("Kdbukti"));

            JbuktiJanjikontrakLookupControl dclookup = new JbuktiJanjikontrakLookupControl();
            string title = ConstantDict.Translate(dclookup.XMLName);
            string[] keys = new String[] { "Kdbukti", "Nmbukti" };
            string[] targets = new String[] { "Kdbukti", "Nmbukti" };
            ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
            {
                Label = "Jenis Bukti",
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
            return "Kdbukti=Nmbukti";
        }
    }
    #endregion JbuktiJanjikontrakLookup
}


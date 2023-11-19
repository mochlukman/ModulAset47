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
    #region Usadi.Valid49.BO.InvnotaLookupControl, CoreNET.Common.BO
    [Serializable]
    public class InvnotaLookupControl : InvnotaControl, IDataControlLookup, IHasJSScript
    {
        #region Singleton
        private static InvnotaLookupControl _Instance = null;
        public static InvnotaLookupControl Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new InvnotaLookupControl();
                }
                return _Instance;
            }
        }

        private static List<InvnotaControl> _ListData = null;
        public static void SetListDataNull()
        {
            _ListData = null;
        }
        public static List<InvnotaControl> GetListDataSingleton()
        {
            if (_ListData == null)
            {
                InvnotaLookupControl dc = new InvnotaLookupControl();
                dc.SetPageKey();
                _ListData = (List<InvnotaControl>)dc.View(BaseDataControl.LOOKUP);
            }
            return _ListData;
        }
        #endregion
        public InvnotaLookupControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLINVNOTA;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
            return cViewListProperties;
        }
        public new void SetFilterKey(BaseBO bo)
        {
            Unitkey2 = (string)bo.GetValue("Unitkey2");
        }
        public new IList View()
        {
            IList list = this.View("Invpbs");
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
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nonpb=Nota Permintaan Barang"), typeof(int), 25, HorizontalAlign.Left).SetEditable(false));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left).SetEditable(false));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 25, HorizontalAlign.Left).SetEditable(false));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Qty=Quantity"), typeof(string), 10, HorizontalAlign.Center).SetEditable(false));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 10, HorizontalAlign.Center).SetEditable(false));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Untuk2=Keperluan"), typeof(string), 25, HorizontalAlign.Left).SetEditable(false));
            return columns;
        }
        public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
              && string.IsNullOrEmpty((string)callerCtr.GetValue("Nonpb"));

            InvnotaLookupControl dclookup = new InvnotaLookupControl();
            string title = ConstantDict.Translate(dclookup.XMLName);
            string[] keys = new String[] { "Nonpb", "Untuk2", "Nonpb" };
            string[] targets = new String[] { "Nonpb", "Untuk2", "Nonpb" };
            ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
            {
                Label = "Nomor Nota",
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
            return "Nonpb=Untuk2";
        }
    }
    #endregion InvnotaLookup
}


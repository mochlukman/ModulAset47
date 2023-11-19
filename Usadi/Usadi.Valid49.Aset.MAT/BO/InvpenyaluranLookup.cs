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
    #region Usadi.Valid49.BO.InvpenyaluranLookupControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class InvpenyaluranLookupControl : InvpenyaluranControl, IDataControlLookup, IHasJSScript
    {
        #region Singleton
        private static InvpenyaluranLookupControl _Instance = null;
        public static InvpenyaluranLookupControl Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new InvpenyaluranLookupControl();
                }
                return _Instance;
            }
        }

        private static List<InvpenyaluranControl> _ListData = null;
        public static void SetListDataNull()
        {
            _ListData = null;
        }
        public static List<InvpenyaluranControl> GetListDataSingleton()
        {
            if (_ListData == null)
            {
                InvpenyaluranLookupControl dc = new InvpenyaluranLookupControl();
                dc.SetPageKey();
                _ListData = (List<InvpenyaluranControl>)dc.View(BaseDataControl.LOOKUP);
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
        public InvpenyaluranLookupControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLINVPENYALURAN;
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
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nosppb=Nomor Penyaluran"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglsppb=Tanggal Dokumen"), typeof(DateTime), 30, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 40, HorizontalAlign.Left).SetEditable(true));

            return columns;
        }
        public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
              && string.IsNullOrEmpty((string)callerCtr.GetValue("Nosppb"));

            InvpenyaluranLookupControl dclookup = new InvpenyaluranLookupControl();
            string title = ConstantDict.Translate(dclookup.XMLName);
            string[] keys = new String[] { "Tglsppblookup", "Nosppb", "Ket" };
            string[] targets = new String[] { "Tglsppblookup", "Nosppb", "Ket" };
            ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 50, 20 }, targets)
            {
                Label = "No Surat Penyaluran",
                VisibleControls = new bool[] { true, true, !entry },
                AllowRefresh = !entry,
                DCLookup = dclookup,
                IsTree = false,
                SelectionCriteria = ParameterRow.SELECTION_CRITERIA_TYPE,
                SelectionLevel = "D"
            };
            return par;
        }
        public string GetFieldValueMap()
        {
            return "Tglsppblookup=Nosppb";
        }
    }
    #endregion InvpenyaluranControlLookup
}



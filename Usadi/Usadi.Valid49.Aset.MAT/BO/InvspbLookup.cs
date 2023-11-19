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
    #region Usadi.Valid49.BO.InvspbLookupControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class InvspbLookupControl : InvspbControl, IDataControlLookup, IHasJSScript
    {
        #region Singleton
        private static InvspbLookupControl _Instance = null;
        public static InvspbLookupControl Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new InvspbLookupControl();
                }
                return _Instance;
            }
        }
       
        private static List<InvspbControl> _ListData = null;
        public static void SetListDataNull()
        {
            _ListData = null;
        }
        public static List<InvspbControl> GetListDataSingleton()
        {
            if (_ListData == null)
            {
                InvspbLookupControl dc = new InvspbLookupControl();
                dc.SetPageKey();
                _ListData = (List<InvspbControl>)dc.View(BaseDataControl.LOOKUP);
            }
            return _ListData;
        }
        public new HashTableofParameterRow GetFilters()
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            return hpars;
        }
        //public static InvspbControl FindAndSetValuesInto(IDataControlUI dc)
        //{
        //    InvspbControl founddc = null;
        //    List<InvspbControl> _ListData = GetListDataSingleton();
        //    if (_ListData != null)
        //    {
        //        founddc = (InvspbControl)_ListData.Find(o => o.Asetkey.Equals(dc.GetValue("Asetkey")));
        //        if (founddc != null)
        //        {
        //            if (typeof(InvspbControl).IsInstanceOfType(dc))
        //            {
        //                founddc.CopyPropertyBOTo(dc);
        //            }
        //            else
        //            {
        //                dc.SetValue("Asetkey", founddc.Asetkey);
        //            }
        //        }
        //    }
        //    return founddc;
        //}
        #endregion
        public InvspbLookupControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLINVSPB;
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
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nospb=Nomor SPB"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Tglspb=Tanggal Dokumen"), typeof(DateTime), 30, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(string), 40, HorizontalAlign.Left).SetEditable(true));

            return columns;
        }
        public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
        {
            //bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
            //  && string.IsNullOrEmpty((string)callerCtr.GetValue("Nospb"));

            InvspbLookupControl dclookup = new InvspbLookupControl();
            string title = ConstantDict.Translate(dclookup.XMLName);
            string[] keys = new String[] { "Tglspblookup", "Nospb", "Ket" };
            string[] targets = new String[] { "Tglspblookup", "Nospb", "Ket" };
            ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 50, 20 }, targets)
            {
                Label = "No Surat Permintaan Barang",
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
            return "Tglspblookup=Nospb";
        }
    }
    #endregion InvspbControlLookup
}



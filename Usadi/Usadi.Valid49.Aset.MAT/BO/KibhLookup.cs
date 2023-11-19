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
    #region Usadi.Valid49.BO.KibhLookupControl, Usadi.Valid49.Aset.MAT
    [Serializable]
    public class KibhLookupControl : KibhControl, IDataControlLookup, IHasJSScript
    {
        #region Singleton
        private static KibhLookupControl _Instance = null;
        public static KibhLookupControl Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new KibhLookupControl();
                }
                return _Instance;
            }
        }
        
        private static List<KibhControl> _ListData = null;
        public static void SetListDataNull()
        {
            _ListData = null;
        }
        public new HashTableofParameterRow GetFilters()
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
            HashTableofParameterRow hpars = new HashTableofParameterRow();

            return hpars;
        }
        public static List<KibhControl> GetListDataSingleton()
        {
            if (_ListData == null)
            {
                KibhLookupControl dc = new KibhLookupControl();
                dc.SetPageKey();
                _ListData = (List<KibhControl>)dc.View(BaseDataControl.LOOKUP);
            }
            return _ListData;
        }
        public static KibhControl FindAndSetValuesInto(IDataControlUI dc)
        {
            KibhControl founddc = null;
            List<KibhControl> _ListData = GetListDataSingleton();
            if (_ListData != null)
            {
                founddc = (KibhControl)_ListData.Find(o => o.Idbrg.Equals(dc.GetValue("Idbrg")));
                if (founddc != null)
                {
                    if (typeof(KibhControl).IsInstanceOfType(dc))
                    {
                        founddc.CopyPropertyBOTo(dc);
                    }
                    else
                    {
                        dc.SetValue("Idbrg", founddc.Idbrg);
                    }
                }
            }
            return founddc;
        }
        public static string[] GetFieldValueProps()
        {
            return new string[] { "Idbrg" };
        }
        #endregion
        public KibhLookupControl()
        {
            XMLName = ConstantTablesAsetMAT.XMLKIBH;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            return cViewListProperties;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 30, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Spesifikasi"), typeof(string), 25, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdsatuan=Satuan"), typeof(string), 5, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmsatuan=Satuan"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Merk"), typeof(string), 20, HorizontalAlign.Left));

            return columns;
        }
        public new void SetFilterKey(BaseBO bo)
        {
            if (typeof(IDataControlMenu).IsInstanceOfType(bo))
            {
                Last_by = ((BaseBO)bo).Userid;
            }
            Unitkey = (string)bo.GetValue("Unitkey");
        }
        public new IList View()
        {
            IList list = this.View("Invspb");
            return list;
        }
        public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
        {
            //bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
            //  && string.IsNullOrEmpty((string)callerCtr.GetValue("Idbrg"));

            KibhLookupControl dclookup = new KibhLookupControl();
            string title = ConstantDict.Translate(dclookup.XMLName);
            string[] keys = new String[] { "Kdaset", "Nmaset", "Idbrg" };
            string[] targets = new String[] { "Kdaset", "Nmaset", "Idbrg", "Spesifikasi","Kdsatuan", "Nmsatuan", "Merk" };
            ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
            {
                Label = "Kode Barang",
                VisibleControls = new bool[] { true, true, !entry },
                AllowRefresh = !entry,
                DCLookup = dclookup,
                IsTree = false,
                SelectionCriteria = ParameterRow.SELECTION_CRITERIA_TYPE,
                SelectionLevel = "D"
                //SelectionType = "3"
            };
            //par.SetEnable(enableFilter);
            return par;
        }
        public string GetFieldValueMap()
        {
            return "Kdaset=Nmaset";
        }
    }
    #endregion KibhLookup
}


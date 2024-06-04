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
    #region Usadi.Valid49.BO.DaftasetObjekInvestasiLookupControl, Usadi.Valid49.Aset.DM
    [Serializable]
    public class DaftasetObjekInvestasiLookupControl : DaftasetControl, IDataControlLookup, IHasJSScript
    {
        #region Singleton
        private static DaftasetObjekInvestasiLookupControl _Instance = null;
        public static DaftasetObjekInvestasiLookupControl Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DaftasetObjekInvestasiLookupControl();
                }
                return _Instance;
            }
        }

        private static List<DaftasetControl> _ListData = null;
        public static void SetListDataNull()
        {
            _ListData = null;
        }
        public static List<DaftasetControl> GetListDataSingleton()
        {
            if (_ListData == null)
            {
                DaftasetObjekInvestasiLookupControl dc = new DaftasetObjekInvestasiLookupControl();
                dc.SetPageKey();
                _ListData = (List<DaftasetControl>)dc.View(BaseDataControl.LOOKUP);
            }
            return _ListData;
        }
        public static DaftasetControl FindAndSetValuesInto(IDataControlUI dc)
        {
            DaftasetControl founddc = null;
            List<DaftasetControl> _ListData = GetListDataSingleton();
            if (_ListData != null)
            {
                founddc = (DaftasetControl)_ListData.Find(o => o.Asetkey.Equals(dc.GetValue("Asetkey")));
                if (founddc != null)
                {
                    if (typeof(DaftasetControl).IsInstanceOfType(dc))
                    {
                        founddc.CopyPropertyBOTo(dc);
                    }
                    else
                    {
                        dc.SetValue("Asetkey", founddc.Asetkey);
                    }
                }
            }
            return founddc;
        }
        public static string[] GetFieldValueProps()
        {
            return new string[] { "Asetkey" };
        }
        #endregion
        public DaftasetObjekInvestasiLookupControl()
        {
            XMLName = ConstantTablesAsetDM.XMLDAFTASET;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            return cViewListProperties;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Golkib=Kode KIB"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdaset=Kode Barang"), typeof(string), 20, HorizontalAlign.Left));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Nmaset=Nama Barang"), typeof(string), 60, HorizontalAlign.Left));

            return columns;
        }
        public new void SetPageKey()
        {
        }
        public new void SetFilterKey(BaseBO bo)
        {
            //Kddana = (string)bo.GetValue("Kddana");
        }
        public new IList View()
        {
            IList list = this.View("ObjekInvestasi");
            return list;
        }
        public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
        {
            //bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev())
            //  && string.IsNullOrEmpty((string)callerCtr.GetValue("Asetkey"));

            DaftasetObjekInvestasiLookupControl dclookup = new DaftasetObjekInvestasiLookupControl();
            string title = ConstantDict.Translate(dclookup.XMLName);
            string[] keys = new String[] { "Kdobjekaset", "Nmobjekaset", "Asetkey" };
            string[] targets = new String[] { "Kdobjekaset=Kdaset", "Nmobjekaset=Nmaset", "Asetkey" };
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
            return "Kdobjekaset=Nmobjekaset";
        }
    }
    #endregion DaftasetObjekInvestasiLookup
}


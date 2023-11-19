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
    #region Usadi.Valid49.BO.DaftunitUkLookupControl, Usadi.Valid49.Aset.DM
    [Serializable]
    public class DaftunitUkLookupControl : DaftunitControl, IDataControlLookup, IHasJSScript
    {
        private static DaftunitUkLookupControl _Instance = null;
        public static DaftunitUkLookupControl Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DaftunitUkLookupControl();
                }
                return _Instance;
            }
        }
        public DaftunitUkLookupControl()
        {
            XMLName = ConstantTablesAsetDM.XMLDAFTUNIT;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            return cViewListProperties;
        }
        public new void SetPageKey()
        {
        }
        public new void SetFilterKey(BaseBO bo)
        {
            if (typeof(IDataControlMenu).IsInstanceOfType(bo))
            {
                Last_by = ((BaseBO)bo).Userid;
            }
            //Unitkey = (string)bo.GetValue("Unitkey");
        }
        public new IList View()
        {
            IList list = this.View("UnitPb");
            return list;
        }
       
        public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
        {
            DaftunitUkLookupControl dclookup = new DaftunitUkLookupControl();
            string title = ConstantDict.Translate(dclookup.XMLName);
            string[] keys = new String[] { "Kdunit2", "Nmunit2", "Unitkey2" };
            string[] targets = new String[] { "Kdunit2=Kdunit", "Nmunit2=Nmunit", "Unitkey2=Unitkey" };
            ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
            {
                Label = "Unit Organisasi",
                VisibleControls = new bool[] { true, true, !entry },
                AllowRefresh = !entry,
                DCLookup = dclookup,
                IsTree = true,
                SelectionCriteria = ParameterRow.SELECTION_CRITERIA_LEVEL,
                //SelectionType = "D"
                SelectionLevel = "4,5"
            };
            return par;
        }
        public string GetFieldValueMap()
        {
            return "Kdunit=Nmunit";
        }
    }
    #endregion DaftunitUkLookup
}


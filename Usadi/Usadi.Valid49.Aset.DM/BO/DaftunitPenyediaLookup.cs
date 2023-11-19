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
    #region Usadi.Valid49.BO.DaftunitPenyediaLookupControl, Usadi.Valid49.Aset.DM
    [Serializable]
    public class DaftunitPenyediaLookupControl : DaftunitControl, IDataControlLookup, IHasJSScript
    {
        private static DaftunitPenyediaLookupControl _Instance = null;
        public static DaftunitPenyediaLookupControl Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DaftunitPenyediaLookupControl();
                }
                return _Instance;
            }
        }
        public DaftunitPenyediaLookupControl()
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
            Kdunit2 = (string)bo.GetValue("Kdunit2");
        }
        public new IList View()
        {
            IList list = this.View("UnitPenyedia");
            return list;
        }

        public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
        {
            DaftunitPenyediaLookupControl dclookup = new DaftunitPenyediaLookupControl();
            string title = ConstantDict.Translate(dclookup.XMLName);
            string[] keys = new String[] { "Kdunit", "Nmunit", "Unitkey" };
            string[] targets = new String[] { "Kdunit", "Nmunit", "Unitkey" };
            ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 20, 75, 0 }, targets)
            {
                Label = "Unit Organisasi",
                VisibleControls = new bool[] { true, true, !entry },
                AllowRefresh = !entry,
                DCLookup = dclookup,
                IsTree = false,
                SelectionCriteria = ParameterRow.SELECTION_CRITERIA_LEVEL,
                //SelectionType = "D"
                SelectionLevel = "3,4,5"
            };
            return par;
        }
        public string GetFieldValueMap()
        {
            return "Kdunit=Nmunit";
        }
    }
    #endregion DaftunitPenyediaLookup
}


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
    #region Usadi.Valid49.BO.DaftunitMutasiInternalLookupControl, Usadi.Valid49.Aset.DM
    [Serializable]
    public class DaftunitMutasiInternalLookupControl : DaftunitControl, IDataControlLookup, IHasJSScript
    {
        private static DaftunitMutasiInternalLookupControl _Instance = null;
        public static DaftunitMutasiInternalLookupControl Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DaftunitMutasiInternalLookupControl();
                }
                return _Instance;
            }
        }
        public DaftunitMutasiInternalLookupControl()
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
            Unitkey = (string)bo.GetValue("Unitkey");
        }
        public new IList View()
        {
            IList list = this.View("Subunit");
            return list;
        }
        public ParameterRow GetLookupParameterRow(IDataControl callerCtr, bool entry)
        {
            DaftunitMutasiInternalLookupControl dclookup = new DaftunitMutasiInternalLookupControl();
            string title = "SKPD Tujuan"; // ConstantDict.Translate(dclookup.XMLName);
            string[] keys = new String[] { "Kdunit2", "Nmunit2", "Unitkey2" };
            string[] targets = new String[] { "Kdunit2=Kdunit", "Nmunit2=Nmunit", "Unitkey2=Unitkey" };
            ParameterRowLookup2 par = new ParameterRowLookup2(callerCtr, keys, new int[] { 26, 63, 0 }, targets)
            {
                Label = title,
                VisibleControls = new bool[] { true, true, !entry },
                AllowRefresh = !entry,
                DCLookup = dclookup,
                IsTree = true,
                SelectionCriteria = ParameterRow.SELECTION_CRITERIA_LEVEL,
                //SelectionType = "D"
                SelectionLevel = "1,2"
            };
            return par;
        }
        public string GetFieldValueMap()
        {
            return "Kdunit=Nmunit";
        }
    }

    #endregion DaftunitMutasiInternalLookup
}


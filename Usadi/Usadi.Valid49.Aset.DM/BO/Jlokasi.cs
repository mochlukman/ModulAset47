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
    #region Usadi.Valid49.BO.JlokasiControl, Usadi.Valid49.Aset.DM
    [Serializable]
    public class JlokasiControl : BaseDataControlAsetDM, IDataControlUIEntry, IHasJSScript
    {
        #region Properties
        public long Id { get; set; }
        public string Uraijlok { get; set; }
        public new string Ket { get; set; }
        public string Kdjlok { get; set; }
        public string Jenis { get; set; }

        #endregion Properties 

        #region Methods 
        public JlokasiControl()
        {
            XMLName = ConstantTablesAsetDM.XMLJLOKASI;
        }
        public new IProperties GetProperties()
        {
            ViewListProperties cViewListProperties = (ViewListProperties)base.GetProperties();
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Kdjlok" };
            cViewListProperties.IDKey = "Id";
            cViewListProperties.IDProperty = "Id";
            cViewListProperties.ReadOnlyFields = new String[] { };
            cViewListProperties.SortFields = new String[] { "Kdjlok" };
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_FORM;
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_READONLY;
            cViewListProperties.PageSize = 20;
            return cViewListProperties;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection();
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Level=Kode"), typeof(string), 15, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Uraijlok=Jenis Lokasi"), typeof(string), 30, HorizontalAlign.Center));
            columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Ket=Keterangan"), typeof(int), 50, HorizontalAlign.Left));

            return columns;
        }
        public new IList View()
        {
            IList list = this.View(BaseDataControl.ALL);
            return list;
        }
        public new IList View(string label)
        {
            IList list = ((BaseDataControl)this).View(label);
            List<JlokasiControl> ListData = new List<JlokasiControl>();
            foreach (JlokasiControl dc in list)
            {
                ListData.Add(dc);
            }

            return ListData;
        }
        public new int Delete()
        {
            Status = -1;
            int n = ((BaseDataControlUI)this).Delete(BaseDataControl.DEFAULT);
            return n;
        }
        public override HashTableofParameterRow GetEntries()
        {
            bool enable = true;
            HashTableofParameterRow hpars = new HashTableofParameterRow();
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Level=Kode"), false, 10).SetEnable(enable));
            hpars.Add(new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Uraijlok=Lokasi"), true, 50).SetEnable(enable));

            return hpars;
        }

        #endregion Methods 
    }
    #endregion Jlokasi
}


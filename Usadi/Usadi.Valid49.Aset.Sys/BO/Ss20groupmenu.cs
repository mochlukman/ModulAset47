using CoreNET.Common.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace CoreNET.Common.BO
{
    #region CoreNET.Common.BO.Ss20groupmenuAsetControl, CoreNET.Common.BO
    [Serializable]
    public class Ss20groupmenuAsetControl : Ss01appmenuControl, IDataControlTreeGrid3, IHasJSScript, IExtLoadCsv
    {
        #region Methods 
        public Ss20groupmenuAsetControl()
        {
            XMLName = ConstantTablesSys.XMLSS20GROUPMENU;
        }

        private ViewListProperties cViewListProperties = null;
        public new IProperties GetProperties()
        {
            if (cViewListProperties == null)
            {
                cViewListProperties = (ViewListProperties)base.GetProperties();
            }
            cViewListProperties.TitleList = ConstantDict.Translate(XMLName);
            cViewListProperties.PrimaryKeys = new String[] { "Kdgroup", "Idmenu" };
            cViewListProperties.IDKey = "Idmenu";//IDKey for ID Notes
            cViewListProperties.IDProperty = "Idmenu";//UniqueKey in gridview
            cViewListProperties.ReadOnlyFields = new String[] { "Kdgroup" };//Key in GetFilters should put here
            cViewListProperties.SortFields = new String[] { "Kdmenu" };//
            cViewListProperties.ModeEditable = ViewListProperties.MODE_EDITABLE_ADD_DEL;
            cViewListProperties.EntryStyle = ViewListProperties.ENTRY_STYLE_LOOKUP;
            cViewListProperties.LookupDC = "CoreNET.Common.BO.Ss20groupmenuLookupForSelfControl, CoreNET.Common.Sys";
            cViewListProperties.LookupLabelQuery = BaseDataControl.LOOKUP_FOR_SELF;
            cViewListProperties.AllowMultiDelete = true;
            return cViewListProperties;
        }
        public new void SetPrimaryKey()
        {
            Status = 0;
            Stfilter = true;
            Stload = true;
            Stinsert = false;
            Stupdate = false;
            Stdelete = false;
        }
        public new void SetFilterKey(BaseBO bo)
        {
            if (bo.GetType().Equals(typeof(Ss20groupControl)))
            {
                Kdgroup = ((Ss20groupControl)bo).Kdgroup;
                Nmgroup = ((Ss20groupControl)bo).Nmgroup;
            }
            else if (bo.GetType().Equals(typeof(Ss20groupAppControl)))
            {
                Kdgroup = ((Ss20groupAppControl)bo).Kdgroup;
                Nmgroup = ((Ss20groupAppControl)bo).Nmgroup;
                Idapp = ((Ss20groupAppControl)bo).Idapp;
                Kdapp = ((Ss20groupAppControl)bo).Kdapp;
                Nmapp = ((Ss20groupAppControl)bo).Nmapp;
            }
            else if (typeof(Ss20groupmenuLookupForSelfControl).IsInstanceOfType(bo))
            {
                Idmenu = ((Ss20groupmenuLookupForSelfControl)bo).Idmenu;
            }
        }
        public HashTableofParameterRow GetFilters(bool enableFilter)
        {
            HashTableofParameterRow hpars = new HashTableofParameterRow
      {
        Ss20groupLookupControl.Instance.GetLookupParameterRow(this, false).SetEnable(enableFilter),
        //Ss00appLookupControl.Instance.GetLookupParameterRowDetil(this, false).SetEnable(enableFilter)
      };
            return hpars;
        }
        public new HashTableofParameterRow GetFilters()
        {
            bool enableFilter = string.IsNullOrEmpty(GlobalAsp.GetRequestIdPrev());
            return GetFilters(enableFilter);
        }

        public new void Insert()
        {
            if (IsValid())
            {
                base.Insert();
            }
            else
            {
                throw new Exception(ConstantDict.Translate("LBL_INVALID_INSERT"));
            }
        }
        private bool IsValid()
        {
            bool valid = true;
            if (string.IsNullOrEmpty(Kdgroup))
            {
                throw new Exception("Field Kelompok tidak boleh kosong");
            }
            return valid;
        }

        public new IList View()
        {
            IList list = View(BaseDataControl.ALL);
            return list;
        }
        public new IList View(string label)
        {
            IList list = ((BaseDataControl)this).View(label);
            List<Ss01appmenuControl> ListData = new List<Ss01appmenuControl>();
            foreach (Ss01appmenuControl dc in list)
            {
                DmstatusLookupControl.FindAndSetValuesInto(dc);
                ListData.Add(dc);
            }
            //Update(ListData);
            return ListData;
        }
        public override DataControlFieldCollection GetColumns()
        {
            DataControlFieldCollection columns = new DataControlFieldCollection
            {
                Fields.Create(ConstantDict.GetColumnTitle("Statusicon"), typeof(Ext.Net.Icon), 5, HorizontalAlign.Center),
                Fields.Create(ConstantDict.GetColumnTitle("Kdmenu"), typeof(string), EditCmd, 30, HorizontalAlign.Left),
                Fields.Create(ConstantDict.GetColumnTitle("Nmmenu"), typeof(string), 50, HorizontalAlign.Left),
                Fields.Create(ConstantDict.GetColumnTitle("Stinsert"), typeof(bool), 10, HorizontalAlign.Center).SetEditable(true),
                Fields.Create(ConstantDict.GetColumnTitle("Stupdate"), typeof(bool), 10, HorizontalAlign.Center).SetEditable(true),
                Fields.Create(ConstantDict.GetColumnTitle("Stdelete"), typeof(bool), 10, HorizontalAlign.Center).SetEditable(true),
                Fields.Create(ConstantDict.GetColumnTitle("Stload"), typeof(bool), 10, HorizontalAlign.Center).SetEditable(true),
                Fields.Create(ConstantDict.GetColumnTitle("Stfilter"), typeof(bool), 10, HorizontalAlign.Center).SetEditable(true)
            };
            if (ModePreviewIndex == 1)
            {
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Kdlevel"), typeof(int), 7, HorizontalAlign.Center).SetEditable(true));
                columns.Add(Fields.Create(ConstantDict.GetColumnTitle("Type"), typeof(string), 7, HorizontalAlign.Center).SetEditable(true));
            }
            return columns;
        }
        public override HashTableofParameterRow GetEntries()
        {
            bool enable = true;
            if (hpars == null)
            {
                hpars = new HashTableofParameterRow
        {
          new ParameterRowTextBox(this, ConstantDict.GetColumnTitle("Kdmenu"), false, 100).SetEnable(false),
          new ParameterRowMemo(this, ConstantDict.GetColumnTitle("Nmmenu"), false, 3).SetEnable(false),
          new ParameterRowCek(this, ConstantDict.GetColumnTitle("Stinsert"), true).SetEnable(enable),
          new ParameterRowCek(this, ConstantDict.GetColumnTitle("Stupdate"), true).SetEnable(enable),
          new ParameterRowCek(this, ConstantDict.GetColumnTitle("Stdelete"), true).SetEnable(enable),
          new ParameterRowCek(this, ConstantDict.GetColumnTitle("Stload"), true).SetEnable(enable),
          new ParameterRowCek(this, ConstantDict.GetColumnTitle("Stfilter"), true).SetEnable(enable),
          new ParameterRowSelect(ConstantDict.GetColumnTitleEntry("Status"),
          DmstatusLookupControl.GetListDataSingleton(string.Empty), "Idstatus=Nmstatus", 30).SetAllowRefresh(false).SetEnable(enable),
          new ParameterRowUploadFile(this, true),
          new ParameterRowHelp(this, true),
          new ParameterRowForum(this, true)
        };
            }
            return hpars;
        }
        public new string[] GetScript()
        {
            string script = @"
        var ShowHidden = function (type) {
          if (type=='D') {
            return 'visible';/**/
          }else{
            return 'hidden';/**/
          }
        };
        var prepareNchildstrCommand = function(grid, command, record, row, col, value)
        {
          command.command = command.command.replace('Detil', 'Link');
        };
      ";
            return new string[] { script };
        }
        public new string[] GetKeys()
        {
            return new string[] { "Kdgroup", "Idmenu", "Kdmenu"
        , "Stinsert", "Stupdate", "Stdelete", "Stload", "Stfilter"
        , "Status", "Statusicon", "Statusname", "Stricon"
        , "Last_by", "Last_date"
        , "Nmmenu", "Url", "Kdlevel", "Type" };
        }
        public new string[] GetCsvColumns(int mode)
        {
            return new string[] { "Kdmenu", "Nmmenu", "Stinsert", "Stupdate", "Stdelete", "Stload", "Stfilter", "Kdlevel", "Type" };
        }
        public new string[] GetLoadCsvColumns()
        {
            return new string[] { "Kdgroup", "Idmenu", "Stinsert", "Stupdate", "Stdelete", "Stload", "Stfilter", "Kdlevel", "Type" };
        }
        #endregion Methods 
    }
    #endregion Ss20groupmenuAset
}


<%@ Application Language="C#" %>

<script RunAt="server">

  void Application_Start(object sender, EventArgs e)
  {

    System.GC.Collect();
    CoreNET.Common.Base.MasterAppConstants.Instance.StatusTesting = (ConfigurationManager.AppSettings["Testing"] == "1");
    CoreNET.Common.Base.SysUtils Sys = new CoreNET.Common.Base.SysUtils();

    //CoreNET.Common.Base.AssemblyUtils.WriteBeginLog(true, 1);
    CoreNET.Common.Base.MasterAppConstants.Instance.SetValue(
    CoreNET.Common.Base.MasterAppConstants.URLBASE, ConfigurationManager.AppSettings["URLBase"]);
    CoreNET.Common.Base.SQLDataSource.Instance.SetDataSourceConfig(CoreNET.Common.Base.GlobalAsp.GetDataSourceConfig());
    IBatisNet.DataAccess.Configuration.DomDaoManagerBuilder builder = new IBatisNet.DataAccess.Configuration.DomDaoManagerBuilder();
    builder.Configure("dao.config");


    CoreNET.Common.Base.ConstantDict.Translate(Sys.GetType().Name);//Inisialisasi Dictionary
    //CoreNET.Common.Base.AssemblyUtils.WriteEndLog();
  }

  void Application_End(object sender, EventArgs e)
  {
  }

  void Application_Error(object sender, EventArgs e)
  {
  }

  void Session_Start(object sender, EventArgs e)
  {
  }

  void Session_End(object sender, EventArgs e)
  {
  }

</script>

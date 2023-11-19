<%@ Import Namespace="CoreNET.Common.Base" %>
<%@ Import Namespace="CoreNET.Common.BO" %>
<script runat="server" language="C#">

  public void Page_Load(Object sender, EventArgs e)
  {
    if (Request.QueryString["action"] != null)
    {
      Session["ScreenResolution"] = Request.QueryString["res"].ToString();
      Response.Redirect("Menu.aspx?app=" + Request["app"]);
    }
  }
</script>
<html>
<head>
  <title></title>
  <script type="text/javascript" language="javascript">
    /* Get Query String By Name */
    function getQSByName(name) {
      name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
      var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
      return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    }
  </script>
</head>
<body>
  <script language="javascript">
    res = "&res=" + screen.width + "x" + screen.height + "&d=" + screen.colorDepth
    top.location.href = "detectscreen.aspx?action=set" + res + "&page=" + getQSByName("page")
  </script>
</body>
</html>

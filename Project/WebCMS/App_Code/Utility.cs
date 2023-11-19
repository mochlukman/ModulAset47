using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using CoreNET.Common.Base;
using CoreNET.Common.BO;


/// <summary>
/// Summary description for Utility
/// </summary>
public class Utility
{
  public Utility()
  {
  }
  public static string ConstructCommentEntry(IDataControl ctrl, int LastAnchor, string cmte, DateTime curdate)
  {
    //IDataControlAppuserUI user = GlobalExt.GetSessionUser();
    string ce = string.Format(@"<div ID=""notes{0}"" class=""information""><p><h3><a id=""{1}"">#{2}</a> Tgl: {3} Oleh: {4}</h3>{5}</p></div>",
       LastAnchor, LastAnchor, LastAnchor, curdate, ((BaseBO)ctrl).Last_by, cmte);
    return ce;
  }

  public static string ConstructCoaching(IList msgs)
  {
    string rowHtml = string.Empty;
    int lastAnchor = -1;
    try
    {
      if (msgs.Count > 0)
      {
        rowHtml = string.Empty;
        lastAnchor = msgs.Count;
        foreach (NotesControl c in msgs)
        {
          rowHtml += ConstructCommentEntry(c, lastAnchor--, c.Notes, c.Tglnotes);
        }
      }
    }
    catch (Exception ex)
    {
      UtilityBO.Log(ex);
    }
    return rowHtml;
  }

  public static int getLastNotesNumber(string strCmt)
  {
    string notesMarker = @"<div ID=""notes";
    if (string.IsNullOrEmpty(strCmt)) return 0;
    int pos = strCmt.IndexOf(notesMarker) + notesMarker.Length;
    if ((pos >= 0) && (strCmt.Length > pos))
    {
      int postEnd = strCmt.IndexOf(" ", pos);
      string lastNoteNumberStr = strCmt.Substring(pos, postEnd - pos - 1);
      return int.Parse(lastNoteNumberStr);
    }
    return 0;
  }

}
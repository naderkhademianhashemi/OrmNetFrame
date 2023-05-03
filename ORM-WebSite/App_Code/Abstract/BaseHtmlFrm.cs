using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for BaseHtmlFrm
/// </summary>
public abstract class BaseHtmlFrm
{
    public ORM.Forms _local_form;
    public Panel output_panel = new Panel();
    public Page _sender;

}
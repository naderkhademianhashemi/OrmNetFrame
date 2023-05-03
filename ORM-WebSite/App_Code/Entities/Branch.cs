using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


/// <summary>
/// Summary description for Branch
/// </summary>
/// 
namespace ORM
{
    public class Branch : BaseBrnch
    {
        public Branch()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static DataTable get_Branchs()
        {
            var CN = new SQL_Connector();
            var SQL = "select Branch ,Branchname , State_of_branch , " +
                "branch_Rank , limit , [Description] "+
                " , BID , (cast(Branch as varchar) " +
                "+ ' - ' + Branchname)  as [title]  from branch";
            return CN.Select(SQL);
        }
        
    }
}
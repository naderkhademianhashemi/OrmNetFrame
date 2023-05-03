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
/// Summary description for ORM_Types
/// </summary>
/// 
namespace ORM
{
    public class ORM_Types : BaseOrmTyps
    {
        public static Question_Type Get_Question_Type(int qtype)
        {
            Question_Type output = Question_Type.Text;
            switch (qtype)
            {
                case 1:
                    output = Question_Type.Text;
                    break;
                case 2:
                    output = Question_Type.ComboBoxList;
                    break;
                case 3:
                    output = Question_Type.RadioButtonList;
                    break;
                case 4:
                    output = Question_Type.DropDownList;
                    break;
                case 5:
                    output = Question_Type.Number;
                    break;
                case 6:
                    output = Question_Type.Date;
                    break;
                case 7:
                    output = Question_Type.Table;
                    break;
                case 8:
                    output = Question_Type.ChckBxLstTxt;
                    break;
                case 9:
                    output = Question_Type.RdoBtnLstTxt;
                    break;
                case 10:
                    output = Question_Type.DrpDnLstTxt;
                    break;
            }

            return output;

        }

        public static Template_Type Get_Template_Type(int ttype)
        {
            Template_Type output = Template_Type.State;
            switch (ttype)
            {
                case 1:
                    output = Template_Type.State;
                    break;
                case 2:
                    output = Template_Type.Branch;
                    break;
                case 3:
                    output = Template_Type.Department;
                    break;
                case 4:
                    output = Template_Type.Manual;
                    break;
            }

            return output;

        }

        public static int Get_Question_Type(Question_Type qtype)
        {
            int output = 0;
            switch (qtype)
            {
                case Question_Type.Text:
                    output = 1;
                    break;
                case Question_Type.ComboBoxList:
                    output = 2;
                    break;
                case Question_Type.RadioButtonList:
                    output = 3;
                    break;
                case Question_Type.DropDownList:
                    output = 4;
                    break;
                case Question_Type.Number:
                    output = 5;
                    break;
                case Question_Type.Date:
                    output = 6;
                    break;
                case Question_Type.Table:
                    output = 7;
                    break;
                case Question_Type.ChckBxLstTxt:
                    output = 8;
                    break;
                case Question_Type.RdoBtnLstTxt:
                    output = 9;
                    break;
                case Question_Type.DrpDnLstTxt:
                    output = 10;
                    break;
            }

            return output;

        }

        public static int Get_Template_Type(Template_Type ttype)
        {
            int output = 4;
            switch (ttype)
            {
                case Template_Type.State:
                    output = 1;
                    break;
                case Template_Type.Branch:
                    output = 2;
                    break;
                case Template_Type.Department:
                    output = 3;
                    break;
                case Template_Type.Manual:
                    output = 4;
                    break;
            }

            return output;

        }

        public static String get_Col_Name(String Qid)
        {
            ORM.ORM_Types.Question_Type qtype = ORM.Question.get_type(Qid);
            ORM.ORM_Types.Template_Type ttype = ORM.Question.get_template(Qid);

            switch (qtype)
            {
                case ORM.ORM_Types.Question_Type.Number:
                    return "Number";

                case ORM.ORM_Types.Question_Type.Text:
                    return "Text";

                case ORM.ORM_Types.Question_Type.Date:
                    return "Date";

                case ORM.ORM_Types.Question_Type.ComboBoxList:
                case ORM.ORM_Types.Question_Type.DropDownList:
                case ORM.ORM_Types.Question_Type.RadioButtonList:
                case ORM.ORM_Types.Question_Type.ChckBxLstTxt:
                case ORM.ORM_Types.Question_Type.RdoBtnLstTxt:
                case ORM.ORM_Types.Question_Type.DrpDnLstTxt:
                    if (ttype == ORM.ORM_Types.Template_Type.Manual)
                        return "Selected_Item";
                    if (ttype == ORM.ORM_Types.Template_Type.Department)
                        return "Dep_Item";
                    if (ttype == ORM.ORM_Types.Template_Type.Branch)
                        return "Branch_Item";
                    if (ttype == ORM.ORM_Types.Template_Type.State)
                        return "State_Item";
                    return "";
            }
            return "";
        }

        public static Index_Type Get_Index_Type(int qtype)
        {
            Index_Type output = Index_Type.ASC;
            switch (qtype)
            {
                case 1:
                    output = Index_Type.ASC;
                    break;
                case 2:
                    output = Index_Type.DES;
                    break;
                case 3:
                    output = Index_Type.REL;
                    break;
            }

            return output;

        }

        public static int Get_Index_Type(Index_Type qtype)
        {
            int output = 0;
            switch (qtype)
            {
                case Index_Type.ASC:
                    output = 1;
                    break;
                case Index_Type.DES:
                    output = 2;
                    break;
                case Index_Type.REL:
                    output = 3;
                    break;
            }

            return output;

        }

        public static Bound_Type Get_Bound_Type(int qtype)
        {
            Bound_Type output = Bound_Type.Goal;
            switch (qtype)
            {
                case 1:
                    output = Bound_Type.Goal;
                    break;
                case 2:
                    output = Bound_Type.Alarm;
                    break;
                case 3:
                    output = Bound_Type.Danger;
                    break;
            }

            return output;

        }

        public static int Get_Bound_Type(Bound_Type qtype)
        {
            int output = 0;
            switch (qtype)
            {
                case Bound_Type.Goal:
                    output = 1;
                    break;
                case Bound_Type.Alarm:
                    output = 2;
                    break;
                case Bound_Type.Danger:
                    output = 3;
                    break;
            }

            return output;

        }

        public ORM_Types()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
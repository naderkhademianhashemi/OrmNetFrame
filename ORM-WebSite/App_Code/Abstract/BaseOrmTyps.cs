namespace ORM
{
    public abstract class BaseOrmTyps
    {
        public enum Question_Type
        {
            Text,
            ComboBoxList,
            RadioButtonList,
            DropDownList,
            Number,
            Date,
            Table,
            ChckBxLstTxt,
            RdoBtnLstTxt,
            DrpDnLstTxt,
            Table2
        };


        public enum Template_Type { State, Branch, Department, Manual };

        public enum Index_Type { ASC, DES, REL };

        public enum Bound_Type { Goal, Danger, Alarm };
    }
}
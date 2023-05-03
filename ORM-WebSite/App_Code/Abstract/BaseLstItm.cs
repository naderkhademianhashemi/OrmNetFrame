using System;
using System.Data;

namespace ORM
{
    public abstract class BaseLstItm
    {
        public String _item_id = "";
        public String _item_text = "";
        public String _list_id = "";
        public Boolean _item_option = false;
        public Boolean _item_filled = false;
        public Int16 _item_type = 0;
        public Int32 _item_table = 0;

        public String Item_ID
        {
            get { return _item_id; }
            set { _item_id = value; }
        }

        public String Item_Text
        {
            get { return _item_text; }
            set { _item_text = value; }
        }

        public String List_ID
        {
            get { return _list_id; }
            set { _list_id = value; }
        }
        public Boolean Item_Option
        {
            get { return _item_option; }
            set { _item_option = value; }
        }
        public Int16 Item_Type
        {
            get { return _item_type; }
            set { _item_type = value; }
        }
        public Boolean Item_Filled
        {
            get { return _item_filled; }
            set { _item_filled = value; }
        }
        public Int32 Item_Table
        {
            get { return _item_table; }
            set { _item_table = value; }
        }
        public DataTable Item_Tables
        {
            get
            {
                SQL_Connector connector = new SQL_Connector();
                String sql_query = "Select ID,Name,Fname From dbo.Tables";
                return connector.Select(sql_query);
            }
        }

    }
}
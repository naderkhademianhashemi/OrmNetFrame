using System.Collections.Generic;
using System;

namespace ORM
{
    public abstract class BaseLst
    {
        public String _list_id = "";
        public String _question_id = "";
        public String _description = "";
        public List<ORM.List_Items> _list_items;

        public String List_ID
        {
            get { return _list_id; }
            set { _list_id = value; }
        }

        public String Question_ID
        {
            get { return _question_id; }
            set { _question_id = value; }
        }

        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public List<ORM.List_Items> List_Items
        {
            get { return _list_items; }
            set { _list_items = value; }
        }
    }
}
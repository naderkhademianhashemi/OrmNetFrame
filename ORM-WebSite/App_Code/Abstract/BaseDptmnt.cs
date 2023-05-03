using System;

namespace ORM
{
    public abstract class BaseDptmnt
    {
        public String _dep_id = "";
        public String _dep_name = "";
        public String _description = "";
        public String Dep_ID
        {
            get { return _dep_id; }
            set { _dep_id = value; }
        }

        public String Dep_Name
        {
            get { return _dep_name; }
            set { _dep_name = value; }
        }

        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
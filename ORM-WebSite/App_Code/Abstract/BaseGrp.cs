using System.Collections.Generic;
using System;

namespace ORM
{
    public abstract class BaseGrp
    {
        public String _group_id;
        public String _group_name;
        public String _group_description;
        public List<Group_Permissions> _group_permissions;
        public List<string> _group_users;

        public List<string> Group_Users
        {
            get { return _group_users; }
            set { _group_users = value; }
        }

        public List<Group_Permissions> Group_Permissions
        {
            get { return _group_permissions; }
            set { _group_permissions = value; }
        }
    }
}
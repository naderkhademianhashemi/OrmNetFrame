using System;

namespace ORM
{
    public abstract class BaseState
    {
        public String _state_id = "";
        public String _state_name = "";
        public String _flashcode = "";
        public String _description = "";

        public String State_ID
        {
            get { return _state_id; }
            set { _state_id = value; }
        }

        public String State_Name
        {
            get { return _state_name; }
            set { _state_name = value; }
        }

        public String FlashCode
        {
            get { return _flashcode; }
            set { _flashcode = value; }
        }

        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }

    }
}
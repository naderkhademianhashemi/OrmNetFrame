using System;

namespace ORM
{
    public abstract class BaseBrnch
    {
        public String _branch_id = "";
        public String _branch_code = "";
        public String _branch_name = "";
        public int _state_of_branch = 0;
        public int _branch_rank = 0;
        public int _limit = 0;
        public String _description = "";
        public String Branch_ID
        {
            get { return _branch_id; }
            set { _branch_id = value; }
        }

        public String Branch_Code
        {
            get { return _branch_code; }
            set { _branch_code = value; }
        }

        public String Branch_Name
        {
            get { return _branch_name; }
            set { _branch_name = value; }
        }

        public int State_Of_Branch
        {
            get { return _state_of_branch; }
            set { _state_of_branch = value; }
        }

        public int Branch_Rank
        {
            get { return _branch_rank; }
            set { _branch_rank = value; }
        }

        public int Limit
        {
            get { return _limit; }
            set { _limit = value; }
        }

        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
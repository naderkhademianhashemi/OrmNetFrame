using System.Collections.Generic;
using System;

namespace ORM
{
    public abstract class BaseFrmQueInstnc
    {
        public Form_Instance _form_instance;
        public String _question_id;
        public String _item_id;
        public String _answer_id;
        public String _text_field;
        public String _date_field;
        public String _number_field;
        public List<string> _list_choice;
        public List<string> _state_choice;
        public List<string> _branch_choice;
        public List<string> _dep_choice;

        public String Text_Field
        {
            get { return _text_field; }
            set { _text_field = value; }
        }

        public String Date_Field
        {
            get { return _date_field; }
            set { _date_field = value; }
        }

        public String Number_filed
        {
            get { return _number_field; }
            set { _number_field = value; }
        }

        public List<string> List_Choice
        {
            get { return _list_choice; }
            set { _list_choice = value; }
        }

        public List<string> State_Choice
        {
            get { return _state_choice; }
            set { _state_choice = value; }
        }

        public List<string> Branch_Choice
        {
            get { return _branch_choice; }
            set { _branch_choice = value; }
        }

        public List<string> Dep_Choice
        {
            get { return _dep_choice; }
            set { _dep_choice = value; }
        }

        public String Question_ID
        {
            get { return _question_id; }
            set { _question_id = value; }
        }
        public String Answer_ID
        {
            get { return _answer_id; }
            set { _answer_id = value; }
        }
        public String Item_ID
        {
            get { return _item_id; }
            set
            {
                _item_id = value;

            }

        }
    }
}
using System;

namespace ORM
{
    public abstract class BaseGrpPrm
    {
        public String _form_id;
        public String _group_id;
        public Boolean _readable;
        public Boolean _writable;
        public Boolean _updatable;
        public Boolean _deleteable;
        public Boolean _addQuestion;
        public Boolean _ReadOtherAnswers;
    }
}
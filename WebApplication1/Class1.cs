using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class Class1
    {
        private string _uid= string.Empty;
        private string _psd = string.Empty;
        private string _yid = string.Empty;
        private string _msg = string.Empty;

        protected string Uid
        {
            set { _uid = value; }
            get { return _uid; }
        }
        protected string Psd
        {
            set { _psd = value; }
            get { return _psd; }
        }
        protected string Yid
        {
            set { _yid = value; }
            get { return _yid; }
        }
        protected string Msg
        {
            set { _msg = value; }
            get { return _msg; }
        }
    }
}
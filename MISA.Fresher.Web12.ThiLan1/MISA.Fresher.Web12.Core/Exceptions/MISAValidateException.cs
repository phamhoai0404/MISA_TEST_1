using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Fresher.Web12.Core.Exceptions
{
    public class MISAValidateException: Exception
    {
        string? MsgErrorValiate = null;

        public MISAValidateException(string msg)
        {
            this.MsgErrorValiate = msg;
        }

        public override string Message
        {
            get
            {
                return this.MsgErrorValiate;
            }
        }
    }
}

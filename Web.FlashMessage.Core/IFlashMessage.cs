using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vereyon.Web
{
    public interface IFlashMessage
    {

        void Queue(string message);
    }
}

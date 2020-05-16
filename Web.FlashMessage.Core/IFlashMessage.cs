using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vereyon.Web
{
    public interface IFlashMessage
    {

        /// <summary>
        /// Queues a flash message for display with the specified message.
        /// </summary>
        /// <param name="flashMessage"></param>
        void Queue(FlashMessageModel flashMessage);
    }
}

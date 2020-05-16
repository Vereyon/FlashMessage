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

        /// <summary>
        /// Retrieves the queued flash messages for display and clears them.
        /// </summary>
        /// <returns></returns>
        List<FlashMessageModel> Retrieve();

        /// <summary>
        /// Retrieves the queued messages for the current response without clearing them.
        /// </summary>
        /// <returns></returns>
        List<FlashMessageModel> Peek();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vereyon.Web
{
    /// <summary>
    /// IFlashMessageTransport defines a common interface for transporting flash messages between requests.
    /// </summary>
    public interface IFlashMessageTransport
    {

        /// <summary>
        /// Queues the flash message data. Pass NULL or empty string to erase.
        /// </summary>
        /// <param name="data"></param>
        void Queue(IList<FlashMessageModel> messages);

        /// <summary>
        /// Returns a list of the queued flash messages.
        /// </summary>
        /// <remarks>Difference from Peek() is that these are the messages queued in the current response, while Peek() and Retrieve() deal with messages which were queued in the previous response.</remarks>
        /// <returns></returns>
        List<FlashMessageModel> GetQueued();

        /// <summary>
        /// Retrieves and clears the flash message data.
        /// </summary>
        /// <returns>A string containing the serialized flash messages, or and empty string in case not flash messages are available.</returns>
        List<FlashMessageModel> Retrieve();

        /// <summary>
        /// Retrives the flash message data without clearing it.
        /// </summary>
        /// <returns></returns>
        List<FlashMessageModel> Peek();
    }
}

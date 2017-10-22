using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vereyon.Web
{
    public interface IFlashMessage
    {

        /// <summary>
        /// Queues a flash message for display with the specified message for display as an info notification.
        /// </summary>
        /// <param name="message"></param>
        void Queue(string message);

        /// <summary>
        /// Queues a flash message for display with the specified message and title for display as an info notification.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        void Queue(string title, string message);

        /// <summary>
        /// Queues a flash message for display with the specified message.
        /// </summary>
        /// <param name="message"></param>
        void Queue(FlashMessageType messageType, string message);

        /// <summary>
        /// Queues a flash message for display with the specified message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="messageType"></param>
        /// <param name="isHtml"></param>
        void Queue(FlashMessageType messageType, string message, string title, bool isHtml = false);

        /// <summary>
        /// Queues the passed message as an informational message.
        /// </summary>
        /// <param name="message"></param>
        void Info(string message);

        /// <summary>
        /// Formats and queues the passed message as an informational message.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void Info(string format, params object[] args);

        /// <summary>
        /// Formats and queues the passed message as an informational message with title.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void Info(string title, string format, params object[] args);

        /// <summary>
        /// Queues the passed message as a warning message.
        /// </summary>
        /// <param name="message"></param>
        void Warning(string message);

        /// <summary>
        /// Queues the passed message as a warning message with title.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        void Warning(string title, string message);

        /// <summary>
        /// Formats and queues the passed message as a warning message.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void Warning(string format, params object[] args);

        /// <summary>
        /// Formats and queues the passed message as a warning message with title.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void Warning(string title, string format, params object[] args);

        /// <summary>
        /// Queues the passed message as a danger message.
        /// </summary>
        /// <param name="message"></param>
        void Danger(string message);

        /// <summary>
        /// Queues the passed message as a danger message with title.
        /// </summary>
        /// <param name="message"></param>
        void Danger(string title, string message);

        /// <summary>
        /// Formats and queues the passed message as a danger message.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void Danger(string format, params object[] args);

        /// <summary>
        /// Formats and queues the passed message as a danger message with title.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void Danger(string title, string format, params object[] args);

        /// <summary>
        /// Queues the passed message as a confirmation message.
        /// </summary>
        /// <param name="message"></param>
        void Confirmation(string message);

        /// <summary>
        /// Queues the passed message as a confirmation message with title.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        void Confirmation(string title, string message);

        /// <summary>
        /// Formats and queues the passed message as a confirmation message.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void Confirmation(string format, params object[] args);

        /// <summary>
        /// Formats and queues the passed message as a confirmation message with title.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        void Confirmation(string title, string format, params object[] args);
    }
}

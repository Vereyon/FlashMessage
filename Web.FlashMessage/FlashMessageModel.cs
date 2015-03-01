using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vereyon.Web
{
    /// <summary>
    /// The FlashMessageModel class represents an individual flash message.
    /// </summary>
    /// <remarks>
    /// Based on http://blogs.us.sogeti.com/swilliams/2010/05/03/extensibility-in-aspnet-mvc-via-messaging/
    /// </remarks>
    public class FlashMessageModel
    {

        /// <summary>
        /// Gets / sets if the message contains raw HTML. If set to true, the title will not be rendered and the contents of the message parameter will
        /// be written directly to the output.
        /// </summary>
        public bool IsHtml { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }

        public FlashMessageType Type { get; set; }

        public FlashMessageModel()
        {
            IsHtml = false;
        }
    }

    /// <summary>
    /// The FlashMessageType enum indicates the type of flash message.
    /// </summary>
    public enum FlashMessageType : byte
    {
        Info,
        Warning,
        Danger,
        Confirmation,

        /// <summary>
        /// Custom css code.
        /// </summary>
        Custom = 255
    }
}

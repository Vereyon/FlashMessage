using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net;

namespace Vereyon.Web
{
    /// <summary>
    /// Renders flash messages as Twitter Bootstrap Alerts
    /// </summary>
    [OutputElementHint("div")]
    [HtmlTargetElement("flash", TagStructure = TagStructure.WithoutEndTag)]
    public class FlashTagHelper : TagHelper
    {

        private IFlashMessage _flashMessage;
        private IFlashMessageSerializer _serializer;

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        /// <summary>
        /// Gets / sets if the message should be dismissible.
        /// </summary>
        public bool Dismissable { get; set; } = false;

        public FlashTagHelper(IFlashMessage flashMessage, IFlashMessageSerializer serializer)
        {
            _flashMessage = flashMessage;
            _serializer = serializer;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            // Ensure both a start and end tag are generated.
            output.TagMode = TagMode.StartTagAndEndTag;

            var messages = Retrieve();
            foreach(var message in messages)
            {
                var html = RenderFlashMessage(message);
                output.Content.AppendHtml(html);
            }
        }

        protected List<FlashMessageModel> Retrieve()
        {

            // Retrieve the data from the session store, guard for cases where it does not exist.
            var data = ViewContext.TempData[FlashMessage.KeyName];
            if (data == null)
                return new List<FlashMessageModel>();

            // Clear the data and return.
            ViewContext.TempData.Remove(FlashMessage.KeyName);
            return _serializer.Deserialize((string)data);
        }

        /// <summary>
        /// Renders the passed flash message as a Twitter Bootstrap alert component.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="dismissable">Indicates if this message should be dismissable</param>
        /// <returns></returns>
        protected HtmlString RenderFlashMessage(FlashMessageModel message)
        {
            var cssClasses = GetCssStyle(message.Type);
            if (Dismissable)
                cssClasses += " alert-dismissible";
            string result = $"<div class=\"{cssClasses}\" role=\"alert\">\r\n";

            if (Dismissable)
                result += "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>\r\n";

            if (!string.IsNullOrWhiteSpace(message.Title))
                result += "<strong>" + WebUtility.HtmlEncode(message.Title) + "</strong> ";

            if (message.IsHtml)
                result += message.Message;
            else
                result += WebUtility.HtmlEncode(message.Message);

            result += "</div>";
            return new HtmlString(result);
        }

        /// <summary>
        /// Returns the Twitter bootstrap css style for the passed message type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GetCssStyle(FlashMessageType type)
        {
            switch (type)
            {
                case FlashMessageType.Danger:
                    return "alert alert-danger";
                default:
                case FlashMessageType.Info:
                    return "alert alert-info";
                case FlashMessageType.Warning:
                    return "alert alert-warning";
                case FlashMessageType.Confirmation:
                    return "alert alert-success";
            }
        }
    }
}

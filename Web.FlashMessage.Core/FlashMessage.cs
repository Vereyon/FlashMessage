using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Vereyon.Web
{
    public class FlashMessage : IFlashMessage
    {


        public ITempDataDictionary _tempData;

        public ITempDataDictionary TempData
        {
            protected set { _tempData = value; }
            get
            {
                if (_tempData == null)
                    _tempData = _tempDataFactory.GetTempData(_httpContextAccessor.HttpContext);

                return _tempData;
            }
        }

        private ITempDataDictionaryFactory _tempDataFactory;

        private IHttpContextAccessor _httpContextAccessor;

        private IFlashMessageSerializer _messageSerializer;

        public FlashMessage(ITempDataDictionaryFactory tempDataFactory, IHttpContextAccessor httpContextAccessor, 
            IFlashMessageSerializer messageSerializer)
        {

            _tempDataFactory = tempDataFactory;
            _httpContextAccessor = httpContextAccessor;
            _messageSerializer = messageSerializer;
        }

        public static string KeyName { get; set; } = "_FlashMessage";

        /// <summary>
        /// Queues a flash message for display with the specified message for display as an info notification.
        /// </summary>
        /// <param name="message"></param>
        public void Queue(string message)
        {
            Queue(FlashMessageType.Info, message, string.Empty, false);
        }

        /// <summary>
        /// Queues a flash message for display with the specified message and title for display as an info notification.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public void Queue(string title, string message)
        {
            Queue(FlashMessageType.Info, message, title);
        }

        /// <summary>
        /// Queues a flash message for display with the specified message.
        /// </summary>
        /// <param name="message"></param>
        public void Queue(FlashMessageType messageType, string message)
        {
            Queue(messageType, message, string.Empty);
        }

        /// <summary>
        /// Queues a flash message for display with the specified message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="messageType"></param>
        /// <param name="isHtml"></param>
        public void Queue(FlashMessageType messageType, string message, string title, bool isHtml = false)
        {

            // Append the new message.
            var flashMessage = new FlashMessageModel { IsHtml = isHtml, Message = message, Title = title, Type = messageType };
            Queue(flashMessage);
        }


        /// <summary>
        /// Queues the passed flash message for display.
        /// </summary>
        /// <param name="message"></param>
        public void Queue(FlashMessageModel message)
        {

            List<FlashMessageModel> messages;

            // Retrieve the currently queued cookies.
            messages = Queued(TempData);

            // Append the new message.
            messages.Add(message);

            // Store the messages.
            Store(messages);
        }

        #region Shorthand Methods
        
        /// <summary>
        /// Queues the passed message as an infomational message.
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message)
        {
            Queue(FlashMessageType.Info, message);
        }

        /// <summary>
        /// Formats and queues the passed message as an infomational message.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void Info(string format, params object[] args)
        {

            var message = string.Format(format, args);
            Queue(FlashMessageType.Info, message);
        }

        /// <summary>
        /// Formats and queues the passed message as an infomational message with title.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void Info(string title, string format, params object[] args)
        {

            var message = string.Format(format, args);
            Queue(FlashMessageType.Info, message, title);
        }

        /// <summary>
        /// Queues the passed message as a warning message.
        /// </summary>
        /// <param name="message"></param>
        public void Warning(string message)
        {
            Queue(FlashMessageType.Warning, message);
        }

        /// <summary>
        /// Queues the passed message as a warning message with title.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public void Warning(string title, string message)
        {
            Queue(FlashMessageType.Warning, message, title);
        }

        /// <summary>
        /// Formats and queues the passed message as a warning message.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void Warning(string format, params object[] args)
        {

            var message = string.Format(format, args);
            Queue(FlashMessageType.Warning, message);
        }

        /// <summary>
        /// Formats and queues the passed message as a warning message with title.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void Warning(string title, string format, params object[] args)
        {

            var message = string.Format(format, args);
            Queue(FlashMessageType.Warning, message, title);
        }

        /// <summary>
        /// Queues the passed message as a danger message.
        /// </summary>
        /// <param name="message"></param>
        public void Danger(string message)
        {
            Queue(FlashMessageType.Danger, message);
        }

        /// <summary>
        /// Queues the passed message as a danger message with title.
        /// </summary>
        /// <param name="message"></param>
        public void Danger(string title, string message)
        {
            Queue(FlashMessageType.Danger, message, title);
        }

        /// <summary>
        /// Formats and queues the passed message as a danger message.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void Danger(string format, params object[] args)
        {

            var message = string.Format(format, args);
            Queue(FlashMessageType.Danger, message);
        }

        /// <summary>
        /// Formats and queues the passed message as a danger message with title.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void Danger(string title, string format, params object[] args)
        {

            var message = string.Format(format, args);
            Queue(FlashMessageType.Danger, message, title);
        }

        /// <summary>
        /// Queues the passed message as a confirmation message.
        /// </summary>
        /// <param name="message"></param>
        public void Confirmation(string message)
        {
            Queue(FlashMessageType.Confirmation, message);
        }

        /// <summary>
        /// Queues the passed message as a confirmation message with title.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public void Confirmation(string title, string message)
        {
            Queue(FlashMessageType.Confirmation, message, title);
        }

        /// <summary>
        /// Formats and queues the passed message as a confirmation message.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void Confirmation(string format, params object[] args)
        {

            var message = string.Format(format, args);
            Queue(FlashMessageType.Confirmation, message);
        }

        /// <summary>
        /// Formats and queues the passed message as a confirmation message with title.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void Confirmation(string title, string format, params object[] args)
        {

            var message = string.Format(format, args);
            Queue(FlashMessageType.Confirmation, message, title);
        }
        
        #endregion

        

        /// <summary>
        /// Queues the passed flash messages for display, replacing any queued messages.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="messages"></param>
        private void Store(IList<FlashMessageModel> messages)
        {

            // Serialize the messages.
            var data = _messageSerializer.Serialize(messages);

            // Set the data without doing any further securing or transformations.
            TempData[KeyName] = data;
        }

        /// <summary>
        /// Retrieves the queued flash messages for display and clears them.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public List<FlashMessageModel> Retrieve(ITempDataDictionary dictionary)
        {

            // Retrieve the data from the session store, guard for cases where it does not exist.
            var data = dictionary[KeyName];
            if (data == null)
                return new List<FlashMessageModel>();

            // Clear the data and return.
            dictionary.Remove(KeyName);
            return _messageSerializer.Deserialize((string)data);
        }

        /// <summary>
        /// Retrieves the queued messages for the current response without clearing them.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public List<FlashMessageModel> Queued(ITempDataDictionary dictionary)
        {

            // For this transport Peek() and Queued() have equal implementations.
            return Peek(dictionary);
        }

        public List<FlashMessageModel> Peek(ITempDataDictionary dictionary)
        {

            // Retrieve the data from the session store, guard for cases where it does not exist.
            var data = dictionary.Peek(KeyName);
            if (data == null)
                return new List<FlashMessageModel>();

            // Deserialize messages and return them.
            return _messageSerializer.Deserialize((string)data);
        }
    }
}

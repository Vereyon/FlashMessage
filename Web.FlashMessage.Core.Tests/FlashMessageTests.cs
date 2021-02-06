using System.Collections.Generic;
using Xunit;
using Moq;
using Vereyon.Web;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http;

namespace Web.FlashMessage.Core.Tests
{
    public class FlashMessageBaseTests
    {

        private Mock<ITempDataDictionary> _tempDataMock;

        public FlashMessageBaseTests()
        {

            _tempDataMock = new Mock<ITempDataDictionary>();
        }

        /// <summary>
        /// Tests queuing a single flash message
        /// </summary>
        [Fact]
        public void Queue()
        {

            var tempData = new Mock<ITempDataDictionaryFactory>();
            tempData.Setup(x => x.GetTempData(It.IsAny<HttpContext>())).Returns(_tempDataMock.Object);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var serializer = new Mock<IFlashMessageSerializer>();
            var flashMessage = new Vereyon.Web.FlashMessage(tempData.Object, contextAccessor.Object, serializer.Object);

            // Queue a message
            flashMessage.Queue(FlashMessageType.Confirmation, "Test");

            // Verify the message was serialized, and that it was stored in the temp data dictionary.
            serializer.Verify(x => x.Serialize(It.IsAny<List<FlashMessageModel>>()), Times.Once);
            _tempDataMock.VerifySet(x => x["_FlashMessage"] = It.IsAny<string>(), Times.Once);
        }

        [Fact]
        public void QueueExtensionMethods()
        {

            var tempData = new Mock<ITempDataDictionaryFactory>();
            tempData.Setup(x => x.GetTempData(It.IsAny<HttpContext>())).Returns(_tempDataMock.Object);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var serializer = new Mock<IFlashMessageSerializer>();
            var flashMessage = new Vereyon.Web.FlashMessage(tempData.Object, contextAccessor.Object, serializer.Object);

            flashMessage.Info("Test");
            flashMessage.Confirmation("Test");
            flashMessage.Warning("Test");
            flashMessage.Danger("Test");
        }
    }
}

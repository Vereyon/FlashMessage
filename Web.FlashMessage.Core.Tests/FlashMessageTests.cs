using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Vereyon.Web;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Http;

namespace Web.FlashMessage.Core.Tests
{
    public class FlashMessageTests
    {

        private Mock<ITempDataDictionary> _tempDataMock;

        public FlashMessageTests()
        {

            _tempDataMock = new Mock<ITempDataDictionary>();
        }

        [Fact]
        public void Queue()
        {

            var tempData = new Mock<ITempDataDictionaryFactory>();
            tempData.Setup(x => x.GetTempData(It.IsAny<HttpContext>())).Returns(_tempDataMock.Object);
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var flashMessage = new Vereyon.Web.FlashMessage(tempData.Object, contextAccessor.Object);

            flashMessage.Info("Test");
        }
    }
}

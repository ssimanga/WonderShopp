using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using WonderShopp.Core.Contracts;
using WonderShopp.Core.Models;
using WonderShopp.Core.ViewModels;
using WonderShopp.UI;
using WonderShopp.UI.Controllers;

namespace WonderShopp.UI.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
       public void IndexReturnProduct()
        {
            IRepository<Product> productContext = new Mocks.MockContext<Product>();
            IRepository<ProductCategory> categoryContext = new Mocks.MockContext<ProductCategory>();
            HomeController controller = new HomeController(productContext, categoryContext);
            productContext.Insert(new Product());

            var result = controller.Index() as ViewResult;
            var viewModel = (ProductListVM)result.ViewData.Model;

            Assert.AreEqual(1, viewModel.Products.Count());

        }

     
    }
}

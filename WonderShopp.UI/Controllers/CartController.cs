using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WonderShopp.Core.Contracts;
using WonderShopp.Core.Models;
using WonderShopp.Services;

namespace WonderShopp.UI.Controllers
{
    public class CartController : Controller
    {
        ICartService cartService;
        IOrderService orderService;
        IRepository<Customer> customers;
        public CartController(ICartService CartService, IOrderService OrderService, IRepository<Customer> Customers)
        {
            this.cartService = CartService;
            this.orderService = OrderService;
            this.customers = Customers;
        }
        public ActionResult Index()
        {
            var model = cartService.GetCartItems(this.HttpContext);
            return View(model);
        }

        public ActionResult AddToCart(string Id) {
            cartService.AddToCart(this.HttpContext, Id);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(string Id)
        {
            cartService.RemoveFromCart(this.HttpContext, Id);
            return RedirectToAction("Index");
        }
        public PartialViewResult CartSummary()
        {
            var cartSummary = cartService.GetCartSummary(this.HttpContext);
            return PartialView(cartSummary);
        }
        [Authorize]
        public ActionResult Checkout()
        {
          Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);
            if(customer != null)
            {
                Order order = new Order()
                {
                    Email = customer.Email,
                    State = customer.State,
                    Street = customer.Street,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    ZipCode = customer.ZipCode
                };
                return View(order);

            }
            else
            {
                return RedirectToAction("Error");
            }
            
        }
        [HttpPost]
        public ActionResult Checkout(Order order)
        {
            var cartItems = cartService.GetCartItems(this.HttpContext);
            order.OrderStatus = "Order Created";
            order.Email = User.Identity.Name;
            //process the payment via 3rd
            order.OrderStatus = "Payment Processed";
            orderService.CreateOrder(order, cartItems);
            cartService.ClearCart(this.HttpContext);
            return RedirectToAction("Thankyou", new {OrderId = order.Id});
        }
        public ActionResult Thankyou(string OrderId)
        {
            ViewBag.OrderId = OrderId;
            return View();
        }
    }
}
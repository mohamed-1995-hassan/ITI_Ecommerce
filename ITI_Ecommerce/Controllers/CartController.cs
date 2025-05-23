﻿using ITI_Ecommerce.IRepositories;
using ITI_Ecommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Ecommerce.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<IActionResult> AddItem(int bookId, int quantity=1, int redirect=0)
        {
            var cartCount = await _cartRepository.AddItem(bookId, quantity);
            if (redirect == 0)
                return Ok(cartCount);
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> RemoveItem(int bookId)
        {
            var cartCount = await _cartRepository.RemoveItem(bookId);
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartRepository.GetUserCart();
            return View(cart);
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            var count = await _cartRepository.GetCartItemCount();
            return Ok(count);
        }

        public async Task<IActionResult> Checkout()
        {
            var card = await _cartRepository.GetUserCart();
            var checkoutDisplay = new CheckoutDisplayModel
            {
                CartDetails = card.CartDetails
            };
            return View(checkoutDisplay);
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutDisplayModel model)
        {
            ModelState.Remove("CartDetails");
            if (!ModelState.IsValid)
            {
                var card = await _cartRepository.GetUserCart();
                model.CartDetails = card.CartDetails;
                return View(model);
            }
            bool isCheckedOut = await _cartRepository.DoCheckout(model.CheckoutModel);
            if (!isCheckedOut)
                return RedirectToAction(nameof(OrderFailure));
            return RedirectToAction(nameof(OrderSuccess));
        }

        public IActionResult OrderSuccess()
        {
            return View();
        }

        public IActionResult OrderFailure()
        {
            return View();
        }
    }
}

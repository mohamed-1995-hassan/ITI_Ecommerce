using ITI_Ecommerce.Constants;
using ITI_Ecommerce.IRepositories;
using ITI_Ecommerce.Models;
using ITI_Ecommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Ecommerce.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class CouponController : Controller
    {
        private readonly ICouponRepository _couponRepository;

        public CouponController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public async Task<IActionResult> Index()
        {
            var genres = await _couponRepository.GetCoupon();
            return View(genres);
        }

        public IActionResult AddCoupon()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCoupon(CouponDto couponDto)
        {
            if (!ModelState.IsValid)
            {
                return View(couponDto);
            }
            try
            {
                var couponToAdd = new Coupon
                {
                    Code = couponDto.Code,
                    DisCountAmount = couponDto.DisCountAmount,
                    ExpiryDate = couponDto.ExpiryDate,
                    MaximumTimeUsed = couponDto.MaximumTimeUsed
                };
                await _couponRepository.AddCoupon(couponToAdd);
                TempData["successMessage"] = "Genre added successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Genre could not added!";
                return View(couponDto);
            }

        }

        public async Task<IActionResult> UpdateCoupon(int id)
        {
            var coupon = await _couponRepository.GetCouponById(id);
            if (coupon is null)
                throw new InvalidOperationException($"Coupon with id: {id} does not found");
            var couponToUpdate = new CouponDto
            {
                Id = coupon.Id,
                Code = coupon.Code,
                MaximumTimeUsed = coupon.MaximumTimeUsed,
                DisCountAmount = coupon.DisCountAmount,
                ExpiryDate = coupon.ExpiryDate
            };
            return View(couponToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCoupon(CouponDto couponToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(couponToUpdate);
            }
            try
            {
                var genre = new Coupon
                {
                   Id = couponToUpdate.Id,
                   Code = couponToUpdate.Code,
                   DisCountAmount = couponToUpdate.DisCountAmount,
                   ExpiryDate = couponToUpdate.ExpiryDate,
                   MaximumTimeUsed = couponToUpdate.MaximumTimeUsed
                };
                await _couponRepository.UpdateCoupon(genre);
                TempData["successMessage"] = "Coupon is updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Genre could not updated!";
                return View(couponToUpdate);
            }

        }

        public async Task<IActionResult> DeleteCoupon(int id)
        {
            var coupon = await _couponRepository.GetCouponById(id);
            if (coupon is null)
                throw new InvalidOperationException($"Coupon with id: {id} does not found");
            await _couponRepository.DeleteCoupon(coupon);
            return RedirectToAction(nameof(Index));

        }
    }
}

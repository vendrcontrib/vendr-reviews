using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Vendr.Core;
using Vendr.Contrib.Reviews.Web.Dtos;
using Vendr.Core.Exceptions;
using Vendr.Core.Web.Api;
using Vendr.Contrib.Reviews.Services;
using Vendr.Contrib.Reviews.Models;
using System.Configuration;
using System.Net;
using System;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using Vendr.Core.Models;

namespace Vendr.Contrib.Reviews.Web.Controllers
{
    public class VendrReviewsController : SurfaceController, IRenderController
    {
        private readonly IVendrApi _vendrApi;
        private readonly IReviewService _reviewService;

        public VendrReviewsController(IVendrApi vendrAPi, IReviewService reviewService)
        {
            _vendrApi = vendrAPi;
            _reviewService = reviewService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddReview(AddReviewDto dto)
        {
            try
            {
                ValidateCaptcha();

                using (var uow = _vendrApi.Uow.Create())
                {
                    var review = new Review(dto.StoreId, dto.ProductReference, dto.CustomerReference)
                    {
                        Rating = dto.Rating,
                        Title = dto.Title,
                        Email = dto.Email,
                        Name = dto.Name,
                        Body = dto.Body,
                        RecommendProduct = dto.RecommendProduct
                    };

                    _reviewService.SaveReview(review);

                    uow.Complete();
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("", "Failed to submit review: " + ex.Message);

                return CurrentUmbracoPage();
            }

            TempData["SuccessMessage"] = "Review successfully submitted";

            return RedirectToCurrentUmbracoPage();
        }

        private void ValidateCaptcha()
        {
            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["VendrReviews:hCaptcha:SecretKey"])
                && !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["VendrReviews:hCaptcha:SiteKey"])
                && !string.IsNullOrWhiteSpace(Request.Form["h-captcha-response"]))
            {
                try
                {
                    var postData = $"response={Request.Form["h-captcha-response"]}&secret={ConfigurationManager.AppSettings["VendrReviews:hCaptcha:SecretKey"]}&sitekey={ConfigurationManager.AppSettings["VendrReviews:hCaptcha:SiteKey"]}";
                    var byteArray = Encoding.UTF8.GetBytes(postData);

                    var request = (HttpWebRequest)WebRequest.Create("https://hcaptcha.com/siteverify");
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.Accept = "application/json";
                    request.Method = "POST";
                    request.ContentLength = byteArray.Length;

                    var dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();

                    var response = (HttpWebResponse)request.GetResponse();
                    if (response.StatusDescription == "OK")
                    {
                        var responseStream = response.GetResponseStream();

                        using (var reader = new StreamReader(responseStream))
                        {
                            var responseFromServer = reader.ReadToEnd();
                            var data = JObject.Parse(responseFromServer);

                            if (data["success"].Value<bool>() == false)
                            {
                                _vendrApi.Log.Info<VendrReviewsController>("Failed hCaptcha validation with error codes: ",
                                    string.Join(", ", data["error-codes"].ToObject<string[]>()));

                                throw new ValidationException(new[] {
                                    new ValidationError("Failed hCaptcha validation")
                                });
                            }
                        }
                    }
                }
                catch (ValidationException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    _vendrApi.Log.Error<VendrReviewsController>(ex, "Exception was thrown whilst validating a hCaptcha");
                }
            }
        }
    }
}
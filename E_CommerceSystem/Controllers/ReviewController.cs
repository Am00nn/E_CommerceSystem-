using E_CommerceSystem.Helpers;
using E_CommerceSystem.Models;
using E_CommerceSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceSystem.Controllers
{
    [ApiController]
    [Route("api/Reviews")]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }


        [HttpPost]
        public IActionResult AddNewReviwe([FromBody] Review review)
        {

            try


            {
                //Extract Token from Request

                var token = Helper_Functions.ExtractTokenFromRequest(Request);

               // Get User ID from Token
                var userId = Helper_Functions.GetUserIDFromToken(token);


                //add new reviwe 
                var AddNewReview = _reviewService.AddReview(int.Parse(userId), review.PID, review.Rating, review.Comment);



                //Return Success Response

                return CreatedAtAction(nameof(GetReviews), new { productId = review.PID }, AddNewReview);
            }
            catch (Exception ex)
            {


                return BadRequest(new { Error = ex.Message });



            }



        }

        [HttpGet("{productId}")]
        public IActionResult GetReviews(int P_ID, [FromQuery] int page = 1, [FromQuery] int Page_Size = 10)
        {
            try
            {
                var all_reviews = _reviewService.GetReviews(P_ID, page, Page_Size);


                return Ok(all_reviews);
            }

            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("{reviewId}")]
        public IActionResult UpdateReview(int R_ID, [FromBody] Review R)
        {
            try
            {
                var token = Helper_Functions.ExtractTokenFromRequest(Request);

                var userId = Helper_Functions.GetUserIDFromToken(token);

                var updatedReview = _reviewService.UpdateReview(int.Parse(userId), R_ID, R.Rating, R.Comment);

                return Ok(updatedReview);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }


        [HttpDelete("{reviewId}")]

        public IActionResult DeleteReview(int R_Id)
        {
            try
            {
                var token = Helper_Functions.ExtractTokenFromRequest(Request);

                var userId = Helper_Functions.GetUserIDFromToken(token);

                _reviewService.DeleteReviwe(int.Parse(userId), R_Id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }



    }
}


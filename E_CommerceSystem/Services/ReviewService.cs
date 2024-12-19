using E_CommerceSystem.Models;
using E_CommerceSystem.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Cryptography;

namespace E_CommerceSystem.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepo _reviewRepo;

        private readonly IProductRepo _productRepo;

        private readonly IOrderRepo _orderRepo;


        public ReviewService(IReviewRepo reviewRepo, IProductRepo productRepo, IOrderRepo orderRepo)
        {
            _reviewRepo = reviewRepo;

            _productRepo = productRepo;

            _orderRepo = orderRepo;
        }


        public Review AddReview(int U_ID, int p_ID, int rating, string commment)
        {

            //check if user alrady buy prouducct by P_ID

            var U_order = _orderRepo.GetOrdersByUserId(U_ID);

            if (!U_order.Any(i => i.OrderItems.Any(o => o.PID == p_ID)))
                throw new Exception("This user dont buy this product ");



            // check if user already make review for product 


            var current_Riview = _reviewRepo.GetReviewsByProductId(p_ID, 1, int.MaxValue).FirstOrDefault(R => R.UID == U_ID);



            if (current_Riview != null)

                throw new Exception("user make review for this product before ");  //make exception 

            //Add new reveiw
            //
            var R = new Review
            {

                UID = U_ID,

                PID = p_ID,

                Rating = rating,

                Comment = commment



            };

            var addNewReview = _reviewRepo.AddReview(R);


            //make new overallRating in ther product 


            NewOverallRating(p_ID);

            return addNewReview;
        }

        public void NewOverallRating(int PID)
        {


            // Get all reviews for the given product
            var reviews = _reviewRepo.GetReviewsByProductId(PID, 1, int.MaxValue);


            decimal overallRating = 5;


            // Calculate the overall rating if there are any reviews
            if (reviews.Any())
            {
                overallRating = (decimal)reviews.Average(r => r.Rating);
            }



            //get product by ID 
            var product = _productRepo.GetProductById(PID);




            // If the product exists, update its overall rating
            if (product != null)
            {
                product.OverallRating = overallRating;
                _productRepo.UpdateProduct(product);
            }

        }

        //get reviwe 
        public IEnumerable<Review> GetReviews(int productId, int page, int pageSize)
        {
            return _reviewRepo.GetReviewsByProductId(productId, page, pageSize);
        }


        public Review UpdateReview(int U_ID, int R_ID, int rating, string comment)
        {
            // get the review by ID
            var R = _reviewRepo.GetReviewById(R_ID);




            // Validate the review and check if the user owns it

            if (R == null || R.UID != U_ID)
                throw new Exception("your Reviwe not found ");



            // Update the review rating and comment
            R.Rating = rating;

            R.Comment = comment;

            // Update the review in Rpo 
            var updatedReview = _reviewRepo.UpdateReview(R);

            // give new ovarallRating 

            NewOverallRating(R.PID);


            return updatedReview;
        }

        public void DeleteReviwe(int U_id, int R_id)
        {


            var R = _reviewRepo.GetReviewById(R_id);

            // Validate the review and check if the user owns it

            if (R == null || R.UID != U_id)
                throw new Exception("your Reviwe not found ");


            // delete form Rpo 
            _reviewRepo.DeleteReview(R);


            // give new ovarallRating 

            NewOverallRating(R.PID);



        }









    }
}
using E_CommerceSystem.Models;

namespace E_CommerceSystem.Services
{
    public interface IReviewService
    {
        Review AddReview(int U_ID, int p_ID, int rating, string commment);
        void DeleteReviwe(int U_id, int R_id);
        IEnumerable<Review> GetReviews(int productId, int page, int pageSize);
        void NewOverallRating(int PID);
        Review UpdateReview(int U_ID, int R_ID, int rating, string comment);
    }
}
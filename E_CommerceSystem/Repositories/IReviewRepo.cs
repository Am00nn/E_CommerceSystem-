using E_CommerceSystem.Models;

namespace E_CommerceSystem.Repositories
{
    public interface IReviewRepo
    {
        Review AddReview(Review R);
        void DeleteReview(Review review);
        Review GetReviewById(int R_Id);
        IEnumerable<Review> GetReviewsByProductId(int P_Id, int Page_Number, int Page_Size);
        Review UpdateReview(Review R);
    }
}
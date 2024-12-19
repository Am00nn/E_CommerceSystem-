using E_CommerceSystem.Models;

namespace E_CommerceSystem.Repositories
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepo(ApplicationDbContext context)
        {
            _context = context;
        }


        public Review AddReview(Review R)
        {
            _context.Reviews.Add(R);

            _context.SaveChanges();
            return R;
        }


        public Review GetReviewById(int R_Id)
        {
            return _context.Reviews.FirstOrDefault(r => r.R_ID == R_Id);
        }



        public Review UpdateReview(Review R)
        {
            _context.Reviews.Update(R);
            _context.SaveChanges();
            return R; // Return the updated review
        }





        public IEnumerable<Review> GetReviewsByProductId(int P_Id, int Page_Number, int Page_Size)
        {
            return _context.Reviews
                           .Where(r => r.PID == P_Id)
                           .Skip((Page_Number - 1) * Page_Size)
                           .Take(Page_Size)
                           .ToList();


        }



        public void DeleteReview(Review review)
        {
            _context.Reviews.Remove(review);

            _context.SaveChanges();
        }



    }
}

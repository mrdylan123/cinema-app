using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cinema.IVH7B4.Domain.Entities;

namespace Cinema.IVH7B4.WebUI.Models
{
    public class ReviewLogic
    {
        public string GetRating(int filmID, List<Review> reviews)
        {
            if (reviews.Count > 0)
            {
                List<Review> reviewsForFilm = reviews.Where(r => r.FilmID == filmID).ToList();

                int totalRating = reviewsForFilm.Sum(r => r.Rating);
                int amountReviews = reviewsForFilm.Count();

                if (amountReviews != 0)
                {
                    int avgRating = totalRating / amountReviews;
                    return avgRating.ToString();
                }
                else
                {
                    return "Geen reviews";
                }
            }
            else
            {
                return "Geen reviews";
            }
        }

        public string GetRatingByFilmName(string filmName, List<Review> reviews, List<Film> films)
        {
            if (reviews.Count > 0)
            {
                int filmID = films.Where(f => f.Name == filmName).Select(f => f.FilmID).ToList()[0];

                List<Review> reviewsforfilm = reviews.Where(r => r.FilmID == filmID).ToList();

                int totalrating = reviewsforfilm.Sum(r => r.Rating);
                int amountreviews = reviewsforfilm.Count();

                if (amountreviews != 0)
                {
                    int avgrating = totalrating / amountreviews;
                    return avgrating.ToString();
                }
                else
                {
                    return "Geen reviews";
                }
            }

            else
            {
                return "Geen reviews";
            }
        
        }

    }
}
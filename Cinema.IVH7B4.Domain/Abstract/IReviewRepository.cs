﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.IVH7B4.Domain.Entities;

namespace Cinema.IVH7B4.Domain.Abstract
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetReviews();
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.IVH7B4.Domain.Abstract;
using Cinema.IVH7B4.Domain.Entities;

namespace Cinema.IVH7B4.Domain.Concrete
{
    public class ShowingRepository : IShowingRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Showing> GetShowings()
        {
            return context.Showings;
        }
    }
}

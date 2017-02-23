using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinema.IVH7B4.Domain.Entities;

namespace Cinema.IVH7B4.Domain.Abstract
{
    public interface IFilmRepository
    {
        IEnumerable<Film> GetFilms();
    }
}

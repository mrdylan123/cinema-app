using Cinema.IVH7B4.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Cinema.IVH7B4.WebUI.Models
{
    public class FilmVerifier
    {
        public virtual Film verify(string inputString, List<Film> filmList)
        {
            Debug.WriteLine(inputString);
            Film film = null;
            foreach (Film currentFilm in filmList)
            {
                inputString = inputString.ToLower();
                string verifyString = currentFilm.Name.ToLower();
                if (verifyString == inputString)
                {
                    film = currentFilm;
                }
            }
            return film;
        }
    }
}
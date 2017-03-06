using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.IVH7B4.WebUI.Models
{

    //To-do: call method from FilmOverViewLogic instead of making own method
    public static class UpcomingFilmLogic
    {
        public static string getImageByteCode(this byte[] imageCode)
        {
            var image = @"data:image/jpg;base64," + Convert.ToBase64String(imageCode);

            return image;
        }
    }

}
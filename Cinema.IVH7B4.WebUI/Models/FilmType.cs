using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.IVH7B4.WebUI.Models {
    public enum FilmType {
        Children,
        Action,
        Drama,
        Horror,
        Comedy
    }

    static class FilmTypeMethods {
        public static String GetDutchString(this FilmType ft) {
            switch (ft) {
                case FilmType.Children: return Resources.Global.FilmOverView_Gerne_Children; // 0
                case FilmType.Action: return Resources.Global.FilmOverView_Gerne_Action; // 1
                case FilmType.Comedy: return Resources.Global.FilmOverView_Gerne_Comedy; // 2
                case FilmType.Drama: return "Drama"; // 3
                case FilmType.Horror: return "Horror"; // 4

                default: return "ERROR";
            }
        }
    }
}
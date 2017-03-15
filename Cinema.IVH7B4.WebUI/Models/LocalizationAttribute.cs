﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cinema.IVH7B4.WebUI.Models {
    public class LocalizedControllerActivator : IControllerActivator {
        private string _DefaultLanguage = "nl";

        public IController Create(RequestContext requestContext, Type controllerType) {
            //Get the {language} parameter in the RouteData
            string lang = (requestContext.RouteData.Values["lang"] != null) ? requestContext.RouteData.Values["lang"].ToString() : _DefaultLanguage;
            if (lang != _DefaultLanguage) {
                try {
                    Thread.CurrentThread.CurrentCulture =
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
                } catch (Exception e) {
                    throw new NotSupportedException(String.Format("ERROR: Invalid language code '{0}'.", lang));
                }
            }

            return DependencyResolver.Current.GetService(controllerType) as IController;
        }
    }
}
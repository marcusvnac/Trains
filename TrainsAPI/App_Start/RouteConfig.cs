﻿using System.Web.Mvc;
using System.Web.Routing;

namespace TrainsAPI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        }
    }
}

using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace PingPongLeague
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Ladder",	
				"Ladder/{year}/{month}",                      
			new { controller = "Match", action = "Ladder", year = DateTime.Now.Year, month = DateTime.Now.Month } 
		);
			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}

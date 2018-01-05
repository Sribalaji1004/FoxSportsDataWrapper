using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace FoxSportsDataWrapper
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
               name: "TodayGamesAPI",
               routeTemplate: "api/{controller}/{ClientID}/{League}/{Today}"
           );

            config.Routes.MapHttpRoute(
               name: "ClientsList",
               routeTemplate: "api/Ticker/V1/{controller}"
           );

            config.Routes.MapHttpRoute(
               name: "PlaylistsList",
               routeTemplate: "api/Ticker/V1/{controller}/{ClientID}"
           );

            config.Routes.MapHttpRoute(
                name: "PlaylistGroupNote",
                routeTemplate: "api/Ticker/V1/{controller}/{ClientID}/{PlaylistID}/{PlaylistGroupID}"

                );

           

            config.Routes.MapHttpRoute(
                name: "Game_Note",
                routeTemplate: "api/Ticker/V1/{controller}/Game/{ID}",
                defaults: new { controller = "Notes", ID = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
                name: "GroupNote",
                routeTemplate: "api/Ticker/V1/Notes/{controller}/{NotesID}",
                defaults: new { controller = "Group", NotesID = RouteParameter.Optional }
                );

            config.Routes.MapHttpRoute(
               name: "Events",
               routeTemplate: "api/{controller}/v1/Events/{Day}/{SportType}/{LeagueCode}",
               defaults: new { controller = "SportsData" , Day =RouteParameter.Optional, SportType =RouteParameter.Optional, LeagueCode = RouteParameter.Optional}
               );
        }
    }
}

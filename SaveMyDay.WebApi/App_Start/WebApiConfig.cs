using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Microsoft.OData.Edm;
using SaveMyDate.Entities;

namespace SaveMyDay.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            // Web API configuration and services
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            //builder.EntitySet<Company>("Company").EntityType.HasKey(p => p.Id);
            //builder.EntitySet<Appointment>("Appointment");
            //var action = builder.EntityType<Appointment>().
            //    Function("PostAppointment").Returns<string>();
            //action.EntityParameter<Appointment>("Appointment");




            EntitySetConfiguration<Appointment> contactType = builder.EntitySet<Appointment>("Appointment");
            var actionY = contactType.EntityType.Action("PostAppointment");
            actionY.Parameter<Appointment>("Appointment");
            actionY.Returns<bool>();

            var changePersonStatusAction = contactType.EntityType.Collection.Action("PostAppointment");
            changePersonStatusAction.Parameter<Appointment>("Appointment");
            changePersonStatusAction.Returns<bool>();

            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: "api",
                model: builder.GetEdmModel());







            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        //private static IEdmModel GetEdmModel()
        //{
        //    var builder = new ODataConventionModelBuilder();
        //   // builder.EntitySet<Company>("Company");
        //    builder.EntitySet<Appointment>("Appointment").EntityType.HasKey(p => p.Id); ;
        //    builder.EntitySet<User>("User");
        //    return builder.GetEdmModel();
        //}
    }
}

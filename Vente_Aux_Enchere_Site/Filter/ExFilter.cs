using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vente_Aux_Enchere_Site.Filter
{
    public class ExFilter:FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if(filterContext.Exception is NotImplementedException)
            {

            }
            else
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error"
                };
                filterContext.ExceptionHandled = true;
            }
        }
    }
}
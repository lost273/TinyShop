using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TinyShop.Filters {
    public class CatchException : FilterAttribute, IExceptionFilter {

        public void OnException (ExceptionContext exceptionContext) {
            if (!exceptionContext.ExceptionHandled) {
                exceptionContext.Result = new RedirectResult("/Content/ExceptionFound.html");
                exceptionContext.ExceptionHandled = true;
            }
        }
    }
}
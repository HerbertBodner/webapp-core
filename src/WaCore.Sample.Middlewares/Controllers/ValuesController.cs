using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WaCore.Web.Exceptions;

namespace WaCore.Sample.Middlewares.Controllers
{
    public class ValuesController : Controller
    {
        public string NotImplemented()
        {
            throw new NotImplementedException();
        }

        public string ResourceNotFound()
        {
            throw new ResourceNotFoundException("foo bar");
        }

        public string Bad()
        {
            throw new BadRequestException("foo bar", new Dictionary<string, string>
            {
                {"foo", "foo1"},
                {"bar", "bar1" },
            });
        }
    }
}
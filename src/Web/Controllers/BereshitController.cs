using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    public class BereshitController : BookController
    {
        
        public override string BookName => "Gen";

        public override int BookNumber => 1;

    }
}

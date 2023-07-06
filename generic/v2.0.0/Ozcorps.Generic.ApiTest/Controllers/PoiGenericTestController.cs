using Microsoft.AspNetCore.Mvc;
using Ozcorps.Generic.Controllers;
using Ozcorps.Generic.Bll;
using Ozcorps.Logger;
using Ozcorps.Generic.ApiTest.Entity;

namespace Ozcorps.Generic.ApiTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PoiGenericTestController : CrudController<Poi>
{
    public PoiGenericTestController(IEntityServiceProvider _provider, IOzLogger _logger) :
        base(_provider, _logger)
    {
    }
}
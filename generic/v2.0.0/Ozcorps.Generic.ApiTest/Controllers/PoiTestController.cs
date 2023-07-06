using Microsoft.AspNetCore.Mvc;
using Ozcorps.Core.Models;
using Ozcorps.Generic.ApiTest.Bll;
using Ozcorps.Logger;

namespace Ozcorps.Generic.ApiTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PoiTestController
{
    private readonly IPoiService _PoiService;

    private readonly IOzLogger _Logger;

    public PoiTestController(IPoiService _poiService, IOzLogger _logger)
    {
        _PoiService = _poiService;

        _Logger = _logger;
    }

    [HttpGet("intersect/{_wkt}")]
    public Response Intersect(string _wkt)
    {
        var _result = new Response();

        try
        {
            _result.Data = _PoiService.Intersect(_wkt);

            _result.Success = true;
        }
        catch (Exception _ex)
        {
            _Logger.Error(_ex);

        }

        return _result;
    }
}
using Microsoft.AspNetCore.Mvc;
using Ozcorps.Ozt;
using Ozcorps.Core.Models;

namespace Ozcorps.Logger.ApiTest.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class OzLoggerActionFilterTestController : ControllerBase
{
    private readonly IOzLogger _Logger;

    private readonly OztTool _OztTool;

    private OztUser _TokenUser = new OztUser
    {
        Email = "test@gmail.com",
        Permissions = new List<string> { "something" },
        RoleId = 1,
        UserId = 1,
        Roles = new List<string> { "admin" },
        Username = "oz"
    };

    public OzLoggerActionFilterTestController(IOzLogger _logger, OztTool _oztTool)
    {
        _Logger = _logger;

        _OztTool = _oztTool;
    }

    [HttpGet]
    public object GetToken()
    {
        object _result = null;

        try
        {
            _result = _OztTool.GenerateToken(_TokenUser);
        }
        catch (Exception _ex)
        {
            Console.WriteLine(_ex);
        }

        return _result;
    }

    [HttpGet]
    [OzLoggerActionFilter]
    public object LogAction()
    {
        var _result = new Response();

        try
        {
            _result.Data = new ResponseDto
            {
                Guid = Guid.NewGuid().ToString()
            };

            _result.Success = true;

        }
        catch (Exception _ex)
        {
            Console.WriteLine(_ex);
        }

        return _result;
    }

    [HttpPost]
    [OztActionFilter]
    [OzLoggerActionFilter]
    public object LogAction(RequestDto _dto)
    {
        var _result = new Response();

        try
        {
            _result.Data = new ResponseDto
            {
                Guid = Guid.NewGuid().ToString()
            };

            _result.Success = true;

        }
        catch (Exception _ex)
        {
            Console.WriteLine(_ex);
        }

        return _result;
    }

    [HttpPost]
    [OzLoggerActionFilter]
    public object LogActionWithoutUser(RequestDto _dto)
    {
        var _result = new Response();

        try
        {
            _result.Data = new ResponseDto
            {
                Guid = Guid.NewGuid().ToString()
            };

            _result.Success = true;

        }
        catch (Exception _ex)
        {
            Console.WriteLine(_ex);
        }

        return _result;
    }

    [HttpPost]
    [OzLoggerActionFilter]
    public object LogActionAddedUser(RequestDto _dto)
    {
        var _result = new Response();

        try
        {
            Request.HttpContext.Request.Headers.Add("ozt-user-id", "4");

            Request.HttpContext.Request.Headers.Add("ozt-username", "test-username");

            Request.HttpContext.Request.Headers.Add("ozt-roles", "test-roles");

            _result.Data = new ResponseDto
            {
                Guid = Guid.NewGuid().ToString()
            };

            _result.Success = true;

        }
        catch (Exception _ex)
        {
            Console.WriteLine(_ex);
        }

        return _result;
    }

    public class RequestDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class ResponseDto
    {
        public string Guid {get; set;}
    }
}
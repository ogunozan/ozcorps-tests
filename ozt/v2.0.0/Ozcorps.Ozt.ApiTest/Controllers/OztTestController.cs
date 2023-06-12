using Microsoft.AspNetCore.Mvc;
using Ozcorps.Core.Models;

namespace Ozcorps.Ozt.ApiTest.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class OztTestController : ControllerBase
{
    public class ResponseValidateOztDto
    {
        public OztUser OztUser { get; set; }

        public long UserId { get; set; }
        
        public bool IsAdmin { get; set; }
    }

    private readonly OztTool _TokenTool;

    public List<OztUser> _Users = new List<OztUser>
        {
            new OztUser
            {
                Email = "orhan@gmail.com",
                Permissions = new List<string> { "a", "b", "c" },
                RoleId = 1,
                Roles = new List<string> { "admin" },
                UserId = 1,
                IsLdapLogin = false,
                Username = "orhan"
            },
            new OztUser
            {
                Email = "devrim@gmail.com",
                Permissions = new List<string> { "c" },
                RoleId = 2,
                Roles = new List<string> { "tester" },
                UserId = 2,
                IsLdapLogin = true,
                Username = "devrim"
            },
            new OztUser
            {
                UserId = 3,
            }
        };

    public OztTestController(OztTool _tokenTool) =>
        _TokenTool = _tokenTool;

    [HttpGet]
    public Response GenerateOzt(long _userId)
    {
        var _result = new Response();

        try
        {
            var _user = _Users.FirstOrDefault(x => x.UserId == _userId);

            if (_user == null)
            {
                _result.Message = "user couldn't found!";

                return _result;
            }

            _result.Data = _TokenTool.GenerateToken(_user);

            _result.Success = true;

        }
        catch (Exception _ex)
        {
            Console.WriteLine(_ex);
        }

        return _result;
    }

    [HttpPost]
    public Response ValidatOztManual()
    {
        var _result = new Response();

        try
        {
            //get ozt from request header
            var _token = HttpContext.GetOzt();

            _result.Data = _TokenTool.ValidateToken(_token);

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
    public Response ValidateOzt()
    {
        var _result = new Response();

        try
        {
            var _user = HttpContext.GetOztUser();

            var _userId = HttpContext.GetUserId();

            var _isAdmin = HttpContext.IsAdmin();

            _result.Data = new ResponseValidateOztDto
            {
                OztUser = _user,
                UserId = _userId,
                IsAdmin = _isAdmin
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
    [OztActionFilter(Permissions = "a,b")]
    public Response CheckPermissions()
    {
        var _result = new Response();

        try
        {
            _result.Success = true;
        }
        catch (Exception _ex)
        {
            Console.WriteLine(_ex);
        }

        return _result;
    }

    [HttpPost]
    [OztActionFilter(Roles = "admin,tester")]
    public Response CheckRoles()
    {
        var _result = new Response();

        try
        {
            _result.Success = true;
        }
        catch (Exception _ex)
        {
            Console.WriteLine(_ex);
        }

        return _result;
    }
}
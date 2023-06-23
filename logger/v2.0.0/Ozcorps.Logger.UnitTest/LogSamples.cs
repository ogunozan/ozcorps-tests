using System;
using System.Collections.Generic;

namespace Ozcorps.Logger.UnitTest;

public class LogSamples
{
    public static ActionLog ActionLog => new ActionLog
    {
        ActionName = "LogAction",
        ControllerName = "LoggerTestController",
        Date = DateTime.Now,
        FullName = "fullname",
        Url = "https://localhost/test",
        Request = "request",
        Response = "response",
        MilliSeconds = 12,
        UserIpAddress = "127.0.0.1",
        Username = "oz",
        UserId = 60,
        UserRoles = "admin"
    };

    public static List<AuditLog> AuditLogs => new List<AuditLog>
    {
        new AuditLog
        {
            Date = DateTime.Now,
            Entity = "entity",
            EntityId = 6,
            Table = "table",
            Geoloc = "POINT(43 35)",
            Json = new String("hoba"),
            Operation = "Add",
            UserId = 7,
            Username = "oz",
            UserRoles = "admin"
        }
    };

    public static UserLog UserLog => new UserLog
    {
        Date = DateTime.Now,
        UserId = 8,
        UserLogType = UserLogType.LogIn,
        Username = "oz",
        UserRoles = "admin",
    };
}
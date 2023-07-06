using Ozcorps.Generic.ApiTest.Entity;

namespace Ozcorps.Generic.ApiTest.Bll;

public interface IPoiService
{
    IEnumerable<Poi> Intersect(string _wkt);
}
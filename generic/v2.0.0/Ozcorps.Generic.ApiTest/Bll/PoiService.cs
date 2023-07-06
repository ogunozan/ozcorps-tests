using Ozcorps.Core.Extensions;
using Ozcorps.Generic.ApiTest.Entity;
using Ozcorps.Generic.Bll;
using Ozcorps.Generic.Dal;

namespace Ozcorps.Generic.ApiTest.Bll;

public class PoiService : DbServiceBase, IPoiService
{
    private readonly IRepository<Poi> _PoiRepository;

    public PoiService(IUnitOfWork _unitOfWork) : base(_unitOfWork)
    {
        _PoiRepository = _unitOfWork.GetRepository<Poi>();
    }

    public IEnumerable<Poi> Intersect(string _wkt)
    {
        return _PoiRepository.GetAll(x=>x.Geoloc.Intersects(_wkt.WktToGeometry())).ToList();
    }
}
using NetTopologySuite.Geometries;
using Ozcorps.Core.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Ozcorps.Core.Tests.Extensions;

public class GeometryExtensionsTest
{
    private static LinearRing GetSampleLinearRing()
    {
        var _sample = new LinearRing(new List<Coordinate>
        {
            new Coordinate(1,2),
            new Coordinate(3,4),
            new Coordinate(5,6),
            new Coordinate(1,2)
        }.ToArray());

        return _sample;
    }

    private static Geometry GetSampleGeometry4326()
    {
        var _wkt = "LINESTRING (35.21362 39.72284, 38.86108 39.41797)";

        return _wkt.WktToGeometry();
    }

    private static Geometry GetSampleGeometry3857()
    {
        var _wkt = "LINESTRING (3919962.247387834 4825747.507704027, " +
            "4325995.637276668 4781720.261177444)";

        return _wkt.WktToGeometry();
    }

    private static Geometry GetSampleGeometry2319()
    {
        var _wkt = "LINESTRING (1204685.0600779052 4431322.51212998, " +
            "1522835.9529524469 4432832.16488216)";

        return _wkt.WktToGeometry();
    }

    private static Geometry GetSampleGeometryUtm35ED50()
    {
        var _wkt = "LINESTRING (689760.3657174937 4399515.897876556, " +
            "1004730.9825301256 4379760.867458412)";

        return _wkt.WktToGeometry();
    }

    [Fact]
    public void SimplfyTest()
    {
        var _polygon = GetSampleLinearRing();

        var _simplified = _polygon.Simplfy(100);

        Assert.True(_simplified.Coordinates.Length < _polygon.Coordinates.Length);
    }

    [Fact]
    public void WktToGeometryTest()
    {
        var _expected = "LINESTRING (35.21362 39.72284, 38.86108 39.41797)";

        var _geoemtry = _expected.WktToGeometry();

        Assert.Multiple(() => Assert.Equal(35.21362, _geoemtry.Coordinates.First().X),
            () => Assert.Equal(39.41797, _geoemtry.Coordinates.Last().Y));
    }

    [Fact]
    public void ToWktTest()
    {
        var _geoemtry = GetSampleLinearRing();

        var _wkt = _geoemtry.ToWkt();

        var _expected = "LINEARRING (" + _geoemtry.Coordinates.First().X + " " +
            _geoemtry.Coordinates.First().Y;

        Assert.StartsWith(_expected, _wkt);
    }

    [Fact]
    public void ToLineWKtTest()
    {
        var _geoemtry = new List<List<double>>{
            new List<double>{1,2},
            new List<double>{3,4},
        };

        var _wkt = _geoemtry.ToLineWKt();

        var _expected = "LINESTRING (1 2, 3 4)";

        Assert.Equal(_expected, _wkt);
    }

    [Fact]
    public void ToPolygonWKtTest()
    {
        var _geoemtry = new List<List<double>>{
            new List<double>{1,2},
            new List<double>{3,4},
            new List<double>{1,2},
        };

        var _wkt = _geoemtry.ToPolygonWkt();

        var _expected = "POLYGON ((1 2, 3 4, 1 2))";

        Assert.Equal(_expected, _wkt);
    }

    [Fact]
    public void ToWkbWkbToGeometryTest()
    {
        var _expected = GetSampleLinearRing();

        var _geoemtry = _expected.ToWKbHex().WkbHexToGeometry();

        Assert.Equal(_expected.Coordinates.First().X, _geoemtry.Coordinates.First().X);
    }

    [Fact]
    public void ToWebMerkatorTest()
    {
        var _sample = GetSampleGeometry4326();

        var _geoemtry = _sample.ToWebMerkator();

        Assert.Equal(3857, _geoemtry.SRID);
    }

    [Fact]
    public void ToWgs84From3857Test()
    {
        var _sample = GetSampleGeometry3857();

        var _geoemtry = _sample.ToWgs84From3857();

        Assert.Equal(4326, _geoemtry.SRID);
    }

    [Fact]
    public void ToWgs84FromTm27Test()
    {
        var _sample = GetSampleGeometry2319();

        var _geoemtry = _sample.ToWgs84FromTm27();

        Assert.Equal(4326, _geoemtry.SRID);
    }

    [Fact]
    public void ToTm27FromWgs84Test()
    {
        var _sample = GetSampleGeometry4326();

        var _geoemtry = _sample.ToTm27FromWgs84();

        Assert.Equal(2319, _geoemtry.SRID);
    }

    [Fact]
    public void ToUtm35ED50FromWgs84Test()
    {
        var _sample = GetSampleGeometry4326();

        var _geoemtry = _sample.ToUtm35ED50FromWgs84();

        Assert.Equal(23036, _geoemtry.SRID);
    }

    [Fact]
    public void ToWgs84FromUtm35ED50Test()
    {
        var _sample = GetSampleGeometryUtm35ED50();

        var _geoemtry = _sample.ToWgs84FromUtm35ED50();

        Assert.Equal(4326, _geoemtry.SRID);
    }

    [Fact]
    public void ToTm45FromWgs84Test()
    {
        var _sample = GetSampleGeometry4326();

        var _geoemtry = _sample.ToTm45FromWgs84();

        Assert.Equal(5259, _geoemtry.SRID);
    }

    [Fact]
    public void MakeCCWTest()
    {
        var _sample = GetSampleLinearRing();

        var _geometry = new Polygon(_sample).MakeCCW();

        Assert.Equal(_sample.Coordinates[1].X, _geometry.Coordinates[2].X);
    }
}
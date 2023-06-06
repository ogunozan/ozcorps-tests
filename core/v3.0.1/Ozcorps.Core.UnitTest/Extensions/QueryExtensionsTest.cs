using Ozcorps.Core.Extensions;
using Ozcorps.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ozcorps.Core.Tests.Extensions;

public class QueryExtensionsTest
{
    private class QueryableItem
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public int Age { get; init; }
        public DateTime Date { get; init; }
        public bool IsActive { get; init; }

        public QueryableItem(long _id, string _name, int _age, DateTime _date, bool _isActive)
        {
            Id = _id;

            Name = _name;

            Age = _age;

            Date = _date;

            IsActive = _isActive;
        }
    }

    private readonly IQueryable _QueryableAnonSample = new[]
        {
            new {Id=3, Name = "Ozan", Age=45, Date=DateTime.Now.AddDays(-5), IsActive = true},
            new {Id=2, Name = "Ali", Age=35, Date=DateTime.Now.AddDays(-5), IsActive = true},
            new {Id=1, Name = "Veli", Age=27, Date=DateTime.Now.AddDays(2), IsActive = false},
            new {Id=5, Name = "Ahmet", Age=55, Date=DateTime.Now.AddDays(-2), IsActive = false},
            new {Id=4, Name = "Ayşe", Age=27, Date=DateTime.Now.AddDays(-10), IsActive = true}
        }.
        AsQueryable();

    private readonly IQueryable<QueryableItem> _QueryableSample = new List<QueryableItem>
        {
            new QueryableItem(3,"Ozan", 45, DateTime.Now.AddDays(-5), true),
            new QueryableItem(2,"Ali", 35, DateTime.Now.AddDays(5), true),
            new QueryableItem(1,"Veli", 27, DateTime.Now.AddDays(2), false),
            new QueryableItem(5,"Ahmet", 55, DateTime.Now.AddDays(-2), false),
            new QueryableItem(4,"Ayşe", 27, DateTime.Now.AddDays(-10), true),
        }.
        AsQueryable();

    [Fact]
    public void SearchAnonymousTest()
    {
        var _expected = "Veli";

        var _result = _QueryableAnonSample.Search(
            new string[] { "IsActive", "Date" },
            new string[] { "false", DateTime.Now.AddDays(2).ToString("MM/dd/yyyy") }, true).
            ToArray();

        Assert.Multiple(() => Assert.Single(_result),
            () => Assert.Equal(_expected, (_result[0] as dynamic).Name));
    }

    [Fact]
    public void SearchAndAnonymousTest()
    {
        var _expected = "Veli";

        var _result = _QueryableAnonSample.SearchAnd(
            new string[] { "Name", "Age" }, new string[] { "li", "27" }).
            ToArray();

        Assert.Multiple(() => Assert.Single(_result),
            () => Assert.Equal(_expected, (_result[0] as dynamic).Name));
    }

    [Fact]
    public void SearchOrAnonymousTest()
    {
        var _result = _QueryableAnonSample.SearchOr(
            new string[] { "Name", "Age" }, new string[] { "li", "27" }).
            ToArray();

        Assert.Equal(3, _result.Count());
    }

    [Fact]
    public void SearchTest()
    {
        var _expected = "Veli";

        var _result = _QueryableSample.Search(
            new string[] { "Name", "Age" }, new string[] { "li", "27" }, true).
            ToList();

        Assert.Multiple(() => Assert.Single(_result),
            () => Assert.Equal(_expected, _result.FirstOrDefault()?.Name));
    }

    [Fact]
    public void SearchAndTest()
    {
        var _expected = "Veli";

        var _result = _QueryableSample.SearchAnd(
            new string[] { "Name", "Age" }, new string[] { "li", "27" }).
            ToList();

        Assert.Multiple(() => Assert.Single(_result),
            () => Assert.Equal(_expected, _result.FirstOrDefault()?.Name));
    }

    [Fact]
    public void SearchOrTest()
    {
        var _result = _QueryableSample.SearchOr(
            new string[] { "Name", "Age" }, new string[] { "li", "27" }).
            ToList();

        Assert.Equal(3, _result.Count());
    }

    [Fact]
    public void OrderHelperAnonTest()
    {
        var _result = _QueryableAnonSample.OrderHelper(
               new string[] { "asc", "desc" }, new string[] { "Age", "Id" }).
               ToArray();

        Assert.Multiple(() => Assert.Equal("Ayşe", (_result[0] as dynamic).Name),
            () => Assert.Equal("Veli", (_result[1] as dynamic).Name));
    }

    [Fact]
    public void OrderHelperTest()
    {
        var _result = _QueryableSample.OrderHelper(
               new string[] { "asc", "desc" }, new string[] { "Age", "Id" }).
               ToList();

        Assert.Multiple(() => Assert.Equal("Ayşe", _result[0].Name),
            () => Assert.Equal("Veli", _result[1].Name));
    }

    [Fact]
    public void OrderByHelperAnonTest()
    {
        var _expected = "Veli";

        var _result = _QueryableAnonSample.OrderByHelper("Id").ToArray();

        Assert.Equal(_expected, (_result[0] as dynamic).Name);
    }

    [Fact]
    public void OrderByHelperTest()
    {
        var _expected = "Veli";

        var _result = _QueryableSample.OrderByHelper("Id").ToList();

        Assert.Equal(_expected, _result.FirstOrDefault().Name);
    }

    [Fact]
    public void OrderByDescendingHelperTest()
    {
        var _expected = "Ahmet";

        var _result = _QueryableSample.OrderByDescendingHelper("Id").ToList();

        Assert.Equal(_expected, _result.FirstOrDefault().Name);
    }

    [Fact]
    public void ThenByTest()
    {
        var _expected = "Veli";

        var _result = _QueryableSample.OrderByHelper("Age").
            ThenBy("Id").
            ToList();

        Assert.Equal(_expected, _result.FirstOrDefault().Name);
    }

    [Fact]
    public void ThenByDescendingTest()
    {
        var _expected = "Ayşe";

        var _result = _QueryableSample.OrderByHelper("Age").
            ThenByDescending("Id").
            ToList();

        Assert.Equal(_expected, _result.FirstOrDefault().Name);
    }

    [Fact]
    public void IfWhereTest()
    {
        var _result = _QueryableSample.IfWhere(_QueryableSample.Count() > 2, x => x.Age > 30).
            ToList();

        Assert.Equal(3, _result.Count);
    }

    [Fact]
    public void UnionTest()
    {
        var _result = _QueryableAnonSample.SearchAnd(new string[] { "Age" }, new string[] { "27" }).
            Union(_QueryableAnonSample.SearchAnd(new string[] { "Id" }, new string[] { "5" })).
            ToArray();

        Assert.Equal(3, _result.Length);
    }

    [Fact]
    public void PaginateTest()
    {
        var _expected = "Ozan";

        var _result = _QueryableAnonSample.Paginate(new PaginatorDto
        {
            FilterColumns = new string[] { "IsActive" },
            FilterValues = new string[] { "true" },
            Limit = 1,
            OrderValues = new string[] { "asc" },
            OrderColumns = new string[] { "Name" },
            Offset = 2,
            SearchColumns = new string[] { "Name" },
            SearchText = "a"
        }, out int _count).
        ToArray();

        Assert.Multiple(() => Assert.Single(_result),
            () => Assert.Equal(_expected, (_result[0] as dynamic).Name),
            () => Assert.Equal(3, _count));

        var _genericResult = _QueryableSample.Paginate(new PaginatorDto
        {
            FilterColumns = new string[] { "IsActive" },
            FilterValues = new string[] { "true" },
            Limit = 1,
            OrderValues = new string[] { "asc" },
            OrderColumns = new string[] { "Name" },
            Offset = 2,
            SearchColumns = new string[] { "Name" },
            SearchText = "a"
        }, out int _countGeneric).ToListGeneric<QueryableItem>();

        var _paginatorResponse = new PaginatorResponseDto<QueryableItem>
        {
            Count = _countGeneric,
            Rows = _genericResult
        };
        Assert.Multiple(() => Assert.Single(_paginatorResponse.Rows),
            () => Assert.Equal(_expected, _paginatorResponse.Rows.First().Name),
            () => Assert.Equal(3, _paginatorResponse.Count));
    }

    [Fact]
    public void ToArrayTest()
    {
        var _result = _QueryableAnonSample.ToArray();

        Assert.Equal(5, _result.Length);
    }

    [Fact]
    public void ToArraySizeTest()
    {
        var _result = _QueryableAnonSample.ToArray(10);

        Assert.Equal(5, _result.Length);
    }

    [Fact]
    public void ToArrayGenericSizeTest()
    {
        var _result = _QueryableSample.ToArray<QueryableItem>(10);

        Assert.Equal(5, _result.Length);
    }

    [Fact]
    public void ToArrayGenericTest()
    {
        var _result = _QueryableSample.ToArrayGeneric<QueryableItem>();

        Assert.Equal(5, _result.Length);
    }

    [Fact]
    public void ToIListTest()
    {
        var _result = _QueryableAnonSample.ToIList();

        Assert.Equal(5, _result.Count);
    }

    [Fact]
    public void ToListTest()
    {
        var _result = _QueryableSample.ToListGeneric<QueryableItem>();

        Assert.Equal(5, _result.Count);
    }

    [Fact]
    public void ToListDynamicTest()
    {
        var _result = _QueryableSample.ToListDynamic<QueryableItem>();

        Assert.Equal(5, _result.Count);
    }
}
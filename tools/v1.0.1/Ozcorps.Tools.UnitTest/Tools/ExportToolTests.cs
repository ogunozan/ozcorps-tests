using System;
using System.Collections.Generic;
using System.Linq;

namespace Ozcorps.Tools.Tests;

public class ExportToolTests
{
    private class ExportItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }

        public ExportItem(long _id, string _name, int _age, DateTime _date, bool _isActive)
        {
            Id = _id;

            Name = _name;

            Age = _age;

            Date = _date;

            IsActive = _isActive;
        }
    }

    private readonly List<ExportItem> _Sample = new List<ExportItem>{
        new ExportItem(new Random().Next(), "oz", 25, DateTime.Now, true),
        new ExportItem(new Random().Next(), "ali", 35, DateTime.Now.AddDays(-2), true),
        new ExportItem(new Random().Next(), "veli", 45, DateTime.Now.AddDays(1), true),
        new ExportItem(new Random().Next(), "ahmet", 55, DateTime.Now.AddMinutes(20), true)
    };

    [Fact]
    public void ExcelTest()
    {
        ExportTool.Excel(_Sample.ToList<object>(), "test-sheet", "test-file.xlsx");
    }

    [Fact]
    public void ToDataTableTest()
    {
        var _result = ExportTool.ToDataTable(_Sample.ToList<object>());

        Assert.Multiple(() => Assert.Equal(_Sample.Count, _result.Rows.Count),
            () => Assert.Contains(Convert.ToInt32(_result.Rows[0][0]), _Sample.Select(x => x.Id)));
    }
}
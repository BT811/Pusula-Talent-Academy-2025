using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class FilterEmployeesSolution
{
    public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {   
        // Çalışanlar listesini belirli kriterlere göre filtrele
        var filtered = employees
            .Where(e =>
                e.Age >= 25 && e.Age <= 40 &&
                (e.Department == "IT" || e.Department == "Finance") &&
                e.Salary >= 5000m && e.Salary <= 9000m &&
                e.HireDate > new DateTime(2017, 1, 1)
            )
            .OrderByDescending(e => e.Name.Length)
            .ThenBy(e => e.Name)
            .ToList();

        // Filtrelenen çalışanlar listesinden özet verileri hesapla.
        var result = new
        {
            Names = filtered.Select(e => e.Name).ToList(),
            TotalSalary = filtered.Any() ? filtered.Sum(e => e.Salary) : 0,
            AverageSalary = filtered.Any() ? Math.Round(filtered.Average(e => e.Salary), 2) : 0,
            MinSalary = filtered.Any() ? filtered.Min(e => e.Salary) : 0,
            MaxSalary = filtered.Any() ? filtered.Max(e => e.Salary) : 0,
            Count = filtered.Count
        };

        // Sonuç nesnesini JSON'a serileştir, karakter kaçışlarını önle.
        return JsonSerializer.Serialize(result, new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });

    }
}
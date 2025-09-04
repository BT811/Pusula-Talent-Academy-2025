using System;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;

public class FilterPeopleFromXmlSolution
{
    public static string FilterPeopleFromXml(string xmlData)
    {
        // Gelen XML verisini XDocument'a ayrıştır.
        var doc = XDocument.Parse(xmlData);

        // Her bir "Person" elementini anonim bir nesneye dönüştür.
        var query = doc.Descendants("Person")
            .Select(p => new {
                Name = (string?)p.Element("Name") ?? "",
                Age = (int?)p.Element("Age") ?? 0,
                Department = (string?)p.Element("Department") ?? "",
                Salary = (decimal?)p.Element("Salary") ?? 0,
                HireDate = DateTime.TryParse((string?)p.Element("HireDate"), out var dt) ? dt : DateTime.MinValue
            });

        // Belirlenen koşullara göre filtreleme yap.
        var filtered = query.Where(p =>
            p.Age > 30 &&
            p.Department == "IT" &&
            p.Salary > 5000 &&
            p.HireDate < new DateTime(2019, 1, 1)
        );

        // Filtrelenmiş verilerden istenen sonuçları hesapla.
        var result = new {
            Names = filtered.Select(p => p.Name).OrderBy(n => n).ToList(),
            TotalSalary = filtered.Any() ? filtered.Sum(p => p.Salary) : 0,
            AverageSalary = filtered.Any() ? filtered.Average(p => p.Salary) : 0,
            MaxSalary = filtered.Any() ? filtered.Max(p => p.Salary) : 0,
            Count = filtered.Count()
        };
        
        // Sonucu JSON formatında serileştir ve döndür
        return JsonSerializer.Serialize(result);
    }
}



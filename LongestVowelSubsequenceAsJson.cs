using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

public static class LongestVowelSubsequenceAsJsonSolution
{
    public static string LongestVowelSubsequenceAsJson(List<string> words)
    {
        if (words == null || !words.Any())
        {
            return "[]";
        }

        var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };

        // Her kelimeyi işleyip sonuçları anonim bir nesneye dönüştür.
        var results = words.Select(word =>
        {
            string currentVowelSequence = "";
            string longestVowelSequence = "";

            // Kelimeyi harf harf kontrol et ve en uzun sesli harf dizisini bul.
            foreach (char c in word.ToLower())
            {
                if (vowels.Contains(c))
                {
                    currentVowelSequence += c;
                    if (currentVowelSequence.Length > longestVowelSequence.Length)
                    {
                        longestVowelSequence = currentVowelSequence;
                    }
                }
                else
                {
                    currentVowelSequence = "";
                }
            }

            return new
            {
                word = word,
                sequence = longestVowelSequence,
                length = longestVowelSequence.Length
            };
        }).ToList();

        // Sonuç listesini JSON formatına dönüştürerek geri döndür.
        return JsonSerializer.Serialize(results);
    }
}

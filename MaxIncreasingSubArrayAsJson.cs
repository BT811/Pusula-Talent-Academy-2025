using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

public class MaxIncreasingSubArrayAsJsonSolution
{

    // Verilen bir tamsayı listesindeki, ardışık olarak artan ve toplamı en yüksek olan alt diziyi JSON formatında döndürür.

    public static string MaxIncreasingSubArrayAsJson(List<int> numbers)
    {
        // Liste null veya boş ise boş bir JSON dizisi döndürülmeli.
        if (numbers == null || !numbers.Any())
        {
            return "[]";
        }

        List<int> bestSubarray = new List<int>();
        // Long kullanıyoruz çünkü;
        // 1- Negatif tam sayıları doğru handle etmek için long kullanacağız,
        // 2- Toplamın integer sınırlarını aşma ihtimaline karşı long kullanacağız.
        long maxSum = long.MinValue;

        List<int> currentSubarray = new List<int> { numbers[0] };
        long currentSum = numbers[0];

        for (int i = 1; i < numbers.Count; i++)
        {
            // Artan bir dizi ise, currentSubarray'e ekle ve currentSum'u güncelle.
            if (numbers[i] > numbers[i - 1])
            {

                currentSubarray.Add(numbers[i]);
                currentSum += numbers[i];
            }
            else
            {
                // Mevcut `currentSubarray`'in toplamını kontrol et
                if (currentSum > maxSum)
                {
                    maxSum = currentSum;
                    // bestSubarray'e currentSubarray'in bir kopyasını atıyoruz.
                    // Aksi takdirde her ikisi de aynı nesneyi işaret ederdi.
                    bestSubarray = new List<int>(currentSubarray);
                }

                // Yeni bir alt dizi başlat.
                currentSubarray.Clear();
                currentSubarray.Add(numbers[i]);
                currentSum = numbers[i];
            }

        }

        // Son alt diziyi kontrol et
        if (currentSum > maxSum)
        {
            bestSubarray = new List<int>(currentSubarray);
        }

        return JsonSerializer.Serialize(bestSubarray);
    }


}


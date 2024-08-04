using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public static class RandomNumberGenerator
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<int> GetRandomNumberAsync(int max)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://www.randomnumberapi.com/" +
                    $"api/v1.0/random?min=0&max={max - 1}&count=1");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var numbers = System.Text.Json.JsonSerializer.Deserialize<List<int>>(responseBody);

                if (numbers != null && numbers.Count > 0)
                {
                    return numbers[0];
                }
                else
                {
                    throw new Exception("Неверный ответ от API");
                }
            }
            catch (Exception)
            {

                Random random = new Random();
                return random.Next(0, max);
            }
        }
    }
}

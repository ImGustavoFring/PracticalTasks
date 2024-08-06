﻿using BusinnessLogic.StringWork;
using ConsoleApp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StringManipulationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly HashSet<string> _blackList;

        public StringManipulationController(IConfiguration configuration)
        {
            _configuration = configuration;
            _blackList = _configuration.GetSection("Kestrel:Settings:BlackList")
                                       .Get<HashSet<string>>() ?? new HashSet<string>();
        }

        [HttpGet("manipulate")]
        public async Task<IActionResult> ManipulateString([FromQuery] string input)
        {
            await Task.Delay(3000); // таким образом, мы проверям, работает ли наш middleware с конфигом
                                    // для проверки дублируем вкладки и пытаемся одновременно отправить запросы

            if (string.IsNullOrEmpty(input))
            {
                return BadRequest(new { Error = "Ошибка: строка не должна быть пустой." });
            }

            var (isValidBoundaries, invalidCharacters) = BoundariesChecker.Check(input);

            if (!isValidBoundaries)
            {
                return BadRequest(new
                {
                    Error = $"Ошибка:" +
                    $" введены неподходящие символы - {string.Join(", ", invalidCharacters)}"
                });
            }

            var (isValidSequences, invalidisValidSequences) = BlackListChecker.Check(_blackList, input);

            if (!isValidSequences)
            {
                return BadRequest(new
                {
                    Error = $"Ошибка:" +
                    $" строка содержит запрещенные последовательности - {string.Join(", ", invalidisValidSequences)}"
                });
            }

            string result = StringChanger.Change(input);
            var characterCounts = CharacterCounter.CountCharacters(result);
            string longestSubstring = SubstringFinder.FindLongestVowelSubstring(result);
            string sortedResult = Quicksort.Sort(result);
            string randomApiUrl = _configuration.GetValue<string>("RandomApi");
            int randomIndex = await RandomNumberGenerator.GetRandomNumberAsync(randomApiUrl, result.Length);
            string truncatedResult = RemoveCharacterAtIndex(result, randomIndex);

            var response = new
            {
                ManipulatedString = result,
                CharacterCounts = characterCounts,
                LongestVowelSubstring = longestSubstring,
                SortedString = sortedResult,
                TruncatedString = truncatedResult
            };

            return Ok(response);
        }

        private static string RemoveCharacterAtIndex(string text, int index)
        {
            if (index < 0 || index >= text.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне допустимого диапазона.");
            }

            return text.Remove(index, 1);
        }
    }
}

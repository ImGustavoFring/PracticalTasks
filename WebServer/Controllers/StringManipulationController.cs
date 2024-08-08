using BusinnessLogic.Sorting;
using ConsoleApp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using BusinnessLogic.StringWork;

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
        public async Task<IActionResult> ManipulateString([FromQuery] string input,
            [FromQuery] AllSortingTypes sortType)
        {
            await Task.Delay(3000); 

            if (string.IsNullOrEmpty(input))
            {
                return BadRequest(new { Error = "Ошибка: строка не должна быть пустой." });
            }

            var (isValidBoundaries, invalidCharacters) = BoundariesChecker.Check(input);

            if (!isValidBoundaries)
            {
                return BadRequest(new
                {
                    Error = $"Ошибка: введены неподходящие символы - {string.Join(", ", invalidCharacters)}"
                });
            }

            var (isValidSequences, invalidSequences) = BlackListChecker.Check(_blackList, input);

            if (!isValidSequences)
            {
                return BadRequest(new
                {
                    Error = $"Ошибка: строка содержит запрещенные последовательности - {string.Join(", ", invalidSequences)}"
                });
            }

            string result = StringChanger.Change(input);
            var characterCounts = CharacterCounter.CountCharacters(result);
            string longestSubstring = SubstringFinder.FindLongestVowelSubstring(result);

            string sortedResult;

            try
            {
                sortedResult = GetSortedString(result, sortType);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

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

        private static string GetSortedString(string text, AllSortingTypes sortType)
        {
            return sortType switch
            {
                AllSortingTypes.QUICK_SORT => Quicksort.Sort(text),
                AllSortingTypes.TREE_SORT => TreeSort.Sort(text),
                _ => throw new ArgumentException("Ошибка: некорректный тип сортировки.", nameof(sortType))
            };
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

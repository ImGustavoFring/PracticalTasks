using ConsoleApp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StringManipulationController : ControllerBase
    {
        [HttpGet("manipulate")]
        public async Task<IActionResult> ManipulateString([FromQuery] string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return BadRequest(new { Error = "Ошибка: строка не должна быть пустой."});
            }

            var (isValid, invalidCharacters) = BoundariesChecker.Check(input);

            if (!isValid)
            {
                return BadRequest(new { Error = $"Ошибка:" +
                    $" введены неподходящие символы - {string.Join(", ", invalidCharacters)}" });
            }

            string result = StringChanger.Change(input);

            var characterCounts = CharacterCounter.CountCharacters(result);

            string longestSubstring = SubstringFinder.FindLongestVowelSubstring(result);

            string sortedResult = Quicksort.Sort(result);

            int randomIndex = await RandomNumberGenerator.GetRandomNumberAsync(result.Length);
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

using BusinnessLogic.Sorting;
using ConsoleApp;
using System;
using System.Collections.Generic;
using NUnit.Framework;

// Condolences to whoever checks this
namespace UnitTests 
{
    [TestFixture]
    public class Tests
    {
        [TestCase("a", "aa")]
        [TestCase("abcdef", "cbafed")]
        [TestCase("abcde", "edcbaabcde")]
        public void RunFirstTask(string input, string expected)
        {
            string result = StringChanger.Change(input);
            Assert.AreEqual(expected, result);
        }

        [TestCase("a", "aa", TestName = "Valid Single Character")]
        [TestCase("abcdef", "cbafed", TestName = "Valid Even Length")]
        [TestCase("abcde", "edcbaabcde", TestName = "Valid Odd Length")]
        [TestCase("abc123", "", "123", TestName = "Invalid Characters Included")]
        [TestCase("hello!", "", "!", TestName = "Special Characters Included")]
        public void RunSecondTask(string input, string expected, string invalidChars = "")
        {
            var (isValid, invalidCharacters) = BoundariesChecker.Check(input);

            string result = isValid ? StringChanger.Change(input) : "";

            Assert.AreEqual(expected, result);

            if (!isValid)
            {
                CollectionAssert.AreEqual(invalidChars.ToCharArray(), invalidCharacters);
            }
            else
            {
                Assert.IsEmpty(invalidCharacters);
            }
        }

        [Test]
        public void RunThirdTask()
        {
            var testCases = new[]
            {
                new
                {
                    Input = "a",
                    ExpectedResult = "aa",
                    ExpectedCounts = new Dictionary<char, int> { ['a'] = 2 },
                    ExpectedInvalidChars = new List<char>()
                },
                new
                {
                    Input = "abcdef",
                    ExpectedResult = "cbafed",
                    ExpectedCounts = new Dictionary<char, int>
                    {
                        ['a'] = 1, ['b'] = 1, ['c'] = 1, ['d'] = 1, ['e'] = 1, ['f'] = 1
                    },
                    ExpectedInvalidChars = new List<char>()
                },
                new
                {
                    Input = "abcde",
                    ExpectedResult = "edcbaabcde",
                    ExpectedCounts = new Dictionary<char, int>
                    {
                        ['a'] = 2, ['b'] = 2, ['c'] = 2, ['d'] = 2, ['e'] = 2
                    },
                    ExpectedInvalidChars = new List<char>()
                },
                new
                {
                    Input = "abc123",
                    ExpectedResult = "",
                    ExpectedCounts = new Dictionary<char, int>(),
                    ExpectedInvalidChars = new List<char> { '1', '2', '3' }
                },
                new
                {
                    Input = "hello!",
                    ExpectedResult = "",
                    ExpectedCounts = new Dictionary<char, int>(),
                    ExpectedInvalidChars = new List<char> { '!' }
                }
            };

            foreach (var testCase in testCases)
            {
                var (isValid, invalidCharacters) = BoundariesChecker.Check(testCase.Input);
                string result = isValid ? StringChanger.Change(testCase.Input) : "";

                var characterCounts = CharacterCounter.CountCharacters(result);

                Assert.AreEqual(testCase.ExpectedResult, result);

                if (!isValid)
                {
                    CollectionAssert.AreEqual(testCase.ExpectedInvalidChars, invalidCharacters);
                }
                else
                {
                    foreach (var kvp in testCase.ExpectedCounts)
                    {
                        Assert.IsTrue(characterCounts.ContainsKey(kvp.Key), $"Character '{kvp.Key}' is missing.");
                        Assert.AreEqual(kvp.Value, characterCounts[kvp.Key], $"Character '{kvp.Key}' count is incorrect.");
                    }
                }
            }
        }

        [Test]
        public void RunFourthTask()
        {
            var testCases = new[]
            {
                new
                {
                    Input = "a",
                    ExpectedResult = "aa",
                    ExpectedLongestVowelSubstring = "aa",
                    ExpectedCounts = new Dictionary<char, int> { ['a'] = 2 },
                    ExpectedInvalidChars = new List<char>()
                },
                new
                {
                    Input = "abcdef",
                    ExpectedResult = "cbafed",
                    ExpectedLongestVowelSubstring = "afe",
                    ExpectedCounts = new Dictionary<char, int>
                    {
                        ['a'] = 1, ['b'] = 1, ['c'] = 1, ['d'] = 1, ['e'] = 1, ['f'] = 1
                    },
                    ExpectedInvalidChars = new List<char>()
                },
                new
                {
                    Input = "abcde",
                    ExpectedResult = "edcbaabcde",
                    ExpectedLongestVowelSubstring = "edcbaabcde",
                    ExpectedCounts = new Dictionary<char, int>
                    {
                        ['a'] = 2, ['b'] = 2, ['c'] = 2, ['d'] = 2, ['e'] = 2
                    },
                    ExpectedInvalidChars = new List<char>()
                }
            };

            foreach (var testCase in testCases)
            {
                var (isValid, invalidCharacters) = BoundariesChecker.Check(testCase.Input);
                string result = isValid ? StringChanger.Change(testCase.Input) : "";

                var characterCounts = CharacterCounter.CountCharacters(result);
                string longestVowelSubstring = SubstringFinder.FindLongestVowelSubstring(result);

                Assert.AreEqual(testCase.ExpectedResult, result);
                Assert.AreEqual(testCase.ExpectedLongestVowelSubstring, longestVowelSubstring);

                if (!isValid)
                {
                    CollectionAssert.AreEqual(testCase.ExpectedInvalidChars, invalidCharacters);
                }
                else
                {
                    foreach (var kvp in testCase.ExpectedCounts)
                    {
                        Assert.IsTrue(characterCounts.ContainsKey(kvp.Key), $"Character '{kvp.Key}' is missing.");
                        Assert.AreEqual(kvp.Value, characterCounts[kvp.Key], $"Character '{kvp.Key}' count is incorrect.");
                    }
                }
            }
        }

        [Test]
        public void RunFifthTask()
        {
            var testCases = new[]
            {
                new
                {
                    Input = "a",
                    SortingType = AllSortingTypes.QUICK_SORT,
                    ExpectedResult = "aa",
                    ExpectedLongestVowelSubstring = "aa",
                    ExpectedCounts = new Dictionary<char, int> { {'a', 2} },
                    ExpectedSortedResult = "aa"
                },
                new
                {
                    Input = "abcdef",
                    SortingType = AllSortingTypes.QUICK_SORT,
                    ExpectedResult = "cbafed",
                    ExpectedLongestVowelSubstring = "afe",
                    ExpectedCounts = new Dictionary<char, int>
                    {
                        {'a', 1}, {'b', 1}, {'c', 1}, {'d', 1}, {'e', 1}, {'f', 1}
                    },
                    ExpectedSortedResult = "abcdef"
                },
                new
                {
                    Input = "abcde",
                    SortingType = AllSortingTypes.TREE_SORT,
                    ExpectedResult = "edcbaabcde",
                    ExpectedLongestVowelSubstring = "edcbaabcde",
                    ExpectedCounts = new Dictionary<char, int>
                    {
                        {'a', 2}, {'b', 2}, {'c', 2}, {'d', 2}, {'e', 2}
                    },
                    ExpectedSortedResult = "aabbccddee"
                }
            };

            foreach (var testCase in testCases)
            {
                var (isValid, invalidCharacters) = BoundariesChecker.Check(testCase.Input);
                Assert.IsTrue(isValid, $"Invalid characters: {string.Join(", ", invalidCharacters)}");

                string changedString = StringChanger.Change(testCase.Input);

                var characterCounts = CharacterCounter.CountCharacters(changedString);

                string longestVowelSubstring = SubstringFinder.FindLongestVowelSubstring(changedString);

                string sortedString = testCase.SortingType switch
                {
                    AllSortingTypes.QUICK_SORT => Quicksort.Sort(changedString),
                    AllSortingTypes.TREE_SORT => TreeSort.Sort(changedString),
                    _ => throw new ArgumentException("Unsupported sorting type")
                };

                Assert.AreEqual(testCase.ExpectedResult, changedString, $"Failed for input '{testCase.Input}' with sorting type '{testCase.SortingType}'");
                Assert.AreEqual(testCase.ExpectedLongestVowelSubstring, longestVowelSubstring, $"Longest vowel substring check failed for input '{testCase.Input}'");

                foreach (var kvp in testCase.ExpectedCounts)
                {
                    Assert.IsTrue(characterCounts.ContainsKey(kvp.Key), $"Character '{kvp.Key}' is missing in the counts.");
                    Assert.AreEqual(kvp.Value, characterCounts[kvp.Key], $"Character '{kvp.Key}' count is incorrect.");
                }

                Assert.AreEqual(testCase.ExpectedSortedResult, sortedString, $"Sorting check failed for input '{testCase.Input}' with sorting type '{testCase.SortingType}'");
            }
        }
    }
}

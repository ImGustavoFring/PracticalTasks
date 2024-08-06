using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinnessLogic.StringWork
{
    public static class BlackListChecker
    {
        public static (bool isValid, List<string> invalidSequences) Check(HashSet<string> blackList, string text)
        {
            var invalidSequences = new List<string>();
            bool isValid = true;

            foreach (var item in blackList)
            {
                if (text.Contains(item))
                {
                    isValid = false;
                    invalidSequences.Add(item);
                }
            }

            return (isValid, invalidSequences);
        }
    }
}
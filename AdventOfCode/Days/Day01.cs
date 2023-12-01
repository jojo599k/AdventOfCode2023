using System;
using System.IO;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode.Days
{
    public sealed class Day01 : BaseDay
    {
        private readonly string[] _input;
        private readonly string[] _spelledOutWords;

        // First wanted to do it with regex, but just manually traversed the string then
        // private const string patternPart1 = @"^[a-z]*([0-9]{1})[a-z]*(([0-9]{1})[a-z]*)*$";

        public Day01()
        {
            _input = File.ReadAllLines(InputFilePath);
            _spelledOutWords =
            [
                "one",
                "two",
                "three",
                "four",
                "five",
                "six",
                "seven",
                "eight",
                "nine"
            ];
        }

        public override ValueTask<string> Solve_1()
        {
            var total = 0;

            foreach (var line in _input)
            {
                var firstNumber = -1;
                var lastNumber = 0;

                foreach (var c in line)
                {
                    if (char.IsDigit(c))
                    {
                        if (firstNumber == -1)
                        {
                            firstNumber = c - '0';
                        }
                        lastNumber = c - '0';
                    }
                }

                if (firstNumber == -1)
                {
                    throw new InvalidOperationException("No digit was found in input!");
                }
                total += firstNumber * 10 + lastNumber;
            }

            return new(total.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var total = 0;

            foreach (var line in _input)
            {
                var firstNumber = -1;
                var lastNumber = -1;

                for (int i = 0; i < line.Length; i++)
                {
                    if (char.IsDigit(line[i]))
                    {
                        if (firstNumber == -1)
                        {
                            firstNumber = line[i] - '0';
                        }
                        lastNumber = line[i] - '0';
                    }
                    else
                    {
                        var foundMatchingWord = false;

                        for (int j = 0; j < _spelledOutWords.Length && !foundMatchingWord; j++)
                        {
                            var match = true;

                            for (int k = 0; k < _spelledOutWords[j].Length && match; k++)
                            {
                                if (i + k >= line.Length ||
                                    line[i + k] != _spelledOutWords[j][k])
                                {
                                    match = false;
                                }
                            }

                            if (match)
                            {
                                if (firstNumber == -1)
                                {
                                    firstNumber = j + 1;
                                }
                                lastNumber = j + 1;
                                // Apparently words can share letters `oneight` => 1, 8
                                // i += _spelledOutWords[j].Length - 1;
                                foundMatchingWord = true;
                            }
                        }
                    }
                }

                if (firstNumber == -1)
                {
                    throw new InvalidOperationException("No digit was found in input!");
                }
                total += firstNumber * 10 + lastNumber;
            }

            return new(total.ToString());
        }
    }
}
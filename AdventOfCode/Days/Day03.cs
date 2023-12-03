using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode.Days
{
    public sealed class Day03 : BaseDay
    {
        private readonly string[] _input;
        private readonly string _symbols;

        public Day03()
        {
            _input = File.ReadAllLines(InputFilePath);
            _symbols = BuildSymbolList();
            Console.WriteLine("Symbols: " + _symbols);
        }

        private string BuildSymbolList()
        {
            var set = new HashSet<char>();

            foreach (var line in _input)
            {
                foreach (var c in line)
                {
                    if (c != '.' && !char.IsDigit(c))
                    {
                        set.Add(c);
                    }
                }
            }

            return string.Join("", set.Select(s => s));
        }

        public override ValueTask<string> Solve_1()
        {
            var sumPartNumbers = 0;

            for (int i = 0; i < _input.Length; ++i)
            {
                for (int j = 0; j < _input[i].Length; ++j)
                {
                    if (_symbols.Contains(_input[i][j]))
                    {
                        sumPartNumbers += GetPartNumbersForSymbol(i, j);
                    }
                }
            }

            return new(sumPartNumbers.ToString());
        }

        private int GetPartNumbersForSymbol(int row, int col)
        {
            var sum = 0;

            for (int i = row - 1; i <= row + 1; ++i)
            {
                if (i < 0 || i >= _input.Length)
                {
                    continue;
                }

                for (int j = col - 1; j <= col + 1; ++j)
                {
                    if (j < 0 || j >= _input[i].Length)
                    {
                        continue;
                    }

                    if (char.IsDigit(_input[i][j]))
                    {
                        int left = j, right = j;
                        while (left - 1 >= 0 && char.IsDigit(_input[i][left - 1]))
                        {
                            --left;
                        }
                        while (right + 1 < _input[i].Length && char.IsDigit(_input[i][right + 1]))
                        {
                            ++right;
                        }

                        if (!int.TryParse(
                            _input[i].AsSpan(left, right - left + 1),
                            out var num
                        ))
                        {
                            throw new InvalidOperationException(string.Format("Number is no int! line=`{0}`, left={1}, right={2}", _input[i], left, right));
                        }

                        sum += num;
                        if (right > j)
                        {
                            j += right - j;
                        }
                    }
                }
            }

            return sum;
        }

        public override ValueTask<string> Solve_2()
        {
            var sumGearRatios = 0;

            for (int i = 0; i < _input.Length; ++i)
            {
                for (int j = 0; j < _input[i].Length; ++j)
                {
                    if (_input[i][j] == '*')
                    {
                        sumGearRatios += GetGearRatioForLocation(i, j);
                    }
                }
            }

            return new(sumGearRatios.ToString());
        }

        private int GetGearRatioForLocation(int row, int col)
        {
            var sum = 0;
            var nums = new List<int>();

            for (int i = row - 1; i <= row + 1; ++i)
            {
                if (i < 0 || i >= _input.Length)
                {
                    continue;
                }

                for (int j = col - 1; j <= col + 1; ++j)
                {
                    if (j < 0 || j >= _input[i].Length)
                    {
                        continue;
                    }

                    if (char.IsDigit(_input[i][j]))
                    {
                        int left = j, right = j;
                        while (left - 1 >= 0 && char.IsDigit(_input[i][left - 1]))
                        {
                            --left;
                        }
                        while (right + 1 < _input[i].Length && char.IsDigit(_input[i][right + 1]))
                        {
                            ++right;
                        }

                        if (!int.TryParse(
                            _input[i].AsSpan(left, right - left + 1),
                            out var num
                        ))
                        {
                            throw new InvalidOperationException(string.Format("Number is no int! line=`{0}`, left={1}, right={2}", _input[i], left, right));
                        }

                        nums.Add(num);
                        if (right > j)
                        {
                            j += right - j;
                        }
                    }
                }
            }

            if (nums.Count == 2)
            {
                sum = nums[0] * nums[1];
            }
            return sum;
        }
    }
}
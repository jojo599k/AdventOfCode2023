using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode.Days
{
    public sealed class Day02 : BaseDay
    {
        // GameId: cg 1
        // Subset of games: cg 2
        // Count of reds : cg 4
        // Count of greens: cg 7
        // Count of blues : cg 10
        // private const string gameParsePattern = @"^Game ([0-9]+): ((([0-9]+) red(, )?)?(([0-9]+) green(, )?)?(([0-9]+) blue(, )?)?(; )?)+$";
        private const string gameIdPattern = @"^Game ([0-9]+):";
        private const string colorPattern = @"([0-9]+) (red|green|blue)";

        private readonly string[] _input;

        public Day02()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override ValueTask<string> Solve_1()
        {
            var gameIdRegex = new Regex(gameIdPattern);
            var colorRegex = new Regex(colorPattern);
            var sum = 0;

            foreach (var line in _input)
            {
                var gameIdMatch = gameIdRegex.Match(line);
                if (!gameIdMatch.Success)
                {
                    throw new InvalidOperationException($"Line `{line}`could not be parsed!");
                }
                int.TryParse(gameIdMatch.Groups[1].Value, out var gameId);

                var isPossible = true;
                var colorMatches = colorRegex.Matches(line);

                for (int i = 0; isPossible && i < colorMatches.Count; i++)
                {
                    var match = colorMatches[i];
                    int.TryParse(match.Groups[1].Value, out var value);

                    if (match.Groups[2].Value == "red")
                    {
                        if (value > 12)
                        {
                            isPossible = false;
                        }
                    }
                    else if (match.Groups[2].Value == "green")
                    {
                        if (value > 13)
                        {
                            isPossible = false;
                        }
                    }
                    else if (match.Groups[2].Value == "blue")
                    {
                        if (value > 14)
                        {
                            isPossible = false;
                        }
                    }
                }

                if (isPossible)
                {
                    sum += gameId;
                }
            }

            return new(sum.ToString());
            
            // Code for doing shit
            // Should read the instructions next time
            /* foreach (var line in _input)
            {
                var gameIdMatch = gameIdRegex.Match(line);
                var colorMatches = colorRegex.Matches(line);
                if (!gameIdMatch.Success)
                {
                    throw new InvalidOperationException($"Line `{line}`could not be parsed!");
                }

                int.TryParse(gameIdMatch.Groups[1].Value, out var gameId);
                int red = 0, green = 0, blue = 0;
                foreach (Match match in colorMatches)
                {
                    int.TryParse(match.Groups[1].Value, out var v);

                    if (match.Groups[2].Value == "red")
                    {
                        red += v;
                    }
                    else if (match.Groups[2].Value == "green")
                    {
                        green += v;
                    }
                    else if (match.Groups[2].Value == "blue")
                    {
                        blue += v;
                    }
                }

                games.Add(new Game(gameId, red, green, blue));
                Console.WriteLine(games.Last());
            }

            return new ValueTask<string>(games
                .Where(g => g.IsPossible())
                .Select(g => g.Id)
                .Sum()
                .ToString()); */
        }

        public override ValueTask<string> Solve_2()
        {
            var colorRegex = new Regex(colorPattern);
            var power = 0;

            foreach (var line in _input)
            {
                var colorMatches = colorRegex.Matches(line);
                var colors = new Dictionary<string, int>
                {
                    { "red", 0 },
                    { "green", 0 },
                    { "blue", 0 }
                };

                foreach (Match match in colorMatches)
                {
                    var color = match.Groups[2].Value;
                    int.TryParse(match.Groups[1].Value, out var value);

                    if (colors[color] < value)
                    {
                        colors[color] = value;
                    }
                }

                power += colors.Values.Aggregate(1, (acc, val) => acc * val);
            }

            return new(power.ToString());
        }

        /* private sealed class Game(int id, int red, int green, int blue)
        {
            public static Game Target = new(-1, 12, 13, 14);

            public int Id { get; } = id;
            public int Red { get; } = red;
            public int Green { get; } = green;
            public int Blue { get; } = blue;

            public override int GetHashCode()
            {
                return HashCode.Combine(Id, Red, Green, Blue);
            }

            public override bool Equals(object? obj)
            {
                if (ReferenceEquals(this, obj))
                    return true;
                if (obj is not Game g)
                    return false;

                return Id == g.Id && Red == g.Red && Green == g.Green && Blue == g.Blue;
            }

            public override string ToString()
            {
                return $"Game {Id}: {Red} red, {Green} green, {Blue} blue";
            }

            public bool IsPossible()
            {
                return SameNumberOfColors(this, Target);
            }

            public static bool SameNumberOfColors(Game? toTest, Game? target)
            {
                if (toTest is null || target is null)
                    return false;

                return toTest.Red <= target.Red && toTest.Green <= target.Green && toTest.Blue <= target.Blue;
            }
        }*/
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode.Days
{
    public sealed class Day04 : BaseDay
    {
        private readonly string[] _input;

        public Day04()
        {
            _input = File.ReadAllLines(InputFilePath);
        }

        public override ValueTask<string> Solve_1()
        {
            var points = 0;

            foreach (var line in _input)
            {
                var lists = line.Split(':').Last().Split('|');
                if (lists.Length != 2)
                {
                    throw new InvalidOperationException($"The line `{line}` does not match the format!");
                }

                var n1 = lists[0]
                    .Split(' ')
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Select(s => Convert.ToInt32(s))
                    .ToList();
                var n2 = lists[1]
                    .Split(' ')
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Select(s => Convert.ToInt32(s))
                    .ToList();

                var winningNumbers = n1.Intersect(n2).ToList();
                if (winningNumbers.Count > 0)
                {
                    points += (int)Math.Pow(2, winningNumbers.Count - 1);
                }
            }

            return new(points.ToString());
        }

        public override ValueTask<string> Solve_2()
        {
            var scratchCards = ParseInput();
            var cardsToLook = new List<ScratchCard>(scratchCards.Values);

            for (int i = 0; i < cardsToLook.Count; ++i)
            {
                foreach (var win in cardsToLook[i].WonCards())
                {
                    cardsToLook.Add(scratchCards[win]);
                }
            }

            return new(cardsToLook.Count.ToString());
        }

        private Dictionary<int, ScratchCard> ParseInput()
        {
            const string cardNumberPattern = @"^Card\s+([0-9]+)";
            var cardNumberRegex = new Regex(cardNumberPattern);

            var scratchCards = new Dictionary<int, ScratchCard>();

            foreach (var line in _input)
            {
                var lineSplit = line.Split(':');
                if (lineSplit.Length != 2)
                {
                    throw new InvalidOperationException($"The line `{line}` does not match the format!");
                }
                var cardNumberMatch = cardNumberRegex.Match(lineSplit.First());
                if (!cardNumberMatch.Success)
                {
                    throw new InvalidOperationException($"The line `{line}` does not match the format!");
                }
                var lists = lineSplit.Last().Split('|');
                if (lists.Length != 2)
                {
                    throw new InvalidOperationException($"The line `{line}` does not match the format!");
                }

                int.TryParse(cardNumberMatch.Groups[1].Value, out var cardNumber);
                var n1 = lists[0]
                    .Split(' ')
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Select(s => Convert.ToInt32(s))
                    .ToList();
                var n2 = lists[1]
                    .Split(' ')
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Select(s => Convert.ToInt32(s))
                    .ToList();

                var winningNumbers = n1.Intersect(n2).ToList();
                var card = new ScratchCard(cardNumber, n1, n2, winningNumbers);
                scratchCards.Add(cardNumber, card);
            }

            return scratchCards;
        }

        private sealed class ScratchCard
        {
            public ScratchCard(
                int cardNumber,
                IEnumerable<int> firstList,
                IEnumerable<int> secondList,
                IEnumerable<int> winningNumbers)
            {
                CardNumber = cardNumber;
                FirstList = new List<int>(firstList);
                SecondList = new List<int>(secondList);
                WinningNumbers = new List<int>(winningNumbers);
            }

            public int CardNumber { get; init; }
            public List<int> FirstList { get; init; }
            public List<int> SecondList { get; init; }
            public List<int> WinningNumbers { get; init; }

            public IEnumerable<int> WonCards()
            {
                return Enumerable.Range(CardNumber + 1, WinningNumbers.Count);
            }
        }
    }
}
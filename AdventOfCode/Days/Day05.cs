using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AoCHelper;

namespace AdventOfCode.Days
{
    public sealed class Day05 : BaseDay
    {
        private const string mapPattern = @"([0-9]+) ([0-9]+) ([0-9]+)";

        private readonly string[] _input;
        private readonly Regex _mapRegex;

        public Day05()
        {
            _input = File.ReadAllLines(InputFilePath);
            _mapRegex = new Regex(mapPattern);
        }

        private Tuple<List<Seed>, List<Map>> ParseInput()
        {
            var seeds = new List<Seed>();
            var maps = new List<Map>();
            var current = State.Seed;

            foreach (var line in _input)
            {
                switch (current)
                {
                    case State.Seed:
                        if (line.StartsWith("seeds: "))
                        {
                            var l = line["seeds: ".Length..];
                            var nums = l.Split(' ');
                            foreach (var num in nums)
                            {
                                if (!int.TryParse(num, out var value))
                                {
                                    throw new InvalidOperationException("Expected int32 got: " + num);
                                }
                                seeds.Add(new Seed(){ SeedId = value });
                            }
                            current = State.SeedToSoil;
                        }
                        break;
                    case State.SeedToSoil:
                        if (line.StartsWith("soil-to-fertilizer map:"))
                        {
                            current = State.SoilToFertilizer;
                            break;
                        }
                        break;
                    default:
                        throw new InvalidOperationException("Did not see this coming!");
                }
            }
        }

        public override ValueTask<string> Solve_1()
        {
            throw new System.NotImplementedException();
        }

        public override ValueTask<string> Solve_2()
        {
            throw new System.NotImplementedException();
        }

        private enum State 
        {
            Seed,
            SeedToSoil,
            SoilToFertilizer,
            FertilizerToWater,
            WaterToLight,
            LightToTemperature,
            TemperatureToHumidity,
            HumidityToLocation,
        }
    
        private sealed class Seed 
        {
            public int SeedId { get; set; }
            public int SoilId { get; set; }
            public int FertilizerId { get; set; }
            public int WaterId { get; set; }
            public int LightId { get; set; }
            public int TemperatureId { get; set; }
            public int HumidityId { get; set; }
            public int LocationId { get; set; }
        }
    
        private sealed class Map
        {
            public State Type { get; set; }
            public List<MapEntry> Entries { get; set; }
        }

        private sealed class MapEntry 
        {
            public int Begin { get; set; }
            public int End { get; set; }
            public int Operator { get; set; }
        }
    }
}
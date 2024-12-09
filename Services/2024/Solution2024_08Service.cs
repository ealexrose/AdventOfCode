using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2024/08.txt
    public class Solution2024_08Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 8, example);

            int answer = 0;

            List<FrequencyMap> frequencyMaps = new List<FrequencyMap>();
            List<Tuple<int, int>> antinodeList = new List<Tuple<int, int>>();
            for (int x = 0; x < lines.Count; x++)
            {
                for (int y = 0; y < lines[0].Length; y++)
                {
                    if (lines[x][y] == '.')
                        continue;

                    if (!frequencyMaps.Any(map => map.frequency == lines[x][y]))
                    {
                        frequencyMaps.Add(new FrequencyMap(lines[x][y], new Tuple<int, int>(lines.Count, lines[0].Length)));
                    }

                    frequencyMaps.First(map => map.frequency == lines[x][y]).antennaCoordinates.Add(new Tuple<int, int>(x, y));
                }
            }

            foreach (FrequencyMap frequencyMap in frequencyMaps)
            {
                frequencyMap.CalculateAntinodes(ref antinodeList);
                answer = antinodeList.Count;
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 8, example);

            int answer = 0;

            List<FrequencyMap> frequencyMaps = new List<FrequencyMap>();
            List<Tuple<int, int>> antinodeList = new List<Tuple<int, int>>();
            for (int x = 0; x < lines.Count; x++)
            {
                for (int y = 0; y < lines[0].Length; y++)
                {
                    if (lines[x][y] == '.')
                        continue;

                    if (!frequencyMaps.Any(map => map.frequency == lines[x][y]))
                    {
                        frequencyMaps.Add(new FrequencyMap(lines[x][y], new Tuple<int, int>(lines.Count, lines[0].Length)));
                    }

                    frequencyMaps.First(map => map.frequency == lines[x][y]).antennaCoordinates.Add(new Tuple<int, int>(x, y));
                }
            }

            foreach (FrequencyMap frequencyMap in frequencyMaps)
            {
                frequencyMap.CalculateResonantAntinodes(ref antinodeList);
            }

            answer = antinodeList.Count;
            return answer.ToString();
        }

        public class FrequencyMap
        {
            public char frequency;
            public List<Tuple<int, int>> antennaCoordinates = new List<Tuple<int, int>>();
            public Tuple<int, int> mapBounds;

            public FrequencyMap(char frequencyCharacter, Tuple<int, int> mapBounds)
            {
                frequency = frequencyCharacter;
                this.mapBounds = mapBounds;
            }

            public void CalculateAntinodes(ref List<Tuple<int, int>> antinodeCoordinates)
            {
                for (int i = 0; i < antennaCoordinates.Count; i++)
                {
                    for (int j = i + 1; j < antennaCoordinates.Count; j++)
                    {
                        Tuple<int, int> differenceVector = new Tuple<int, int>(antennaCoordinates[i].Item1 - antennaCoordinates[j].Item1, antennaCoordinates[i].Item2 - antennaCoordinates[j].Item2);

                        Tuple<int, int> antinode1 = new Tuple<int, int>(antennaCoordinates[i].Item1 + differenceVector.Item1, antennaCoordinates[i].Item2 + differenceVector.Item2);
                        Tuple<int, int> antinode2 = new Tuple<int, int>(antennaCoordinates[j].Item1 - differenceVector.Item1, antennaCoordinates[j].Item2 - differenceVector.Item2);

                        if (!antinodeCoordinates.Any(x => x.Item1 == antinode1.Item1 && x.Item2 == antinode1.Item2) &&
                            antinode1.Item1 >= 0 && antinode1.Item1 < mapBounds.Item1 &&
                            antinode1.Item2 >= 0 && antinode1.Item2 < mapBounds.Item2)
                        {
                            antinodeCoordinates.Add(antinode1);
                        }

                        if (!antinodeCoordinates.Any(x => x.Item1 == antinode2.Item1 && x.Item2 == antinode2.Item2) &&
                            antinode2.Item1 >= 0 && antinode2.Item1 < mapBounds.Item1 &&
                            antinode2.Item2 >= 0 && antinode2.Item2 < mapBounds.Item2)
                        {
                            antinodeCoordinates.Add(antinode2);
                        }
                    }
                }
            }

            public void CalculateResonantAntinodes(ref List<Tuple<int, int>> antinodeCoordinates)
            {
                for (int i = 0; i < antennaCoordinates.Count; i++)
                {
                    for (int j = i + 1; j < antennaCoordinates.Count; j++)
                    {
                        Tuple<int, int> differenceVector = new Tuple<int, int>(antennaCoordinates[i].Item1 - antennaCoordinates[j].Item1, antennaCoordinates[i].Item2 - antennaCoordinates[j].Item2);
                        differenceVector = ReduceFraction(differenceVector);
                        //Get Positive Direction Antinodes
                        int nodeCount = 0;
                        Tuple<int, int> nodeCoordinates = antennaCoordinates[i];
                        while (nodeCoordinates.Item1 >= 0 && nodeCoordinates.Item1 < mapBounds.Item1 &&
                            nodeCoordinates.Item2 >= 0 && nodeCoordinates.Item2 < mapBounds.Item2) 
                        {
                            if (!antinodeCoordinates.Any(x => x.Item1 == nodeCoordinates.Item1 && x.Item2 == nodeCoordinates.Item2))
                            {
                                antinodeCoordinates.Add(nodeCoordinates);
                            }

                            nodeCount++;
                            int newX = antennaCoordinates[i].Item1 + differenceVector.Item1 * nodeCount;
                            int newY = antennaCoordinates[i].Item2 + differenceVector.Item2 * nodeCount;
                            nodeCoordinates = new Tuple<int, int> (newX, newY );
                        }

                        //Get Negative Direction Antinodes
                        nodeCount = 0;
                        nodeCoordinates = antennaCoordinates[i];
                        while (nodeCoordinates.Item1 >= 0 && nodeCoordinates.Item1 < mapBounds.Item1 &&
                            nodeCoordinates.Item2 >= 0 && nodeCoordinates.Item2 < mapBounds.Item2)
                        {
                            if (!antinodeCoordinates.Any(x => x.Item1 == nodeCoordinates.Item1 && x.Item2 == nodeCoordinates.Item2))
                            {
                                antinodeCoordinates.Add(nodeCoordinates);
                            }

                            nodeCount++;
                            int newX = antennaCoordinates[i].Item1 + -differenceVector.Item1 * nodeCount;
                            int newY = antennaCoordinates[i].Item2 + -differenceVector.Item2 * nodeCount;
                            nodeCoordinates = new Tuple<int, int>(newX, newY);
                        }
                    }
                }
            }

            public Tuple<int, int> ReduceFraction(Tuple<int, int> slope) 
            {
                int gcd = GetGCD(slope.Item1, slope.Item2);
                return new Tuple<int, int>(slope.Item1 / gcd, slope.Item2 / gcd);
            }

            public int GetGCD(int a, int b) 
            {
                if (b == 0)
                    return a;
                return GetGCD(b, a % b);
            }
        }
    }
}
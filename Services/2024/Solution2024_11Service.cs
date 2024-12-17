namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2024/11.txt
    public class Solution2024_11Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 11, example);

            long answer = 0;
            List<RockInfo> rocks = new List<RockInfo>();

            foreach (string line in lines)
            {
                long[] startingRocks = line.Split(' ').ToLongs().ToArray();
                foreach (long startingRock in startingRocks)
                {
                    RockInfo newRock = new RockInfo(startingRock);
                    newRock.currentRocks++;
                    rocks.Add(newRock);
                }
            }

            long cycles = 25;
            for (long i = 0; i < cycles; i++) 
            {
                List<RockInfo> newRocks = new List<RockInfo>();
                rocks.ForEach(x => x.MoveRocks(ref newRocks));
                newRocks.ForEach(x => x.currentRocks = x.incomingRocks);
                rocks = newRocks;
            }

            answer = rocks.Sum(x => x.currentRocks);
            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 11, example);

            long answer = 0;
            List<RockInfo> rocks = new List<RockInfo>();

            foreach (string line in lines)
            {
                long[] startingRocks = line.Split(' ').ToLongs().ToArray();
                foreach (long startingRock in startingRocks)
                {
                    RockInfo newRock = new RockInfo(startingRock);
                    newRock.currentRocks++;
                    rocks.Add(newRock);
                }
            }

            long cycles = 75;
            for (long i = 0; i < cycles; i++)
            {
                List<RockInfo> newRocks = new List<RockInfo>();
                rocks.ForEach(x => x.MoveRocks(ref newRocks));
                newRocks.ForEach(x => x.currentRocks = x.incomingRocks);
                rocks = newRocks;
            }

            answer = rocks.Sum(x => x.currentRocks);
            return answer.ToString();
        }

        public class RockInfo
        {
            public long rockNumber;
            public long currentRocks;
            public long incomingRocks;
            List<long> resultRocks = null;

            public RockInfo(long rockNumber)
            {
                this.rockNumber = rockNumber;
                currentRocks = 0;
                incomingRocks = 0;
                resultRocks = new List<long>();
                if (rockNumber == 0)
                    resultRocks.Add(1);
                else if (rockNumber.ToString().Length % 2 == 0)
                {
                    string stringNumber = rockNumber.ToString();
                    long firstHalf = long.Parse(stringNumber.Substring(0, stringNumber.Length / 2));
                    long secondHalf = long.Parse(stringNumber.Substring(stringNumber.Length / 2, stringNumber.Length / 2));
                    resultRocks.Add(firstHalf);
                    resultRocks.Add(secondHalf);
                }
                else
                {
                    resultRocks.Add(rockNumber * 2024);
                }

            }

            public void MoveRocks(ref List<RockInfo> rockSet)
            {
                foreach (long resultRock in resultRocks)
                {
                    if (!rockSet.Any(x => x.rockNumber == resultRock))
                    {
                        rockSet.Add(new RockInfo(resultRock));
                    }
                    rockSet.First(x => x.rockNumber == resultRock).incomingRocks += currentRocks;
                }
            }
        }
    }
}
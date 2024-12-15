using Microsoft.AspNetCore.Components;

namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2024/10.txt
    public class Solution2024_10Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 10, example);

            int answer = 0;
            Trail[,] trails = new Trail[lines.Count, lines[0].Length];
            List<(int x, int y)> trailHeads = new List<(int x, int y)>();

            int trailIndex = 0;
            for (int i = 0; i < trails.GetLength(0); i++) 
            {
                for (int j = 0; j < trails.GetLength(1); j++)
                {
                    int height = lines[i][j].ToInt();
                    trails[i, j] = new Trail(height, trailIndex);
                    if (height == 0)
                    {
                        trailIndex++;
                        trailHeads.Add((i, j));
                    }

                }
            }
            foreach ((int x, int y) trailHead in trailHeads)
            {
                answer += CountDistinctTrailsFromTrailHead(ref trails, trailHead, trails[trailHead.x, trailHead.y].trailHeadId, 0, new List<(int x, int y)>(), false);
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 10, example);

            int answer = 0;
            Trail[,] trails = new Trail[lines.Count, lines[0].Length];
            List<(int x, int y)> trailHeads = new List<(int x, int y)>();

            int trailIndex = 0;
            for (int i = 0; i < trails.GetLength(0); i++)
            {
                for (int j = 0; j < trails.GetLength(1); j++)
                {
                    int height = lines[i][j].ToInt();
                    trails[i, j] = new Trail(height, trailIndex);
                    if (height == 0)
                    {
                        trailIndex++;
                        trailHeads.Add((i, j));
                    }

                }
            }
            foreach ((int x, int y) trailHead in trailHeads)
            {
                answer += CountDistinctTrailsFromTrailHead(ref trails, trailHead, trails[trailHead.x, trailHead.y].trailHeadId, 0, new List<(int x, int y)>(), false);
            }

            return answer.ToString();
        }


        public int CountDistinctTrailsFromTrailHead(ref Trail[,] trailMap, (int x, int y) position, int trailId, int count, List<(int x, int y)> previousTrails, bool allowDescent = false) 
        {
            previousTrails.Add(position);
            trailMap[position.x, position.y].accessibleByTrails.Add(trailId);
            int currentHeight = trailMap[position.x, position.y].height;
            int oldCount = count;

            if (currentHeight == 9)
                count++;

            if (position.x != 0 && ValidHeightDifference(currentHeight, trailMap[position.x - 1, position.y].height, allowDescent))
                count += CountDistinctTrailsFromTrailHead(ref trailMap, (position.x - 1, position.y), trailId, oldCount, previousTrails);

            if (position.x < trailMap.GetLength(0) - 1 && ValidHeightDifference(currentHeight, trailMap[position.x + 1, position.y].height, allowDescent))
                count += CountDistinctTrailsFromTrailHead(ref trailMap, (position.x + 1, position.y), trailId, oldCount, previousTrails);

            if (position.y != 0 && ValidHeightDifference(currentHeight, trailMap[position.x, position.y - 1].height, allowDescent))
                count += CountDistinctTrailsFromTrailHead(ref trailMap, (position.x, position.y - 1), trailId, oldCount, previousTrails);

            if (position.y < trailMap.GetLength(1) - 1 && ValidHeightDifference(currentHeight, trailMap[position.x, position.y + 1].height, allowDescent))
                count += CountDistinctTrailsFromTrailHead(ref trailMap, (position.x, position.y + 1), trailId, oldCount, previousTrails);

            return count;
        }

        public bool ValidHeightDifference(int heightOne, int heightTwo, bool allowDescent = false) 
        {
            if (heightOne == heightTwo && allowDescent) return true;
            if (heightOne - heightTwo == -1) return true;
            if (heightOne - heightTwo == 1 && allowDescent) return true;
            return false;
        }

        public class Trail 
        {
            public int height;
            public bool trailHead;
            public int trailHeadId;
            public List<int> accessibleByTrails;

            public Trail(int height, int trailHeadId) 
            {
                this.height = height;
                trailHead = height == 0;
                this.trailHeadId = trailHead ?  trailHeadId : -1;
                accessibleByTrails = new List<int>();
            
            }
        
        }
    }
}
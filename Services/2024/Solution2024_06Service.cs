using static AdventOfCode.Services.Solution2024_06Service;

namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2024/06.txt
    public class Solution2024_06Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 6, example);

            int answer = 0;

            LabPosition[,] labGrid = new LabPosition[lines[0].Length, lines.Count];
            Guard guard = null;

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];

                for (int j = 0; j < lines[0].Length; j++)
                {
                    labGrid[j, i] = new LabPosition();

                    char c = lines[i][j];
                    if (c == '#')
                    {
                        labGrid[j, i].obstacle = true;
                    }

                    if (c == '^')
                    {
                        labGrid[j, i].visited = true;
                        guard = new Guard(j, i);
                    }
                }
            }


            bool repeatPosition = false;
            do
            {
                int xMod = 0;
                int yMod = 0;

                switch (guard!.direction)
                {
                    case Direction.none:
                        //Something has gone horribly wrong. The Guard has died
                        repeatPosition = true;
                        break;
                    case Direction.up:
                        yMod = -1;
                        break;
                    case Direction.right:
                        xMod = 1;
                        break;
                    case Direction.down:
                        yMod = 1;
                        break;
                    case Direction.left:
                        xMod = -1;
                        break;
                    default:
                        break;
                }
                if (guard.x + xMod < 0 ||
                    guard.x + xMod >= lines[0].Length ||
                    guard.y + yMod < 0 ||
                    guard.y + yMod >= lines.Count)
                {
                    repeatPosition = true;
                    break;
                }
                if (labGrid[guard.x + xMod, guard.y + yMod].obstacle)
                {
                    guard.direction = (Direction)((int)(guard.direction + 1) % 4);
                    continue;
                }
                else
                {
                    guard.x += xMod;
                    guard.y += yMod;
                    labGrid[guard.x, guard.y].visited = true;
                    if (labGrid[guard.x, guard.y].directionsVisitedFrom.Contains(guard.direction))
                    {
                        repeatPosition = true;
                        break;
                    }

                    labGrid[guard.x, guard.y].directionsVisitedFrom.Add(guard.direction);
                }

            }
            while (!repeatPosition);


            answer = GetCountOfVisitedPositions(ref labGrid);

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 6, example);

            int answer = 0;

            LabPosition[,] labGrid = new LabPosition[lines[0].Length, lines.Count];
            List<GridCoordinates> obstacleCandidates = new List<GridCoordinates>();
            GridCoordinates guardStart = null;
            Guard guard = null;

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];

                for (int j = 0; j < lines[0].Length; j++)
                {
                    labGrid[j, i] = new LabPosition();

                    char c = lines[i][j];
                    if (c == '#')
                    {
                        labGrid[j, i].obstacle = true;
                    }

                    if (c == '^')
                    {
                        labGrid[j, i].visited = true;
                        guard = new Guard(j, i);
                        guardStart = new GridCoordinates(j, i);
                    }
                }
            }


            bool repeatPosition = false;
            do
            {
                int xMod = 0;
                int yMod = 0;

                switch (guard!.direction)
                {
                    case Direction.none:
                        //Something has gone horribly wrong. The Guard has died
                        repeatPosition = true;
                        break;
                    case Direction.up:
                        yMod = -1;
                        break;
                    case Direction.right:
                        xMod = 1;
                        break;
                    case Direction.down:
                        yMod = 1;
                        break;
                    case Direction.left:
                        xMod = -1;
                        break;
                    default:
                        break;
                }
                if (guard.x + xMod < 0 ||
                    guard.x + xMod >= lines[0].Length ||
                    guard.y + yMod < 0 ||
                    guard.y + yMod >= lines.Count)
                {
                    repeatPosition = true;
                    break;
                }
                if (labGrid[guard.x + xMod, guard.y + yMod].obstacle)
                {
                    guard.direction = (Direction)((int)(guard.direction + 1) % 4);
                    continue;
                }
                else
                {
                    guard.x += xMod;
                    guard.y += yMod;
                    labGrid[guard.x, guard.y].visited = true;
                    if (labGrid[guard.x, guard.y].directionsVisitedFrom.Contains(guard.direction))
                    {
                        repeatPosition = true;
                        break;
                    }

                    labGrid[guard.x, guard.y].directionsVisitedFrom.Add(guard.direction);
                }

            }
            while (!repeatPosition);

            obstacleCandidates = GetObstacleCandidates(ref labGrid);

            foreach (GridCoordinates obstacleCandidate in obstacleCandidates) 
            {
                if (obstacleCandidate.x == guardStart.x && obstacleCandidate.y == guardStart.y)
                    continue;
                if (DoesGridLoopWithObstacle(ref labGrid, obstacleCandidate, guardStart))
                    answer++;
            }

            return answer.ToString();
        }


        public int GetCountOfVisitedPositions(ref LabPosition[,] labGrid)
        {
            int visitedPositions = 0;

            foreach (LabPosition labPosition in labGrid)
            {
                if (labPosition.visited)
                    visitedPositions++;
            }

            return visitedPositions;
        }

        public List<GridCoordinates> GetObstacleCandidates(ref LabPosition[,] labGrid)
        {
            List<GridCoordinates> candidates = new List<GridCoordinates>();
            for (int i = 0; i < labGrid.GetLength(0); i++) 
            {
                for (int j = 0; j < labGrid.GetLength(0); j++)
                {
                    if (labGrid[i,j].visited)
                        candidates.Add(new GridCoordinates(i, j));
                }
            }

            return candidates;
        }

        public bool DoesGridLoopWithObstacle(ref LabPosition[,] labGrid, GridCoordinates obstacle, GridCoordinates guardStart) 
        {
            foreach (LabPosition labPosition in labGrid) 
            {
                labPosition.visited = false;
                labPosition.directionsVisitedFrom = new List<Direction>();
                if (labPosition.tempObstacle) 
                {
                    labPosition.obstacle = false;
                    labPosition.tempObstacle = false;
                }

            }

            labGrid[obstacle.x, obstacle.y].obstacle = true;
            labGrid[obstacle.x, obstacle.y].tempObstacle = true;
            Guard guard = new Guard(guardStart.x, guardStart.y);
            labGrid[guardStart.x, guardStart.y].visited = true;
            labGrid[guardStart.x, guardStart.y].directionsVisitedFrom.Add(Direction.up);

            bool repeatPosition = false;
            do
            {
                int xMod = 0;
                int yMod = 0;

                switch (guard!.direction)
                {
                    case Direction.none:
                        //Something has gone horribly wrong. The Guard has died
                        repeatPosition = true;
                        break;
                    case Direction.up:
                        yMod = -1;
                        break;
                    case Direction.right:
                        xMod = 1;
                        break;
                    case Direction.down:
                        yMod = 1;
                        break;
                    case Direction.left:
                        xMod = -1;
                        break;
                    default:
                        break;
                }
                if (guard.x + xMod < 0 ||
                    guard.x + xMod >= labGrid.GetLength(0) ||
                    guard.y + yMod < 0 ||
                    guard.y + yMod >= labGrid.GetLength(1))
                {
                    return false;
                }
                if (labGrid[guard.x + xMod, guard.y + yMod].obstacle)
                {
                    guard.direction = (Direction)((int)(guard.direction + 1) % 4);
                    continue;
                }
                else
                {
                    guard.x += xMod;
                    guard.y += yMod;
                    labGrid[guard.x, guard.y].visited = true;
                    if (labGrid[guard.x, guard.y].directionsVisitedFrom.Contains(guard.direction))
                    {
                        repeatPosition = true;
                        return true;
                    }
                    labGrid[guard.x, guard.y].directionsVisitedFrom.Add(guard.direction);
                }

            }
            while (!repeatPosition);

            return false;
        }

        public enum Direction
        {
            none = -1,
            up = 0,
            right = 1,
            down = 2,
            left = 3,
        }

        public class GridCoordinates
        {
            public int x;
            public int y;

            public GridCoordinates(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public class LabPosition()
        {
            public bool visited = false;
            public List<Direction> directionsVisitedFrom = new List<Direction>();
            public bool obstacle = false;
            public bool tempObstacle = false;
        }

        public class Guard
        {
            public Direction direction;
            public int x;
            public int y;

            public Guard(int x, int y)
            {
                this.direction = Direction.up;
                this.x = x;
                this.y = y;
            }
        }
    }
}
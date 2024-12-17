using System.Reflection.Metadata;

namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2024/12.txt
    public class Solution2024_12Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 12, example);

            long answer = 0;
            GridItem[,] grid = new GridItem[lines.Count, lines[0].Length];
            List<GridRegion> regions = new List<GridRegion>();

            for (int i = 0; i < grid.GetLength(0); i++) 
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = new GridItem(lines[i][j]);
                }
            }

            int regionCount = 0;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (!grid[i, j].mapped)
                    {
                        List<(int x, int y)> regionPositions = MapRegion(ref grid, (i, j));
                        regions.Add(new GridRegion(regionCount, regionPositions));
                    }                       
                }
            }

            foreach (GridRegion region in regions) 
            {
                answer += region.gridSize * region.Perimeter();
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 12, example);

            long answer = 0;
            GridItem[,] grid = new GridItem[lines.Count, lines[0].Length];
            List<GridRegion> regions = new List<GridRegion>();

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = new GridItem(lines[i][j]);
                }
            }

            int regionCount = 0;
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (!grid[i, j].mapped)
                    {
                        List<(int x, int y)> regionPositions = MapRegion(ref grid, (i, j));
                        regions.Add(new GridRegion(regionCount, regionPositions));
                    }
                }
            }

            foreach (GridRegion region in regions)
            {
                answer += region.gridSize * region.Corners();
            }

            return answer.ToString();
        }

        public List<(int x, int y)> MapRegion(ref GridItem[,] gridMap, (int x, int y) gridPosition)
        {
            List<(int x, int y)> neighbors = new List<(int x, int y)>();
            neighbors.Add(gridPosition);

            gridMap[gridPosition.x, gridPosition.y].mapped = true;
            char currentIdentifier = gridMap[gridPosition.x, gridPosition.y].identifierChar;

            if (gridPosition.x > 0 && gridMap[gridPosition.x - 1, gridPosition.y].ValidNeighbor(currentIdentifier))
                neighbors.AddRange(MapRegion(ref gridMap, (gridPosition.x - 1, gridPosition.y)));

            if (gridPosition.x < gridMap.GetLength(0) - 1 && gridMap[gridPosition.x + 1, gridPosition.y].ValidNeighbor(currentIdentifier))
                neighbors.AddRange(MapRegion(ref gridMap, (gridPosition.x + 1, gridPosition.y)));

            if (gridPosition.y > 0 && gridMap[gridPosition.x, gridPosition.y - 1].ValidNeighbor(currentIdentifier))
                neighbors.AddRange(MapRegion(ref gridMap, (gridPosition.x, gridPosition.y - 1)));

            if (gridPosition.y < gridMap.GetLength(1) - 1 && gridMap[gridPosition.x, gridPosition.y + 1].ValidNeighbor(currentIdentifier))
                neighbors.AddRange(MapRegion(ref gridMap, (gridPosition.x, gridPosition.y + 1)));
            return neighbors;
        }

        public class GridItem 
        {
            public char identifierChar;
            public bool mapped;

            public GridItem(char c) 
            {
                identifierChar = c;
                mapped = false;
            }

            public bool ValidNeighbor(char comparisonChar) 
            {
                return !mapped && comparisonChar == identifierChar;
            }
        }

        public class GridRegion
        {
            int id = 0;
            List<(int x, int y)> gridPositions = new List<(int x, int y)>();
            public int gridSize { get { return gridPositions.Count(); } }


            public GridRegion(int id, List<(int x, int y)> gridPositions) 
            {
                this.id = id;
                this.gridPositions = gridPositions;
            }

            public int Perimeter()
            {
                int perimeter = 0;
                foreach ((int x, int y) pos in gridPositions)
                {
                    if (!gridPositions.Any(p => (p.x == pos.x + 1) && (p.y == pos.y)))
                        perimeter++;
                    if (!gridPositions.Any(p => (p.x == pos.x - 1) && (p.y == pos.y)))
                        perimeter++;
                    if (!gridPositions.Any(p => (p.x == pos.x) && (p.y == pos.y + 1)))
                        perimeter++;
                    if (!gridPositions.Any(p => (p.x == pos.x) && (p.y == pos.y - 1)))
                        perimeter++;
                }
                return perimeter;
            }

            public int Corners()
            {
                int corners = 0;

                foreach ((int x, int y) pos in gridPositions)
                {
                    corners += GetExteriorCorners(pos);
                    corners += GetInteriorCorners(pos);
                }
                return corners;
            }


            public int GetExteriorCorners((int x, int y) targetPosition) 
            {
                int corners = 0;
                for (int i = -1; i <= 1; i += 2) 
                {
                    for (int j = -1; j <= 1; j += 2)
                    {
                        if (!gridPositions.Any(pos => pos.x == targetPosition.x + i && pos.y == targetPosition.y) && 
                            !gridPositions.Any(pos => pos.x == targetPosition.x && pos.y == targetPosition.y + j))
                            corners++;
                    }
                }
                return corners;
            }

            public int GetInteriorCorners((int x, int y) targetPosition)
            {
                int corners = 0;
                for (int i = -1; i <= 1; i += 2)
                {
                    for (int j = -1; j <= 1; j += 2)
                    {
                        if (gridPositions.Any(pos => pos.x == targetPosition.x + i && pos.y == targetPosition.y) &&
                            gridPositions.Any(pos => pos.x == targetPosition.x && pos.y == targetPosition.y + j) &&
                            !gridPositions.Any(pos => pos.x == targetPosition.x + i && pos.y == targetPosition.y + j))
                            corners++;
                    }
                }
                return corners;
            }
        }
    }
}
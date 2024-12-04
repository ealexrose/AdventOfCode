using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2024/04.txt
    public class Solution2024_04Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 4, example);

            int answer = 0;

            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    char candidate = lines[i][j];
                    if (candidate == 'X')
                        answer += GetXMasCountFromPoint(ref lines, i, j);
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 4, example);

            int answer = 0;


            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    char candidate = lines[i][j];
                    if (candidate == 'A')
                        answer += GetCrossMasCountFromPoint(ref lines, i, j);
                }
            }


            return answer.ToString();
        }

        public int GetXMasCountFromPoint(ref List<string> grid, int x, int y)
        {
            char[] neededLetters = ['M', 'A', 'S'];

            int matches = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    bool matching = true;

                    for (int k = 0; k < neededLetters.Length; k++)
                    {
                        int xOffset = i * (k + 1);
                        int yOffset = j * (k + 1);
                        if (x + xOffset < 0 || y + yOffset < 0)
                        {
                            matching = false;
                            break;
                        }

                        if (x + xOffset >= grid.Count || y + yOffset >= grid[x + xOffset].Length)
                        {
                            matching = false;
                            break;
                        }
                        char candidate = grid[x + xOffset][y + yOffset];
                        if (candidate == neededLetters[k])
                        {
                            continue;
                        }
                        else
                        {
                            matching = false;
                            break;
                        }
                    }
                    if (matching)
                        matches++;
                }
            }

            return matches;
        }

        public int GetCrossMasCountFromPoint(ref List<string> grid, int x, int y)
        {
            if (x < 1 || x >= grid.Count - 1)
                return 0;

            if (y < 1 || y >= grid[x].Length - 1)
                return 0;

            int[] cornerSetOne = [-1, -1, +1, +1];
            int[] cornerSetTwo = [-1, +1, +1, -1];

            if ((int)grid[x + cornerSetOne[0]][y + cornerSetOne[1]] + (int)grid[x + cornerSetOne[2]][y + cornerSetOne[3]] == (int)'M' + (int)'S' &&
                (int)grid[x + cornerSetTwo[0]][y + cornerSetTwo[1]] + (int)grid[x + cornerSetTwo[2]][y + cornerSetTwo[3]] == (int)'M' + (int)'S')
            {
                return 1;
            }

            return 0;
        }
    }
}
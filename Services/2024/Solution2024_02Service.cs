namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2024/02.txt
    public class Solution2024_02Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 2, example);

            int answer = 0;

            foreach (string line in lines)
            {
                string[] lineParts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                List<int> lineInts = new List<int>();
                foreach (var item in lineParts)
                {
                    lineInts.Add(int.Parse(item));
                }
                int direction = MathF.Sign(lineInts.Last() - lineInts.First());


                if (direction == 0)
                    continue;

                bool allSameDirection = true;
                for (int i = 1; i < lineInts.Count(); i++)
                {
                    if (MathF.Sign(lineInts[i] - lineInts[i - 1]) != direction || MathF.Abs(lineInts[i] - lineInts[i - 1]) > 3)
                    {
                        allSameDirection = false;
                        break;
                    }
                }

                if (allSameDirection)
                    answer++;
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 2, example);

            int answer = 0;

            foreach (string line in lines)
            {
                string[] lineParts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                List<int> lineInts = new List<int>();
                foreach (var item in lineParts)
                {
                    lineInts.Add(int.Parse(item));
                }
                int direction = MathF.Sign(lineInts.Last() - lineInts.First());


                //Check if the unaltered list is valid
                if (direction == 0)
                    continue;

                bool allSameDirectionNoDampen = true;
                for (int i = 1; i < lineInts.Count(); i++)
                {
                    if (MathF.Sign(lineInts[i] - lineInts[i - 1]) != direction || MathF.Abs(lineInts[i] - lineInts[i - 1]) > 3)
                    {
                        allSameDirectionNoDampen = false;
                        break;
                    }
                }
                
                //If the list was valid skip to the next one
                if (allSameDirectionNoDampen)
                {
                    answer++;
                    continue;
                }

                //If the list wasn't valid try each iteration of the list that could exist by removing any given member
                for (int j = 0; j < lineInts.Count(); j++)
                {
                    List<int> dampenedList = new List<int>(lineInts);
                    dampenedList.RemoveAt(j);
                    direction = MathF.Sign(dampenedList.Last() - dampenedList.First());
                    if (direction == 0)
                        continue;

                    bool allSameDirection = true;
                    for (int i = 1; i < dampenedList.Count(); i++)
                    {
                        if (MathF.Sign(dampenedList[i] - dampenedList[i - 1]) != direction || MathF.Abs(dampenedList[i] - dampenedList[i - 1]) > 3)
                        {
                            allSameDirection = false;
                            break;
                        }
                    }
                    if (allSameDirection)
                    {
                        answer++;
                        break;
                    }

                }

            }

            return answer.ToString();
        }
    }
}
namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2024/01.txt
    public class Solution2024_01Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 1, example);

            int answer = 0;

            List<int> leftList = new List<int>();
            List<int> rightList = new List<int>();
            foreach (string line in lines)
            {
                string[] lineParts = line.Split(' ');
                leftList.Add(int.Parse(lineParts[0]));
                rightList.Add(int.Parse(lineParts.Last()));
            }
            leftList.Sort();
            rightList.Sort();

            for(int i = 0; i < leftList.Count; i++)
            {
                answer += Math.Abs(leftList[i] - rightList[i]);
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 1, example);

            int answer = 0;

            List<int> leftList = new List<int>();
            List<int> rightList = new List<int>();
            foreach (string line in lines)
            {
                string[] lineParts = line.Split(' ');
                leftList.Add(int.Parse(lineParts[0]));
                rightList.Add(int.Parse(lineParts.Last()));
            }
            leftList.Sort();
            rightList.Sort();

            for (int i = 0; i < leftList.Count; i++)
            {
                answer += rightList.Where(x => x == leftList[i]).ToArray().Count() * leftList[i];
            }

            return answer.ToString();
        }
    }
}
namespace AdventOfCode.Services
{
    public class Solution2023_02Service : ISolutionDayService
    {
        public Solution2023_02Service() { }

        public string FirstHalf()
        {
            List<string> lines =  File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2023", "02.txt")).ToList();

            int answer = 0;

            foreach (string line in lines) {

            }

            return answer.ToString();
        }

        public string SecondHalf()
        {
            List<string> lines =  File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2023", "02.txt")).ToList();

            int answer = 0;

            foreach (string line in lines) {

            }

            return answer.ToString();
        }
    }
}
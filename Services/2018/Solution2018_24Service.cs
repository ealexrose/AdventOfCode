namespace AdventOfCode.Services
{
    public class Solution2018_24Service : ISolutionDayService
    {
        public Solution2018_24Service() { }

        public async Task<string> FirstHalf(bool send)
        {
            List<string> lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2018_24.txt")).ToList();

            int answer = 0;

            foreach (string line in lines)
            {

            }

            return await Utility.SubmitAnswer(2018, 24, false, answer, send);
        }

        public async Task<string> SecondHalf(bool send)
        {
            List<string> lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2018_24.txt")).ToList();

            int answer = 0;

            foreach (string line in lines)
            {

            }

            return await Utility.SubmitAnswer(2018, 24, true, answer, send);
        }
    }
}
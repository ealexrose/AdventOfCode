namespace AdventOfCode.Services
{
    public class Solution2019_11Service : ISolutionDayService
    {
        public Solution2019_11Service() { }

        public async Task<string> FirstHalf(bool send)
        {
            List<string> lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2019_11.txt")).ToList();

            int answer = 0;

            foreach (string line in lines)
            {

            }

            return await Utility.SubmitAnswer(2019, 11, false, answer, send);
        }

        public async Task<string> SecondHalf(bool send)
        {
            List<string> lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2019_11.txt")).ToList();

            int answer = 0;

            foreach (string line in lines)
            {

            }

            return await Utility.SubmitAnswer(2019, 11, true, answer, send);
        }
    }
}
namespace AdventOfCode.Services
{
    public class Solution2018_21Service : ISolutionDayService
    {
        public Solution2018_21Service() { }

        public async Task<string> FirstHalf(bool send)
        {
            List<string> lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2018_21.txt")).ToList();

            int answer = 0;

            foreach (string line in lines)
            {

            }

            return await Utility.SubmitAnswer(2018, 21, false, answer, send);
        }

        public async Task<string> SecondHalf(bool send)
        {
            List<string> lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2018_21.txt")).ToList();

            int answer = 0;

            foreach (string line in lines)
            {

            }

            return await Utility.SubmitAnswer(2018, 21, true, answer, send);
        }
    }
}
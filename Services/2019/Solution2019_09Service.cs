namespace AdventOfCode.Services
{
    public class Solution2019_09Service : ISolutionDayService
    {
        public Solution2019_09Service() { }

        public async Task<string> FirstHalf(bool send)
        {
            List<string> lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2019_09.txt")).ToList();

            int answer = 0;

            foreach (string line in lines)
            {

            }

            return await Utility.SubmitAnswer(2019, 9, false, answer, send);
        }

        public async Task<string> SecondHalf(bool send)
        {
            List<string> lines = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Inputs", "2019_09.txt")).ToList();

            int answer = 0;

            foreach (string line in lines)
            {

            }

            return await Utility.SubmitAnswer(2019, 9, true, answer, send);
        }
    }
}
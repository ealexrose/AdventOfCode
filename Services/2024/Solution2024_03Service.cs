using System.Text.RegularExpressions;

namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2024/03.txt
    public class Solution2024_03Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 3, example);

            int answer = 0;
            string mulCapturePattern = "(mul\\(([0-9]{1,3}),([0-9]{1,3})\\))";
            Regex mulCapture = new Regex(mulCapturePattern);

            foreach (string line in lines)
            {
                foreach (Match multInstruction in Regex.Matches(line, mulCapturePattern, RegexOptions.None)) 
                {
                    string matchedString = multInstruction.Value;
                    string[] instructionParts = matchedString.Split(',');
                    int firstNumber = int.Parse(instructionParts[0].Substring(4, instructionParts[0].Length - 4));
                    int secondNumber = int.Parse(instructionParts[1].Substring(0, instructionParts[1].Length - 1));
                    answer += (firstNumber * secondNumber);
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 3, example);

            int answer = 0;
            string mulCapturePattern = "(mul\\(([0-9]{1,3}),([0-9]{1,3})\\))|(do\\(\\))|(don't\\(\\))";
            Regex mulCapture = new Regex(mulCapturePattern);

            bool doing = true;
            foreach (string line in lines)
            {
                foreach (Match multInstruction in Regex.Matches(line, mulCapturePattern, RegexOptions.None))
                {
                    if (multInstruction.Value == "do()")
                    {
                        doing = true;
                        continue;
                    }
                    if (multInstruction.Value == "don't()") 
                    {
                        doing = false;
                        continue;
                    }

                    if(!doing)
                        continue;
                    string matchedString = multInstruction.Value;
                    string[] instructionParts = matchedString.Split(',');
                    int firstNumber = int.Parse(instructionParts[0].Substring(4, instructionParts[0].Length - 4));
                    int secondNumber = int.Parse(instructionParts[1].Substring(0, instructionParts[1].Length - 1));
                    answer += (firstNumber * secondNumber);
                }
            }

            return answer.ToString();
        }
    }
}
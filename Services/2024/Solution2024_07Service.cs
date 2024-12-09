namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2024/07.txt
    public class Solution2024_07Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 7, example);

            long answer = 0;
            List<InstructionSet> instructionSets = new List<InstructionSet>();
            foreach (string line in lines)
            {
                string[] lineParts = line.Split(':');
                long targetNumber = long.Parse(lineParts[0]);
                long[] functionNumbers = lineParts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToLongs().ToArray();
                instructionSets.Add(new InstructionSet(targetNumber, functionNumbers));
            }

            foreach (InstructionSet instructionSet in instructionSets) 
            {

                Operator[] testOperators = new Operator[instructionSet.functionNumbers.Length - 1];

                long validOperatorTypes = 2;
                long maxValue = 1;
                for (int i = 0; i < testOperators.Length; i++) 
                {
                    maxValue *= validOperatorTypes;
                }


                for (long j = 0; j < maxValue; j++) 
                {
                    long operatorNumber = j;
                    for (long k = 1; k <= testOperators.Length; k++) 
                    {

                        long operatorValue = operatorNumber % ((long)Math.Pow(validOperatorTypes, k));
                        Operator newOperator = (Operator)(operatorValue / (long)Math.Pow(validOperatorTypes, k - 1));
                        testOperators[k - 1] = newOperator;
                        operatorNumber -= operatorValue;
                    }
                    long operatorResults = instructionSet.ApplyOperators(testOperators);
                    if (operatorResults == instructionSet.targetNumber) 
                    {
                        answer += instructionSet.targetNumber;
                        break;
                    }
                }
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 7, example);

            long answer = 0;
            List<InstructionSet> instructionSets = new List<InstructionSet>();
            foreach (string line in lines)
            {
                string[] lineParts = line.Split(':');
                long targetNumber = long.Parse(lineParts[0]);
                long[] functionNumbers = lineParts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToLongs().ToArray();
                instructionSets.Add(new InstructionSet(targetNumber, functionNumbers));
            }

            foreach (InstructionSet instructionSet in instructionSets)
            {

                Operator[] testOperators = new Operator[instructionSet.functionNumbers.Length - 1];

                long validOperatorTypes = 3;
                long maxValue = 1;
                for (int i = 0; i < testOperators.Length; i++)
                {
                    maxValue *= validOperatorTypes;
                }


                for (long j = 0; j < maxValue; j++)
                {
                    long operatorNumber = j;
                    for (long k = 1; k <= testOperators.Length; k++)
                    {

                        long operatorValue = operatorNumber % ((long)Math.Pow(validOperatorTypes, k));
                        Operator newOperator = (Operator)(operatorValue / (long)Math.Pow(validOperatorTypes, k - 1));
                        testOperators[k - 1] = newOperator;
                        operatorNumber -= operatorValue;
                    }
                    long operatorResults = instructionSet.ApplyOperators(testOperators);
                    if (operatorResults == instructionSet.targetNumber)
                    {
                        answer += instructionSet.targetNumber;
                        break;
                    }
                }
            }

            return answer.ToString();
        }

        public enum Operator
        {
            Add = 0,
            Multiply = 1,
            Concat = 2,
        }


        public class InstructionSet
        {
            public long targetNumber;
            public long[] functionNumbers;

            public long ApplyOperators(Operator[] operatorSet)
            {
                if (operatorSet.Length != functionNumbers.Length - 1)
                    return -1;

                long operatortionsTotal = functionNumbers[0];
                for (long i = 0; i < operatorSet.Length; i++)
                {
                    switch (operatorSet[i])
                    {
                        case Operator.Add:
                            operatortionsTotal += functionNumbers[i+1];
                            break;
                        case Operator.Multiply:
                            operatortionsTotal *= functionNumbers[i+1];
                            break;
                        case Operator.Concat:
                            operatortionsTotal = long.Parse(String.Concat(operatortionsTotal.ToString(), functionNumbers[i + 1].ToString()));
                            break;
                    }
                }

                return operatortionsTotal;
            }

            public bool AddAllExceedsTarget() 
            {
                if (functionNumbers.Sum() > targetNumber)
                    return true;
                return false;
            }

            public bool MultAllMeetsTarget() 
            {
                if (functionNumbers.Aggregate(1L, (x, y) => x * y) >= targetNumber)
                    return true;
                return false;
            }

            public InstructionSet(long targetNumber, long[] functionNumbers)
            {
                this.targetNumber = targetNumber;
                this.functionNumbers = functionNumbers;
            }
        }
    }
}
using System.Reflection.Metadata;
using static AdventOfCode.Services.Solution2024_09Service;

namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2024/09.txt
    public class Solution2024_09Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 9, example);

            long answer = 0;

            List<MemoryBlock> memoryBlocks = BuildDrive(lines[0]);

            for (int i = memoryBlocks.Count - 1; i >= 0; i--)
            {
                if (memoryBlocks[i].freeMemory != true)
                {
                    int freeMemoryIndex = memoryBlocks.FindIndex(0, i, x => x.freeMemory == true);
                    if (freeMemoryIndex != -1)
                        SwapMemoryBlock(ref memoryBlocks, i, freeMemoryIndex);
                    else
                        break;
                }
            }

            answer = GetChecksum(ref memoryBlocks);

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 9, example);

            long answer = 0;

            List<MemoryBlock> memoryBlocks = BuildDrive(lines[0]);

            for (int i = memoryBlocks.Count - 1; i >= 0; i--)
            {
                if (memoryBlocks[i].freeMemory != true)
                {
                    int freeMemoryIndex = memoryBlocks.FindIndex(0, i, x => x.freeMemory == true && x.fileSize >= memoryBlocks[i].fileSize);
                    if (freeMemoryIndex != -1)
                        SwapFile(ref memoryBlocks, i, freeMemoryIndex);
                    else
                        continue;
                }
            }

            answer = GetChecksum(ref memoryBlocks);

            return answer.ToString();
        }

        public class MemoryBlock
        {
            public bool freeMemory = true;
            public int id = -1;
            public int currentIndex = 0;
            public int fileSize = 0;

            public MemoryBlock(int id, bool freeMemory, int fileSize)
            {
                this.id = id;
                this.freeMemory = freeMemory;
                if (freeMemory)
                    this.id = -1;
                this.fileSize = fileSize;
            }
        }

        private void SwapFile(ref List<MemoryBlock> memoryBlocks, int fileIndex, int freeSpaceIndex)
        {
            int fileSize = memoryBlocks[fileIndex].fileSize;
            fileIndex = fileIndex - (fileSize - 1);
            int freeSpaceSize = memoryBlocks[freeSpaceIndex].fileSize;
            for (int i = 0; i < fileSize; i++)
            {
                SwapMemoryBlock(ref memoryBlocks, fileIndex + i, freeSpaceIndex + i);
            }
            for (int i = fileSize; i < freeSpaceSize; i++)
            {
                memoryBlocks[freeSpaceIndex + i].fileSize = freeSpaceSize - fileSize;
            }
        }


        private void SwapMemoryBlock(ref List<MemoryBlock> memoryBlocks, int indexOne, int indexTwo)
        {
            MemoryBlock temp = memoryBlocks[indexOne];
            memoryBlocks[indexOne] = memoryBlocks[indexTwo];
            memoryBlocks[indexOne].currentIndex = indexOne;
            memoryBlocks[indexTwo] = temp;
            memoryBlocks[indexTwo].currentIndex = indexTwo;
        }

        public List<MemoryBlock> BuildDrive(string diskMap)
        {
            List<MemoryBlock> memoryBlocks = new List<MemoryBlock>();
            bool freeMemory = false;
            int currentId = 0;

            foreach (char c in diskMap)
            {
                int blockSize = int.Parse(c.ToString());
                memoryBlocks.AddRange(BuildMemoryBlocks(blockSize, currentId, freeMemory));

                if (!freeMemory && blockSize != 0)
                    currentId++;
                freeMemory = !freeMemory;
            }

            for (int i = 0; i < memoryBlocks.Count; i++)
            {
                memoryBlocks[i].currentIndex = i;
            }
            return memoryBlocks;
        }

        public List<MemoryBlock> BuildMemoryBlocks(int length, int id, bool freeMemory)
        {
            List<MemoryBlock> memoryBlocks = new List<MemoryBlock>();
            for (int i = 0; i < length; i++)
            {
                MemoryBlock memoryBlock = new MemoryBlock(id, freeMemory, length);
                memoryBlocks.Add(memoryBlock);
            }
            return memoryBlocks;
        }

        public long GetChecksum(ref List<MemoryBlock> memoryBlocks)
        {
            long answer = 0;
            for (int i = 0; i < memoryBlocks.Count; i++)
            {
                if (!memoryBlocks[i].freeMemory)
                    answer += memoryBlocks[i].id * i;
            }
            return answer;
        }
    }
}
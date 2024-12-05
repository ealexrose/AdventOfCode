namespace AdventOfCode.Services
{
    // (ctrl/command + click) the link to open the input file
    // file://./../../Inputs/2024/05.txt
    public class Solution2024_05Service : ISolutionDayService
    {
        public string FirstHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 5, example);

            int answer = 0;
            bool pageRuleSection = true;
            List<PageRule> pageRules = new List<PageRule>();
            List<List<int>> pageUpdates = new List<List<int>>();
            foreach (string line in lines)
            {
                if (pageRuleSection)
                {
                    if (line == "")
                    {
                        pageRuleSection = false;
                        continue;
                    }

                    if (pageRuleSection == true)
                    {
                        PageRule rule = new PageRule();
                        string[] pageRuleNumbers = line.Split('|');
                        rule.firstPage = int.Parse(pageRuleNumbers[0]);
                        rule.secondPage = int.Parse(pageRuleNumbers[1]);
                        pageRules.Add(rule);
                    }
                }
                else
                {
                    string[] pageUpdateNumbers = line.Split(',');
                    pageUpdates.Add(pageUpdateNumbers.Select(int.Parse).ToList());
                }
                
            }

            pageRules = pageRules.OrderBy(p => p.firstPage).ToList();

            foreach (List<int> pageUpdate in pageUpdates) 
            {
                bool validPageUpdate = true;
                for (int i = 0; i < pageUpdate.Count; i++) 
                {
                    if (!pageRules.Any(x => x.firstPage == pageUpdate[i]))
                        continue;

                    List<PageRule> releventPageRules = pageRules.Where(x => x.firstPage == pageUpdate[i]).ToList();
                    foreach (PageRule releventPageRule in releventPageRules) 
                    {
                        if (pageUpdate.Contains(releventPageRule.secondPage) && pageUpdate.FindIndex(x => x == releventPageRule.secondPage) < i)
                        {
                            validPageUpdate = false;
                            break;
                        }
                    }
                }

                if (validPageUpdate)
                    answer += pageUpdate[pageUpdate.Count/2];
            }

            return answer.ToString();
        }

        public string SecondHalf(bool example)
        {
            List<string> lines = Utility.GetInputLines(2024, 5, example);

            int answer = 0;
            bool pageRuleSection = true;
            List<PageRule> pageRules = new List<PageRule>();
            List<List<int>> pageUpdates = new List<List<int>>();
            foreach (string line in lines)
            {
                if (pageRuleSection)
                {
                    if (line == "")
                    {
                        pageRuleSection = false;
                        continue;
                    }

                    if (pageRuleSection == true)
                    {
                        PageRule rule = new PageRule();
                        string[] pageRuleNumbers = line.Split('|');
                        rule.firstPage = int.Parse(pageRuleNumbers[0]);
                        rule.secondPage = int.Parse(pageRuleNumbers[1]);
                        pageRules.Add(rule);
                    }
                }
                else
                {
                    string[] pageUpdateNumbers = line.Split(',');
                    pageUpdates.Add(pageUpdateNumbers.Select(int.Parse).ToList());
                }

            }

            pageRules = pageRules.OrderBy(p => p.firstPage).ToList();

            foreach (List<int> pageUpdate in pageUpdates)
            {
                bool updateWasSorted = false;
for (int i = 0; i < pageUpdate.Count; i++)
{
    if (!pageRules.Any(x => x.firstPage == pageUpdate[i]))
        continue;

    List<PageRule> releventPageRules = pageRules.Where(x => x.firstPage == pageUpdate[i]).ToList();
    foreach (PageRule releventPageRule in releventPageRules)
    {
        if (pageUpdate.Contains(releventPageRule.secondPage) && pageUpdate.FindIndex(x => x == releventPageRule.secondPage) < i)
        {
            pageUpdate.RemoveAt(i);
            pageUpdate.Insert(pageUpdate.FindIndex(x => x == releventPageRule.secondPage), releventPageRule.firstPage);
            i = 0;
            updateWasSorted = true;
        }
    }
}
                if(updateWasSorted)
                    answer += pageUpdate[pageUpdate.Count / 2];
            }

            return answer.ToString();
        }
    }

    public class PageRule 
    {
        public int firstPage;
        public int secondPage;
    }
}
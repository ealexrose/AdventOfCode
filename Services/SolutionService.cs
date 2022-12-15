namespace AdventOfCode.Services
{
    public class SolutionService : ISolutionService
    {
        private readonly IServiceProvider serviceProvider;

        public SolutionService(IServiceProvider serviceProvider, ISolutionDayService solutionDayService)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<string> GetSolution(int year, int day, bool secondHalf, bool send)
        {
            // Fetch the specific service
            IEnumerable<ISolutionDayService> services = serviceProvider.GetServices<ISolutionDayService>();
            ISolutionDayService service = services.FirstOrDefault(s => s.GetType().ToString() == $"AdventOfCode.Services.Solution{year}_{day:D2}Service");

            // If the service was not found, throw an exception
            if (service == null)
            {
                throw new SolutionNotFoundException($"No solutions found for day {day}/{year}.");
            }

            // Get the specific solutino
            return secondHalf ? await service.SecondHalf(send) : await service.FirstHalf(send);
        }
    }
}
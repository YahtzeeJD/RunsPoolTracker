using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using RunsPoolTracker.AppService;
using RunsPoolTracker.Model;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Razor.Pages
{
    public class StandingsModel : PageModel
    {
        public TeamRunsCollection TeamRunsCollection { get; set; }
        public int Round { get; set; }

        public async Task OnGet()
        {
            Initialize(out DateTime roundStartDate, out MlbAppService appService);
            TeamRunsCollection = await appService.ProcessDailyGamesForRound(roundStartDate);
        }

        private void Initialize(out DateTime roundStartDate, out MlbAppService appService)
        {
            IConfigurationRoot configuration = InitializeConfiguration();
            Round = 4;
            var roundStartDateFromConfig = $"round{Round}_start_date";
            roundStartDate = Convert.ToDateTime(configuration[roundStartDateFromConfig]);
            appService = new MlbAppService();
        }

        private IConfigurationRoot InitializeConfiguration()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            return configuration;
        }
    }
}

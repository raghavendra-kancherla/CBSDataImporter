using CBSDataImporter.Models;
using CBSDataImporter.Repositories;
using CBSDataImporter.Repositories.Interfaces;
using CBSDataImporter.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBSDataImporter
{
    public class Application
	{
		private readonly ICRMRepository crmRepository;
		private readonly ICBSRepository cbsRepository;
		private readonly int MAX_DEGREE_PARALLELISM = 20;
		public Application(ICRMRepository crmRepository, ICBSRepository cbsRepository)
		{
			this.crmRepository = crmRepository;
			this.cbsRepository = cbsRepository;
		}

		public void OnExecute()
		{
			Console.WriteLine("CBSDataImporter started at {0}", DateTime.Now);

			//Fetch all the sports to process
			var sportsToProcess = crmRepository.GetSports();
			Parallel.ForEach(sportsToProcess, new ParallelOptions { MaxDegreeOfParallelism = MAX_DEGREE_PARALLELISM }, (sport) =>
			{
			//Get players data from CBS
			var players = cbsRepository.GetPlayers(sport.SportName);
				if (players?.Count > 0)
				{
					//break into smaller chunks of data to process

					IEnumerable<Player[]> playerSublists = players.Chunk(500);

					Parallel.ForEach(playerSublists, new ParallelOptions { MaxDegreeOfParallelism = MAX_DEGREE_PARALLELISM }, (playerSublist) =>
						{
							crmRepository.SavePlayers(playerSublist.ToList(), sport.SportId);
						});
				}
			});
			Console.WriteLine("CBSDataImporter Ended at {0}", DateTime.Now);
		}
	}
}

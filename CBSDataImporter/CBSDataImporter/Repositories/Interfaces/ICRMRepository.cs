using CBSDataImporter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBSDataImporter.Repositories.Interfaces
{
	public interface ICRMRepository
	{
		void SavePlayers(List<Player> players, int sportType);

		List<Sport> GetSports();
	}
}

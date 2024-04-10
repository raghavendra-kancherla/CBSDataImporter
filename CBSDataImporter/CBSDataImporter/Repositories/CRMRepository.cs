using CBSDataImporter.Models;
using CBSDataImporter.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBSDataImporter.Repositories
{
	public class CRMRepository : ICRMRepository
	{
		private readonly IConfiguration configuration;
		private const string GET_AVAILABLE_SPORT = "GetAvailableSports";
		private const string SAVE_PLAYERS = "SavePlayers";

		public CRMRepository(IConfiguration configuration) 
		{
			this.configuration = configuration;
		}
		public void SavePlayers(List<Player> players, int sportType)
		{
			var udt = CreateUdtPlayer(players, sportType);
			using (SqlConnection connection = new SqlConnection(configuration["ConnectionStrings:CRM"]))
			{
				using (SqlCommand command = new SqlCommand(SAVE_PLAYERS, connection))
				{
					connection.Open();
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@Players", udt);
					var reader = command.ExecuteNonQuery();
				}
			}
		}

		private DataTable CreateUdtPlayer(List<Player> players, int sportType)
		{
			DataTable udt = new DataTable();
			udt.Columns.Add("PlayerExternalId");
			udt.Columns.Add("FirstName");
			udt.Columns.Add("LastName");
			udt.Columns.Add("Age");
			udt.Columns.Add("Photo");
			udt.Columns.Add("ProStatus");
			udt.Columns.Add("ProTeam");
			udt.Columns.Add("Position");
			udt.Columns.Add("OffenseDefenseEligibility");
			udt.Columns.Add("EliasId");
			udt.Columns.Add("Bats");
			udt.Columns.Add("Throws");
			udt.Columns.Add("ByeWeek");
			udt.Columns.Add("Icons");
			udt.Columns.Add("SportId");

			foreach (var player in players)
			{
				udt.Rows.Add(player.ExternalId,
							 player.FirstName,
							 player.LastName,
							 player.Age,
							 player.Photo,
							 player.ProStatus,
							 player.ProTeam,
							 player.Position,
							 player.EligibleForOffenseAndDefense,
							 player.EliasId,
							 player.Bats,
							 player.Throws,
							 player.ByeWeek,
							 JsonConvert.SerializeObject(player.Icons),
							 sportType
							 );
			}
			return udt;
		}

		public List<Sport> GetSports()
		{
			using (SqlConnection connection = new SqlConnection(configuration["ConnectionStrings:CRM"]))
			{
				using (SqlCommand command = new SqlCommand(GET_AVAILABLE_SPORT, connection))
				{
					connection.Open();
					command.CommandType = CommandType.StoredProcedure;
					var reader = command.ExecuteReader();

					return ConvertToSports(reader);
				}
			}
		}

		private List<Sport> ConvertToSports(SqlDataReader reader)
		{
			var result = new List<Sport>();
			if (reader.HasRows)
			{
				var idxSportId = reader.GetOrdinal("SportId");
				var idxName = reader.GetOrdinal("SportName");

				while (reader.Read())
				{
					result.Add(new Sport()
					{
						SportId = reader.GetInt32(idxSportId),
						SportName = reader.GetString(idxName)
					});
				}
			}
			return result;
		}
	}
}

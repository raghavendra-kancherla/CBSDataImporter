using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CBSDataImporter.Models
{
	public class Player
	{
		[JsonProperty("lastname")]
		public string LastName { get; set; }
		
		[JsonProperty("photo")]
		public string Photo { get; set; }

		[JsonProperty("position")]
		public string Position { get; set; }

		[JsonProperty("pro_team")]
		public string ProTeam { get; set; }

		[JsonProperty("eligible_for_offense_and_defense")]
		public int EligibleForOffenseAndDefense { get; set; }

		[JsonProperty("elias_id")]
		public string EliasId { get; set; }

		[JsonProperty("pro_status")]
		public string ProStatus { get; set; }

		[JsonProperty("fullname")]
		public string FullName { get; set; }

		[JsonProperty("bats")]
		public string Bats { get; set; }

		[JsonProperty("firstname")]
		public string FirstName { get; set; }

		[JsonProperty("id")]
		public string ExternalId { get; set; }

		[JsonProperty("eligible_positions_display")]
		public string EligiblePositionsDisplay { get; set; }

		[JsonProperty("throws")]
		public string Throws { get; set; }

		[JsonProperty("age")]
		public int? Age { get; set; }
		
		[JsonProperty("jersey")]
		public string Jersey { get; set; }
		
		[JsonProperty("icons")]
		public Icons Icons { get; set; }
		
		[JsonProperty("bye_week")]
		public string ByeWeek { get; set; }
	}
}

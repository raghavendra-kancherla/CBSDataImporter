using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBSDataImporter.Models
{
	public  class CBSResponse
	{
		public int statusCode { get; set; }
		public string uriAlias { get; set; }
		public string statusMessage { get; set; }
		public Body body { get; set; }
		public string uri { get; set; }
	}
	
	public class Body
	{
		public List<Player> players { get; set; }
	}
}

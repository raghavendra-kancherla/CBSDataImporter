using CBSDataImporter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBSDataImporter.Services.Interfaces
{
    public interface ICBSRepository
    {
        List<Player> GetPlayers(string sportName);
    }
}

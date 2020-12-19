using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Types;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess;
using System.Data;

namespace DotaInfoSystem.Models
{
    public interface IConnection
    {        
        
        bool Login(string userName, string passWord);

        DataTable GetAllHeroes();

        DataTable GetHeroesByName(string name);

        DataTable GetHeroesByAttack(string type);

        DataTable GetHeresByMainAttribute(string attr);

        DataTable GetAllSpells();

        DataTable GetSpellsByName(string name);

        DataTable GetSpellsByHero(string hero);

        void CloseConnection();

        

       
    }
}

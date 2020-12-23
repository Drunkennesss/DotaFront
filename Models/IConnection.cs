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

        DataTable GetAllHeroes(int lvl);

        DataTable GetHeroesByName(int lvl, string name);

        DataTable GetHeroesByAttack(int lvl, string type);

        DataTable GetHeresByMainAttribute(int lvl, string attr);

        DataTable GetAllSpells();

        DataTable GetSpellsByName(string name);

        DataTable GetSpellsByHero(string hero);

        void CloseConnection();

        

       
    }
}

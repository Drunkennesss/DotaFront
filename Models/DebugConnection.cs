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
    class DebugConnection : IConnection
    {
        private const string connect = "Data Source=(DESCRIPTION="
                                       + "(ADDRESS=(PROTOCOL=TCP)(PORT=1521))"
                                       + "(CONNECT_DATA=(SERVICE_NAME=xe)));"
                                       + "User Id=alex;Password=qwerty;";
        private OracleConnection oracle;

        public DebugConnection()
        {
            oracle = new OracleConnection(connect);
            oracle.Open();
        }
        
        bool IConnection.Login(string userName, string passWord)
        //burn this shit down
        {
            var temp = new[] { ("admin", "admin"), ("user", "qwerty") };
            foreach (var item in temp)
            {
                var aaa =  item switch
                {
                    var (a, b) when a == userName && b == passWord => true,
                    _ => false,
                };
                if (aaa) { return true; }
            }
            return false;
        }

        DataTable IConnection.GetAllHeroes()
        {
            var query = "select * from alex.heroes";
            return GetQuery(query);
        }        

        DataTable IConnection.GetHeroesByName(string name)
        {
            var query = $"select * from alex.heroes where NAME = '{name}'";
            return GetQuery(query);
        }

        DataTable IConnection.GetHeroesByAttack(string type)
        {
            var query = $"select * from alex.heroes where ATTACK_TYPE = '{type}'";
            return GetQuery(query);
        }

        DataTable IConnection.GetHeresByMainAttribute(string attr)
        {
            var query = $"select * from alex.heroes where MAIN_STAT = '{attr}'";
            return GetQuery(query);
        }


        DataTable IConnection.GetAllSpells()
        {
            var query = "select * from alex.spells";
            return GetQuery(query);
            
        }

        DataTable IConnection.GetSpellsByName(string name)
        {
            var query = @"select h.name Name, s.id Id, st.type_name TypeName," +
                        @" REGEXP_REPLACE(regexp_substr(s.DESCRIPTION, '{''name'': [''""]" + 
                        @"[A-Za-z ''!-/)/(]+[''""],'), '{''name'': |''|,','') AS Name, " +
                        @" REGEXP_REPLACE(s.DESCRIPTION, '{''name'': [''""][A-Za-z ''!-/)/(]+[''""],|}', '') AS Description, " +
                        @"s.manacost Manacost, s.cooldown as Cooldown " +
                        @"from heroes h, hero_spells hs, spells s, spell_type st " +
                        @"where s.id = hs.spell_id and h.id = hs.hero_id and s.spell_type = st.id " +
                        @"and s.description like '{''name'': " + $"''{name}''%'";
            return GetQuery(query);
        }

        DataTable IConnection.GetSpellsByHero(string hero)
        {
            var query = @"select h.name Name, s.id Id, st.type_name TypeName," +
                        @" REGEXP_REPLACE(regexp_substr(s.DESCRIPTION, '{''name'': [''""]" +
                        @"[A-Za-z ''!-/)/(]+[''""],'), '{''name'': |''|,','') AS Name, " +
                        @" REGEXP_REPLACE(s.DESCRIPTION, '{''name'': [''""][A-Za-z ''!-/)/(]+[''""],|}', '') AS Description, " +
                        @"s.manacost Manacost, s.cooldown as Cooldown " +
                        @"from heroes h, hero_spells hs, spells s, spell_type st " +
                        @"where s.id = hs.spell_id and h.id = hs.hero_id and s.spell_type = st.id " +
                        $"and h.name = '{hero}'";
            return GetQuery(query);
        }

        void IConnection.CloseConnection()
        {
            oracle?.Close();
        }

        private DataTable GetQuery(string queryText)
        {
            var dt = new DataTable();
            var com = oracle.CreateCommand();
            com.CommandText = queryText;
            dt.Load(com.ExecuteReader());
            return dt;
        }

        private string GetHeroStatsWithLevel(int level)
        {
            throw new NotImplementedException();
        }

        
    }
}

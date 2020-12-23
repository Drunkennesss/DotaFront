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
        //burn this **** down
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

        DataTable IConnection.GetAllHeroes(int lvl)
        {
            
            var query = GetHeroStatsWithLevel(lvl);
            return GetQuery(query);
        }        

        DataTable IConnection.GetHeroesByName(int lvl, string name)
        {
            var query = GetHeroStatsWithLevel(lvl) + $" where NAME = '{name}'";
            return GetQuery(query);
        }

        DataTable IConnection.GetHeroesByAttack(int lvl, string type)
        {
            var query = GetHeroStatsWithLevel(lvl) + $" where ATTACK_TYPE = '{type}'";
            return GetQuery(query);
        }

        DataTable IConnection.GetHeresByMainAttribute(int lvl, string attr)
        {
            var query = GetHeroStatsWithLevel(lvl) + $" where MAIN_STAT = '{attr}'";
            return GetQuery(query);
        }


        DataTable IConnection.GetAllSpells()
        {
            var query = "select * from alex.spells";
            return GetQuery(query);
            
        }

        DataTable IConnection.GetSpellsByName(string name)
        {
            var query = @"select h.name Hero, s.id Id, st.type_name TypeName," +
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
            var query = @"select h.name Hero, s.id Id, st.type_name TypeName," +
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

        private string GetHeroStatsWithLevel(int lvl)
        {
            return $"SELECT id, name, main_stat, attack_type, attack_range, " +
                    $"my_agil(agil, agil_incr, {lvl}) as agil," +
                    $" my_intel(intel, int_incr, {lvl}) as intel," +
                    $" my_stren(stren, stren_incr, {lvl}) as stren," +
                    $" agil_incr, int_incr, stren_incr," +
                    $" my_damage(id, {lvl}) as damage," +
                    $" my_hp(hp_base, stren, stren_incr, {lvl}) as hp," +
                    $" my_mana(mana_base, intel, int_incr, {lvl}) as mana," +
                    $" my_armor(armour_base, agil, agil_incr, {lvl}) as armor," +
                    $" my_attackspeed(attack_speed_base, agil, agil_incr, {lvl}) as  attack_speed," +
                    $" my_movement(movespeed_base, agil, agil_incr, {lvl}) as movespeed," +
                    $" my_spellresist(spellresist_base, stren, stren_incr, {lvl}) as spellresist," +
                    $" my_hpregen(hp_regen_base, stren, stren_incr, {lvl}) as hp_regen," +
                    $" my_manaregen(mana_regen_base, intel, int_incr, {lvl}) as mana_regen," +
                    $" view_range, projectile_speed FROM heroes";
        }

        
    }
}

/*
$"SELECT id, name, main_stat, attack_type, attack_range, my_agil(agil, agil_incr, {lvl}) as agil," +
$" my_intel(intel, int_incr, {lvl}) as intel, my_stren(stren, stren_incr, {lvl}) as stren, agil_incr, int_incr, stren_incr," +
$" my_damage(id, {lvl}) as damage, my_hp(hp_base, stren, stren_incr, {lvl}) as hp, my_mana(mana_base, intel, int_incr, {lvl}) as mana," +
$" my_armor(armour_base, agil, agil_incr, {lvl}) as armor, my_attackspeed(attack_speed_base, agil, agil_incr, {lvl}) as  attack_speed," +
$" my_movement(movespeed_base, agil, agil_incr, {lvl}) as movespeed," +
$" my_spellresist(spellresist_base, stren, stren_incr, {lvl}) as spellresist," +
$" my_hpregen(hp_regen_base, stren, stren_incr, {lvl}) as hp_regen, my_manaregen(mana_regen_base, intel, int_incr, {lvl}) as mana_regen," +
$" view_range, projectile_speed FROM heroes";
*/
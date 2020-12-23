using System;
using System.Collections.Generic;
using System.Text;
using DotaInfoSystem.Models;

namespace DotaInfoSystem.Models
{
    public abstract class DMlCommand : MyCommand
    {
        protected IDataGridContainer gridContainer;
        protected IConnection connection;
        

        public DMlCommand(IConnection con, IDataGridContainer container)
        {
            gridContainer = container;
            connection = con;
            
        }
    }
    public class GetAllHeroesCommand : DMlCommand
    {
        private ILevelContainer level;
        public GetAllHeroesCommand(IConnection con, IDataGridContainer container, ILevelContainer levelContainer) 
            : base(con, container)
        {
            level = levelContainer;
        }

        public override void Execute(object param)
        {
            var dt = connection.GetAllHeroes((int)level.Level);
            gridContainer.ChangeDataGrid(dt.DefaultView);
        }
    }

    public class GetHeroesByNameCommand : DMlCommand
    {
        private ILevelContainer level;
        public GetHeroesByNameCommand(IConnection con, IDataGridContainer container, ILevelContainer levelContainer)
            : base(con, container)
        {
            level = levelContainer;
        }

        public override void Execute(object param)
        {
            var name = (param as string) ?? "";
            var dt = connection.GetHeroesByName((int)level.Level, name);
            gridContainer.ChangeDataGrid(dt.DefaultView);
        }
    }

    public class GetHeroesByAttackCommand : DMlCommand
    {
        private ILevelContainer level;
        public GetHeroesByAttackCommand(IConnection con, IDataGridContainer container, ILevelContainer levelContainer)
            : base(con, container)
        {
            level = levelContainer;
        }

        public override void Execute(object param)
        {
            var type = (param as string) ?? "";
            var dt = connection.GetHeroesByAttack((int)level.Level, type);
            gridContainer.ChangeDataGrid(dt.DefaultView);
        }
    }

    public class GetHeroesByMainAttributeCommand : DMlCommand
    {
        private ILevelContainer level;
        public GetHeroesByMainAttributeCommand(IConnection con, IDataGridContainer container, ILevelContainer levelContainer)
            : base(con, container)
        {
            level = levelContainer;
        }

        public override void Execute(object param)
        {
            var attr = (param as string) ?? "";
            var dt = connection.GetHeresByMainAttribute((int)level.Level, attr);
            gridContainer.ChangeDataGrid(dt.DefaultView);
        }
    }

    public class GetAllSpellsCommand : DMlCommand
    {
        public GetAllSpellsCommand(IConnection con, IDataGridContainer container)
            : base(con, container)
        { }

        public override void Execute(object param)
        {            
            var dt = connection.GetAllSpells();
            gridContainer.ChangeDataGrid(dt.DefaultView);
        }
    }


    public class GetSpellsByNameCommand : DMlCommand
    {
        public GetSpellsByNameCommand(IConnection con, IDataGridContainer container )
            : base(con, container)
        { }

        public override void Execute(object param)
        {
            var name = (param as string) ?? "";
            var dt = connection.GetSpellsByName(name);
            gridContainer.ChangeDataGrid(dt.DefaultView);
        }
    }


    public class GetSpellsByHeroCommand : DMlCommand
    {
        public GetSpellsByHeroCommand(IConnection con, IDataGridContainer container )
            : base(con, container)
        { }

        public override void Execute(object param)
        {
            var hero = (param as string) ?? "";
            var dt = connection.GetSpellsByHero(hero);
            gridContainer.ChangeDataGrid(dt.DefaultView);
        }
    }
}

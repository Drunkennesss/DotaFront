using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using DotaInfoSystem.Models;
using System.Data;


namespace DotaInfoSystem.ViewModels
{
    class HeroesViewModel : DataGridViewModel, ILevelContainer
    {
        
        private double level;

        public ICommand ToSpells { get; protected set; }
        public double Level
        {
            get => level;
            set
            {
                level = value;
                base.OnPropertyChanged(nameof(Level));
            }
        }

       

        public HeroesViewModel(IConnection con, IPageHolder pageHolder) : base(con, pageHolder)
        {
            ToSpells = new ToSpellsCommand(pages);
        }        

        protected override void ChangeSubmitCommand()
        {
            ICommand neededCommand;
            if (QueryText == string.Empty)
            {
                neededCommand = new GetAllHeroesCommand(connection, this, this);
            }
            else
            {
                var reg = new Regex(@"^.+ ");
                var str = reg.Replace(Selection, "");
                neededCommand = str switch
                {
                    "Name" => new GetHeroesByNameCommand(connection, this, this),
                    "Attr" => new GetHeroesByMainAttributeCommand(connection, this, this),
                    "Type" => new GetHeroesByAttackCommand(connection, this, this),
                    _ => new GetAllHeroesCommand(connection, this, this)
                };
            }
            Submit = neededCommand;
        }

        
    }
}

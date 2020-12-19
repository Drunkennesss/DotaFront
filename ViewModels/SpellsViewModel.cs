using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using DotaInfoSystem.Models;
using System.Data;


namespace DotaInfoSystem.ViewModels
{
    class SpellsViewModel : DataGridViewModel
    {       

        public ICommand ToHeroes { get; private set; }
        
        public SpellsViewModel(IConnection con, IPageHolder pageHolder) : base(con, pageHolder)
        {
            
            ToHeroes = new ToHeroesCommand(pages);
            
        }

        protected override void ChangeSubmitCommand()
        {
            ICommand neededCommand;
            if (QueryText == string.Empty)
            {
                neededCommand = new GetAllSpellsCommand(connection, this);
            }
            else
            {
                var reg = new Regex(@"^.+ ");
                var str = reg.Replace(Selection, "");
                neededCommand = str switch
                {
                    "Name" => new GetSpellsByNameCommand(connection, this),
                    "Hero" => new GetSpellsByHeroCommand(connection, this),                    
                    _ => new GetAllSpellsCommand(connection, this)
                };
            }
            Submit = neededCommand;
        }
    }
}

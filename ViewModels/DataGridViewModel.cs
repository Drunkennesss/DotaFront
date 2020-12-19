using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Input;
using DotaInfoSystem.Models;

namespace DotaInfoSystem.ViewModels
{
    public abstract class DataGridViewModel : ViewModelBase, IDataGridContainer
    {
        protected IPageHolder pages;
        protected string selection;
        protected string queryText;
        protected DataView data;
        protected ICommand submit;

        public ICommand ToLog { get; protected set; }        

        public ICommand Submit
        {
            get => submit;
            protected set
            {
                submit = value;
                base.OnPropertyChanged(nameof(Submit));
            }
        }

        public string Selection
        {
            get => selection;
            set
            {
                selection = value;
                base.OnPropertyChanged(nameof(Selection));
                ChangeSubmitCommand();
            }
        }

        public string QueryText
        {
            get => queryText;
            set
            {
                queryText = value;
                base.OnPropertyChanged(nameof(QueryText));
                ChangeSubmitCommand();
            }
        }


        public DataView DataView
        {
            get => data;
            set
            {
                data = value;
                base.OnPropertyChanged(nameof(DataView));
            }
        }
        public DataGridViewModel(IConnection con, IPageHolder pageHolder) : base(con)
        {
            pages = pageHolder;
            ToLog = new ToLogCommand(pages);            
            Selection = "";
        }

        protected abstract void ChangeSubmitCommand();

        public void ChangeDataGrid(DataView dataView)
        {
            DataView = dataView;
        }

        void IDataGridContainer.ChangeDataGrid(DataView dataView)
        {
            ChangeDataGrid(dataView);
        }
    }
}

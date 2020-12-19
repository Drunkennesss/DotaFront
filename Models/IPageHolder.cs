using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;


namespace DotaInfoSystem.Models
{
    public interface IPageHolder
    {
        Page CurrentPage { get; }

        void ChangeCurrentPage(PageNumber val);

    }

    public enum PageNumber : int
    {
        Login = 0,
        Heroes,
        Spells
    }

}

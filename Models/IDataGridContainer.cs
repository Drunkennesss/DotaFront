using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DotaInfoSystem.Models
{
    public interface IDataGridContainer
    {
        void ChangeDataGrid(DataView dataView);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgGrid
{
    /// <summary>
    /// Infinite Scrolling Datasource, based on:
    ///   https://www.ag-grid.com/javascript-grid-infinite-scrolling/#datasource-interface
    /// </summary>
    public interface IGridDatasource
    {
        // Callback the grid calls that you implement to fetch rows from the server. See below for params.
        //getRows(params: IGetRowsParams) : void;
        Task GetRows(IGetRowsParams getParams);

        // optional destroy method, if your datasource has state it needs to clean up
        //destroy? (): void;
        Task Destroy();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgGrid
{
    /// <summary>
    /// Params for <see cref="IDatasource.GetRows()">Infinite Scrolling Datasource</see>, based on:
    ///   https://www.ag-grid.com/javascript-grid-infinite-scrolling/#datasource-interface
    /// </summary>
    public interface IGetRowsParams
    {
        // The first row index to get.
        //startRow: number;
        int StartRow { get; }

        // The first row index to NOT get.
        //endRow: number;
        int EndRow { get; }

        // If doing Server-side sorting, contains the sort model
        //sortModel: any,
        SortModel[] SortModel { get; }

        // If doing Server-side filtering, contains the filter model
        //filterModel: any;
        object FilterModel { get; }

        // The grid context object
        //context: any;
        object Context { get; }

        string CallbackId { get; }

        //// Callback to call when the request is successful.
        ////successCallback(rowsThisBlock: any[], lastRow?: number) : void;
        Task SuccessCallback(object[] rowsThisBlock, int? lastRow = null);

        //// Callback to call when the request fails.
        ////failCallback() : void;
        Task FailCallback();
    }
}

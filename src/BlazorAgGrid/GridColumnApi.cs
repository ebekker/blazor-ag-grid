using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAgGrid
{
    /// <summary>
    /// Strongly-typed access to:
    ///   https://www.ag-grid.com/javascript-grid-column-api/
    /// </summary>
    public class GridColumnApi
    {
        internal string CallColumnApi = "blazor_ag_grid.gridOptions_callColumnApi";

        private IJSRuntime _js;
        private string _id;

        internal GridColumnApi(IJSRuntime js, string id)
        {
            _js = js;
            _id = id;
        }

        public Task SizeColumnsToFit(int width)
        {
            return CallApi("sizeColumnsToFit", width);
        }

        public Task AutoSizeColumn(string colKey)
        {
            return CallApi("autoSizeColumn", colKey);
        }

        public  Task AutoSizeColumns(string[] colKeys)
        {
            // Cast to make sure arg is not unwound
            return CallApi("autoSizeColumns", (object)colKeys);
        }

        private Task CallApi(string name, params object[] args)
        {
            return _js.InvokeVoidAsync(CallColumnApi, _id, name, args).AsTask();
        }
    }
}

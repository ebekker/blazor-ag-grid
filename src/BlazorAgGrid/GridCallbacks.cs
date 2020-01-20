using System;
using System.Text.Json;

namespace BlazorAgGrid
{
    /// <summary>
    /// Strongly-typed counterpart of:
    ///    https://www.ag-grid.com/javascript-grid-callbacks/
    /// </summary>
    public partial class GridCallbacks
    {
        public Func<JsonElement, string> GetRowNodeId { set => Set(value); }
    }
}

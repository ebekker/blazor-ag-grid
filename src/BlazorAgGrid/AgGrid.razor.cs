using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorAgGrid
{
    public partial class AgGrid : IDisposable
    {
        private static readonly JsonSerializerOptions AgGridJsonSerOptions = new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

#pragma warning disable CS0649
        // These are referenced by nested components as cascading parameters
        private List<ColumnDefinition> _columnDefinitions = new List<ColumnDefinition>();
        //private RowData _rowData = new RowData();
        private List<object> _rowData = new List<object>();
        // This will be populated by the component @ref
        private ElementReference _gridDiv;
#pragma warning restore CS0649

        private bool _isRendered = false;

        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string HeightStyle { get; set; } = "500px";
        [Parameter] public string WidthStyle { get; set; } = "500px";
        [Parameter] public GridOptions Options { get; set; }
        [Parameter] public GridEvents Events { get; set; }
        [Parameter] public GridCallbacks Callbacks { get; set; }

        [Inject] private IJSRuntime JS { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!_isRendered)
            {
                _isRendered = true;
                await RenderGrid();
            }
        }

        private async Task RenderGrid()
        {
            if (Options == null)
                Options = new GridOptions();

            if (_columnDefinitions?.Count > 0)
                Options.InternalColumnDefinitions = _columnDefinitions;

            if (_rowData?.Count > 0)
                Options.InternalRowData = _rowData;

            // Execute any needed adjustments or tranformations
            // before handing off the options for JS interop
            Options.FirePrepareForInterop(JS);

            // ag-Grid treats null values differently from undefined values in its
            // inputs which means we can't just set (or leave) values as null to
            // _not provide a value_ for them.  AND, unfortunately, until
            // [https://github.com/dotnet/aspnetcore/issues/12685] or
            // [https://github.com/dotnet/corefx/issues/39735] is addressed
            // we can't control the JSRuntime serializaton to "ignore null values"
            // so we have to wrap the options in a class that declares a custom
            // converter which will serialize its constituent components using
            // customized serialization options
            var interopOptions = new InteropGridOptions { Options = Options };

            //Console.WriteLine("Raw       GridOpts: "
            //    + System.Text.Json.JsonSerializer.Serialize(Options));
            //Console.WriteLine("Sanitized GridOpts: "
            //    + System.Text.Json.JsonSerializer.Serialize(interopOptions));

            await JS.InvokeVoidAsync("blazor_ag_grid.createGrid",
                _gridDiv, interopOptions, Events, Callbacks);
        }

        public async void Dispose()
        {
            try
            {
                await JS.InvokeVoidAsync("blazor_ag_grid.destroyGrid", _gridDiv);
            }
            catch (Exception ex)
            {
                Console.WriteLine("WARNING: failure during clean up: " + ex);
            }
        }
    }
}

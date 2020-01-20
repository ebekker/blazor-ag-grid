using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlazorAgGrid
{
    // We define the Datasource property to allow clients to
    // register a custom data source, and also add the needed
    // plumbing to make access to the DS work through JS interop
    public partial class GridOptions
    {
        private IGridDatasource _Dataource;

        [JsonIgnore]
        public IGridDatasource Datasource
        {
            get => _Dataource;
            set
            {
                if (_Dataource != value)
                {
                    if (value != null)
                        PrepareForInterop += PrepareDatasource;
                    else
                        PrepareForInterop -= PrepareDatasource;
                }
                _Dataource = value;
            }
        }

        [JsonPropertyName("datasource")]
        public DotNetObjectReference<InteropDatasourceProxy> InteropDatasource { get; set; }

        private void PrepareDatasource(object source, PrepareForInteropEventArgs ev)
        {
            Console.WriteLine("Preparing DS");
            InteropDatasource = DotNetObjectReference.Create(
                new InteropDatasourceProxy(ev.JS, Datasource));
        }

        // Wrapper DS around user-provided DS with proper JS-interop handling
        public class InteropDatasourceProxy
        {
            private IJSRuntime _js;
            private IGridDatasource _inner;

            public InteropDatasourceProxy(IJSRuntime js, IGridDatasource inner)
            {
                _js = js;
                _inner = inner;
            }

            [JSInvokable]
            public Task GetRows(InteropGetRowsParams getParams)
            {
                var proxy = new GetRowsParamsProxy(_js, getParams);
                return _inner.GetRows(proxy);
            }

            [JSInvokable]
            public Task Destroy() => _inner.Destroy();
        }

        // Substitute for ag-Grid-provided param for DS.GetRows(...) with JS-interop handling
        public class InteropGetRowsParams : IGetRowsParams
        {
            public int StartRow { get; set; }

            public int EndRow { get; set; }

            public SortModel[] SortModel { get; set; }

            public object FilterModel { get; set; }

            public object Context { get; set; }

            public string CallbackId { get; set; }

            public Task FailCallback() =>
                throw new NotImplementedException();

            public Task SuccessCallback(object[] rowsThisBlock, int? lastRow = null) =>
                throw new NotImplementedException();
        }

        public class GetRowsParamsProxy : IGetRowsParams
        {
            private IJSRuntime _js;
            private IGetRowsParams _inner;

            public GetRowsParamsProxy(IJSRuntime js, IGetRowsParams inner)
            {
                _js = js;
                _inner = inner;
            }

            // The first row index to get.
            //startRow: number;
            public int StartRow => _inner.StartRow;

            // The first row index to NOT get.
            //endRow: number;
            public int EndRow => _inner.EndRow;

            // If doing Server-side sorting, contains the sort model
            //sortModel: any,
            public SortModel[] SortModel => _inner.SortModel;

            // If doing Server-side filtering, contains the filter model
            //filterModel: any;
            public object FilterModel => _inner.FilterModel;

            // The grid context object
            //context: any;
            public object Context => _inner.Context;

            public string CallbackId => _inner.CallbackId;

            //// Callback to call when the request is successful.
            ////successCallback(rowsThisBlock: any[], lastRow?: number) : void;
            public Task SuccessCallback(object[] rowsThisBlock, int? lastRow = null)
            {
                Console.WriteLine("GetRowsParamsProxy.SuccessCallback: {0}", CallbackId);
                return _js.InvokeVoidAsync("blazor_ag_grid.datasource_successCallback",
                    CallbackId, rowsThisBlock, lastRow).AsTask();
            }

            //// Callback to call when the request fails.
            ////failCallback() : void;
            public Task FailCallback()
            {
                Console.WriteLine("GetRowsParamsProxy.FailCallback: {0}", CallbackId);
                return _js.InvokeVoidAsync("blazor_ag_grid.datasource_failCallback",
                    CallbackId).AsTask();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BlazorAgGrid
{
    public partial class GridOptions
    {
        private List<object> _InternalRowData;

        [JsonPropertyName("rowData")]
        public IEnumerable<object> RowData { get; set; }

        [JsonIgnore]
        internal List<object> InternalRowData
        {
            get => _InternalRowData;
            set
            {
                if (_InternalRowData != value)
                {
                    if (value != null)
                        PrepareForInterop += PrepareRows;
                    else
                        PrepareForInterop -= PrepareRows;
                }
                _InternalRowData = value;
            }
        }

        private void PrepareRows(object source, PrepareForInteropEventArgs args)
        {
            Console.WriteLine("Preparing Rows");
            // Merge declared and programatic if neeeded
            if (_InternalRowData?.Count > 0)
            {
                if (RowData != null)
                    _InternalRowData.AddRange(RowData);
                RowData = _InternalRowData;
            }
        }
    }
}

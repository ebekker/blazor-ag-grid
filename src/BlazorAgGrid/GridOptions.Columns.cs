using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BlazorAgGrid
{
    public partial class GridOptions
    {
        private List<ColumnDefinition> _InternalColumnDefinitions;

        [JsonPropertyName("columnDefs")]
        public IEnumerable<ColumnDefinition> ColumnDefinitions { get; set; }

        [JsonIgnore]
        internal List<ColumnDefinition> InternalColumnDefinitions
        {
            get => _InternalColumnDefinitions;
            set
            {
                if (_InternalColumnDefinitions != value)
                {
                    if (value != null)
                        PrepareForInterop += PrepareColumns;
                    else
                        PrepareForInterop -= PrepareColumns;
                }
                _InternalColumnDefinitions = value;
            }
        }

        private void PrepareColumns(object source, PrepareForInteropEventArgs args)
        {
            Console.WriteLine("Preparing Columns");
            // Merge declared and programatic if neeeded
            if (_InternalColumnDefinitions?.Count > 0)
            {
                if (ColumnDefinitions != null)
                    _InternalColumnDefinitions.AddRange(ColumnDefinitions);
                ColumnDefinitions = _InternalColumnDefinitions;
            }
        }
    }
}

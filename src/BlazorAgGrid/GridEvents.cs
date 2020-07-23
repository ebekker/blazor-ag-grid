using System;

namespace BlazorAgGrid
{
    /// <summary>
    /// Strongly-typed counterpart of:
    ///    https://www.ag-grid.com/javascript-grid-events/
    /// </summary>
    public partial class GridEvents
    {
        public Action<RowNode[]> SelectionChanged { set => Set(value); }

        public Action<CellValueChangedDetail> CellValueChanged { set => Set(value); }
    }

    public class CellValueChangedDetail
    {
        public string RowNodeId { get; set; }

        public string Field { get; set; }

        public string ColumnId { get; set; }

        public int RowIndex { get; set; }

        public object OldValue { get; set; }

        public object NewValue { get; set; }
    }
}

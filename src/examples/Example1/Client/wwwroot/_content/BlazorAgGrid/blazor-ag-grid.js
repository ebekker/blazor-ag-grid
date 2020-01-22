// This file is to show how a library package may provide JavaScript interop features
// wrapped in a .NET API

window.exampleJsFunctions = {
  showPrompt: function (message) {
    return prompt(message, 'Type anything here');
  }
};


window.blazor_ag_grid = {
    callbackMap: {}
    , renderCount: 0
    , createGrid: function (gridDiv, gridOptions, gridEvents, gridCallbacks, gridExtras) {
        console.log("JS-creating grid...");
        if (gridOptions.datasource) {
            console.log("DS Ref: " + JSON.stringify(gridOptions.datasource));
            blazor_ag_grid.createGrid_wrapDatasource(gridOptions, gridOptions.datasource);
        }

        if (gridCallbacks) {
            blazor_ag_grid.createGrid_wrapCallbacks(gridOptions, gridCallbacks);
        }

        if (gridEvents) {
            blazor_ag_grid.createGrid_wrapEvents(gridOptions, gridEvents);
        }

        // create the grid passing in the div to use together with the columns & data we want to use
        new agGrid.Grid(gridDiv, gridOptions);
    }
    , destroyGrid: function (gridDiv) {
        console.log("JS-destroying grid...");

        // NOTHING TO DO FOR NOW
        // TODO: What should we do to properly clean up resources???
    }
    , createGrid_wrapDatasource: function (gridOptions, dsRef) {
        // Need to "wrap" the data source
        console.log("Wrapping datasource");
        var nativeDS = {
            destroy: function () {
                console.log("destroying  datasource...");
                dsRef.invokeMethodAsync('Destroy');
            },
            getRows: function (getRowsParams) {
                console.log("getting rows for: " + JSON.stringify(getRowsParams));
                var callbackId = blazor_ag_grid.util_genId();
                blazor_ag_grid.callbackMap[callbackId] = getRowsParams;
                getRowsParams.callbackId = callbackId;
                console.log("mapped callback ID for ds: " + callbackId + "; " + JSON.stringify(dsRef));
                dsRef.invokeMethodAsync('GetRows', getRowsParams);
            }
        };
        gridOptions.datasource = nativeDS;
    }
    , createGrid_wrapCallbacks: function (gridOptions, gridCallbacks) {
        console.log("Got GridCallbakcs: " + JSON.stringify(gridCallbacks));
        if (gridCallbacks.handlers.GetRowNodeId) {
            console.log("Wrapping GetRowNodeId handler");
            gridOptions.getRowNodeId = function (data) {
                //console.log("gridOptions.getRowNodeId <<< " + JSON.stringify(data));
                var id = gridCallbacks.handlers.GetRowNodeId.jsRef.invokeMethod("Invoke", data);
                //console.log("gridOptions.getRowNodeId >>> [" + id + "]");
                return id;
            }
        }
    }
    , createGrid_wrapEvents: function (gridOptions, gridEvents) {
        console.log("Got GridEvents: " + JSON.stringify(gridEvents));
        if (gridEvents.handlers.SelectionChanged) {
            console.log("Wrapping SelectionChanged handler");
            gridOptions.onSelectionChanged = function () {
                blazor_ag_grid.gridOptions_onSelectionChanged(gridOptions, gridEvents);
            }
        }
    }
    , gridOptions_onSelectionChanged: function (gridOptions, gridEvents) {
        console.log("js-onSelectionChanged");
        var selectedNodes = gridOptions.api.getSelectedNodes();
        var json = blazor_ag_grid.util_stringify(selectedNodes);
        var mapped = selectedNodes.map(this.util_mapRowNode);
        console.log("js-selectedNodes: " + JSON.stringify(mapped));
        gridEvents.handlers.SelectionChanged.jsRef.invokeMethodAsync('Invoke', mapped);
    }
    , datasource_successCallback: function (callbackId, rowsThisBlock, lastRow) {
        var getRowsParams = blazor_ag_grid.callbackMap[callbackId];
        console.log("datasource_successCallback: " + callbackId);
        getRowsParams.successCallback(rowsThisBlock, lastRow);
        console.log("unmapping callback: " + callbackId);
        delete blazor_ag_grid.callbackMap[callbackId];
    }
    , datasource_failCallback: function (callbackId, rowsThisBlock, lastRow) {
        var getRowsParams = blazor_ag_grid.callbackMap[callbackId];
        console.log("datasource_failCallback: " + callbackId);
        getRowsParams.failCallback();
        console.log("unmapping callback: " + callbackId);
        delete blazor_ag_grid.callbackMap[callbackId];
    }
    // Cycle-safe version of JSON.stringify, useful for debugging
    , util_stringify: function (obj) {
        // Note: cache should not be re-used by repeated calls to JSON.stringify.
        var cache = [];
        var json = JSON.stringify(obj, function (key, value) {
            if (typeof value === 'object' && value !== null) {
                if (cache.indexOf(value) !== -1) {
                    // Duplicate reference found, discard key
                    return;
                }
                // Store value in our collection
                cache.push(value);
            }
            return value;
        });
        cache = null; // Enable garbage collection
    }
    // Maps raw Row Node objects to something safer for passing back to .NET
    , util_mapRowNode: function (n) {
        let newN = {};

        // Standard properties as defined here:
        //    https://www.ag-grid.com/javascript-grid-row-node/#row-object-aka-row-node
        newN["id"] = n.id;
        newN["data"] = n.data;
        if (n.parent) {
            newN["parent"] = mapNode(n.parent);
        }
        newN["level"] = n.level;
        newN["uiLevel"] = n.uiLevel;
        newN["group"] = n.group;
        newN["rowPinned"] = n.rowPinned;
        newN["canFlower"] = n.canFlower;
        newN["childFlower"] = n.childFlower;
        newN["childIndex"] = n.childIndex;
        newN["firstChild"] = n.firstChild;
        newN["lastChild"] = n.lastChild;
        newN["stub"] = n.stub;
        newN["rowHeight"] = n.rowHeight;
        newN["rowTop"] = n.rowTop;
        newN["quickFilterAggregateText"] = n.quickFilterAggregateText;

        // Additional properties found through observation
        newN["selectable"] = n.selectable;
        newN["alreadyRendered"] = n.alreadyRendered;
        newN["selected"] = n.selected;
        newN["master"] = n.master;
        newN["expanded"] = n.expanded;
        newN["allChildrenCount"] = n.allChildrenCount;
        newN["rowHeightEstimated"] = n.rowHeightEstimated;
        newN["rowIndex"] = n.rowIndex;

        return newN;
    }
    , util_genId: function () {
        return Math.random().toString(36).substr(2);
    }
};

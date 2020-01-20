# blazor-ag-grid
Blazor-wrapped component over [ag-Grid](https://github.com/ag-grid/ag-grid).

----

This project implements a Blazor component that wraps the [ag-Grid](https://www.ag-grid.com/)
JavaScript data grid.

## Overview

ag-Grid is a very feature-rich and capable JS control, however this Blazor-compatible,
wrapped component only attempts to expose a relatively small subset of these features
that were useful and needed for my own purposes.

Over time, this subset may grow as my own requirements change, and of course, from any
community contributions.

Here is a list of features that are currently supported:

* Inline static & programmatic column definitions
* Inline static and inline dynamic & programatic row data
* Client-side and Infinite row model types
  * Custom Datasource
    * Custom Row ID resolution
* Single- and Multi-row selection
  * Selection notification
* Paging
* Sorting
* Various tweaks and customizations to the features above such as:
  * page size
  * cell-selection suppression
  * datasource page caching
  * row deselection

## Usage

In general, this Blazor component tries to follow the configuration
approach of the native ag-Grid control, which is primarily to use the
[Grid Options](https://www.ag-grid.com/javascript-grid-reference-overview/#grid-options) interface.
However, because of the need to perform
[JS Interop](https://docs.microsoft.com/en-us/aspnet/core/blazor/javascript-interop?view=aspnetcore-3.1) between the .NET and the JS
runtimes, there are some challenges to simply using the Grid Options
interface natively.

To that end, the Options interface is actually broken out into three
core classes on the .NET side that are used to configure the ag-Grid
and its behavior:

* GridOptions
* GridCallbacks
* GridEvents

### Grid Options

The `GridOptions` class defines all the currently supported simple
flags and feature configurations (mostly boolean and numerical) values.

It also provides you with the ability to define Column Definitions
and simple Row Data programmatically.  Alternatively you can define
each of these using inline child components.

Grid Options is also where you would provide a custom IDatasource
implementation that would allow you to provide data from any source,
such as from in-memory collection or from a back-end server.

### Grid Callbacks

The `GridCallbacks` class defines all supported [Callbacks](https://www.ag-grid.com/javascript-grid-callbacks/)
of ag-Grid.

This is currently limited to resolving a Row Node ID when you provide
a custom Datasource.

### Grid Events

The `GridEvents` class defines all supported [Events](https://www.ag-grid.com/javascript-grid-events/)
of ag-Grid.

This is currently limited to being notified of a change in row
selection.

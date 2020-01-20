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

To use this component you'll need to add a few basic resource
references to ag-Grid assets such as CSS and JS files as described
in the [docs](https://www.ag-grid.com/javascript-grid/#add-ag-grid-to-your-project).

### ag-Grid Assets

Add this to the `<head>` section of your `index.html` file.

```javascript
    <script src="https://unpkg.com/ag-grid-community/dist/ag-grid-community.min.noStyle.js"></script>
    <link rel="stylesheet" href="https://unpkg.com/ag-grid-community/dist/styles/ag-grid.css">
    <link rel="stylesheet" href="https://unpkg.com/ag-grid-community/dist/styles/ag-theme-balham.css">
```

The last line _assumes_ you're using the [`Balham` theme](https://www.ag-grid.com/javascript-grid-themes-provided/#balham-themes).
If you choose to use another [theme](https://www.ag-grid.com/javascript-grid-styling/),
adjust appropriately.

### Blazor ag-Grid Assets and Component

Next you want to add a reference to the JS interop support file
specific to this Blazor component by also adding this to your
`<head>` section:

```javascript
    <script src="_content/BlazorAgGrid/blazor-ag-grid.js"></script>
```

Finally, in your Blazor pages, drop in the `<AgGrid>` component
wherever you want to use it and configure with these properties
and child compoenents:

* Properties:
  * `WidthStyle` & `HeightStyle`
  * `Options`, `Callbacks` and `Events` (see below)
* Child Components:
  * `<ColumnDefinition>`
  * `<RowData>`

See the [Example1](src/Example1) and [Example2](src/Example2)
projects for usage examples.

## Configuration

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

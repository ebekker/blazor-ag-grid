# blazor-ag-grid
Blazor-wrapped component over [ag-Grid](https://github.com/ag-grid/ag-grid).

Blazor WASM demo can be found [here](https://blog.bkkr.us/blazor-ag-grid).

----

[![GitHub WorkFlow - CI](https://github.com/ebekker/blazor-ag-grid/workflows/CI/badge.svg)](https://github.com/ebekker/blazor-ag-grid/actions?CI)
[![GitHub Release Notes (latest by date)](https://img.shields.io/github/v/release/ebekker/blazor-ag-grid?include_prereleases)](https://github.com/ebekker/blazor-ag-grid/releases/latest)
[![GitHub Preview](https://img.shields.io/badge/github%20nuget-latest%20preview-orange)](https://github.com/ebekker/blazor-ag-grid/packages/112336)

<!-- FUTURE when we publish to nuget.org
[![Nuget  Release](https://img.shields.io/nuget/v/BlazorAgGrid)](https://www.nuget.org/packages/Zyborg.AWS.Lambda.Kerberos/)
-->

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
* Grid and Column APIs
* local JS script configuration
* Works with both Blazor WASM and Blazor Server hosting models
  (with some caveats)

## Examples

There are several [examples](src/examples) that each demonstrate
some typical usage under different scenarios:

* [Example1](src/examples/Example1) - demonstrates
  Blazor WASM + ASP.NET Core hosted example
* [Example2](src/examples/Example2) - similar to Example1 but this
  is purely WASM client with no back-end.  This Example is the one
  that is used for the [public demo](https://blog.bkkr.us/blazor-ag-grid/).
* [Example3](src/examples/Example3) - demonstrates Blazor Server
  hosting model.  This is an adaptation of Example1 and
  demonstrates some of the caveats that need to be considered
  and addressed with the Server hosting model (more details below).

## Usage

Firstly, this component is still in very early stage and only being
published as a nuget in the GitHub Package Repository (GPR).

Add a nuget.config file to your project (e.g. `dotnet add nugetconfig`)
and edit it to include the package source for this repo:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <!--To inherit the global NuGet package sources remove the <clear/> line below -->
    <clear />
    <add key="nuget" value="https://api.nuget.org/v3/index.json" />
    <add key="github" value="https://nuget.pkg.github.com/ebekker/index.json" />
  </packageSources>
</configuration>
```

Note, you must also ***authenticate*** to GPR, more details can be found
[here](https://help.github.com/en/github/managing-packages-with-github-packages/configuring-dotnet-cli-for-use-with-github-packages).

Then add the nuget to your project:

```pwsh
PS> dotnet add package BlazorAgGrid
```

### ag-Grid Assets

To use this component you'll need to add a few basic resource
references to ag-Grid assets such as CSS and JS files as described
in the [docs](https://www.ag-grid.com/javascript-grid/#add-ag-grid-to-your-project).

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

See the [example projects](src/examples) as described above for
usage examples.

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

> NOTE: Grid Callbacks should ***NOT*** be used with the **Blazor Server** hosting model, see more details below.

### Grid Events

The `GridEvents` class defines all supported [Events](https://www.ag-grid.com/javascript-grid-events/)
of ag-Grid.

This is currently limited to being notified of a change in row
selection.

### Grid Configuration Script

This component provides an optional `ConfigureScript` parameter
to allow you to specify your own native JS configuration logic.

This feature was added for scenarios where you *must* use a
JavaScript-local routine to perform some pre-create configuration
of the Grid Options instance.  If specified, this should be the
name of a function scoped against the browser's `window` object
that will be invoked with an argument of the Grid Options instance.

This function will be invoked after all the other configuration
elements above have been processed and just before the ag-Grid
instance is created with these options.  You can use it to perform
any final validation or mutation of the options instance, such as
registering JS-local callbacks or event handlers.

See more details below about the Blazor Server hosting model where
this comes in handy.

## Grid & Column API

The Grid component exposes `Api` and `ColumnApi` properties that
provide access to the corresponding ag-Grid APIs.

Currently each of these interfaces only contain a very small
number of sample API methods to invoke.  Right now these include
samples for column resizing and purging/refreshing the cache used
for the `Infinite` row model type.

## Blazor Hosting Modes

This component has been developed and tested to work with both
WebAssembly and Server [hosting models](https://docs.microsoft.com/en-us/aspnet/core/blazor/hosting-models?view=aspnetcore-3.1)
currently supported by Blazor.

For Blazor WASM hosting model, all features should work as expected.

However, for the Blazor Server hosting model, there are some caveats to
consider.  When using the Blazor Server hosting model, interop
invocations across JS/.NET runtimes ***must*** be performed
asynchronously in both directions.  This is a necessity because in the
Server hosting model, all interop is taking place over a SignalR
connection and network activity inevitably incurs latency and possible
disconnects that are not a consideration in the WASM hosting model.

Therefore, anywhere this component _must_ use a synchronous invocation
should be avoided.  Specifically, the `GridCallbacks` interface should
not be used for the Server hosting model as this is a mechanism that
the ag-Grid instance is calling into _your_ code to resolve some data
and ag-Grid does not support asynchronous invocations for any of the
callback functions.

There is an _out_ for this scenario.  This Blazor compenent does
support an optional `ConfigureScript` parameter as described above.
As long as your logic can be specified and implementd fully in
browser-side JS code, you can use this parameter to inject your own
logic such as adding callbacks and event handlers on the Grid Options
instance just before the Grid instance is created.

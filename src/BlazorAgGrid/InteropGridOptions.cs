using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorAgGrid
{
    // ag-Grid treats null values differently from undefined values in its
    // inputs which means we can't just set (or leave) values as null to
    // _not provide a value_ for them.  AND, unfortunately, until
    // [https://github.com/dotnet/aspnetcore/issues/12685] or
    // [https://github.com/dotnet/corefx/issues/39735] is addressed
    // we can't control the JSRuntime serializaton to "ignore null values"
    // so we have to use this intermediate converter to serialize its
    // constituent components with our preferred JSON serialization options
    [JsonConverter(typeof(InteropGridOptionsConverter))]
    internal class InteropGridOptions
    {
        public string CallbackId { get; set; }

        public GridOptions Options { get; set; }

        public GridCallbacks Callbacks { get; set; }

        public GridEvents Events { get; set; }
    }

    internal class InteropGridOptionsConverter : JsonConverter<InteropGridOptions>
    {
        public override InteropGridOptions Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new InteropGridOptions
            {
                Options = JsonSerializer.Deserialize<GridOptions>(ref reader, options),
            };
        }

        public override void Write(Utf8JsonWriter writer, InteropGridOptions value, JsonSerializerOptions options)
        {
            // Clone the existing options with some changes for our purposes
            var newOpts = new JsonSerializerOptions
            {
                IgnoreNullValues = true,

                // The following are all copied as-is
                AllowTrailingCommas = options.AllowTrailingCommas,
                DefaultBufferSize = options.DefaultBufferSize,
                DictionaryKeyPolicy = options.DictionaryKeyPolicy,
                Encoder = options.Encoder,
                IgnoreReadOnlyProperties = options.IgnoreReadOnlyProperties,
                MaxDepth = options.MaxDepth,
                PropertyNameCaseInsensitive = options.PropertyNameCaseInsensitive,
                PropertyNamingPolicy = options.PropertyNamingPolicy,
                ReadCommentHandling = options.ReadCommentHandling,
                WriteIndented = options.WriteIndented,
            };
            // Copy over all the converters
            foreach (var c in options.Converters)
                newOpts.Converters.Add(c);

            // Serialize as an object
            writer.WriteStartObject();
            // ID
            writer.WriteString(nameof(value.CallbackId), value.CallbackId);
            // Inner options
            writer.WritePropertyName(nameof(value.Options));
            JsonSerializer.Serialize(writer, value.Options, newOpts);
            // Callbacks
            writer.WritePropertyName(nameof(value.Callbacks));
            JsonSerializer.Serialize(writer, value.Callbacks, newOpts);
            // Events
            writer.WritePropertyName(nameof(value.Events));
            JsonSerializer.Serialize(writer, value.Events, newOpts);
            // End-of-Object
            writer.WriteEndObject();
        }
    }
}

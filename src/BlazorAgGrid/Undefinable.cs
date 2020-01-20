using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorAgGrid
{
    // These were feable attempts to work around these issues:
    //     [https://github.com/dotnet/aspnetcore/issues/12685]
    //     [https://github.com/dotnet/corefx/issues/39735]
    // because ag-Grid treats null differently than undefined values
    // for some of the properties of its inputs (like GridOptions).

    //public struct Undefinable
    //{
    //    public static readonly Undefinable Undefined = new Undefinable();
    //}

    //[JsonConverter(typeof(UndefinableConverterFactory))]
    //public struct Undefinable<T> where T : struct
    //{
    //    public static readonly Undefinable<T> Undefined = new Undefinable<T>();

    //    public static Type NullableValueType => typeof(T?);
    //    public bool IsDefined { get; private set; }
    //    public T? Value { get; private set; }

    //    public static implicit operator Undefinable<T>(Undefinable value)
    //    {
    //        return new Undefinable<T>
    //        {
    //            IsDefined = false,
    //            Value = null,
    //        };
    //    }

    //    public static implicit operator Undefinable<T>(T value)
    //    {
    //        return new Undefinable<T>
    //        {
    //            IsDefined = true,
    //            Value = value,
    //        };
    //    }

    //    public static implicit operator Undefinable<T>(T? value)
    //    {
    //        return new Undefinable<T>
    //        {
    //            IsDefined = true,
    //            Value = value,
    //        };
    //    }


    //    public static explicit operator T(Undefinable<T> value)
    //    {
    //        return value.Value.GetValueOrDefault();
    //    }

    //    public static explicit operator T?(Undefinable<T> value)
    //    {
    //        return value.Value;
    //    }
    //}

    //internal class UndefinableConverter<T> : JsonConverter<Undefinable<T>> where T : struct
    //{
    //    public override Undefinable<T> Read(ref Utf8JsonReader reader, Type typeToConvert,
    //        JsonSerializerOptions options)
    //    {
    //        Console.WriteLine("*** READING {0}", typeToConvert.FullName);
    //        if (options?.GetConverter(Undefinable<T>.NullableValueType)
    //            is JsonConverter<T> valueConverter)
    //        {
    //            return valueConverter.Read(ref reader, typeToConvert, options);
    //        }
    //        else
    //        {
    //            return (Undefinable<T>)JsonSerializer.Deserialize<T?>(ref reader, options);
    //        }
    //    }

    //    public override void Write(Utf8JsonWriter writer, Undefinable<T> value,
    //        JsonSerializerOptions options)
    //    {
    //        Console.WriteLine("*** WRITING {0}", typeof(T).FullName);
    //        if (value.IsDefined)
    //        {
    //            if (options?.GetConverter(Undefinable<T>.NullableValueType)
    //                is JsonConverter<T?> valueConverter)
    //            {
    //                valueConverter.Write(writer, value.Value, options);
    //            }
    //            else
    //            {
    //                JsonSerializer.Serialize<T?>(writer, value.Value, options);
    //            }
    //        }
    //        else
    //        {
    //            throw new Exception("FOOBAR");
    //        }
    //    }
    //}

    //internal class UndefinableConverterFactory : JsonConverterFactory
    //{
    //    public override bool CanConvert(Type typeToConvert)
    //    {
    //        return typeToConvert.IsGenericType
    //            && typeToConvert.GetGenericTypeDefinition() == typeof(Undefinable<>);
    //    }

    //    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    //    {
    //        var converterType = typeof(UndefinableConverter<>)
    //            .MakeGenericType(typeToConvert.GetGenericArguments());
    //        return (JsonConverter)Activator.CreateInstance(converterType);
    //    }

    //    //static JsonSerializerOptions CustomOptions = new JsonSerializerOptions
    //    //{
    //    //    IgnoreNullValues = true,
    //    //};

    //    //public override bool CanConvert(Type typeToConvert)
    //    //{
    //    //    return base.CanConvert(typeToConvert);
    //    //}

    //    //public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //    //{
    //    //    Console.WriteLine("*** READING {0}", typeToConvert.FullName);
    //    //    if (typeToConvert != typeof(object)
    //    //        && (options?.GetConverter(typeToConvert) is JsonConverter<T> valueConverter))
    //    //    {
    //    //        return valueConverter.Read(ref reader, typeToConvert, options);
    //    //    }
    //    //    else
    //    //    {
    //    //        return JsonSerializer.Deserialize<T>(ref reader, options);
    //    //    }
    //    //}

    //    //public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    //    //{
    //    //    Console.WriteLine("*** WRITING {0}", typeof(T).FullName);
    //    //    var typeToConvert = typeof(T);
    //    //    if (typeToConvert != typeof(object)
    //    //        && (options?.GetConverter(typeToConvert) is JsonConverter<T> valueConverter))
    //    //    {
    //    //        valueConverter.Write(writer, value, CustomOptions);
    //    //    }
    //    //    else
    //    //    {
    //    //        JsonSerializer.Serialize<T>(writer, value, CustomOptions);
    //    //    }
    //    //}
    //}
}

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherDashboard.API
{
    public partial class ForecastJson
    {
        [JsonProperty("cod")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Cod { get; set; }

        [JsonProperty("message")]
        public double Message { get; set; }

        [JsonProperty("cnt")]
        public long Cnt { get; set; }

        [JsonProperty("list")]
        public List[] List { get; set; }

        [JsonProperty("city")]
        public City City { get; set; }
    }

    public partial class City
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("coord")]
        public Coord Coord { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }

    public partial class List
    {
        [JsonProperty("dt")]
        public long Dt { get; set; }

        [JsonProperty("main")]
        public MainClass Main { get; set; }

        [JsonProperty("weather")]
        public Weather[] Weather { get; set; }

        [JsonProperty("clouds")]
        public Clouds Clouds { get; set; }

        [JsonProperty("wind")]
        public Wind Wind { get; set; }

        [JsonProperty("rain")]
        public Rain Rain { get; set; }

        [JsonProperty("sys")]
        public Sys Sys { get; set; }

        [JsonProperty("dt_txt")]
        public DateTimeOffset DtTxt { get; set; }
    }

    public partial class MainClass
    {
        [JsonProperty("temp")]
        public double Temp { get; set; }

        [JsonProperty("temp_min")]
        public double TempMin { get; set; }

        [JsonProperty("temp_max")]
        public double TempMax { get; set; }

        [JsonProperty("pressure")]
        public double Pressure { get; set; }

        [JsonProperty("sea_level")]
        public double SeaLevel { get; set; }

        [JsonProperty("grnd_level")]
        public double GrndLevel { get; set; }

        [JsonProperty("humidity")]
        public long Humidity { get; set; }

        [JsonProperty("temp_kf")]
        public long TempKf { get; set; }
    }

    public partial class Rain
    {
    }

    public partial class Sys
    {
        [JsonProperty("pod")]
        public Pod Pod { get; set; }
    }

    public enum Pod { D, N };

    public enum Description { BrokenClouds, ClearSky, FewClouds, ScatteredClouds };

    public enum Icon { The01D, The01N, The02D, The02N, The03N, The04N };

    public enum MainEnum { Clear, Clouds };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                PodConverter.Singleton,
                DescriptionConverter.Singleton,
                IconConverter.Singleton,
                MainEnumConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class PodConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Pod) || t == typeof(Pod?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "d":
                    return Pod.D;
                case "n":
                    return Pod.N;
            }
            throw new Exception("Cannot unmarshal type Pod");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Pod)untypedValue;
            switch (value)
            {
                case Pod.D:
                    serializer.Serialize(writer, "d");
                    return;
                case Pod.N:
                    serializer.Serialize(writer, "n");
                    return;
            }
            throw new Exception("Cannot marshal type Pod");
        }

        public static readonly PodConverter Singleton = new PodConverter();
    }

    internal class DescriptionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Description) || t == typeof(Description?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "broken clouds":
                    return Description.BrokenClouds;
                case "clear sky":
                    return Description.ClearSky;
                case "few clouds":
                    return Description.FewClouds;
                case "scattered clouds":
                    return Description.ScatteredClouds;
            }
            throw new Exception("Cannot unmarshal type Description");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Description)untypedValue;
            switch (value)
            {
                case Description.BrokenClouds:
                    serializer.Serialize(writer, "broken clouds");
                    return;
                case Description.ClearSky:
                    serializer.Serialize(writer, "clear sky");
                    return;
                case Description.FewClouds:
                    serializer.Serialize(writer, "few clouds");
                    return;
                case Description.ScatteredClouds:
                    serializer.Serialize(writer, "scattered clouds");
                    return;
            }
            throw new Exception("Cannot marshal type Description");
        }

        public static readonly DescriptionConverter Singleton = new DescriptionConverter();
    }

    internal class IconConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Icon) || t == typeof(Icon?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "01d":
                    return Icon.The01D;
                case "01n":
                    return Icon.The01N;
                case "02d":
                    return Icon.The02D;
                case "02n":
                    return Icon.The02N;
                case "03n":
                    return Icon.The03N;
                case "04n":
                    return Icon.The04N;
            }
            throw new Exception("Cannot unmarshal type Icon");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Icon)untypedValue;
            switch (value)
            {
                case Icon.The01D:
                    serializer.Serialize(writer, "01d");
                    return;
                case Icon.The01N:
                    serializer.Serialize(writer, "01n");
                    return;
                case Icon.The02D:
                    serializer.Serialize(writer, "02d");
                    return;
                case Icon.The02N:
                    serializer.Serialize(writer, "02n");
                    return;
                case Icon.The03N:
                    serializer.Serialize(writer, "03n");
                    return;
                case Icon.The04N:
                    serializer.Serialize(writer, "04n");
                    return;
            }
            throw new Exception("Cannot marshal type Icon");
        }

        public static readonly IconConverter Singleton = new IconConverter();
    }

    internal class MainEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(MainEnum) || t == typeof(MainEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Clear":
                    return MainEnum.Clear;
                case "Clouds":
                    return MainEnum.Clouds;
            }
            throw new Exception("Cannot unmarshal type MainEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (MainEnum)untypedValue;
            switch (value)
            {
                case MainEnum.Clear:
                    serializer.Serialize(writer, "Clear");
                    return;
                case MainEnum.Clouds:
                    serializer.Serialize(writer, "Clouds");
                    return;
            }
            throw new Exception("Cannot marshal type MainEnum");
        }

        public static readonly MainEnumConverter Singleton = new MainEnumConverter();
    }
}

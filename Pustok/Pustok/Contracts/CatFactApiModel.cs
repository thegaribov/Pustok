using System;
using System.Text.Json.Serialization;

namespace Pustok.Contracts;

public class CatFactApiModel
{

    [JsonPropertyName("status")]
    public CatFactStatusApiModel Status { get; set; }

    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [JsonPropertyName("user")]
    public string User { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("__v")]
    public int Version { get; set; }

    [JsonPropertyName("source")]
    public string Source { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("deleted")]
    public bool Deleted { get; set; }

    [JsonPropertyName("used")]
    public bool Used { get; set; }
}

public class CatFactStatusApiModel
{
    [JsonPropertyName("verified")]
    public bool Verified { get; set; }

    [JsonPropertyName("sentCount")]
    public int SentCount { get; set; }
}

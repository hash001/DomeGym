using System.Text.Json.Serialization;

namespace GymManagement.Contracts.Subscriptions;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SubscriptionTypeV1
{
    Free,
    Starter,
    Pro
}
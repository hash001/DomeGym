namespace GymManagement.Contracts.Subscriptions;

public record CreateSubscriptionRequest(
    SubscriptionTypeV1 SubscriptionTypeV1,
    Guid AdminId);
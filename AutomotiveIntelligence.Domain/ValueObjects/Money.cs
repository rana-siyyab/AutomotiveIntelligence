namespace AutomotiveIntelligence.Domain.ValueObjects;

public sealed class Money
{
    public decimal Amount { get; }

    public string Currency { get; }

    public Money(decimal amount, string currency = "PKR")
    {
        if (amount < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Amount cannot be negative.");
        }

        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException(
                "Currency is required.",
                nameof(currency));
        }

        Amount = amount;
        Currency = currency;
    }

    public override string ToString()
    {
        return $"{Currency} {Amount:N0}";
    }
}
namespace eCommerce.Helpers.Logging
{
    public interface IRequestCorrelationIdentifier
    {
        string CorrelationID { get; }
    }
}

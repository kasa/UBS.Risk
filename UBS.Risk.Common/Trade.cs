namespace UBS.Risk.Common;

public class Trade : ITrade
{
    public double Value { get; }
    public string ClientSector { get; }
    public DateTime NextPaymentDate { get; }

    public Trade(double value, string clientSector, DateTime nextPaymentDate)
    {
        Value = value;
        ClientSector = clientSector;
        NextPaymentDate = nextPaymentDate;
    }

    public override string ToString()
    {
        return $"Value: {Value}, ClientSector: {ClientSector}, NextPaymentDate: {NextPaymentDate:d}";
    }
}
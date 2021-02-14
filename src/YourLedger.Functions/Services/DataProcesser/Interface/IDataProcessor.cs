namespace YourLedger.Functions.Services.DataProcesser.Interface
{
    public interface IDataProcessor<T1,T2>
    {
        T2 ProcessBuyOrder(T1 buyData, T2 userData);
        T2 ProcessSellOrder(T1 sellData, T2 userData);
    }
}
namespace PurchaseBasket.Utilites
{
    public class Result<T> 
    {
        public string Message { get; set; } = null;
        public Status Status { get; set; } = Status.OK;
        public T Value { get; set; }
    }

    public class Result
    {
        public string Message { get; set; } = null;
        public Status Status { get; set; } = Status.OK;
    }
}
namespace Domain.DorZnakUA.Result;

public class BaseResult
{
    public bool IsSeccess => ErrorMessage == null;
    public string ErrorMessage { get; set; }
    public int? ErroreCode { get; set; }
}

public class BaseResult<T> : BaseResult
{
    public BaseResult(string errorMessage, int? erroreCode, T date)
    {
        ErrorMessage = errorMessage;
        ErroreCode = erroreCode;
        Date = date;
    }
    public BaseResult()
    {
        
    }
    public T Date { get; set; }
}
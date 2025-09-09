using System.Net;

namespace CashFlow.Exception.ExceptionsBase;
public class ErrorOnValidationException : CashFlowException
{
    private readonly List<string> _errors;

    public override int StatusCode => (int)HttpStatusCode.BadRequest;
    public ErrorOnValidationException(List<string> errormessages) : base(string.Empty)
    {
        _errors = errormessages;
    }    

    public override List<string> GetErrors()
    {
        return _errors;
    }
}

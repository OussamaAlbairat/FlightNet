
namespace FlightNet.Core.Contracts;
public class ValidationException : Exception {
    public ValidationException(string message) : base(message) { }
}
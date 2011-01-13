namespace Twintimidator.Actions
{

    public interface IActionResult
    {
        bool Success { get; set; }
        string ErrorMessage { get; }
    }

    public interface IActionResult<T> :IActionResult
    {
        
        T ReturnValue { get; }
        
    }
}
using BelleVillePrototype.ApiService.OptionType;

namespace BelleVillePrototype.ApiService.Shared.Result;

public class Result<T> 
{
    public Option<string> Error
    {
        get;
    } = Option<string>.None();

    public Option<T> Content
    {
        get;
    } = Option<T>.None();

    protected internal Result(T? content, string? error = null)
    {
        if (error is not null)
        {
            this.Error = Option<string>.Some(error);
            this.Content = Option<T>.None();
        }
        else if (content is not null)
        {
            this.Error = Option<string>.None();
            this.Content = Option<T>.Some(content);
        }
        else
        {
            this.Error = Option<string>.Some("Some error occurred, no message was left");
            this.Content = Option<T>.None();
        }
    }
}
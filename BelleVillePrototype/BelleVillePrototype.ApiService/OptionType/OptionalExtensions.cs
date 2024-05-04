namespace BelleVillePrototype.ApiService.OptionType;

public static class OptionalExtensions 
{
    public static Option<T> ToOption<T>(this T? value) where T: class 
        => value is null ? Option<T>.None() : Option<T>.Some(value);

    public static Option<T> Where<T>(this T? option, Func<T, bool> predicate) where T : class
        => option is not null && predicate(option) ? Option<T>.Some(option) : Option<T>.None();
    
    public static Option<T> WhereNot<T>(this T? option, Func<T, bool> predicate) where T : class
        => option is not null && !predicate(option) ? Option<T>.Some(option) : Option<T>.None();
}
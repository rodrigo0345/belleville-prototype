namespace BelleVillePrototype.ApiService.OptionType;

public class Option<T>: IEquatable<Option<T>> where T: class
{
    private T? _content;
    
    private Option() {}
    
    public static Option<T> None() => new ();
    public static Option<T> Some(T value) => new (){_content = value};
    
    public Option<TResult> Map<TResult>(Func<T, TResult> map) where TResult: class
    {
        return _content is null ? Option<TResult>.None() : Option<TResult>.Some(map(_content));
    }
    
    public Option<T> Where(Func<T, bool> predicate)
    {
        return _content is null || !predicate(_content) ? Option<T>.None() : this;
    }
    public Option<T> WhereNot(Func<T, bool> predicate) => Where(x => !predicate(x));
    
    public T OrElseThrow() => _content ?? throw new InvalidOperationException("Option is empty");
    public T OrElse(T defaultValue) => _content ?? defaultValue;
    public T OrElse(Func<T> defaultValue) => _content ?? defaultValue();

    public override int GetHashCode() {return _content?.GetHashCode() ?? 0;}
    public override bool Equals(object? obj)
    {
        return this.Equals(obj as Option<T>);
    }
    public bool Equals(Option<T>? other)
    {
        return other is not null && (_content?.Equals(other._content) ?? false);
    }
    
    public static bool operator ==(Option<T> left, Option<T> right)
    {
        return left.Equals(right);
    }
    
    public static bool operator !=(Option<T> left, Option<T> right)
    {
        return !(left == right);
    }
}
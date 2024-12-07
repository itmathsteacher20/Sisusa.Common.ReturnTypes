namespace Sisusa.Common.ReturnTypes;

public class Nothing
{
    private Nothing(){}
    
    public static Nothing Instance => new ();

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj);
    }

    public override int GetHashCode()
    {
        return 0;
    }
    
    public static bool operator ==(Nothing left, Nothing right)
    {
        return ReferenceEquals(left, right);
    }

    public static bool operator !=(Nothing left, Nothing right)
    {
        return !ReferenceEquals(left, right);
    }

    public static implicit operator Nothing(Type type)
    {
        if (type == typeof(Nothing))
            return new Nothing();
        
        throw new InvalidOperationException($"Cannot convert type of {type.Name} to Nothing.");
    }
}
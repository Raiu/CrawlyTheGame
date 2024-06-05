using System.Diagnostics.CodeAnalysis;

namespace Crawly;

public readonly struct Hexguid : IComparable<Hexguid>, IEquatable<Hexguid>, IFormattable, IParsable<Hexguid>
{
    private readonly Guid _value;

    private Hexguid(Guid value) => _value = value;

    public static Hexguid NewId() => new(Guid.NewGuid());

    public override string ToString() =>
        Convert.ToBase64String(_value.ToByteArray())[..^2];

    public static Hexguid Parse(string s, IFormatProvider? provider) =>
            new(new Guid(Convert.FromBase64String(s + "==")));


    public static bool operator ==(Hexguid left, Hexguid right) => left._value == right._value;

    public static bool operator !=(Hexguid left, Hexguid right) => left._value != right._value;

    public static bool operator ==(Guid left, Hexguid right) => left == right._value;

    public static bool operator !=(Guid left, Hexguid right) => left != right._value;

    public static bool operator ==(Hexguid left, Guid right) => left._value == right;

    public static bool operator !=(Hexguid left, Guid right) => left._value != right;

    public bool Equals(Hexguid other) => _value == other;

    public override bool Equals(object? obj)
    {
        if (obj is Hexguid hexguid)
            return _value == hexguid._value;

        else if (obj is Guid guid)
            return _value == guid;

        return false;
    }

    public override int GetHashCode() => _value.GetHashCode();

    public int CompareTo(Hexguid other) => _value.CompareTo(other._value);

    public string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out Hexguid result)
    {
        if (s is null)
        {
            result = default;
            return false;
        }

        try
        {
            result = Parse(s, provider);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }
}

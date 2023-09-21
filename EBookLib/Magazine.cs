namespace EBookLib;

/// <summary>
/// Represents class inherited from <see cref="PrintEdition"/>
/// </summary>
public class Magazine : PrintEdition
{
    public uint period; 

    public Magazine(string name, uint pages, uint period) : base(name, pages)
    {
        this.period = period;
    }
    
    public override string ToString()
    {
        return $"name={name}; pages={pages}; period={period}";
    }
}
namespace EBookLib;

/// <summary>
/// Represents abstract class of PrintEdition 
/// </summary>
public abstract class PrintEdition : IPrinting
{
    public string name;
    public uint pages;
    public event EventHandler onPrint;

    public PrintEdition(string name, uint pages)
    {
        this.name = name;
        this.pages = pages;
    }

    /// <summary>
    /// Сauses an event onPrint
    /// </summary>
    public void Print()
    {
        onPrint?.Invoke(this, EventArgs.Empty);
    }

    public override string ToString()
    {
        return $"name={name}; pages={pages}";
    }
}
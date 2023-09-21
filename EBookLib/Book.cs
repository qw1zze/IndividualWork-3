namespace EBookLib;

/// <summary>
/// Represents class inherited from <see cref="PrintEdition"/>
/// </summary>
public class Book : PrintEdition
{
    public string author;

    public Book(string name, uint pages, string author) : base(name, pages)
    {
        this.author = author;
    }
    
    public override string ToString()
    {
        return $"name={name}; pages={pages}; author={author}";
    }
}
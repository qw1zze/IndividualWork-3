using System.Collections;

namespace EBookLib;

/// <summary>
/// Represents class of library 
/// </summary>
public class MyLibrary<T> : IEnumerable<T> where T : PrintEdition
{
    public List<T> library = new List<T>();

    public event EventHandler<MyLibraryEventArgs> onTake;

    /// <summary>
    /// Return meancount of pages at books
    /// </summary>
    public double MeanBooks
    {
        get
        {
            return (double)library.Sum(p => p is Book ? p.pages : 0) / library.Sum(p => p is Book ? 1 : 0);  
        }
    }

    /// <summary>
    /// Return meancount of pages in magazine
    /// </summary>
    public double MeanMagazine
    {
        get
        {
            return (double)library.Sum(p => p is Magazine ? p.pages : 0) / library.Sum(p => p is Magazine ? 1 : 0);
        }
    }

    /// <summary>
    /// Calls event onTake
    /// </summary>
    /// <param name="start"></param>
    public void TakeBooks(char start)
    {
        onTake?.Invoke(this, new MyLibraryEventArgs(start));
    }

    /// <summary>
    /// Add obect in library
    /// </summary>
    /// <param name="printed"></param>
    public void Add(T printed)
    {
        library.Add(printed);
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => new MyLibraryEnumerator<T>(library);

    IEnumerator IEnumerable.GetEnumerator() => new MyLibraryEnumerator<T>(library);


    public override string ToString()
    {
        string s = "";
        s += "Общее число страниц: " + library.Sum(p => p.pages) + "\n"; 

        for (int i = 0; i < library.Count; i++)
        {
            s += library[i].ToString() + "\n";
        }
        return s;
    }
}
using System.Collections;

namespace EBookLib;

/// <summary>
/// Class enumerator
/// </summary>
/// <typeparam name="U"></typeparam>
public class MyLibraryEnumerator<U> : IEnumerator<U>
{
    List<U> lib;
    int Position = -1;

    public MyLibraryEnumerator(List<U> lib)
    {
        this.lib = lib;
    }

    public U Current { get => lib[Position];  }

    object IEnumerator.Current { get => lib[Position]; }

    public void Dispose() {}

    public bool MoveNext()
    {
        if (Position < lib.Count - 1)
        {
            Position++;
            return true;
        }
        else 
        {
            return false;
        }
    }

    public void Reset() => Position = -1;
}
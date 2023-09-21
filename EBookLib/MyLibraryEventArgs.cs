namespace EBookLib;

/// <summary>
/// Class for using standart event
/// </summary>
public class MyLibraryEventArgs : EventArgs
{
    public char start;

    public MyLibraryEventArgs(char start)
    {
        this.start = start;
    }
}
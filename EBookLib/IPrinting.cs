namespace EBookLib;

/// <summary>
/// Interface for realization print inforamtion
/// </summary>
public interface IPrinting
{
    public event EventHandler onPrint;

    public void Print();
}
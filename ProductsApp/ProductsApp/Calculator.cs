using System;
using System.IO.Compression;

namespace ProductsApp;

public class ResultEventArgs : EventArgs
{
    public int Result {get;set;}
}

public class Calculator
{

    public event EventHandler OnSetXY;
    public event EventHandler<ResultEventArgs> OnCompute;

    private int x;
    private int y;

    public void SetXY(int x, int y)
    {
        this.x = x;
        this.y = y;
        OnSetXY(this, new EventArgs());
    }

    public void Execute(Operation operation)
    {
        int result = operation(x, y);
        Console.WriteLine(result);
        OnCompute(this, new ResultEventArgs()
        {
            Result = result
        });
    }
}

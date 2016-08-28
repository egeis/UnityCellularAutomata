using UnityEngine;
using System.Collections;

public class GridCoordinates : MonoBehaviour, IRecycle
{
    public int x = -1;
    public int y = -1;

    public void Shutdown() 
    {
        x = -1;
        y = -1;
    }

    public void Restart() {}
}

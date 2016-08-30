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

    public int State()
    {
        Vector2 cords = new Vector2(x, y);
        GlobalSettings _gs = GlobalSettings.Instance;
        int r = -1;

        if (_gs.States.ContainsKey(cords))
            _gs.States.TryGetValue(cords, out r);

        return r;
    }
}

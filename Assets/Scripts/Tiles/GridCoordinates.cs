using UnityEngine;
using System.Collections;

public class GridCoordinates : MonoBehaviour, IRecycle
{
    public int x = -1;
    public int y = -1;

    public int State { get { return _state; } }
    private int _state;

    public void Shutdown() 
    {
        x = -1;
        y = -1;
    }

    public void Restart()
    {
        GlobalSettings _gs = GlobalSettings.Instance;

        x = _gs._x;
        y = _gs._y;

        Vector2 cords = new Vector2(x, y);
        int _state = -1;

        if (_gs.States.ContainsKey(cords))
            _gs.States.TryGetValue(cords, out _state);
    }
}

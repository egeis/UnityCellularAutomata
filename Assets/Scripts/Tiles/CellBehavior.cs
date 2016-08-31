using UnityEngine;
using System.Collections;

public class CellBehavior : MonoBehaviour, IRecycle
{
    public int x = -1;
    public int y = -1;
    public int _state = -1;

    GlobalSettings _gs;
    Transition transition;

    void Awake()
    {
        _gs = GlobalSettings.Instance;
        transition = GetComponent<Transition>();
    }

    public void Shutdown() 
    {
        x = -1;
        y = -1;
        gameObject.name = "_";
    }

    void Update()
    {
        int nextState = _state;

        //if (_gs == null)
        //    _gs = GlobalSettings.Instance;

        _gs.States.TryGetValue(new Vector2(x, y), out nextState);

        if (nextState != _state && nextState > -1)
        {
            Color next = _gs.Rules.getColorValue(nextState);
            transition.SetColor(next);
        }
    }

    public void Restart()
    {
        _gs = GlobalSettings.Instance;
        transition = GetComponent<Transition>();

        x = _gs._x;
        y = _gs._y;

        Vector2 cords = new Vector2(x, y);
        _state = -1;

        if (_gs.States.ContainsKey(cords))
            _gs.States.TryGetValue(cords, out _state);

        if (_state > -1)
        {
            Color next = _gs.Rules.getColorValue(_state);
            GetComponent<SpriteRenderer>().color = next;
        }
    }
}

using UnityEngine;
using System.Collections;

public class CellBehavior : MonoBehaviour, IRecycle
{
    public int x = -1;
    public int y = -1;

    public int State { get { return _state; } }

    public int _state = -1;
    int _generation = 0;

    GlobalSettings _gs;

    public void Shutdown() 
    {
        x = -1;
        y = -1;
        gameObject.name = "_";
    }

    void Update()
    {
        /*if (_nextState != State && _nextState > -1 && _gs != null)
        {
            Color next = _gs.Rules.getColorValue(_nextState);
            GetComponent<Transition>().Toggle(next);
        }
        else if(_gs == null)
        {
            _gs = GlobalSettings.Instance;
        }*/

    }

    public void Restart()
    {
        _gs = GlobalSettings.Instance;

        x = _gs._x;
        y = _gs._y;

        _generation = _gs.getCurrentGeneration();

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

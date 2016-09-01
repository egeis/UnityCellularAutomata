using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Threading;

using UnityToolbag;

public class GlobalSettings : MonoBehaviour
{
    public float transitionDuration = 1f;
    
    public float generationDelay = 2f;

    public Vector2 CellCount = new Vector2(100, 100);

    public GameObject CellPrefab;

    [Range(0,100)]
    public int maxQueuedCount = 10;

    public Dictionary<Vector2, int> States = new Dictionary<Vector2, int>();
    Dictionary<Vector2, int> lastProcessedStates = new Dictionary<Vector2, int>();

    public Dictionary<string, GameObject> ActiveObjects = new Dictionary<string, GameObject>();

    [HideInInspector]
    public Queue<Dictionary<Vector2, int>> FutureGenerations = new Queue<Dictionary<Vector2, int>>();

    [HideInInspector]
    public int _x = 0;

    [HideInInspector]
    public int _y = 0;

    public Classic Rules;

    [HideInInspector]
    public float GridSize = 8f;

    int current_generation = 0;
    float delay = 0f;
    static GlobalSettings _instance;
    bool scheduleNextState = true;

    public static GlobalSettings Instance
    {
        get
        {
            return _instance;
        }
    }

    public GridVisible Visible
    {
        get { return Camera.main.gameObject.GetComponent<CameraGridVisible>().gridV; }
    }

    void Awake()
    {
        _instance = this;
        Rules = new Classic();

        //States = new int[(int)CellCount.x, (int)CellCount.y];
        //lastProcessedStates = new int[(int)CellCount.x, (int)CellCount.y];

        for (int i = 0; i < CellCount.x; i++)
            for (int j = 0; j < CellCount.y; j++)
            {
                int s = Rules.getRandomCell();
                States.Add(new Vector2(i, j), s);
                lastProcessedStates.Add(new Vector2(i, j), s);
                //States[i, j] = s;
                //lastProcessedStates[i, j] = s;
            }

        incrementCurrentGeneration();
    }

    void Update()
    {
        if(scheduleNextState && (FutureGenerations.Count < maxQueuedCount) )
        {
            Future<Dictionary<Vector2, int>> futureState = new Future<Dictionary<Vector2, int>>();
            futureState = Rules.ComputeNextState(lastProcessedStates, CellCount);

            futureState.OnSuccess((states) => {
                FutureGenerations.Enqueue(states.value);
                lastProcessedStates = states.value;
                scheduleNextState = true;
            });

            scheduleNextState = false;
        }
    }

    void LateUpdate()
    {
        delay += Time.deltaTime;

        if (delay > generationDelay)
        {
            if (FutureGenerations.Count > 0)
            {
                States = FutureGenerations.Dequeue();
                incrementCurrentGeneration();
            }

            delay = 0;
        }
    }

    void OnDestroy()
    {
        _instance = null;
    }

    public int getCurrentGeneration()
    {
        return Interlocked.CompareExchange(ref current_generation, 0, 0);
    }

    public int incrementCurrentGeneration()
    {
        return Interlocked.Increment(ref current_generation);
    }
}
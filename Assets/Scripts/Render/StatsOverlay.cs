using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatsOverlay : MonoBehaviour {

    GlobalSettings _gs;

    public Text queuedStat;
    public Text generationStat;

	void Start ()
    {
        _gs = GlobalSettings.Instance;
	}
	
	void LateUpdate ()
    {
        generationStat.text = _gs.getCurrentGeneration().ToString("D10");
        queuedStat.text = string.Format("{0:D3} of {1:D3}",_gs.FutureGenerations.Count,_gs.maxQueuedCount);
	}
}

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
        generationStat.text = _gs.Generation.ToString("D10");
	}
}

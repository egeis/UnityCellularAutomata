﻿using UnityEngine;
using System.Collections;

public class TileMap : MonoBehaviour
{
    public Vector2 mapSize = new Vector2(20, 20);
    public Vector2 tileSize = new Vector2();
    public GameObject prefab;
    public Vector2 gridSize = new Vector2();
    public int pixelsToUnits = 1;

    void Awake()
    {
        if (PlayerPrefs.HasKey("MapSizeX") && PlayerPrefs.HasKey("MapSizeY"))
        {
            mapSize.x = PlayerPrefs.GetFloat("MapSizeX");
            mapSize.y = PlayerPrefs.GetFloat("MapSizeY");
        }
        else
        {
            PlayerPrefs.SetFloat("MapSizeX",mapSize.x);
            PlayerPrefs.SetFloat("MapSizeY",mapSize.y);
        }

    }

	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}
}

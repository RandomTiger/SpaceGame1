using UnityEngine;
using System.Collections;

public class Faction : MonoBehaviour {

	public int factionNum;

	Color [] factionColors = 
	{
		new Color(0,1,0),
		new Color(1,0,0),
		new Color(0,0,1),
	};

	// Use this for initialization
	void Start () 
	{
		Coloriser [] colorControllers = GetComponentsInChildren<Coloriser> (true);
		foreach (Coloriser col in colorControllers) 
		{
			col.color = factionColors[factionNum];
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

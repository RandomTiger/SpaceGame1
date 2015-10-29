using UnityEngine;
using System.Collections;

// Do the absolute minimum in this class / script
public class StartScene : MonoBehaviour
{	
	void Start ()
    {
        // ASync loading only
        Application.LoadLevelAsync("Loading");
        Application.LoadLevelAsync("Game");
	}
}

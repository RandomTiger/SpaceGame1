using UnityEngine;
using System.Collections;

public class DebugDirectionLines : MonoBehaviour {

    float mLineLen = 5;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame

    public void Update()
    {
    //    Debug.DrawLine(transform.position, transform.position + transform.forward * mLineLen, Color.red);
    //    Debug.DrawLine(transform.position, transform.position + transform.right * mLineLen, Color.blue);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * mLineLen);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.right * mLineLen);
    }

}

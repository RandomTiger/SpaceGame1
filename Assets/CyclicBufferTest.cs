using UnityEngine;

public class CyclicBufferTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CyclicBuffer<int> test = new CyclicBuffer<int>(9);

        int val = 5;
        for (int i = 0; i < 8; i++)
        {
            val++;
            Debug.Log("CyclicBuffer in " + val);
            test.Add(val);
        }

        for (int i = 0; i < 6; i++)
        {
            Debug.Log("CyclicBuffer out " + test.GetFirst());
        }

        for (int i = 0; i < 4; i++)
        {
            val++;
            Debug.Log("CyclicBuffer in " + val);
            test.Add(val);
        }

        for (int i = 0; i < 6; i++)
        {
            Debug.Log("CyclicBuffer out " + test.GetFirst());
        }
    }
}

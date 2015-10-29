using UnityEngine;
using System.Collections;

public class DeathOnTrigger : MonoBehaviour
{
	private bool m_markForDestory;
	
	void OnTriggerEnter2D(Collider2D collision)
	{
		m_markForDestory = true;
	}
	
	void Update()
	{
		if(m_markForDestory)
		{
			Destroy(gameObject);
		}
	}
}
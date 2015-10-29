using UnityEngine;
using System.Collections;

public class DeathOnCollide : MonoBehaviour
{
	private bool m_markForDestory;
	
	void OnCollisionEnter2D(Collision2D collision)
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
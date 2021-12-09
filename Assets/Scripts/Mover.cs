using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
	public float speed;

	void Start ()
	{
		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3 (0 ,speed,0);
	}
}

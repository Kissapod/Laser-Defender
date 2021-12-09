using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBonus : MonoBehaviour
{
    public float startGravity;
    public float newGravity;
    public float changeGravityTime;
    public float dropRate;
    public float speedMove;

    private float ugolX, ugolY;
    private Transform target;
    void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale = startGravity;
        ugolX = Random.Range(-0.005f, 0.005f);
        ugolY = Random.Range(-0.005f, 0.005f);
        target = FindObjectOfType<PlayerController>().transform;
        Invoke(nameof(GravityDown), changeGravityTime);
    }

    private void Update()
    {
        if (target.GetComponent<PlayerController>().magnetState == true)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            Vector3 dir = target.position - transform.position;
            transform.position += (dir.normalized * Time.deltaTime * speedMove);
            transform.up = dir.normalized;
        }
        Vector3 speed = new Vector3(transform.position.x + ugolX, transform.position.y + ugolY, transform.position.z);
        transform.position = speed;
    }
    void GravityDown()
    {
        GetComponent<Rigidbody2D>().gravityScale = newGravity;
    }
}

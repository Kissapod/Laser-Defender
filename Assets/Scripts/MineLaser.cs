using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineLaser : MonoBehaviour
{
    public float startGravity;
    public float newGravity;
    public float changeGravityTime;
    public float mineRotate;

    private float ugolX, ugolY;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale = startGravity;
        Invoke("GravityDown", changeGravityTime);
        ugolX = Random.Range(-0.005f, 0.005f);
        ugolY = Random.Range(-0.005f, 0.005f);
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 speed = new Vector3(transform.position.x + ugolX, transform.position.y + ugolY, transform.position.z);
        transform.Rotate(new Vector3(0, 0, mineRotate) * Time.deltaTime);
        transform.position = speed;
    }

    void GravityDown()
    {
        GetComponent<Rigidbody2D>().gravityScale = newGravity;
    }
}

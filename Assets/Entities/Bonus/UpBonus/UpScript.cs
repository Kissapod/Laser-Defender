using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpScript : MonoBehaviour
{
    public float mSpeed = 1f;
    public float timeDestroy = 1f;
    public Sprite[] upSprites;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", timeDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * mSpeed);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}

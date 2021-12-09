using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionServant : MonoBehaviour
{
    public GameObject parent;

    private Vector3 positionCoord;
    // Start is called before the first frame update
    private void Start()
    {
        positionCoord = transform.position;
        transform.position = transform.parent.position;
    }

    private void Update()
    {
        if (transform.childCount == 0)
        {
            transform.position = transform.parent.position;
        } else
        {
            transform.position = positionCoord;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.3f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Servant : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 targetPosition;
    // Start is called before the first frame update

    void Start()
    {
        startPosition = GetComponentInParent<OverLord>().transform.position;
        targetPosition = GetComponentInParent<Position>().transform.position;
        Debug.Log("Начальная позиция = " + startPosition);
        Debug.Log("Конечная позиция = " + targetPosition);
        //StartCoroutine(MoveCoroutine());
    }

    /*IEnumerator MoveCoroutine()
    {
        for (float i = 0; i < 1; i+= Time.deltaTime) 
        {
           transform.position = Vector3.Lerp(startPosition, targetPosition, EasingInverseSquared(i));
           yield return null;
        }
       transform.position = targetPosition;
    }

   float EasingInverseSquared (float x)
   {
       return 1 - (1 - x) * (1 - x);
   }*/

    private void Update()
    {
        transform.position = Vector3.MoveTowards(startPosition, targetPosition, Time.deltaTime * 10);
    }
}

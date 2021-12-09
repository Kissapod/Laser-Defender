using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public float speed = 5f;
    public float rotationOffset = 90f;

    private Animator animator;
    private float rotZ;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (IsAnimationPlaying("Idle"))
        {
            animator.enabled = false;
        }
        Vector3 playerPosition = FindObjectOfType<PlayerController>().transform.position;
        Vector3 direction = (playerPosition - transform.position).normalized; //находим вектор положения игрока
        rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //расчитываем угол поворота врага
        Quaternion rotation = Quaternion.AngleAxis(rotZ+rotationOffset, Vector3.forward); //Создает вращение, поворачивающееся на angleградусы Сокращение для письма Vector3(0, 0, 1).
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed); //поворачиваем наш объект из одной позиции в другую с определенной скоростью
    }
    public bool IsAnimationPlaying(string animationName)
    {
        // берем информацию о состоянии
        var animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // смотрим, есть ли в нем имя какой-то анимации, то возвращаем true
        if (animatorStateInfo.IsName(animationName))
            return true;
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodShield : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Laser missile = collision.gameObject.GetComponent<Laser>();
        Meteor meteor = collision.gameObject.GetComponent<Meteor>();
        if (missile)
        {
            missile.Hit();
        }
        if (meteor)
        {
            AudioSource.PlayClipAtPoint(meteor.collisionSound, transform.position);
            meteor.DropState(10000);
            meteor.MeteorDestroyState();
            meteor.MeteorState();
        }
    }
}

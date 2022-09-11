using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void NotifyCollission(Collision2D collision);
public class OnCollisionListener : MonoBehaviour
{

    private BoxCollider2D box;
    private Rigidbody2D Rigidbody2D;
    public event NotifyCollission OnCollissionEnterPropagate;
    // Start is called before the first frame update

    private void Start()
    {
        box = this.GetComponent<BoxCollider2D>();
        Rigidbody2D = this.gameObject.AddComponent<Rigidbody2D>();
        Rigidbody2D.isKinematic = true;
        Rigidbody2D.useFullKinematicContacts = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollissionEnterPropagate.Invoke(collision);
        Debug.Log(name +  " touched something");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public Vector3 velo;
  // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position += velo * Time.fixedDeltaTime;
    }
}

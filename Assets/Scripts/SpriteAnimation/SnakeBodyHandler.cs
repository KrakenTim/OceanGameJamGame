using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyHandler : MonoBehaviour
{
     public List<Marker> marker = new List<Marker>();
    /// <summary>
    /// bodyparts you want to spawn
    /// </summary>
    public List<GameObject> BodyParts = new List<GameObject>();

    /// <summary>
    /// bodyparts spawned
    /// </summary>
    List<GameObject> snakeBody = new List<GameObject>();

    [SerializeField] float distance = 0.5f;
    [SerializeField, Range(0, 1)] float slerpFactor;

    void Start()
    {
        for (int i = 0; i < BodyParts.Count; i++)
        {
            GameObject temp = Instantiate(BodyParts[i], transform.position + Vector3.up * distance * (i + 1), transform.rotation);
            snakeBody.Add(temp);

            if (!temp.GetComponent<OnCollisionListener>())
            {
                temp.AddComponent<OnCollisionListener>();
            }
            temp.GetComponent<OnCollisionListener>().OnCollissionEnterPropagate += CollsionHandler;
            temp.name = temp.name + " " +i.ToString();
        }

        InstantiateBuffer();
    }

    /// <summary>
    /// progates collision of the tails
    /// </summary>
    /// <param name="collision"></param>
    public void CollsionHandler(Collision2D collision)
    {
        //
        Debug.Log(collision.gameObject.name);
    }

    public void OnDisable()
    {
        for (int i = 0; i < snakeBody.Count; i++)
        {
            snakeBody[i].GetComponent<OnCollisionListener>().OnCollissionEnterPropagate -= CollsionHandler;
            Destroy(snakeBody[i].gameObject);
        }
    }

    public void InstantiateBuffer()
    {
        for (int i = 0; i < BodyParts.Count * 15 + 1 + 2; i++)
        {
            marker.Add(new Marker(i * Vector3.up * distance * (1 / 15f), Quaternion.identity));
        }
    }

    // HAS TO BE FIXED UPDATE TO AVOID JITTERY MOVEMENT
    void FixedUpdate()
    {
        UpdateMarkers();
        MoveSegments();
    }

    private void UpdateMarkers()
    {
        marker.Add(new Marker(transform.position, transform.rotation));
        marker.RemoveAt(0);
    }

    private void MoveSegments()
    {
        for (int i = snakeBody.Count - 1; i >= 0; i--)
        {
            Marker a, a2, b, b2;
            a = marker[i * 15 + 1];
            a2 = marker[i * 15 + 2];
            b = marker[i * 15];
            b2 = marker[i * 15];

            Vector3 apos, bpos;
            apos = (a.pos + a2.pos) * .5f;
            bpos = (b.pos + b2.pos) * .5f;

            snakeBody[i].transform.position = Vector3.Lerp(apos, bpos, slerpFactor);
            snakeBody[i].transform.rotation = Quaternion.Lerp(a.rot, b.rot, slerpFactor);
        }
    }

    public struct Marker
    {
        public Vector3 pos;
        public Quaternion rot;

        public Marker(Vector3 pos, Quaternion rot)
        {
            this.pos = pos;
            this.rot = rot;
        }
    }
}

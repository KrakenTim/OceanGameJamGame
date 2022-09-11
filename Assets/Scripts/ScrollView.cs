using UnityEngine;

public class ScrollView : MonoBehaviour {

    public GameObject topScrollObject;
    public GameObject midScrollObject;
    public GameObject botScrollObject;

    public Vector2 ScreenBotLine;
    public Vector2 speed;

    private Vector2 posDif;

    private void Start() {
        posDif = topScrollObject.transform.position - midScrollObject.transform.position;

        Rigidbody2D body = topScrollObject.GetComponent<Rigidbody2D>();
        body.velocity = speed;

        body = midScrollObject.GetComponent<Rigidbody2D>();
        body.velocity = speed;

        body = botScrollObject.GetComponent<Rigidbody2D>();
        body.velocity = speed;

    }

    // Update is called once per frame
    void Update() {

        float delta = Time.deltaTime;


        //topScrollObject.transform.position = (Vector2)topScrollObject.transform.position + (speed * delta);
        //midScrollObject.transform.position = (Vector2)midScrollObject.transform.position + (speed * delta);
        //botScrollObject.transform.position = (Vector2)botScrollObject.transform.position + (speed * delta);

        if (botScrollObject.transform.position.y < ScreenBotLine.y) {
            botScrollObject.transform.position = (Vector2)topScrollObject.transform.position + posDif;
            GameObject temp = botScrollObject;
            botScrollObject = midScrollObject;
            midScrollObject = topScrollObject;
            topScrollObject = temp;
        }

    }
}

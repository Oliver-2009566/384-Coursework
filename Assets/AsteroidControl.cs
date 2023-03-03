using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsteroidControl : MonoBehaviour
{
    public float topBoundary    =  6.5f;
    public float bottomBoundary = -6.5f;
    public float leftBoundary   = -12.1f;
    public float rightBoundary  =  12.1f;
    public float speedX;
    public float speedY;

    // Start is called before the first frame update
    void Start() 
    {   
        speedX = Random.Range(-5f, 5f);
        speedY = Random.Range(-5f, 5f);

        Vector3 movement = new Vector3(speedX, speedY, 0);

        movement *= Time.deltaTime;

        Vector2 direction = transform.position + movement;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    // Update is called once per frame
    void Update() 
    {
        Vector3 movement = new Vector3(speedX, speedY, 0);

        movement *= Time.deltaTime;

        transform.Translate(movement.x, movement.y, 0, Camera.main.transform);

        if (transform.position.y > topBoundary) {
            transform.position = new Vector3(transform.position.x, bottomBoundary, 0);
        } else if (transform.position.y < bottomBoundary) {
            transform.position = new Vector3(transform.position.x, topBoundary, 0);
        }

        if (transform.position.x < leftBoundary) {
            transform.position = new Vector3(rightBoundary, transform.position.y, 0);
        } else if (transform.position.x > rightBoundary) {
            transform.position = new Vector3(leftBoundary, transform.position.y, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Vector2 speed = new Vector2(5, 5);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        float topBoundary    =  5.5f;
        float bottomBoundary = -5.5f;
        float leftBoundary   = -10.65f;
        float rightBoundary  =  10.65f;

        Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0); 
        
        movement *= Time.deltaTime;

        transform.Translate(movement.x, movement.y, 0, Camera.main.transform);

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = mousePosition - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, angle);

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

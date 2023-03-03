using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Vector2 speed = new Vector2(5, 5);
    public float topBoundary    =  5.5f;
    public float bottomBoundary = -5.5f;
    public float leftBoundary   = -10.65f;
    public float rightBoundary  =  10.65f;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {   
        // Gets the inputs of the player
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        // Sets where it should move based on the input strength, and the speed of the object
        Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0); 
        // Moves based on time since last frame, in order to keep it consistent on different refresh rates
        movement *= Time.deltaTime;
        // Move the object using the values in movement
        transform.Translate(movement.x, movement.y, 0, Camera.main.transform);

        // Gets the position of the mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Gets a direction from the player character, to the mouse position
        Vector2 direction = mousePosition - transform.position;
        // Calculate the angle at which the mouse is relative to the player character
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        // Rotate the player character to the angle we just calculated
        transform.eulerAngles = new Vector3(0, 0, angle);

        // Screen wrapping. If the player character goes beyond a defined boundary, change its coordinated to the opposite boundary
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

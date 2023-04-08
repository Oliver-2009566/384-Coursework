using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsteroidControl : MonoBehaviour
{
    public float verticalBoundary =  5.5f;
    public float horizontalBoundary = 10.65f;
    public float speedX;
    public float speedY;
    private bool spawnInvicicibility = true;

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

        // Screen wrapping. If the player character goes beyond a defined boundary, change its coordinated to the opposite boundary
        if (transform.position.y > verticalBoundary) {
            transform.position = new Vector3(transform.position.x, -(verticalBoundary), 0);
        } else if (transform.position.y < -(verticalBoundary)) {
            transform.position = new Vector3(transform.position.x, verticalBoundary, 0);
        }

        if (transform.position.x < -(horizontalBoundary)) {
            transform.position = new Vector3(horizontalBoundary, transform.position.y, 0);
        } else if (transform.position.x > horizontalBoundary) {
            transform.position = new Vector3(-(horizontalBoundary), transform.position.y, 0);
        }
    }

    //IEnumerator makeVulnerable
    //{

    //}
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Asteroid : MonoBehaviour
{
    public float verticalBoundary;
    public float horizontalBoundary;
    public float speedX;
    public float speedY;
    private bool vulnerable = false;
    [SerializeField] GameObject explosionVFX;

    // Start is called before the first frame update
    void Start() 
    {   
        StartCoroutine(makeVulnerable());
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

    IEnumerator makeVulnerable()
    {
        yield return new WaitForSeconds(0.05f);
        vulnerable = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.gameObject.tag == "Bullet")
        {
            if(vulnerable)
            {
                float scale = transform.localScale.x / 2;
                if (scale > 0.5f)
                {
                    int children = Random.Range(2, 5);
                    for (int i = 0; i < children; i++)
                    {
                        GameObject asteroid = Instantiate(gameObject);
                        asteroid.transform.localPosition = transform.localPosition;
                        asteroid.transform.localScale = new Vector3(scale, scale, 1);
                        asteroid.GetComponent<Asteroid>().verticalBoundary = verticalBoundary;
                        asteroid.GetComponent<Asteroid >().horizontalBoundary = horizontalBoundary;
                    }
                }
                Destroy(gameObject);
                Camera cam = GameObject.Find("Camera").GetComponent<Camera>();
                cam.GetComponent<Shake>().StartShake();
                GameObject explosion = Instantiate(explosionVFX);
                explosion.transform.position = transform.position;
                explosion.transform.localScale = transform.localScale;
                Destroy(explosion, 0.75f);
            }   
        }
    }  
}

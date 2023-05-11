using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float verticalBoundary;
    public float horizontalBoundary;
    [SerializeField] GameObject explosionVFX;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.gameObject.tag == "Enemy")
        {
            Camera cam = GameObject.Find("Camera").GetComponent<Camera>();
            Destroy(gameObject);
            cam.GetComponent<Shake>().StartShake();
            GameObject explosion = Instantiate(explosionVFX);
            explosion.transform.position = transform.position;
            Destroy(explosion, 0.75f);
        }
    }
    void Update() 
    {
        // Screen wrapping. If the player character goes beyond a defined boundary, change its coordinated to the opposite boundary
        if ((transform.position.y > verticalBoundary) ^ (transform.position.y < -(verticalBoundary)) ^ (transform.position.x < -(horizontalBoundary)) ^ (transform.position.x > horizontalBoundary))
        {
            Destroy(gameObject);
        }
    }
}

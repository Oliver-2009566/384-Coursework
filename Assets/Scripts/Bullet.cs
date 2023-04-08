using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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
}

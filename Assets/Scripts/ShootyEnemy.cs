using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyEnemy : MonoBehaviour
{
    
    private float verticalBoundary = 10f;
    private float horizontalBoundary = 10f;
    public float speedX;
    public float speedY;
    private bool vulnerable = false;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 1f;

    // Start is called before the first frame update
    void Start() 
    {   
        StartCoroutine(makeVulnerable());
        speedX = Random.Range(-2f, 2f);
        speedY = Random.Range(-2f, 2f);

        Vector3 movement = new Vector3(speedX, speedY, 0);

        movement *= Time.deltaTime;

        Vector2 direction = transform.position + movement;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, angle);

        StartCoroutine(Shoot());
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

    IEnumerator Shoot()
    {
        int shot = 0;
        while(true)
        {
            shot = Random.Range(0, 3);
            if (shot == 0)
            {
                StartCoroutine(ForwardBurst());
                yield return new WaitForSeconds(4f);
            }
            else if (shot == 1)
            {
                StartCoroutine(Whirlwind());
                yield return new WaitForSeconds(10f);
            }

        } 
    }




    IEnumerator ForwardBurst()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
            bullet.GetComponent<EnemyBullet>().verticalBoundary = verticalBoundary;
            bullet.GetComponent<EnemyBullet>().horizontalBoundary = horizontalBoundary;
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator Whirlwind()
    {
        for (int i = 0; i < 50; i++)
        {
            for (int a = 0; a < 20; a++)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
                bullet.GetComponent<EnemyBullet>().verticalBoundary = verticalBoundary;
                bullet.GetComponent<EnemyBullet>().horizontalBoundary = horizontalBoundary;
                transform.Rotate(new Vector3(0, 0, 18));
            }
            transform.Rotate(new Vector3(0, 0, 3));
            yield return new WaitForSeconds(0.05f);
        }
    }


}

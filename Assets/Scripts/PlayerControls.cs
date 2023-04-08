using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int fireRate = 10;
    public Rigidbody2D rb;
    public float verticalBoundary =  5.5f;
    public float horizontalBoundary = 10.65f;

    Vector2 moveDirection;
    Vector2 mousePosition;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;
    private bool shot = false;


    // Update is called once per frame
    void Update()
    {   
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if(Input.GetMouseButton(0))
        {
            StartCoroutine(fireBullet());
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    IEnumerator fireBullet()
    {
        if(shot == false)
        {
            shot = true;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(1f / (float)fireRate);
            shot = false;
        }
        
    }
}

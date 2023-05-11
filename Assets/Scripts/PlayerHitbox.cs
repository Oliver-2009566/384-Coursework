using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("DEATH?");
        if(collision.transform.gameObject.tag == "Enemy")
        {
            Debug.Log("DEATH");
            GetComponentInParent<Player>().Death();
        }   
    }
}  


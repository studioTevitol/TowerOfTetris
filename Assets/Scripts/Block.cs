using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //neden böyle bir component yarattım hatırlamıyorum ama kodun her yerinde olduğu için çıkarmaya üşendim. 
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.localPosition = new Vector3(transform.position.x, transform.position.y, 1);
        }
        collision.GetComponent<Player>().SetCurrentBlock(this);
    }*/
}

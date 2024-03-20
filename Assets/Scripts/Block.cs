using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    GameObject Player;
    void Start()
    {
        //Player = FindObjectOfType<Player>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 ptransform = collision.gameObject.transform.position;
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.localPosition = new Vector2(ptransform.x, ptransform.y + 0.64f);
        }
    }
}

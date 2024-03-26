using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.localPosition = new Vector3(transform.position.x, transform.position.y, 1);
        }
        collision.GetComponent<Player>().SetCurrentBlock(gameObject.GetComponent<Block>());
    }
}

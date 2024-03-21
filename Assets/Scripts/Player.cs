using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Tetromino attachedTetromino;
    Rigidbody2D rigidbody;
    Block currentBlock = null;
    public float jumpForce = 5f;
    void Start()
    {
        attachedTetromino = null;
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.isKinematic = false;
            rigidbody.AddForce(new Vector2(0, jumpForce));
        }
        if (Input.GetKeyDown(KeyCode.W)) Move(new Vector2(0, 1)); 
        else if (Input.GetKeyDown(KeyCode.S)) Move(new Vector2(0, -1));
        else if (Input.GetKeyDown(KeyCode.A)) Move(new Vector2(-1, 0));
        else if (Input.GetKeyDown(KeyCode.D)) Move(new Vector2(1, 0));

        //Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        attachedTetromino = collision.gameObject.GetComponentInParent<Tetromino>();
        rigidbody.isKinematic=true;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        attachedTetromino = null;
        currentBlock = null;
    }
    void Move(Vector2 direction)
    {
        Debug.Log(direction.x + ", " + direction.y);
        int currentblockIndexX = 0;
        int currentblockIndexY = 0;
        if (attachedTetromino != null)
        {
            for (int i = 0; i < attachedTetromino.structure.Count; i++)
            {
                if (attachedTetromino.structure[i] == null || attachedTetromino.structure[i].Contains(currentBlock) != true) continue;
                int currentBlockIndexX = attachedTetromino.structure[i].IndexOf(currentBlock);
                int currentBlockIndexY = i;
                Debug.Log(currentBlockIndexX + ", " + currentBlockIndexY);

            }

            currentBlock = attachedTetromino.structure[(int)(currentblockIndexY + direction.y)][(int)(currentblockIndexX + direction.x)];
        }
        
    }
    public void SetCurrentBlock(Block block)
    {
        currentBlock = block;
        attachedTetromino = block.GetComponentInParent<Tetromino>();
        attachedTetromino.SetPlayer(this);
    }
    public Block GetCurrentBlock()
    {
        return currentBlock;
    }
}

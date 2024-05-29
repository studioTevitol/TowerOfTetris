using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    Tetromino attachedTetromino;
    Rigidbody2D Prigidbody;
    Block currentBlock = null;
    GameObject game;
    public float horizontalSpeed = 3f;
    public float jumpForce = 5f;
    public bool isGrounded = false;
    
    void Start()
    {
        attachedTetromino = null;
        Prigidbody = GetComponent<Rigidbody2D>();
        Prigidbody.isKinematic = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        //hold space to jump, press space to release. charge up jump by holding space
        if (Input.GetKeyDown(KeyCode.Space)&&isGrounded)
        {
            currentBlock = null;
            attachedTetromino = null;
            isGrounded = false;
            Prigidbody.isKinematic = false;
            Prigidbody.AddForce(new Vector2(0, jumpForce));
        }
        if (!Prigidbody.isKinematic) Prigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * horizontalSpeed, Prigidbody.velocity.y);

        if (Input.GetKeyDown(KeyCode.W)) Move(new Vector2(0, -1)); 
        else if (Input.GetKeyDown(KeyCode.S)) Move(new Vector2(0, 1));
        else if (Input.GetKeyDown(KeyCode.A)) Move(new Vector2(-1, 0));
        else if (Input.GetKeyDown(KeyCode.D)) Move(new Vector2(1, 0));    
    }


    //collisiona girince ba�l� olunan tetrominoyu ayarla
    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentBlock = collision.GetComponent<Block>();
        if (currentBlock!= null) {
        isGrounded = true;
        transform.localPosition = new Vector3(currentBlock.transform.position.x, currentBlock.transform.position.y, 1);
        attachedTetromino = currentBlock.GetComponentInParent<Tetromino>();
        Prigidbody.isKinematic = true;
        Prigidbody.velocity = Vector2.zero;
        attachedTetromino.SetPlayer(this);
        }
    }
    void Move(Vector2 direction)
    {
        Debug.Log(direction.x + ", " + direction.y);
        int currentBlockIndexX = -1;
        int currentBlockIndexY = -1;
        if (attachedTetromino != null)
        {
            for (int i = 0; i < attachedTetromino.structure.Count; i++)
            {
                if (attachedTetromino.structure[i] == null || !attachedTetromino.structure[i].Contains(currentBlock)) continue;
                currentBlockIndexX = attachedTetromino.structure[i].IndexOf(currentBlock);
                currentBlockIndexY = i;
                Debug.Log("index: " + currentBlockIndexX + ", rank: " + currentBlockIndexY);
                Debug.Log("Findex: " + (int)(currentBlockIndexX + direction.x) + ", Frank: " + (int)(currentBlockIndexY + direction.y));
                break;
            }
            if (currentBlockIndexX != -1 && currentBlockIndexY != -1)
            {
                SetCurrentBlock(attachedTetromino.structure[(int)(currentBlockIndexY + direction.y)][(int)(currentBlockIndexX + direction.x)]);
                transform.localPosition = new Vector3(currentBlock.transform.position.x, currentBlock.transform.position.y, 1);
            }
        }
        direction = new Vector2(0, 0);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image radialBar;
    
    [Header("Movement Settings")]
    [SerializeField] private float horizontalSpeed = 3f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float maxChargeTime = 0f;
    
    private Tetromino attachedTetromino;
    private Rigidbody2D Prigidbody;
    private Block currentBlock = null;
    private GameObject game;
    private bool isGrounded = false;

    // Initialize Player
    private void Start()
    {
        attachedTetromino = null;
        Prigidbody = GetComponent<Rigidbody2D>();
        Prigidbody.isKinematic = true;
    }

    private void Update()
    {
        // Hold space to jump, press space to release. Charge up jump by holding space
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            StartCoroutine(ChargeJump());
        }
        
        if (!Prigidbody.isKinematic)
        {
            Prigidbody.velocity = new Vector2(Input.GetAxis("Horizontal") * horizontalSpeed, Prigidbody.velocity.y);
        }

        // Move character in tetromino with WASD
        if (Input.GetKeyDown(KeyCode.W))
        {
            Move(new Vector2(0, -1));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Move(new Vector2(0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Move(new Vector2(-1, 0));
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Move(new Vector2(1, 0));
        }

        if (currentBlock == null)
        {
            isGrounded = false;
        }

        // Update UI
        radialBar.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.5f, 0.5f, 0));
    }

    // Set attachedTetromino and currentBlock to the block that the player is standing on
    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentBlock = collision.GetComponent<Block>();
        
        if (currentBlock != null)
        {
            isGrounded = true;
            transform.localPosition = new Vector3(currentBlock.transform.position.x, currentBlock.transform.position.y, 1);
            attachedTetromino = currentBlock.GetComponentInParent<Tetromino>();
            Prigidbody.isKinematic = true;
            Prigidbody.velocity = Vector2.zero;
            attachedTetromino.SetPlayer(this);
        }
    }

    private void Move(Vector2 direction)
    {
        int currentBlockIndexX = -1;
        int currentBlockIndexY = -1;

        if (attachedTetromino != null)
        {
            for (int i = 0; i < attachedTetromino.structure.Count; i++)
            {
                if (attachedTetromino.structure[i] == null || !attachedTetromino.structure[i].Contains(currentBlock))
                {
                    continue;
                }

                currentBlockIndexX = attachedTetromino.structure[i].IndexOf(currentBlock);
                currentBlockIndexY = i;
                break;
            }

            if (currentBlockIndexX != -1 && currentBlockIndexY != -1 && attachedTetromino.structure[(int)(currentBlockIndexY + direction.y)][(int)(currentBlockIndexX + direction.x)] != null)
            {
                SetCurrentBlock(attachedTetromino.structure[(int)(currentBlockIndexY + direction.y)][(int)(currentBlockIndexX + direction.x)]);
                transform.localPosition = new Vector3(currentBlock.transform.position.x, currentBlock.transform.position.y, 1);
            }
        }

        direction = Vector2.zero;
    }

    private IEnumerator ChargeJump()
    {
        float chargeTime = 0;

        while (Input.GetKey(KeyCode.Space) && chargeTime < maxChargeTime)
        {
            chargeTime += Time.deltaTime;
            // Update charging bar UI
            radialBar.fillAmount = 1 - chargeTime / maxChargeTime;
            yield return null;
        }

        while (Input.GetKey(KeyCode.Space))
        {
            yield return null;
        }

        currentBlock = null;
        attachedTetromino = null;
        isGrounded = false;
        Prigidbody.isKinematic = false;
        float jumpForceMultiplier = chargeTime / maxChargeTime; // Adjust this formula to change the jump force based on charge time
        Prigidbody.AddForce(new Vector2(0, jumpForce * jumpForceMultiplier));
    }

    public void SetCurrentBlock(Block block)
    {
        currentBlock = block;

        if (block != null)
        {
            attachedTetromino = block.GetComponentInParent<Tetromino>();
        }

        attachedTetromino.SetPlayer(this);
    }

    public Block GetCurrentBlock()
    {
        return currentBlock;
    }
}

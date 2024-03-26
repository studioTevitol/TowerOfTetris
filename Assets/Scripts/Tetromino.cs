using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    [SerializeField]
    public List<List<Block>> structure;
    //public Block[][] structure;
    public Block[] blocks;
    public Player player;
    enum blockType
    {
        I,
        J,
        L,
        S,
        Z,
        T,
        O
    }
    [SerializeField] 
    blockType blocktype;

    // Start is called before the first frame update
    void Start()
    {
        blocks = GetComponentsInChildren<Block>();
        /*if (gameObject.name=="BlockI")
            structure = new Block[][]
            {
            new Block[] { blocks[3] },
            new Block[] { blocks[2] },
            new Block[] { blocks[1] },
            new Block[] { blocks[0] }
            };
        else if (gameObject.name=="BlockJ")
            structure = new Block[][]
            {
            new Block[] { null, blocks[3] },
            new Block[] { null, blocks[2] },
            new Block[] { blocks[0], blocks[1] },
            };
        else if (gameObject.name == "BlockL")
            structure = new Block[][]
            {
            new Block[] { blocks[3], null },
            new Block[] { blocks[2], null },
            new Block[] { blocks[1], blocks[0] },
            };
        else if (gameObject.name == "BlockS")
            structure = new Block[][]
            {
            new Block[] { null, blocks[2], blocks[3] },
            new Block[] { blocks[0], blocks[1], null },
            };
        else if (gameObject.name == "BlockZ")
            structure = new Block[][]
            {
            new Block[] { blocks[3], blocks[2], null },
            new Block[] { null, blocks[1], blocks[0]},
            };
        else if (gameObject.name == "BlockT")
            structure = new Block[][]
            {
            new Block[] { blocks[2], blocks[1], blocks[0] },
            new Block[] { null, blocks[3], null },

            };
        else if (gameObject.name == "BlockO")
            structure = new Block[][]
            {
            new Block[] { blocks[1], blocks[3] },
            new Block[] { blocks[0], blocks[2] },
            };
        */
        if (blocktype==blockType.I)
            structure = new List<List<Block>>
            {
            new List<Block> { blocks[3] },
            new List < Block > { blocks[2] },
            new List < Block > { blocks[1] },
            new List < Block > { blocks[0] }
            };
        else if (blocktype == blockType.J)
            structure = new List<List<Block>>
            {
            new List<Block> { null, blocks[3] },
            new List < Block > { null, blocks[2] },
            new List<Block> { blocks[0], blocks[1] },
            };
        else if (blocktype == blockType.L)
            structure = new List<List<Block>>         
            {
            new List<Block> { blocks[3], null },
            new List<Block> { blocks[2], null },          
            new List<Block> { blocks[1], blocks[0] }
            };
        else if (blocktype == blockType.S)
            structure = new List<List<Block>>
            {
            new List<Block> { null, blocks[2], blocks[3] },
            new List<Block> { blocks[0], blocks[1], null },
            };
        else if (blocktype == blockType.Z)
            structure = new List<List<Block>>         
            {
            new List<Block> { blocks[3], blocks[2], null },
            new List<Block> { null, blocks[1], blocks[0]},
            };
        else if (blocktype == blockType.T)
            structure = new List<List<Block>>
            {
            new List<Block> { blocks[2], blocks[1], blocks[0] },
            new List<Block> { null, blocks[3], null },

            };
        else if (blocktype == blockType.O)
            structure = new List<List<Block>>
            {
            new List<Block> { blocks[1], blocks[3] },
            new List<Block> { blocks[0], blocks[2] },
            };
    }

    public void MoveDown(float SlotWidth)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - SlotWidth, transform.position.z);
        //Debug.Log(player.GetCurrentBlock() == null);
        if (player != null&&player.GetCurrentBlock()!=null) player.transform.localPosition = new Vector3(player.GetCurrentBlock().transform.position.x, player.GetCurrentBlock().transform.position.y, 1);
    }
    public void SetPlayer(Player player)
    {
        this.player = player;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MoveDown(float SlotWidth)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - SlotWidth, transform.position.z);
    }
}

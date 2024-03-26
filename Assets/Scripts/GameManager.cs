using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Tetromino> activeTetrominos = new List<Tetromino>();
    public GameObject[] tetrominoPrefabs;
    public Transform spawnArea;
    [SerializeField]
    float TickSpeed = 1.0f;
    [SerializeField]
    float moveAmount = 0.64f;
    [SerializeField]
    public float spawnFrequency = 2.5f;
    


    void Start()
    {
        activeTetrominos.Add(Instantiate(tetrominoPrefabs[(int)Mathf.Floor(Random.Range(0, tetrominoPrefabs.Length))], spawnArea.position, new Quaternion()).GetComponent<Tetromino>());
        StartCoroutine("TickCycle", TickSpeed);
    }

    
  
       
    
    IEnumerator TickCycle(float TickSpeed)
    {
        int spawnTick = (int)Mathf.Round(spawnFrequency / TickSpeed);

        while (true)
        {
            for (int i = 0; i < activeTetrominos.Count; i++) 
            {
                activeTetrominos[i].MoveDown(moveAmount);
            }
            if (spawnTick == 0)
            {
                activeTetrominos.Add(Instantiate(tetrominoPrefabs[(int)Mathf.Floor(Random.Range(0, tetrominoPrefabs.Length))], new Vector3(Mathf.Round(Random.Range(spawnArea.position.x - 5, spawnArea.position.x + 5)) * 0.64f, spawnArea.position.y, spawnArea.position.z), new Quaternion()).GetComponent<Tetromino>());
                spawnTick = (int)Mathf.Round(spawnFrequency / TickSpeed);
                
            }
            spawnTick--;

            yield return new WaitForSeconds(TickSpeed);
           
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Tetromino> activeTetrominos = new List<Tetromino>();
    public GameObject[] tetrominoPrefabs;
    public Transform spawnArea;

    void Start()
    {
        activeTetrominos.Add(Instantiate(tetrominoPrefabs[(int)Mathf.Floor(Random.Range(0, tetrominoPrefabs.Length))], spawnArea.position, new Quaternion()).GetComponent<Tetromino>());
        StartCoroutine("TickCycle", 1f);
    }

    
  
       
    
    IEnumerator TickCycle(float TickSpeed)
    {
        int spawnTick = 5;

        while (true)
        {
            for (int i = 0; i < activeTetrominos.Count; i++)
            {
                activeTetrominos[i].MoveDown(0.64f);
            }
            if (spawnTick == 0)
            {
                activeTetrominos.Add(Instantiate(tetrominoPrefabs[(int)Mathf.Floor(Random.Range(0, tetrominoPrefabs.Length))], new Vector3(Mathf.Round(Random.Range(spawnArea.position.x - 5, spawnArea.position.x + 5)) * 0.64f, spawnArea.position.y, spawnArea.position.z), new Quaternion()).GetComponent<Tetromino>());
                spawnTick = 5;
                
            }
            spawnTick--;

            yield return new WaitForSeconds(TickSpeed);
           
        }
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : MonoBehaviour
{
    private GameObject player;
    public GameObject[] enemyPrefab;
    public Transform point1;
    public Transform point2;
    public Gamepanel gamepanel;
    public int enemyGenerateAll = 6;
    
    private Vector3 _point1OriginalOffset;
    private Vector3 _point2OriginalOffset;
    public GameObject[] enemys;
    
    void Start()
    {
        player = GameManager.Instance.GetGenPlayer();
        _point1OriginalOffset = point1.position - player.transform.position;
        _point2OriginalOffset = point2.position - player.transform.position;
    }
    
    public int enemycount;
    void Update()
    {
        int indexmax = enemycount > enemyPrefab.Length ? enemyPrefab.Length : enemycount;
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemycount <= enemyGenerateAll)
        {
            if (enemies.Length == 0)
            {
                for (int i = 0; i < enemycount; i++)
                {
                    GenerateEnemy(indexmax);
                    gamepanel.waveText.text = "第"+enemycount+"/"+""+enemyGenerateAll+"波";
                }
                enemycount++;
            }
            
        }
        
        Vector3 playerPos = player.transform.position;
        point1.position = playerPos + _point1OriginalOffset; 
        point2.position = playerPos + _point2OriginalOffset;  
        
    }

    void GenerateEnemy(int indexmax)
    {
        int index = Random.Range(0, indexmax);
        int rx = (int)Random.Range(point1.transform.position.x, point2.transform.position.x);
        int rz = (int)Random.Range(point1.transform.position.z, point2.transform.position.z);
        GameObject obj =  Instantiate(enemyPrefab[index], new Vector3(rx, 0, rz), enemyPrefab[index].transform.rotation);
        obj.GetComponent<Enemy>().player = player;
        
    }

}

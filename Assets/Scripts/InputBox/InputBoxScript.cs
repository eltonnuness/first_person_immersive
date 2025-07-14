using System.Collections;
using UnityEngine;

public class InputBoxScript : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private GameObject[] prefabSpawnList;

    void Start()
    {
        StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine()
    {
        if (prefabSpawnList.Length != 0)
        {
            for (int i = 0; i < 10; i++)
            {
                Instantiate(prefabSpawnList[0], spawnPosition.position, spawnPosition.rotation);
                // Wait for 1 second
                yield return new WaitForSeconds(1);
            }
        }
    }

    void Update()
    {
        
    }
}

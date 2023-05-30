using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlatformPool : MonoBehaviour
{

    [SerializeField] private float platformSpacing = 6f;

    [SerializeField] private GameObject[] platformPrefabs;

    [SerializeField] private float currentXSpawnPosition = -12f;

    [SerializeField] private bool collectionChecks = true;

    [SerializeField] private int maxPoolSize = 15;


    public IObjectPool<GameObject> m_pool { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        m_pool = new ObjectPool<GameObject>(CreatePlatform, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);

        for(int i = 0; i < maxPoolSize; i++)
        {
            SpawnPlatform();
        }
    }

    public void SpawnPlatform() => m_pool.Get();

    public void ReleasePlatform(GameObject platform) => m_pool.Release(platform);
    private GameObject CreatePlatform()
    {
        int randomIndex = Random.Range(0, platformPrefabs.Length);
        GameObject platformPrefab = platformPrefabs[randomIndex];

        GameObject platform = Instantiate(platformPrefab, transform);
        platform.transform.position = new Vector3(currentXSpawnPosition, 0, 0);
        platform.SetActive(false);

        currentXSpawnPosition += platformSpacing;

        if(currentXSpawnPosition >= maxPoolSize * platformSpacing)
        {
            currentXSpawnPosition = -12f;
        }

        return platform;
    }

    private void OnReturnedToPool(GameObject platform) => platform.gameObject.SetActive(false);

    private void OnTakeFromPool(GameObject platform)
    {
        platform.transform.position = new Vector3(currentXSpawnPosition, 0, 0);

        platform.gameObject.SetActive(true);
    }


    private void OnDestroyPoolObject(GameObject platform) => Destroy(platform);
}

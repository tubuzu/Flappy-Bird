using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    private float[] spawnRate = new float[] { 1.4f, 1.3f, 1.2f, 1.1f, 1f };
    private float[] maxHeight = new float[] { 0.8f, 1.3f, 1.8f, 2.3f, 2.8f };
    private float[] minHeight = new float[] { 0f, -0.5f, -1f, -1.5f, -2f };

    private int totalLevel = 5;
    private int currentLevel = 0;
    public float nextLevelTime = 5f;

    private void OnEnable()
    {
        StartCoroutine(SpawnCoroutine());
        InvokeRepeating(nameof(IncreaseLevel), nextLevelTime, nextLevelTime);
    }

    private void OnDisable()
    {
        StopCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate[currentLevel]);

            GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);
            pipes.transform.SetParent(transform);
            pipes.transform.position += Vector3.up * Random.Range(minHeight[currentLevel], maxHeight[currentLevel]);
        }
    }

    public void ResetState()
    {
        currentLevel = 0;
        foreach (Transform t in this.transform)
        {
            Destroy(t.gameObject);
        }
    }

    // private void Spawn()
    // {
    //     GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);
    //     pipes.transform.position += Vector3.up * Random.Range(minHeight[currentLevel], maxHeight[currentLevel]);
    // }

    private void IncreaseLevel()
    {
        currentLevel++;
        if (currentLevel >= totalLevel)
        {
            currentLevel = totalLevel - 1;
            CancelInvoke(nameof(IncreaseLevel));
        }
    }
}

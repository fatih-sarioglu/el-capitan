using System.Collections;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    public GameObject spawnee;

    public Transform mainCameraParent;

    public float rate;

    public bool changeRate = false;

    void Start()
    {
        StartCoroutine(SpawnRocks(rate));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, (mainCameraParent.position.z + 53));

        if (changeRate)
        {
            StopAllCoroutines();
            StartCoroutine(SpawnRocks(rate));

            changeRate = false;
        }
    }

    IEnumerator SpawnRocks(float rate)
    {
        yield return new WaitForSeconds(5f);

        while (true)
        {
            Vector3 randomPos = new Vector3(Random.Range(-11.4f, 11.4f), 0.85f, this.transform.position.z);

            float randomScale = Random.Range(65f, 80f);

            GameObject rockObject = Instantiate(spawnee, randomPos, transform.rotation);
            rockObject.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            yield return new WaitForSeconds(rate);
        }
    }
}

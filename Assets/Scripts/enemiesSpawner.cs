using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemiesSpawner : MonoBehaviour
{
    public GameObject enemies;
    public GameObject mage;
    // Start is called before the first frame update
    void Start()
    {
        mage = GameObject.FindGameObjectWithTag("Player").gameObject;
        StartCoroutine(spawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator spawnEnemies()
    {
        while (true)
        {
            if (!mage.activeSelf)
            {
                yield break;
            }

            yield return new WaitForSeconds(7);

            if (mage.activeSelf)
            {
                float randomX = Random.Range(-15f, 15f);
                float randomZ = Random.Range(-15f, 15f);

                Vector3 randomPosition = new Vector3(randomX, 1f, randomZ);

                Instantiate(enemies, randomPosition, Quaternion.identity);
            }
        }
    }
}

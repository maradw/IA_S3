using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToySpawner : MonoBehaviour
{
    public GameObject toyPrefab;
    public BoxCollider spawnArea;
    public int totalToys = 10;
    public float spawnDelay = 1f;        // Tiempo entre spawns
    public float relocateInterval = 5f;  // Cada cuánto tiempo recolocar
    public float relocateDelay = 0.5f;   // Tiempo entre cada recolocación

    private List<GameObject> toys = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnToysGradually());
    }

    IEnumerator SpawnToysGradually()
    {
        for (int i = 0; i < totalToys; i++)
        {
            Vector3 position = GetRandomPositionInArea();
            GameObject newToy = Instantiate(toyPrefab, position, Quaternion.identity);
            toys.Add(newToy);

            Toy toyScript = newToy.GetComponent<Toy>();
            string[] types = { "Ball", "Doll", "Blocks" };
            Color[] colors = { Color.red, Color.blue, Color.green };
            int index = Random.Range(0, types.Length);

            toyScript.Initialize(types[index], colors[index], Random.Range(1, 10));

            yield return new WaitForSeconds(spawnDelay);
        }

        // Iniciar recolocación luego de que todos se hayan creado
        StartCoroutine(RelocateToysOneByOne());
    }

    IEnumerator RelocateToysOneByOne()
    {
        while (true)
        {
            yield return new WaitForSeconds(relocateInterval);

            for (int i = 0; i < toys.Count; i++)
            {
                if (toys[i] != null)
                {
                    Vector3 newPosition = GetRandomPositionInArea();
                    toys[i].transform.position = newPosition;
                }

                yield return new WaitForSeconds(relocateDelay);
            }
        }
    }

    Vector3 GetRandomPositionInArea()
    {
        Vector3 center = spawnArea.bounds.center;
        Vector3 size = spawnArea.bounds.size;

        float x = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float z = Random.Range(center.z - size.z / 2, center.z + size.z / 2);
        float y = spawnArea.transform.position.y + 0.5f;

        return new Vector3(x, y, z);
    }
}

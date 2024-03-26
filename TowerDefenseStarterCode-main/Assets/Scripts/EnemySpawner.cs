using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    public List<Transform> Path1 = new List<Transform>();
    public List<Transform> Path2 = new List<Transform>();
    public List<GameObject> Enemies = new List<GameObject>();

    private int ufoCounter = 0; // Nieuwe variabele toegevoegd

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public Transform RequestTarget(Enums.Path path, int index)
    {
        List<Transform> selectedPath;

        if (path == Enums.Path.Path1)
            selectedPath = Path1;
        else if (path == Enums.Path.Path2)
            selectedPath = Path2;
        else
        {
            Debug.LogError("Invalid path specified!");
            return null;
        }

        if (index >= 0 && index < selectedPath.Count)
            return selectedPath[index];
        else
        {
            Debug.LogError("Invalid index specified!");
            return null;
        }
    }

    private void SpawnEnemy(int type, Enums.Path path)
    {
        GameObject enemy = Instantiate(Enemies[type], transform.position, Quaternion.identity);
        GameManager.instance.AddInGameEnemy();
    }

    private void SpawnEnemyAtPosition(int type, Enums.Path path) // Hernoemde functie toegevoegd
    {
        Vector3 spawnPosition;
        Quaternion spawnRotation;

        if (path == Enums.Path.Path1)
        {
            spawnPosition = Path1[0].position;
            spawnRotation = Path1[0].rotation;
        }
        else if (path == Enums.Path.Path2)
        {
            spawnPosition = Path2[0].position;
            spawnRotation = Path2[0].rotation;
        }
        else
        {
            spawnPosition = Vector3.zero;
            spawnRotation = Quaternion.identity;
            Debug.LogError("Invalid path specified!");
            return;
        }

        GameObject newEnemy = Instantiate(Enemies[type], spawnPosition, spawnRotation);

        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.path = path;
            enemyScript.target = RequestTarget(path, 1);
        }
        else
        {
            Debug.LogError("Enemy component not found on spawned enemy!");
        }

        GameManager.instance.AddInGameEnemy(); // Counter verhogen bij het spawnen van een vijand
    }

    void Start()
    {
        InvokeRepeating("SpawnTester", 1f, 1f);
    }

    public void StartWave(int number) // Nieuwe functie toegevoegd
    {
        ufoCounter = 0;

        switch (number)
        {
            case 1:
                InvokeRepeating("StartWave1", 1f, 1.5f);
                break;
                // Voeg meer golven toe zoals nodig ...
        }
    }

    public void StartWave1() // Nieuwe functie toegevoegd
    {
        ufoCounter++;

        // Implementeer golflgica hier ...

        if (ufoCounter > 30)
        {
            CancelInvoke("StartWave1");
            GameManager.instance.EndWave();
        }
    }

    private void SpawnTester()
    {
        SpawnEnemyAtPosition(0, Enums.Path.Path1);
    }
}

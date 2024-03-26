using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    public float health = 10f;
    public int points = 1;
    public Enums.Path path { get; set; }
    public Transform target { get; set; }
    private int pathIndex = 1;

    void Update()
    {
        if (target != null) // Controleer of target niet null is voordat je het gebruikt
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                target = EnemySpawner.instance.RequestTarget(path, pathIndex);
                pathIndex++;

                if (target == null)
                {
                    ReachEnd();
                }
            }
        }
    }

    public void Damage(int damage)
    {
        // Verminder de gezondheidswaarde
        health -= damage;

        // Als de gezondheid kleiner is dan of gelijk aan nul, vernietig dan het gameobject
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Verminder het aantal vijanden in het spel
        GameManager.instance.RemoveInGameEnemy();

        // Voeg credits toe aan de speler
        GameManager.instance.AddCredits(points);

        // Vernietig dit vijandelijke object
        Destroy(gameObject);
    }

    void ReachEnd()
    {
        // Als de vijand het einde van het pad bereikt, val dan de poort aan
        GameManager.instance.AttackGate();

        // Vernietig dit vijandelijke object
        Destroy(gameObject);
    }
}

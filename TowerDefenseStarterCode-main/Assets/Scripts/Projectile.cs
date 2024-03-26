using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public float speed;
    public int damage;

    void Start()
    {
        RotateTowardsTarget();
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        MoveTowardsTarget();

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            DealDamage();
            Destroy(gameObject);
        }
        // Beweeg het projectiel naar het doelwit
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Controleer of het projectiel het doelwit heeft bereikt
        if (Vector2.Distance(transform.position, target.position) < 0.2f)
        {
            // Breng schade toe aan het doelwit
            target.GetComponent<Enemy>().Damage(damage);

            // Vernietig dit projectiel
            Destroy(gameObject);
        }
    }

    void RotateTowardsTarget()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    void DealDamage()
    {
        if (target.GetComponent<Enemy>() != null)
        {
            Enemy enemy = target.GetComponent<Enemy>();
            enemy.health -= damage;
            Debug.Log("Dealt " + damage + " damage to the enemy!");
        }
    }
}
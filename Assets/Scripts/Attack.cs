using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyMeleeController enemy = collision.GetComponent<EnemyMeleeController>();

        PlayerController player = collision.GetComponent<PlayerController>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}

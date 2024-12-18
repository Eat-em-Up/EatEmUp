using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ao colidir, salva na vari�vel enemy, o inimigo que foi colidido
        EnemyMeleeController enemy = collision.GetComponent<EnemyMeleeController>();

        //Ao colidir, salva na vari�vel player, o player que foi colidido
        PlayerController player = collision.GetComponent<PlayerController>();

        //Se a colis�o foi um inimigo
        if (enemy != null)
        {
            //Inimigo recebe dano
            enemy.TakeDamage(damage);
        }

        //Se a colis�o foi com um player
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}

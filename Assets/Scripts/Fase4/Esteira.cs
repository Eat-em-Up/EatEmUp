using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Esteira : MonoBehaviour
{

    // Força com que a esteira puxa o player
    public float pullForce = 2f; // Ajuste a força da esteira no editor
    public Vector2 pullDirection = new Vector2(-1, 0); // Direção padrão (puxar para trás)
    private Vector2 originalVelocity;

    void Start()
    {

    }

    private void Update()
    {
        
    }

    //[System.Obsolete]
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    PlayerController playerController = collision.GetComponent<PlayerController>();

    //    if (playerController != null)
    //    {
    //        // Armazena a velocidade original antes de entrar na esteira
    //        originalVelocity = playerController.playerRigidBody.velocity;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController = collision.GetComponent<PlayerController>();

        if (playerController != null)
        {
            // Marca que o Player está na esteira
            playerController.isOnConveyor = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Detecta se o objeto em contato é o player
        PlayerController playerController = collision.GetComponent<PlayerController>();

        EnemyMeleeController enemyController = collision.GetComponent<EnemyMeleeController>();


        if (playerController != null)
        {
            // Movimenta o player na direção da esteira sem interferir nos controles
            Vector2 esteiraMovement = pullDirection.normalized * pullForce;
            playerController.playerRigidBody.linearVelocity = new Vector2(
                playerController.playerDirection.x * playerController.currentSpeed + esteiraMovement.x,
                playerController.playerDirection.y * playerController.currentSpeed + esteiraMovement.y
            );
            playerController.playerSpeed = 3f;
        }

        
        if (enemyController != null)
        {
            Vector2 esteiraMovement = pullDirection.normalized * pullForce;
            enemyController.rb.linearVelocity = new Vector2(
                 enemyController.targetDistance.x * enemyController.enemySpeed + esteiraMovement.x,
                 enemyController.targetDistance.y * enemyController.enemySpeed + esteiraMovement.y
            );
            enemyController.enemySpeed = 1f;
        }

    }

    [System.Obsolete]
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Quando o player sai da esteira, reseta o movimento
        PlayerController playerController = collision.GetComponent<PlayerController>();

        if (playerController != null)
        {
            // Reseta a velocidade residual da esteira
            playerController.playerRigidBody.velocity = Vector2.zero;

            // Retoma o movimento normal do Player
            playerController.currentSpeed = playerController.playerSpeed;

            playerController.isOnConveyor = false;
        }
    }
}
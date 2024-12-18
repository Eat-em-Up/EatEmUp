using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    public Rigidbody2D playerRigidBody;
    public float playerSpeed = 1f;
    public float currentSpeed;

    public Vector2 playerDirection;

    private bool isWalking;

    private Animator playerAnimator;

    //Player olhando para a direita
    private bool playerFacingRight = true;

    //Variável contadora
    private int PunchCount;

    //Tempo de Ataque
    private float timeCross = 0.75f;

    private bool comboControl;

    //Indicar se Player está morto
    private bool isDead;

    //Verifica se o Player está na Esteira
    public bool isOnConveyor = false;

    //Propriedades para UI
    public int maxHealth = 10;
    public int currentHealth;
    public Sprite playerImage;

    void Start()
    {
        //Obtem e inicializa as propriedades do RigidBody2D
        playerRigidBody = GetComponent<Rigidbody2D>();

        //Obtem e inicializa as propriedades do animator
        playerAnimator = GetComponent<Animator>();

        currentSpeed = playerSpeed;

        maxHealth = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

        UpdateAnimator();


        if (Input.GetKeyDown(KeyCode.X))
        {
            if (PunchCount < 2)
            {
                PlayerJab();
                PunchCount++;
                if (!comboControl)
                {
                    //Iniciar o temporizador
                    StartCoroutine(CrossController());
                }
            }
            else if (PunchCount >= 2)
            {
                PlayerCross();
                PunchCount = 0;
            }
            //Parando o temporizador
            StopCoroutine(CrossController());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

        }

    }

    //Fixed Update geralmente é utilizada para implementação de física no jogo
    //Por ter uma execução padronizada em diferentes dispositicos
    [System.Obsolete]
    private void FixedUpdate()
    {
        if (playerDirection != Vector2.zero)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        //Mudança de velocidade dependendo se o player está ou não na esteira
        if (isOnConveyor == true)
        {
            // Na esteira, combine o movimento normal com a velocidade da esteira
            Vector2 movement = playerDirection * currentSpeed * Time.fixedDeltaTime;
            playerRigidBody.velocity = movement + playerRigidBody.velocity;
        }
        else
        {
            // Fora da esteira, reseta o movimento apenas para o controle padrão
            playerRigidBody.linearVelocity = new Vector2(0, 0);
            playerRigidBody.MovePosition(playerRigidBody.position + currentSpeed * Time.fixedDeltaTime * playerDirection);
        }
    }

    void PlayerMove()
    {
        //Pega a entrada do jogador, e cria um Vector2 para usar no player Direction
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Se o player vai para a ESQUERDA e está olhando para a DIREITA
        if (playerDirection.x < 0 && playerFacingRight)
        {
            Flip();
        }

        else if (playerDirection.x > 0 && !playerFacingRight)
        {
            Flip();
        }
    }

    void UpdateAnimator()
    {
        playerAnimator.SetBool("isWalking"/*Declarado no animator*/, isWalking/*Declarado no VC*/);
    }

    void Flip()
    {
        //Vai girar o sprite player em 180° no eixo Y
        //Inverter o valor da variável
        playerFacingRight = !playerFacingRight;

        //Girar o sprite do player em 180° no eixo Y
        //               x,  y , z
        transform.Rotate(0, 180, 0);
    }

    void PlayerJab()
    {
        //Acessa animação do Jab
        //Ativa o gatilho de Ataque;
        playerAnimator.SetTrigger("isJabbing");
    }

    void PlayerCross()
    {
        //Acessa animação do Cross
        //Ativa o gatilho do Ataque;
        playerAnimator.SetTrigger("isCrossing");
    }

    IEnumerator CrossController()
    {
        comboControl = true;

        yield return new WaitForSeconds(timeCross);
        PunchCount = 0;

        comboControl = false;
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;
    }

    void ResetSpeed()
    {
        currentSpeed = playerSpeed;
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            playerAnimator.SetTrigger("HitDamage");
            FindFirstObjectByType<UIManager>().UpdatePlayerHealth(currentHealth);
        }
    }
}
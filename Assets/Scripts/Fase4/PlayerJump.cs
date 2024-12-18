using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 5f;

    [Tooltip("Layer no qual o player pode pular")]
    public LayerMask groundLayer;

    [Tooltip("Verificação de chão")]
    public Transform groundCheck; // Ponto de verificação (crie um empty object abaixo do player)

    public float groundCheckRadius = 0.2f; // Raio da verificação de chão

    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        // Obtém o Rigidbody do Player
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Verifica se o player está no chão
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Pular apenas se pressionar a barra de espaço e estiver no chão
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        // Aplica força para cima no Rigidbody
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // Reseta a velocidade vertical
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnDrawGizmos()
    {
        // Desenha uma esfera no editor para visualizar a verificação de chão
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}

using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 5f;

    [Tooltip("Layer no qual o player pode pular")]
    public LayerMask groundLayer;

    [Tooltip("Verifica��o de ch�o")]
    public Transform groundCheck; // Ponto de verifica��o (crie um empty object abaixo do player)

    public float groundCheckRadius = 0.2f; // Raio da verifica��o de ch�o

    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        // Obt�m o Rigidbody do Player
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Verifica se o player est� no ch�o
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        // Pular apenas se pressionar a barra de espa�o e estiver no ch�o
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        // Aplica for�a para cima no Rigidbody
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // Reseta a velocidade vertical
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnDrawGizmos()
    {
        // Desenha uma esfera no editor para visualizar a verifica��o de ch�o
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}

using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 5f;
    public float forcaPulo = 6f;

    public Transform cameraPlayer;

    private Rigidbody rb;
    private bool noChao;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && noChao)
        {
            rb.AddForce(Vector3.up * forcaPulo, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Pega a direção da câmera
        Vector3 frente = cameraPlayer.forward;
        Vector3 direita = cameraPlayer.right;

        // Impede que olhar para cima/baixo altere o movimento
        frente.y = 0f;
        direita.y = 0f;

        frente.Normalize();
        direita.Normalize();

        // Movimento baseado na direção da câmera
        Vector3 direcao = (frente * v + direita * h) * velocidade;

        direcao.y = rb.linearVelocity.y;

        rb.linearVelocity = direcao;
    }

    void OnCollisionStay(Collision colisao)
    {
        if (colisao.gameObject.CompareTag("Chao"))
        {
            noChao = true;
        }
    }

    void OnCollisionExit(Collision colisao)
    {
        if (colisao.gameObject.CompareTag("Chao"))
        {
            noChao = false;
        }
    }
}
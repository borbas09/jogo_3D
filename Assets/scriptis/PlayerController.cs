using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidadeMovimento = 6f;
    public float velocidadeCorrida = 10f;

    [Header("Rotação (Mouse)")]
    public float sensibilidadeMouse = 2f;

    [Header("Pulo e Gravidade")]
    public float alturaPulo = 1.5f;
    public float gravidade = -20f;

    private CharacterController controller;
    private Vector3 velocidade;
    private bool estaNoChao;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Trava e esconde o cursor (padrão pra jogos 3D com mouse look)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        RotacionarComMouse();
        Mover();
        AplicarGravidadeEPulo();
    }

    void RotacionarComMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadeMouse;
        transform.Rotate(Vector3.up * mouseX);
    }

    void Mover()
    {
        // Se a frente/trás estiver invertida, troque o sinal do Input.GetAxisRaw("Vertical")
        // Se a esquerda/direita estiver invertida, troque o sinal do Input.GetAxisRaw("Horizontal")
        float inputHorizontal = Input.GetAxisRaw("Horizontal");
        float inputVertical = Input.GetAxisRaw("Vertical");

        // Exemplo: Invertendo ambos para corrigir a direção
        Vector3 direcao = (-transform.right * inputHorizontal) + (-transform.forward * inputVertical);

        if (direcao.magnitude > 1f)
        {
            direcao.Normalize();
        }

        float velocidadeAtual = Input.GetKey(KeyCode.LeftShift) ? velocidadeCorrida : velocidadeMovimento;

        controller.Move(direcao * velocidadeAtual * Time.deltaTime);
    }

    void AplicarGravidadeEPulo()
    {
        estaNoChao = controller.isGrounded;

        if (estaNoChao && velocidade.y < 0)
            velocidade.y = -2f;

        if (Input.GetButtonDown("Jump") && estaNoChao)
        {
            velocidade.y = Mathf.Sqrt(alturaPulo * -2f * gravidade);
        }

        velocidade.y += gravidade * Time.deltaTime;
        controller.Move(velocidade * Time.deltaTime);
    }
}


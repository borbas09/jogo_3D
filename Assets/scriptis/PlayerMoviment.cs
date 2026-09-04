using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidade = 5f;
    public float alturaPulo = 2f;
    public float gravidade = -9.81f;

    CharacterController controller;
    Vector3 velocidadeY;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Movimento
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 movimento = transform.right * x + transform.forward * z;
        movimento.y = 0;

        controller.Move(movimento.normalized * velocidade * Time.deltaTime);

        // Gravidade
        if (controller.isGrounded && velocidadeY.y < 0)
        {
            velocidadeY.y = -2f;
        }

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            velocidadeY.y = Mathf.Sqrt(alturaPulo * -2f * gravidade);
        }

        // Aplicar gravidade
        velocidadeY.y += gravidade * Time.deltaTime;

        controller.Move(velocidadeY * Time.deltaTime);
    }
}
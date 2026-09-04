using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float sensibilidade = 200f;
    public Transform player;

    float rotacaoX = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidade * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidade * Time.deltaTime;

        // Olhar para cima e para baixo
        rotacaoX -= mouseY;
        rotacaoX = Mathf.Clamp(rotacaoX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotacaoX, 0, 0);

        // Girar o Player para esquerda e direita
        player.Rotate(Vector3.up * mouseX);
    }
}
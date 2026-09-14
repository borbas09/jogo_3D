using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensibilidade = 100f;
    public Transform player;

    [Header("Posições da Câmera")]
    public Vector3 offsetPrimeiraPessoa = new Vector3(0f, 0.6f, 0f); // Posição dos olhos
    public Vector3 offsetTerceiraPessoa = new Vector3(0f, 1.5f, -3f); // Atrás e acima do player

    private float rotacaoX = 0f;
    private bool ePrimeiraPessoa = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Define a posição inicial na 1ª pessoa
        transform.localPosition = offsetPrimeiraPessoa;
    }

    void Update()
    {
        // Alterna entre 1ª e 3ª pessoa com a tecla C
        if (Input.GetKeyDown(KeyCode.C))
        {
            AlternarCamera();
        }

        // Leitura do Mouse
        float mouseX = Input.GetAxis("Mouse X") * sensibilidade * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidade * Time.deltaTime;

        // Rotação Vertical (Olhar para cima/baixo)
        rotacaoX -= mouseY;
        rotacaoX = Mathf.Clamp(rotacaoX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotacaoX, 0f, 0f);

        // Rotação Horizontal (Girar o Player para os lados)
        player.Rotate(Vector3.up * mouseX);
    }

    void AlternarCamera()
    {
        ePrimeiraPessoa = !ePrimeiraPessoa;

        if (ePrimeiraPessoa)
        {
            transform.localPosition = offsetPrimeiraPessoa;
        }
        else
        {
            transform.localPosition = offsetTerceiraPessoa;
        }
    }
}
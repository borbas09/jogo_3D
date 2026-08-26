using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Alvo")]
    public Transform alvo; // arraste o Player aqui

    [Header("Posicionamento")]
    public float distancia = 4f;
    public float altura = 1.6f;
    public float suavizacao = 10f;

    [Header("Rotação Vertical (olhar cima/baixo)")]
    public float sensibilidadeMouse = 2f;
    public float anguloMinimo = -30f;
    public float anguloMaximo = 60f;

    private float pitch = 15f; // ângulo vertical atual da câmera

    void LateUpdate()
    {
        if (alvo == null) return;

        // Mouse Y controla a inclinação vertical da câmera
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadeMouse;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, anguloMinimo, anguloMaximo);

        // A câmera usa o yaw do próprio personagem (que já vira com o mouse X)
        Quaternion rotacao = Quaternion.Euler(pitch, alvo.eulerAngles.y, 0f);

        // Ponto de referência (altura dos "ombros"/cabeça do personagem)
        Vector3 pontoFoco = alvo.position + Vector3.up * altura;

        // Posição desejada: atrás do personagem, considerando a rotação
        Vector3 posicaoDesejada = pontoFoco - (rotacao * Vector3.forward * distancia);

        transform.position = Vector3.Lerp(transform.position, posicaoDesejada, suavizacao * Time.deltaTime);
        transform.rotation = rotacao;
    }
}

using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Movimento")]

    public float velocidade = 5f;
    public float forcaPulo = 6f;

    private Rigidbody rb;
    private bool noChao;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
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

        Vector3 direcao = new Vector3(h, 0f, v) * velocidade;
        direcao.y = rb.linearVelocity.y;

        rb.linearVelocity = direcao;
    }

     void OnCollisionStay(Collision collisao)
    {
       if(collisao.gameObject.CompareTag("Chao"))
        {
            noChao = true;
        }
    }

    void OnCollisonExit(Collision colisao)
    {
       
        noChao = false;
          
        
    }


}

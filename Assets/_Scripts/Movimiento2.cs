using UnityEngine;

public class Movimiento : MonoBehaviour
{   
    private Rigidbody2D rb2d;

    private Vector3 velocidad = Vector3.zero;
    private bool mirarDerecha = true;

    [Header("Movimiento")]
    private float movimientoHorizontal = 0f;
    [SerializeField] private float velocidadMov = 5f;
    [SerializeField] private float suavizadorMov = 0.05f;

    [Header("Salto")]
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector3 dimensiones;
    [SerializeField] private bool enSuelo;

    private bool salto = false;

    [Header("Animacion")]
    //private Animator animator;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerSprite;



    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadMov;

        animator.SetFloat("Horizontal", Mathf.Abs(movimientoHorizontal));

        if (enSuelo && Input.GetButtonDown("Jump"))
        {
            enSuelo = false;
            rb2d.AddForce(new Vector2(0f, fuerzaSalto));
            animator.SetTrigger("enSuelo");
        }
    }

    void FixedUpdate()
    {   
        //Debug.Log("enSuelo: " + enSuelo);

        Mover(movimientoHorizontal * Time.fixedDeltaTime);
    }

    private void Mover(float mover)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, rb2d.linearVelocity.y);
        rb2d.linearVelocity = Vector3.SmoothDamp(rb2d.linearVelocity, velocidadObjetivo, ref velocidad, suavizadorMov);

        if (mover > 0 && !mirarDerecha)
        {
            Girar();
        }
        else if (mover < 0 && mirarDerecha)
        {
            Girar();
        }
    }

    private void Girar()
    {
        mirarDerecha = !mirarDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            enSuelo = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensiones);
    }
}

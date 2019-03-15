using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //enum State { InFloor, Jumping, Immune, Charging, Shooting }
    enum State { InFloor, Jumping, Immune, Shooting, Swimming }
    private const string ANIM_WALK = "Walking";
    private const string ANIM_JUMP = "Jumping";
    private const string ANIM_SWIMMING = "Swimming";
    private const string ANIM_SLIDING = "Sliding";
    //private const string ANIM_CHARGING = "Charging";
    //private const string ANIM_FIRING = "Firing";
    private State state = State.InFloor;
    private bool firing = false;
    private bool sliding = false;
    private Animator animator;

    [Header("Horizontal speed")]
    [SerializeField] float linearSpeed = 5;
    [Header("Jump force")]
    [SerializeField] float jumpForce;
    [Header("Impulse Force")]
    [SerializeField] float xForce;
    [SerializeField] float yForce;
    private Rigidbody2D rb2d;
    private float x, y;
    private SpriteRenderer sr;

    [SerializeField] float health;
    [SerializeField] GameObject sliderHealth;

    [SerializeField] GameObject snowballPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float force = 200;
    [SerializeField] float cadencia;

    public Joystick joystick;

    private Vector2 lastCheckPointPos;

    private AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip fireSound;
    public AudioClip hurtSound;

    [SerializeField] GameObject immuneParticlePrefab;
    [SerializeField] bool immunity = false;
    [SerializeField] int godMode;


    void Awake() //donde inicializamos NUESTROS componentes
        //y donde se recomienda obtener todas las referencias a otros objetos
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        lastCheckPointPos = transform.position;
        audioSource = GetComponent<AudioSource>();

        godMode = PlayerPrefs.GetInt("modoDios"); // 0 = true, 1 = false
        if (godMode == 0)
        {
            immunity = true;
        }
    }

    //En START inicializamos componentes de OTROS elementos

    // Update is called once per frame
    void Update()
    {
        // PARA USAR CON TECLAS:
        x = Input.GetAxis("Horizontal"); //sustituido por joystick.Horizontal en Walk()
        //y = Input.GetAxis("Vertical");

        //COMENTAR ESTE IF ENTERO SI SOLO QUIERO ENTRADA TACTIL:
        /////sustituido por SetExternal... Abajo. Lo llamarán los botones táctiles.
        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
            //} else if (Input.GetButtonDown("Fire1"))
        }
        /*else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //hacer que cargue disparo y lo suelte con getkeyUp -> no funciona con el tactil
            CargarDisparo();
        }*/
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StartCoroutine("Disparar");
        }

        //para que no acelere muchisimo en el hielo:
        if (rb2d.velocity.x > linearSpeed * 3)
        {
            rb2d.velocity = new Vector2(linearSpeed * 3, rb2d.velocity.y);
        }
        else if (rb2d.velocity.x < -linearSpeed * 3)
        {
            rb2d.velocity = new Vector2(-linearSpeed * 3, rb2d.velocity.y);
        }

        //para que no acelere muchisimo en caida libre:

        else if (rb2d.velocity.y < -linearSpeed * 5)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, -linearSpeed * 5);
        }
        //print("X:" + rb2d.velocity.x + "Y:" + rb2d.velocity.y);
        //print(state);
    }

    private void FixedUpdate()
    {
        Walk();
    }

    private void Walk()
    {
        /*if (state == State.Jumping) {
            return;
        }*/
        if((Mathf.Abs(x) > 0 || Mathf.Abs(joystick.Horizontal) > 0) && sliding == false) {
            animator.SetBool(ANIM_WALK, true);

            //PARA TACTIL:
            if(Mathf.Abs(joystick.Horizontal) > 0)
            {
                rb2d.velocity = new Vector2(joystick.Horizontal * linearSpeed, rb2d.velocity.y);
            }
            //que la Y sea la que tenga
            //otro modo (con AddForce):
            //rb2d.AddForce(new Vector2(joystick.Horizontal * linearSpeed, 0), ForceMode2D.Impulse);

            //PARA TECLAS:
            if (Mathf.Abs(x) > 0)
            {
                rb2d.velocity = new Vector2(x * linearSpeed, rb2d.velocity.y);
            }

            /* VERSIÓN RETRO
            if (x < 0) {
                sr.flipX = true;
            } else {
                sr.flipX = false;
            }
            */
            //sr.flipX = x < 0 ? true : false; //VERSIÓN GUAY (Ternaria)
            //sr.flipX = (x < 0); //VERSIÓN MÁS GUAY AÚN

            //para que rote entero, no solo la animación, y así dispare para atrás:
            if (x > 0 || joystick.Horizontal > 0)
            {
                transform.rotation = Quaternion.Euler(Vector2.zero);
            }
            else if (x < 0 || joystick.Horizontal < 0)
            {
                transform.rotation = Quaternion.Euler(new Vector2(0, 180));
            }
            /*
            if (x < 0) {
                sr.flipX = true;
            } else {
                sr.flipX = false;
            }*/
        } else {
            animator.SetBool(ANIM_WALK, false);
        }
    }

    private void Jump()
    {
        if (state == State.Jumping) {
            return;
        }
        animator.SetBool(ANIM_JUMP, true);
        rb2d.velocity = new Vector2(rb2d.velocity.x + joystick.Horizontal * linearSpeed, jumpForce);
        state = State.Jumping;
        audioSource.PlayOneShot(jumpSound);
    }

    /*
    private void CargarDisparo()
    {
        if (state != State.Shooting)
        {
            state = State.Charging;
            //animator.SetBool(ANIM_CHARGING, true); //Mejor usar una condicion trigger:
            animator.SetTrigger("charging");
        }    
    }
    */
    private IEnumerator Disparar()
    {
        //if (state != State.Shooting)
        if (firing == false)
        {
            //state = State.Shooting;
            firing = true;
            //animator.SetBool(ANIM_CHARGING, true); //Mejor usar una condicion trigger:
            animator.SetTrigger("Charging");
            //animator.SetBool(ANIM_CHARGING, false);
            yield return new WaitForSeconds(0.5f);
            animator.SetTrigger("Firing");
            GameObject proyectil = Instantiate(snowballPrefab, spawnPoint.position, spawnPoint.rotation);
            proyectil.GetComponent<Rigidbody2D>().AddForce(spawnPoint.transform.right * force);
            //audioSource.PlayOneShot(acDisparo);
            Invoke("ActivarArma", cadencia); //puede volver a disparar una vez se ha pasado el tiempo de cadencia
            audioSource.PlayOneShot(fireSound);
        }
    }

    private void ActivarArma()
    {
        //this.state = State.InFloor;
        firing = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.ITEM)) {
            collision.gameObject.GetComponent<Item>().DoAction();
        } else if (collision.CompareTag(Tags.MOBILEPLATFORM))
        {
            transform.SetParent(collision.transform); //se hace hijo de la plataforma
        }
        if (collision.gameObject.tag != "Ice") //lo añado tambien aqui
        {
            animator.SetBool(ANIM_SLIDING, false);
            sliding = false; //ahora ya puedo volver a moverme
        }
        //print("TRIGGER:" + collision.gameObject.tag);

        //usar el collider de los pies para comparar si el objeto contra el que choca es un enemigo
        //y si es asi matarlo al saltar sobre el
        if (collision.gameObject.tag == "Enemy")
        {
            //print("Toma!");
            int jumpDamage = 100; //muy alto para que mate a cualquier enemigo a la primera
            collision.gameObject.GetComponent<Enemy>().ReceiveDamage(jumpDamage);
            //SetImpulse(700); //mejor hago una versión mas simple aquí:
            rb2d.AddForce(new Vector2(0, yForce*800));
            state = State.Jumping;
        }
        if(collision.gameObject.name == "Star")
        {
            state = State.Immune;
            health = 100;
            sliderHealth.GetComponent<Slider>().value = health;
            StartCoroutine("Immunity");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            if (state == State.Swimming)
            {
                animator.SetBool(ANIM_SWIMMING, false);
                //animator.SetTrigger("swimDone");
            }
        }
        //print("NO esta en el suelo");
        state = State.Jumping;
        transform.SetParent(null); //dejamos de ser hijos de la plataforma
        ChangeFriction(0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //print(collision.gameObject.tag);
        if(collision.gameObject.tag == "Floor" || collision.gameObject.tag == "MobilePlatform")
        //las paredes también son floor porque todo el tilemap va junto...
        //if (collision.gameObject.tag == "MobilePlatform")
        {
            //print("esta en el suelo");
            state = State.InFloor;
            animator.SetBool(ANIM_JUMP, false);
            //por si quiero que NO deslice en las plataformas:
            ChangeFriction(0.4f);
        }
        else if (collision.gameObject.tag == "Water")
        {
            if (state != State.Swimming)
            {
                state = State.Swimming;
                animator.SetBool(ANIM_SWIMMING, true);
                //animator.SetTrigger("swim");
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state == State.Jumping) { // si está saltando Y TOCA ALGO, deja de estar saltando
            state = State.InFloor; // y pasa a estar en el suelo
            animator.SetBool(ANIM_JUMP, false);
        }
        if (collision.gameObject.tag != "Ice")
        {
            animator.SetBool(ANIM_SLIDING, false);
            sliding = false; //ahora ya puedo volver a moverme
            //print("HOLAAA2222");
        }
        //print("COLLISION:" + collision.gameObject.tag);

        /*
        if (collision.gameObject.tag == "EnemyPenguin")
        {
            int jumpDamage = 100; //muy alto para que mate a cualquier enemigo a la primera
            collision.gameObject.GetComponent<Enemy>().ReceiveDamage(jumpDamage);
            rb2d.AddForce(new Vector2(0, yForce * 800));
            state = State.Jumping;
        }*/
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ice")
        {
            ChangeFriction(0);
            animator.SetBool(ANIM_JUMP, false);
            animator.SetBool(ANIM_WALK, false);
            animator.SetBool(ANIM_SLIDING, true);
            sliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ice")
        {
            animator.SetBool(ANIM_SLIDING, false);
            animator.SetBool(ANIM_JUMP, true);
            //sliding = false; NO, solo quiero que salte pero no poder controlarlo en el aire
        }
    }

    private void ChangeFriction(float newFriction)
    {
        PhysicsMaterial2D pm2d = GetComponent<CapsuleCollider2D>().sharedMaterial;
        pm2d.friction = newFriction;
        GetComponent<CapsuleCollider2D>().sharedMaterial = pm2d;
    }

    public void ReceiveDamage(int damage)
    {
        if (!immunity)
        {
            health = health - damage;
            sliderHealth.GetComponent<Slider>().value = health;
            audioSource.PlayOneShot(hurtSound);
            if (health <= 0)
            {
                Die();
            }
        }
    }

    public void SetImpulse(float force) //para que retroceda al pincharse con los pinchos por ejemplo
    {
        int multiplier = sr.flipX ? 1 : -1; //está andando hacia atrás (true, 1), o hacia alante (false, -1)
        rb2d.AddForce(new Vector2(xForce * multiplier, yForce) * force);
        state = State.Jumping;
        if (!immunity)
        {
            animator.SetTrigger("Hurt");
        }
    }

    private void Die()
    {
        GameManager.SubstractLive();
        transform.position = lastCheckPointPos;
        health = 100;
        sliderHealth.GetComponent<Slider>().value = health;
    }

    private IEnumerator Immunity()
    {
        immuneParticlePrefab.SetActive(true);
        immunity = true;
        yield return new WaitForSeconds(6);
        immuneParticlePrefab.SetActive(false);
        if (godMode == 1)
        {
            immunity = false;
        }
    }

    public void SetCheckPointPosition(Vector2 newPos)
    {
        lastCheckPointPos = newPos;
    }

    public void SetExternalJump()
    {
        Jump();
    }
 
    public void SetExternalFire()
    {
        StartCoroutine("Disparar");
    }
}

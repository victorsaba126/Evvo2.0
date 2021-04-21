using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //control player
    public float horizontalMove;
    public float verticalMove;
    private Vector3 playerInput;
    public CharacterController player;


    //public Rigidbody rb;
    //public float distToGround;
    RaycastHit hit;
    public LayerMask layerMask;
    public float direction = 2f;
    private Easing_move easing;

    public bool tuto = false;
    public bool bounce = false;

    public float playerSpeed = 10f;
    public float godSpeed = 0.25f;
    public Vector3 movePlayer = Vector3.zero;
    public float gravity = 80f;
    public float fallVelocity = 0f;
    public float jumpForce;
    public float bounceForce;


    //stats
    public int curHealth;
    public int maxHealth = 3;
    public int dmg = 0;
    private bool coldown = false;
    public float cdTime = 0.6f; //tiene que durar lo mismo que la animacion de ataque
    public bool consumir = false;

    //rampa
    public bool isOnSlope = false;
    private Vector3 hitNormal;
    public float slideVelocity = 7f;
    public float slopeForceDown = -10f;


    //interfaz
    private float timeCounter;
    private float timeCounterCd = 0;
    public float velocityHealth = 0.1f;
    public int curSprite = 0;
    public GameObject[] go;
    private OptionsMenu optionsMenu;




    //cameras
    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 camPosition;
    public GameObject camCaida;
    public GameObject camPlayer;
    private MenuManager menuManager;


    //Attack

    public GameObject attackbox;
    private bool attacking = false;
    public GameObject sonidoSalto;
    public GameObject sonidoAttack;


    //godmode
    public bool god = false;
    public bool goUp;
    public bool goDown;
    public float flow = 5f;

    //particles

    public ParticleSystem dust;

    //Tutorial
    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject tutorial3;
    public float oldtimescale = 0f;

    //animation
    private Animator anim;
    private bool jump;
    private bool alreadyjump;
    private bool walk;
    private bool dead = false;


    void Start()
    {
       
        menuManager = FindObjectOfType<MenuManager>();
        player = GetComponent<CharacterController>();
        //rb = GetComponent<Rigidbody>();
        optionsMenu = FindObjectOfType<OptionsMenu>();
        easing = FindObjectOfType<Easing_move>();
        anim = gameObject.GetComponent<Animator>();


        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        curHealth = maxHealth;

       
    }

    void Update()
    {
        //Raycast 

        //Raycast();
        

        // Movimiento

        movement();

        //movimiento + camara

        camDirection();

        //cursor visible

        visibleCursor();

        //attack

        atack();

        //godmode

        checkGod();

        //health

        spriteHealth();

        //Start coldown

        playerColdown();

        //check animations
        attackAnimation();


    }

    //cursor visible y que no se salga de la pantalla 
    public void visibleCursor()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }
    }

    //Ataque y coldown de ataque
    public void atack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && optionsMenu.menuIsOpen == false && attacking == false && dead == false)
        {
            Instantiate(sonidoAttack);
            attacking = true;
        }

        if (attacking == true)
        {
            timeCounterCd += Time.deltaTime;
            if (timeCounterCd >= cdTime)
            {
                timeCounterCd = 0;
                attacking = false;
                attackbox.SetActive(false);
            }
            else
            {
                attackbox.SetActive(false);
                if (timeCounterCd >= 0.5f)
                {
                    attackbox.SetActive(true);
                }
                
            }
        }
    }

    //Copiar el transform de la plataforma y moverse igual

    //public void plataform()
    //{
    //    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
    //    {
            
    //        UnityEngine.Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
    //        UnityEngine.Debug.Log("Hit");
    //    }
    //    else
    //    {
    //        UnityEngine.Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000f, Color.red);
    //        UnityEngine.Debug.Log("Not Hit");
    //    }

    //}
  

    //Movimiento wasd
    public void movement()
    {
        if (!dead)
        {
            horizontalMove = Input.GetAxis("Horizontal");
            verticalMove = Input.GetAxis("Vertical");

            playerInput = new Vector3(horizontalMove, 0, verticalMove);
            playerInput = Vector3.ClampMagnitude(playerInput, 1);
        }
       

        if (movePlayer.x!=0 && jump==false && attacking == false && dead == false || movePlayer.z != 0 && jump == false && attacking == false && dead == false)
        {
            walk = true;
        }else if (movePlayer.x == 0 && movePlayer.z == 0)
        {
            walk = false;
        }

    }

    //Funcion para la direccion a la que mira la camara
    public void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        player.transform.LookAt(player.transform.position + movePlayer);

        camPosition = new Vector3(camPlayer.transform.position.x, camPlayer.transform.position.y, camPlayer.transform.position.z);
    }

    //Fucion para Jump
    public void playerSkills()
    {
        //UnityEngine.Debug.Log("gounded? "+player.isGrounded);
        if (player.isGrounded && Input.GetButtonDown("Jump") && dead==false )
        {
            
            Instantiate(sonidoSalto);
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
            jump = true;
            walk = false;
            alreadyjump = true;

        }else if (player.isGrounded)
        {
            alreadyjump = false;
            jump = false;
        }
        else if (!player.isGrounded)
        {
            
            if(alreadyjump == true)
            {
                jump = true;
            }
            else
            {
                jump = false;
            }
            CreateDust();
        }
        if (dead == false && bounce)
        {
            //UnityEngine.Debug.Log("entra");
            Instantiate(sonidoSalto);
            fallVelocity = bounceForce;
            movePlayer.y = fallVelocity;
            jump = true;
            walk = false;
            alreadyjump = true;
        }
       
    }

    //vida y sprite vida
    public void spriteHealth()
    {
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }


        // Animación sprite vidas

        timeCounter += Time.deltaTime;
        if (timeCounter >= velocityHealth)
        {

            if (curSprite >= 6)
            {
                curSprite = -1;
            }
            curSprite++;
            timeCounter = 0;
        }
    }

    //setear coldown de ataque/ daño recibido etc
    public void playerColdown()
    {
        if (coldown == true)
        {
            timeCounterCd += Time.deltaTime;
            if (timeCounterCd >= cdTime)
            {
                timeCounterCd = 0;
                coldown = false;
            }
        }
    }

    //funcion para la gravedad
    public void setGravity()
    {


        if (player.isGrounded)
        {
            fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
            
        }
        else
        {
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
            
        }
        SlideDown();
    }

    public void SlideDown()
    {
        isOnSlope = Vector3.Angle(Vector3.up, hitNormal) >= player.slopeLimit;

        if(isOnSlope)
        {
            movePlayer.x += ((1f - hitNormal.y) * hitNormal.x) * slideVelocity;
            movePlayer.z += ((1f - hitNormal.y) * hitNormal.z) * slideVelocity;

            movePlayer.y += slopeForceDown;
        }
    }

    // cuando el character controler golpea contra un colider
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitNormal = hit.normal;
    }

    //muerte del jugador
    public void die()
    {
        walk = false;
        jump = false;
        attacking = false;
        dead = true;
        tuto = true;
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        menuManager.GameOver();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Cuando lleguemos a la parte donde se gana, y el god mode sea falso setearemos el cursor visible 
        //y que pueda salir de la ventana del juego, seguidamente pasamos a la escena de win
        if (other.tag == "Win" && god == false)
        {

            UnityEngine.Cursor.visible = true;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            menuManager.WinScene();
        }

        //si el jugador recibe daño cuando haya pasado el coldown que reciba daño setee el coldown a 0 otra vez y haga una animación el player
        if (coldown == false && god == false)
        {
            if (other.tag == "Projectil" || other.tag == "EnemyAtack")
            {
                
                damage(1);
                coldown = true;

            }


        }

        // Si el jugador muere, el cursor se muetra visible , y lo puedes mover fuera de la ventana, seguidamente pasa a la escena de gameover
        if (other.tag == "Lose" && god == false)
        {
            die();
        }

        //Si el jugador cae al vacio y entra en el trigger CamaraCaida se desactivara la camara actual de tercera persona y se activara una que simplemente mire al jugador pero no lo siga
        if (other.tag == "CamaraCaida" && god == false)
        {
            camPlayer.SetActive(true);

            camCaida.SetActive(false);
        }

        //Activar tutorial 1
        if (other.tag == "Tutorial1")
        {
            tutorial1.SetActive(true);
            //oldtimescale = Time.timeScale;
            //Time.timeScale = 0f;
        }
        if (other.tag == "Tutorial2")
        {
            tutorial2.SetActive(true);
            //oldtimescale = Time.timeScale;
            //Time.timeScale = 0f;
        }
        if (other.tag == "Tutorial3")
        {
            tutorial3.SetActive(true);
            //oldtimescale = Time.timeScale;
            //Time.timeScale = 0f;
        }



    }

    private void OnTriggerExit(Collider other)
    {
        //Si el jugador cae al vacio y entra en el trigger CamaraCaida se desactivara la camara actual de tercera persona y se activara una que simplemente mire al jugador pero no lo siga 
        if (other.tag == "CamaraCaida" && god == false)
        {
            camPlayer.SetActive(false);

            camCaida.transform.position = camPosition;
            camCaida.SetActive(true);
        }
    }

    //chequear si es godmode o no
    public void checkGod()
    {
        if (Input.GetKeyDown("f10"))
        {

            if (!god)
            {
                god = true;
            }
            else
            {
                god = false;
            }
        }
        if (god)
        {

            movePlayer = movePlayer * godSpeed;
            godMode();

        }
        else
        {
            movePlayer = movePlayer * playerSpeed;
            setGravity();
            playerSkills();


        }

        player.Move(movePlayer * Time.deltaTime);
        //rb.velocity = ((movePlayer * Time.deltaTime) - rb.position) / Time.fixedDeltaTime;

    }

    //controles de el godmode
    public void godMode()
    {

        if (Input.GetKey("space"))
        {
            goUp = true;
            fallVelocity += 1f * Time.deltaTime;
            fallVelocity = Mathf.Clamp(fallVelocity, -1f, 1f);


        }
        else if (Input.GetKey("z"))
        {
            goDown = true;
            fallVelocity -= 1f * Time.deltaTime;
            fallVelocity = Mathf.Clamp(fallVelocity, -1f, 1f);

        }
        else
        {
            fallVelocity -= fallVelocity * flow * Time.deltaTime;

        }
        movePlayer.y = fallVelocity;

        player.Move(movePlayer);
    }

    //shake camera, quitar vida al jugador, y cuando la vida sea 0 que pase a la muerte del jugador
    public void damage(int dmg)
    {

        curHealth = curHealth - dmg;
        if (curHealth < 0)
        {
            die();
        }
        else if (curHealth == 0)
        {

            go[curHealth].SetActive(false);
            die();
        }
        else
        {
            CameraShake.Instance.ShakeCamera(5f, .2f);
            
            UnityEngine.Debug.Log("vidas = " + curHealth);
            go[curHealth].SetActive(false);

        }
    }

    public void CreateDust()
    {
        dust.Play();
    }
    

    public void SumarVida()
    {
     
        if (curHealth < 3 && curHealth > 0)
        {
            curHealth = curHealth + 1;
            go[curHealth-1].SetActive(true);
            consumir = true;
        }
        else
        {
            consumir = false;
        }


    }
    public void attackAnimation()
    {
        if (attacking == true)
        {
            
            anim.SetBool("Attack", true);

        }
        else
        {
            anim.SetBool("Attack", false);
        }

        if (jump == true)
        {
        
            anim.SetBool("Jump", true);
        }
        else if(jump == false)
        {
            anim.SetBool("Jump", false);
        }
        if (walk == true)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        if (dead)
        {
            anim.SetBool("Dead", true);

        }
        else
        {
            anim.SetBool("Dead", false);
        }
    }

}









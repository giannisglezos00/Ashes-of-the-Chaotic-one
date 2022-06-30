using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Linq;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    #region "Variables etc"

    //Base stats
    public string Yuvo_State;
    public string Moving_Direction;
    public float Looking_Direction;
    public string Last_Direction;
    public bool CanInteract;
    public Vector3 Yuvo_Position;
    public Vector3Int YuvoIntPos;
    public Vector3 TargetPosition;

    public float Yuvo_Ap;
    public float Yuvo_Hp;
    public float Yuvo_Mana;
    public float Yuvo_Movespeed;

    //Max Base stats
    public float Yuvo_Max_Hp;
    public float Yuvo_Max_Mana;
    public float Yuvo_Default_Movespeed;
    public float Yuvo_Running_Movespeed;

    //References
    public Slider HP_slider;
    public Slider MANA_slider;
    public Rigidbody2D rb;
    public Animator anim;
    private Vector2 MovementInput;

    public GameObject EldritchParticle;
    public GameObject Indicator;
    public GameObject IndicatorText;

    public GameObject InteractIndicator;
    public GameObject Inventory;
    public bool Inventory_Open;
    public GameObject PositionText;

    public Tilemap Interactables;
    public Tile Chest;
    public Tile openChest;

    public ParticleSystem MistyStepParticles;
    public ParticleSystem SpawningMistyStepParticles;
    public Vector3 MouseworldPos;


    //Outside Factors
    public float dmg;

    public Grid grid;

    public Vector3 mousePos;
    public Vector3 worldPos;

    public Vector3 Direction;
    public GameObject MistystepSummon;

    #endregion
    void Start()
    {

    }
    void Awake()
    {
        Indicator.GetComponent<Image>().color = new Color32(134, 212, 70, 100);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Yuvo_State = "Idle";
        Yuvo_Hp = Yuvo_Max_Hp;
        Yuvo_Mana = Yuvo_Max_Mana;
        MistyStepParticles = MistyStepParticles.GetComponent<ParticleSystem>();
        MistyStepParticles.enableEmission = false;
        //MistyStepParticles.gameObject.SetActive(false);
        Inventory_Open = false;

    }
    void Update()
    {
        Yuvo_Position = transform.position;
        mousePos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(mousePos);


        MouseworldPos = Camera.main.ScreenToWorldPoint(mousePos);


        switch ( Moving_Direction )
        {
            case "right":
                anim.SetFloat("IdleDir", 0);//IdleRight
                Looking_Direction = 0;


                break;

            case "left":
                anim.SetFloat("IdleDir", 1);//Idle
                Looking_Direction = 1;
                break;
        }
        Mistystep();
        openInventory();
        Running();
        State();
        Animate();
        takedmg(dmg);
        SetHealth(Yuvo_Hp);
        SetMana(Yuvo_Mana);

        if ( Input.GetKeyDown(KeyCode.Space) )
        {
            Yuvo_Hp -= 2f;
            Yuvo_Mana -= 10;
        }

    }
    void FixedUpdate()
    {

        SetYuvoPosition();
        Move();



    }
    void Move()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");

        //Direction 
        switch ( Horizontal )
        {
            case -1:
                Moving_Direction = "left";
                break;
            case 1:
                Moving_Direction = "right";
                break;
        }
        switch ( Vertical )
        {
            case -1:
                Moving_Direction = "down";
                break;
            case 1:
                Moving_Direction = "up";
                break;
        }

        if ( Input.GetAxisRaw("Horizontal") == 0 || Input.GetAxisRaw("Vertical") == 0 )
        {
            if ( Yuvo_State != "Interacting" && Yuvo_State != "MistyStep" )

            {
                Yuvo_State = "Idle";
            }
        }
        if ( Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 )
        {
            if ( Yuvo_State != "Interacting" && Yuvo_State != "MistyStep" )

            {
                Yuvo_State = "Walking";
            }
            if ( Yuvo_State == "Running" )
            {

            }

        }

        MovementInput = new Vector2(Horizontal, Vertical);
        rb.velocity = MovementInput * Yuvo_Movespeed * Time.deltaTime;
        #region
        /*if ( Horizontal == 0 && Vertical == 0 && Yuvo_State != "Casting_EldritchBlast" )
        {
            rb.velocity = new Vector2(0, 0);
            Yuvo_State = "Idle";
            return;
        }
        if ( Horizontal == 0 && Vertical == 0 && Yuvo_State != "Casting_EldritchBlast" )
        {
            Yuvo_State = "Idle";
        }
        if (Horizontal != 0 || Vertical != 0 && Yuvo_State =="Idle" && Yuvo_State != "Casting_EldritchBlast" )
        {
            Yuvo_State = "Walking";
        }
        
        if ( Yuvo_State != "Casting_EldritchBlast" )
        {
            
        }*/
        #endregion
    }
    void State()
    {
        switch ( Yuvo_State )
        {
            case "Idle":
                //IDLE
                Yuvo_Movespeed = Yuvo_Default_Movespeed;
                Debug.Log("IDLE");
                break;

            case "Walking":
                //WALKING
                Debug.Log("IMWALKING HERE");
                break;
            //RUNNING
            case "Running":
                anim.SetBool("isRunning", true);
                Debug.Log("IMRUNNING HERE");
                break;

            //ELDRITCH BLAST 
            case "Casting_EldritchBlast":
                Debug.Log("Eldritch Blast");
                break;
            //INTERACTING
            case "Interacting":
                Yuvo_Movespeed = 0;
                Debug.Log("Can't move");
                break;
            //MISTY STEP
            case "MistyStep":
                Yuvo_Movespeed = 0;
                Debug.Log("Misty Step");
                break;
            //IN BATTLE
            case "InBattle":
                Yuvo_Movespeed = 0;

                Debug.Log("In Battle");
                break;
        }
    }
    private void Animate()
    {
        //ANIMATE Idle

        if ( Yuvo_State == "Idle" )
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("Eldritch Blast", false);
            anim.SetBool("Interacting", false);
            anim.SetBool("MistyStep", false);
        }

        //ANIMATE Walking

        if ( Yuvo_State == "Walking" )
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", false);
            anim.SetBool("Eldritch Blast", false);
            anim.SetBool("Interacting", false);
            anim.SetBool("MistyStep", false);
        }

        //ANIMATE Running

        if ( Yuvo_State == "Running" )
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", true);
            anim.SetBool("Eldritch Blast", false);
            anim.SetBool("Interacting", false);
            anim.SetBool("MistyStep", false);
        }




        //ANIMATE Eldritch Blast

        if ( Yuvo_State == "Casting_EldritchBlast" )
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("Eldritch Blast", true);
            anim.SetBool("Interacting", false);

        }
        if ( Input.GetAxisRaw("Horizontal") == 0 && ( Input.GetAxisRaw("Vertical") == 0 ) )
        {
            anim.SetBool("isWalking", false);
        }
        //ANIMATE MistyStep
        if ( Yuvo_State == "MistyStep" )
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("Eldritch Blast", false);
            anim.SetBool("MistyStep", true);
            anim.SetBool("Interacting", false);
        }



        anim.SetFloat("MovementX", MovementInput.x);
        anim.SetFloat("MovementY", MovementInput.y);

    }
    private void Running()
    {
        if ( Input.GetKey(KeyCode.LeftShift) )
        {
            if ( Yuvo_State != "Interacting" && Yuvo_State != "MistyStep" )
            {
                Yuvo_State = "Running";
                Yuvo_Movespeed = Yuvo_Running_Movespeed;
            }
        }
        else
        {
            Yuvo_Movespeed = Yuvo_Default_Movespeed;
        }

    }
    void ChangeScene()
    {
        if ( Input.GetKeyDown(KeyCode.E) )
        {
            //SceneManager.LoadScene("Battle");
        }
    }

    #region //SLIDERS
    public void SetMaxHealth(float Yuvo_Max_Hp)
    {
        HP_slider.maxValue = Yuvo_Max_Hp;
        HP_slider.value = Yuvo_Max_Hp;
    }
    public void SetHealth(float Yuvo_Hp)
    {
        HP_slider.value = Yuvo_Hp;
    }
    public void SetMaxMana(float Yuvo_Max_Mana)
    {
        MANA_slider.maxValue = Yuvo_Max_Mana;
        MANA_slider.value = Yuvo_Max_Mana;
    }
    public void SetMana(float Yuvo_Mana)
    {
        MANA_slider.value = Yuvo_Mana;
    }
    #endregion
    public void takedmg(float dmg)
    {
        Yuvo_Hp -= dmg;

    }
    public void InitiateInteraction()
    {
        Yuvo_State = "Interacting";

    }
    public void StopInteraction()
    {
        Yuvo_State = "Idle";
    }
    public void OpenChest()
    {


        Debug.Log("Opening Chest");
        Interactables.SetTile(Interactables.WorldToCell(Yuvo_Position), openChest);

    }
    void openInventory()
    {
        if ( Inventory_Open == true )
        {

            Debug.Log("inventory open = true");
            if ( Input.GetKeyDown(KeyCode.Tab) )
            {
                Yuvo_State = "Idle";
                Inventory.SetActive(false);
                Inventory_Open = false;
            }

        }
        else
        {
            Debug.Log("inventory open = false");
            if ( Input.GetKeyDown(KeyCode.Tab) )
            {
                Yuvo_State = "Interacting";
                Inventory.SetActive(true);
                Inventory_Open = true;
            }
        }


    }
    void SetYuvoPosition()
    {
        Yuvo_Position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        PositionText.GetComponent<TextMeshProUGUI>().text = "X- " + ( Mathf.Round(Yuvo_Position.x * 10) / 10 ) + "Y- " + ( Mathf.Round(Yuvo_Position.y * 10) / 10 );
        YuvoIntPos = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
    }

    void AddToInventory()
    {
        //a distance above the player
        Vector3 YuvoPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void CheckForTile()
    {
        //if the player collides with a chest tile, check if the itle is interactable and if it is,  open the chest

        if ( Interactables.GetTile(YuvoIntPos) == Chest )
        {
            Debug.Log("Chest");
            if ( Yuvo_State == "Idle" )
            {
                Yuvo_State = "Interacting";
                OpenChest();
            }
        }

    }
    void CheckIfInsideObject()
    {
        

    }
    void Mistystep()
    {
        //after 1 second the player teleports towards the direction of the mouse
        //if not already casting MistyStep 
        if ( Yuvo_Mana >= 10 )
        {
            if ( Yuvo_State != "MistyStep" )
            {
                if ( Input.GetMouseButtonDown(0) )
                {
                    TargetPosition = new Vector3(Camera.main.ScreenToWorldPoint(mousePos).x, Camera.main.ScreenToWorldPoint(mousePos).y,1);
                    Direction = worldPos - transform.position;
                    Instantiate(SpawningMistyStepParticles, TargetPosition, Quaternion.identity);
                    Instantiate(MistystepSummon, TargetPosition, Quaternion.identity);
                    Debug.Log("Spawn Particles");

                    Yuvo_State = "MistyStep";

                    StartCoroutine(MistyStep());
                }
            }
        }
        //numerator that waits for x seconds and then teleports the player to the direction of the mouse
        IEnumerator MistyStep()
        {
            Yuvo_State = "MistyStep";
            MistyStepParticles.enableEmission = true;
            Yuvo_Mana -= 10;
            //change the camera size
            
            yield return new WaitForSeconds(1.3f);
            
            
            
            Debug.Log(Vector3.Distance(transform.position, TargetPosition));


            

            //if the distance between Yuvo and target is less than 1, teleport the player a certain distance towards the target
            if ( Vector3.Distance(transform.position, TargetPosition) <= 5.3f )
            {
                Debug.Log(">5");
                //Yuvo_Position = Yuvo_Position + Direction;
                transform.position = TargetPosition;
                
            }
            //else teleport the player a spesific distance towards the target
            else
            {
                Debug.Log("<5");
                
                //Yuvo_Position = Yuvo_Position + Direction;
                transform.position = transform.position + Direction /2;
            }



            Yuvo_State = "Idle";

            //MistyStepParticles.gameObject.SetActive(false);
            MistyStepParticles.enableEmission = false;

        }
        
    }
}
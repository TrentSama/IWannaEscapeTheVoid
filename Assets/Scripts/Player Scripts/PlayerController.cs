using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // The different bullet types that can be fired are stored here     
    public IntVariable weaponIndex; // using this to keep track of what weapon we have equipped
    public float fireRate; // the variable that we'll be editing for the firerate, don't ask tbh
    public Transform bulletSpawner;
    float nextFire; // the time between firing bullets, to prevent shooting too fast
    
    public float moveSpeed;

    // References to any global variable
    public FloatVariable HP;
    public FloatVariable maxHP;
    public PlayerWeaponsInventory weaponsInventory;
    public FloatVariable bulletsInScene;

    public bool dead;
    public BoolVariable winLevel;

    // All the variables for Ammo types.
    public FloatVariable superMissleAmmo;

    //[HideInInspector]
    public Transform checkpoint;

    //the rigid body i dunno
    [HideInInspector]
    public Rigidbody2D rb;


    // This is stuff for jumping
    public float jumpVelocity;
    public float groundedSkin = 0.3f;
    public BoxCollider2D playerSizeBox;
    public LayerMask mask; // used for checking what is considered ground for collisions

    public Animator animator;

    // All the bools for the player
    // checking whether the player is grounded or not
    bool grounded;
    bool jumpRequest;
    bool doubleJump; 
    
    //[HideInInspector]
    public bool invincibility;


    //Used for creating a box under the player to check if grounded
    Vector2 playerSize;
    Vector2 boxSize;

    // so that a bunch of dumb stuff doesn't happen when you die I need to be able to disable it
    public Collider2D hurtBox;

    public Canvas canvas;
    public SpriteRenderer sprite;

    // The amount of damage taken from an enemy, the value changes based on what's hit as the value will be passed from the enemy
    [HideInInspector]
    public float invincibilityTime = 3;
    [HideInInspector]
    public float damageTaken;

    public AudioClip[] soundEffects;
    public AudioSource jumpSfx;
    public AudioSource shootSfx;
    public AudioSource hitSfx;
    public AudioSource deadSfx;

    // Start is called before the first frame update
    void Awake()
    {
        winLevel.Switch = false;
        bulletsInScene.Value = 0;
        weaponIndex.Value = 0;
        HP.Value = maxHP.Value;
        rb = GetComponent<Rigidbody2D>();
        playerSize = playerSizeBox.size;

        boxSize = new Vector2(playerSize.x - .05f, groundedSkin);

        canvas.enabled = false;
        invincibility = false;
        jumpRequest = false;
        doubleJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        // This is to make sure current health never exceeds max health. It was too short to put in its own method.
        if (HP.Value > maxHP.Value){
            HP.Value = maxHP.Value;
        }
        if(HP.Value < 0){
            HP.Value = 0;
        }

        //-----------------------------------------------------------------
        if (dead == false && winLevel.Switch == false)
        {           
            Shoot();
            Jumping();
            JumpRequest();
            SwitchWeaponsInput();
            Animations();
        }
        Death();
        if (bulletsInScene.Value < 0)
        {
            bulletsInScene.Value = 0;
        }
    }
    // FixedUpdate is used for any physics based operations, in this case it's just movement
    void FixedUpdate()
    {
        if (dead == false && winLevel.Switch == false)
        {
            Movement();
        }   
        if (winLevel.Switch == true)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }


    void Animations()
    {
        if (Input.GetButton("Left"))
        {
         //   animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
            animator.SetBool("Idle", false);
            if (grounded == true){
                animator.SetBool("Moving", true);
            }
            transform.localScale = new Vector2(-1, 1);
        }
        else if (Input.GetButton("Right"))
        {
           // animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
            animator.SetBool("Idle", false);
            if (grounded == true){
                animator.SetBool("Moving", true);
            }           
            transform.localScale = new Vector2(1, 1);
        }
        else if (!Input.GetButton("Right") && !Input.GetButton("Left"))
        {
            if (grounded == true){
                animator.SetBool("Moving", false);
                if (dead == false){
                    animator.SetBool("Idle", true);
                }
            }
        }
        //----------------------------------------------------------------------
        if (Input.GetButton("Jump") && grounded == false && animator.GetBool("Falling") == false)
        {
            //   ResetAnimations();
            animator.SetBool("Moving", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Jumping", true);
        }
        else if (Input.GetButtonUp("Jump") && grounded == false)
        {
            ResetAnimations();
            animator.SetBool("Falling", true);
        }

        if (grounded == true)
        {
            animator.SetBool("Falling", false);
            animator.SetBool("Jumping", false);
        }

    }

    void Shoot()
    {
            if (Input.GetButtonDown("Fire1") && Time.time > nextFire && bulletsInScene.Value <= weaponsInventory.weapons[weaponIndex.Value].GetComponent<Bullet>().maxInScene -1 && weaponsInventory.ammo[weaponIndex.Value].Value -1 >= 0)
            {
                nextFire = Time.time + weaponsInventory.weapons[weaponIndex.Value].GetComponent<Bullet>().fireRate;
                Instantiate(weaponsInventory.weapons[weaponIndex.Value], bulletSpawner.position, bulletSpawner.rotation);
                shootSfx.clip = weaponsInventory.weapons[weaponIndex.Value].GetComponent<Bullet>().bulletSound;
                shootSfx.Play();
                if (weaponIndex.Value != 0){
                weaponsInventory.ammo[weaponIndex.Value].Value -= 1;
                }

                bulletsInScene.Value += 1;
            }      
        
        // this is debug, remove this later please
        //-------------------------------
        //-------------------
        //---------
       // if (Input.GetButtonDown("Fire2"))
       // {
       //     HP.ApplyChange(-1);
       // }
    }

    void SwitchWeaponsInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            weaponIndex.Value++;
            if (weaponIndex.Value > weaponsInventory.weapons.Count - 1)
            {
                weaponIndex.Value = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            weaponIndex.Value--;
            if (weaponIndex.Value < 0)
            {
                weaponIndex.Value = (weaponsInventory.weapons.Count - 1);
            }
        }
    }

    void Jumping()
    {
        if (jumpRequest)
        {
            jumpSfx.clip = soundEffects[1];
            jumpSfx.Play();
            rb.velocity = new Vector2 (rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            jumpRequest = false;
            grounded = false;
            if (grounded == false)
            {
                ResetAnimations();
                doubleJump = false;
            }

        }
        else
        {
            Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (playerSize.y + boxSize.y) * 0.5f;
            grounded = (Physics2D.OverlapBox(boxCenter, boxSize, 0f, mask) != null);
        }

        if (grounded == true)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Falling", false);
            doubleJump = true;
        }
    }

    void Movement()
    {
        float moveHorizontal = 0;

        if (Input.GetButton("Left")){
            moveHorizontal = -1;
        }
        if (Input.GetButton("Right")){
            moveHorizontal = 1;
        }
  
        rb.velocity = new Vector2(moveHorizontal * moveSpeed, rb.velocity.y);

    }

    void JumpRequest()
    {
        if (Input.GetButtonDown("Jump") && grounded || Input.GetButtonDown("Jump") && doubleJump)
        {
            jumpRequest = true;
        }
    }

    public void Death()
    {
        if (HP.Value <= 0 && dead == false)
        {
            canvas.enabled = true;
          //  hurtBox.enabled = false;
            ResetAnimations();
            animator.SetTrigger("Dead");
            deadSfx.clip = soundEffects[3];
            deadSfx.Play();
            dead = true;
        }
        if (HP.Value <= 0)
        {
            rb.velocity = new Vector2(0, 0);
        }
        if (Input.GetKeyDown(KeyCode.R) && dead == true)
        {           
            canvas.enabled = false;
            dead = false;
            transform.position = checkpoint.transform.position;
            ResetAnimations();
            HP.Value = maxHP.Value;
            invincibility = false;
        }
    }

    public void TakeDamage(float damage)
    {
        HP.Value -= damage;
    }

    void ResetAnimations()
    {
        animator.SetBool("Moving", false);
        animator.SetBool("Jumping", false);
        animator.SetBool("Falling", false);
        animator.SetBool("Idle", false);
    }

    // this will be used whenever contact is made with different triggers 
    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            checkpoint = collision.transform;
        }

        if (!invincibility)
        {
            if (collision.tag == "Enemy" && collision.GetComponent<Collider2D>() != null && dead == false)
            {
                invincibility = true;
                StartCoroutine(FlashWhenHit());
                hitSfx.clip = soundEffects[2];
                hitSfx.Play();
                TakeDamage(collision.GetComponentInParent<Enemy>().enemyDamage);
                yield return new WaitForSeconds(invincibilityTime);
                invincibility = false;
            }
           
        }
              
    }


    //If looking to edit invincibilty look here
    public IEnumerator OnTriggerStay2D(Collider2D collision)
    {
        if (!invincibility)
        {
            if (collision.tag == "Hazard" && dead == false)
           {
                invincibility = true;
               StartCoroutine(FlashWhenHit());
                hitSfx.clip = soundEffects[2];
                hitSfx.Play();
                if (collision.GetComponent<Collider2D>() != null){
                   TakeDamage(collision.GetComponentInParent<Hazard>().damage);
               }
               yield return new WaitForSeconds(invincibilityTime);
                invincibility = false;
            }
        }
    }

    public IEnumerator FlashWhenHit()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();

        while (invincibility == true)
        {
                sprite.enabled = false;   
            yield return new WaitForSeconds(0.05f);
                sprite.enabled = true;        
            yield return new WaitForSeconds(0.05f);
        }
    }
}

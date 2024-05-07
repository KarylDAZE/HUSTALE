using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public const int Frame = 60;
    public float speed = 3, attackCd = 1,invincibleCd = 1;
    public int maxHealth = 20, maxSword = 99;
    public int currentHealth, currentSword;
    public GameObject swordPrefab;
    public AudioClip footstepClip;
    private Animator anim;
    private Rigidbody2D rb;
    private bool isWalk;
    private static AudioSource[] audioSources;
    public Vector2 lookDirection;
    Vector3 moveVelocity = Vector3.zero;
    bool isInvincible = false;
    float invincibleTimer,attackTimer;
    private Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = Frame;
        Time.fixedDeltaTime = 1.0f/Frame;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSources = GetComponents<AudioSource>();
        scene=SceneManager.GetActiveScene();
        if (!Main.hasChangedScene)
        {
            currentHealth=maxHealth;
            currentSword=20;
        }
        UiManager.instance.UpdateHealthBar(currentHealth, maxHealth);
        UiManager.instance.UpdateKnifeCountText(currentSword, maxSword);
        isWalk = false;
        lookDirection=new Vector2(-PlayerPrefs.GetFloat(scene.name+"LookDirection.x"), -PlayerPrefs.GetFloat(scene.name+"LookDirection.y"));
     
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
        if(attackTimer>0) attackTimer-= Time.deltaTime;
        if(attackTimer < 0) attackTimer=0;
        if ((Input.GetKeyDown(KeyCode.X)||Input.GetKeyDown(KeyCode.K))&&!isInvincible&&currentSword>0&&attackTimer==0)
        {
            Attack();
            attackTimer=attackCd;
            ChangeSword(-1);
        }
        if (isInvincible)
        {
            invincibleTimer-=Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }
    }
    private void FixedUpdate()
    {
        if (isWalk)
            rb.position+=speed*lookDirection*Time.deltaTime;
    }
    void Walk()
    {
        isWalk = false;
        float horizontal = Input.GetAxisRaw("Horizontal"),
            vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
        if (move.x > 0.2||move.x<-0.2)
        {
            lookDirection =move.x<0 ? Vector2.left : Vector2.right;
        }
        else if (move.y>0.2||move.y<-0.2)
        {
            lookDirection=move.y>0 ? Vector2.up : Vector2.down;
        }
        if (move.magnitude>0.3)
        {
            isWalk = true;
            if (!audioSources[0].isPlaying) { audioSources[0].PlayOneShot(footstepClip); }
        }
        anim.SetFloat("Horizontal", lookDirection.x);
        anim.SetFloat("Vertical", lookDirection.y);
        anim.SetBool("isWalk", isWalk);

    }
    void Attack()
    {
        GameObject swordObject = Instantiate(swordPrefab, rb.position + Vector2.up * 0.2f, Quaternion.identity);
        if (lookDirection.x==-1)
            swordObject.transform.eulerAngles=new Vector3(0, 0, 45);
        else if (lookDirection.y==-1)
            swordObject.transform.eulerAngles=new Vector3(0, 0, 135);
        else if (lookDirection.y==1)
            swordObject.transform.eulerAngles=new Vector3(0, 0, -45);
        else
            swordObject.transform.eulerAngles=new Vector3(0, 0, -135);
        SwordController sword = swordObject.GetComponent<SwordController>();
        sword.Launch(lookDirection, 4);
    }
    
    public void ChangeHealth(int healthChange)
    {
        if (healthChange<0)
        {
            if (isInvincible) return;
            isInvincible=true;
            invincibleTimer=invincibleCd;
        }
        currentHealth = Mathf.Clamp(currentHealth +healthChange, 0, maxHealth);
        UiManager.instance.UpdateHealthBar(currentHealth, maxHealth);
        if(currentHealth <= 0) {
            Die();
        }
    }
    public void ChangeSword(int amount)
    {
        currentSword = Mathf.Clamp(currentSword+amount, 0, maxSword);
        UiManager.instance.UpdateKnifeCountText(currentSword, maxSword);

    }
    void Die()
    {
        rb.gameObject.SetActive(false);
        Hint.instance.Lose();
        //UI
    }
    public static void PlaySound(AudioClip clip)
    {
        audioSources[1].PlayOneShot(clip);
    }
}

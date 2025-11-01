using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour,IDamageable
{
    [SerializeField]
    private StatusData statusdata;
    private SpriteRenderer spritePlayer;
    private float speed;
    private float damageTime = 0.0f;
    [SerializeField] 
    private Slider hpSlider;
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private float currentHP;
    public float Health => currentHP;
    private bool invincibility;
    private bool isFlashing;
    private bool canMove;
    private Animator animator;
    private Vector2 inputAxis;
    [SerializeField]
    private AudioClip damagedSE;
    [SerializeField]
    private AudioClip dyingSE;
    private AudioSource audioSource;
    private BoxCollider2D boxCollider;
    private Animator_Controller controller;

    [SerializeField]
    private AudioClip guardSE;
    [SerializeField]
    private AudioClip destroyedSE;
    [SerializeField]
    private AudioClip healSE;
    [SerializeField]
    private AudioClip statusUpSE;

    [SerializeField]
    private GameObject shiledPrehub;
    private bool isGuaded = false;
    private int damagedCount = 0;

    [SerializeField]
    private SceneController sceneController;

    [SerializeField]
    private GameObject playerDiePrehub;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        spritePlayer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        controller = GetComponent<Animator_Controller>();
    }

    void Start()
    {
        if (hpSlider != null)
        {
            hpSlider.maxValue = statusdata.MAXHP;
            hpSlider.value = statusdata.MAXHP;
        }
        if (controller != null)
        {
            controller.SetStateToAnimator(null);
        }
        currentHP = statusdata.MAXHP;
        speed = statusdata.SPEED;
        invincibility = false;
        isFlashing = false;
        canMove = true;
    }

    private void OnEnable()
    {
        playerInput.actions["Move"].performed += OnMove;
        playerInput.actions["Move"].canceled += OnMove;
        playerInput.actions["Pause"].performed += OnPause;
    }

    private void OnDisable()
    {
        playerInput.actions["Move"].performed -= OnMove;
        playerInput.actions["Move"].canceled -= OnMove;
        playerInput.actions["Pause"].performed -= OnPause;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        inputAxis = context.ReadValue<Vector2>();
        if(animator != null)
        {
            controller.SetStateToAnimator(inputAxis != Vector2.zero ? inputAxis : (Vector2?)null);
        }
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if(context.performed && sceneController != null)
        {
            sceneController.Pause();
        }
    }

    // Update is called once per frameb
    void Update()
    {
        if (!canMove) return;
        hpSlider.maxValue = statusdata.MAXHP;
        if (invincibility)
        {
            damageTime += Time.deltaTime;
            if (damageTime > 1.0f)
            {
                damageTime = 0.0f;
                invincibility = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (!canMove) return;
        rb.velocity = inputAxis.normalized * speed;
    }

    public void Damage(float damage)
    {
        if (!invincibility)
        {
            if (isGuaded && shiledPrehub != null)
            {
                var shiled = Instantiate(shiledPrehub, transform.position, Quaternion.identity);
                Destroy(shiled, 0.3f);
                audioSource.PlayOneShot(guardSE);
                damagedCount++;
                if (damagedCount == 3)
                {
                    isGuaded = false;
                    audioSource.PlayOneShot(destroyedSE);
                }
            }
            else
            {
                currentHP -= damage;
                invincibility = true;
                audioSource.PlayOneShot(damagedSE);
                if (hpSlider == true)
                {
                    hpSlider.value = currentHP;
                }
                if (currentHP <= 0)
                {
                    StartCoroutine(Die());
                }
                else if (!isFlashing)
                {
                    StartCoroutine(flashSprite());
                }
            }
        }
    }

    public void Heal(float heal)
    {
        if (hpSlider != null)
        {
            audioSource.PlayOneShot(healSE);
            if (currentHP + heal > statusdata.MAXHP)
            {
                heal = statusdata.MAXHP - currentHP;
            }
            currentHP += heal;
            hpSlider.value = currentHP;
            Debug.Log("now HP: " + currentHP);
        }
    }

    public void AddSpeed(float amount)
    {
        audioSource.PlayOneShot(statusUpSE);
        speed = speed * amount;
        Debug.Log("Speed UP!");
    }

    private IEnumerator Die()
    {
        canMove = false;
        boxCollider.enabled = false;
        animator.enabled = false;
        controller.enabled = false;
        spritePlayer.enabled = false;
        rb.velocity = Vector2.zero;
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        audioSource.PlayOneShot(dyingSE);
        Instantiate(playerDiePrehub, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        GMScript.instance.GameEnd();
    }

    private IEnumerator flashSprite()
    {
        isFlashing = true;
        float finishTime = 0.0f;
        while (finishTime < 1.0f)
        {
            spritePlayer.enabled = false;
            yield return new WaitForSeconds(0.1f);

            spritePlayer.enabled = true;
            yield return new WaitForSeconds(0.1f);

            finishTime += 0.1f * 2;
        }
        spritePlayer.enabled = true;
        isFlashing = false;
    }

    public void SetShiled()
    {
        isGuaded = true;
        damagedCount = 0;
    }
}

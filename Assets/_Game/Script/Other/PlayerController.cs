using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("-----Player Details-----")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private float distance;

    private bool isJumping;
    private bool isDead;

    [Header("-----Other-----")]
    [SerializeField] private GameObject trailEff;
    [SerializeField] private Transform model;
    [SerializeField] private Transform checkPos;
    [SerializeField] private LayerMask jumpableGround;

    private Vector3 startPos;
    private Animator anim;
    private Rigidbody rb;

    private void Start()
    {
        OnInit();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    private void Update()
    {
        Move();
        Rotate();
    }

    public void OnInit()
    {
        trailEff.SetActive(true);
        isDead = false;
        isJumping = false;
        transform.position = startPos;
        model.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private void Move()
    {
        float dirZ = Input.GetAxisRaw("Horizontal");
        float dirX = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(-dirX, 0, dirZ).normalized;

        if (isDead)
            return;

        if (rb.velocity.y < 0 && !IsGrounded() && isJumping)
        {
            anim.Play(CacheString.TAG_JUMP);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }


        if (!isJumping)
        {
            if (Mathf.Abs(dirX) > 0.1f && IsGrounded() || Mathf.Abs(dirZ) > 0.1f && IsGrounded())
            {
                anim.Play(CacheString.TAG_RUN);
            }
            else if (IsGrounded())
            {
                anim.Play(CacheString.TAG_IDLE);
            }
        }

        rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);
    }

    private void Rotate()
    {
        Vector3 moveDirection = rb.velocity;
        moveDirection.y = 0;

        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            model.rotation = Quaternion.Slerp(model.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
    
    private void Jump()
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.jump);
        isJumping = true;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        anim.Play(CacheString.TAG_JUMP);
    }

    private bool IsGrounded()
    {
        bool grounded = Physics.Raycast(checkPos.position, Vector3.down, distance, jumpableGround);
        //Debug.Log("Grounded: " + grounded);
        isJumping = false;
        isDead = false;
        return grounded;
    }

    private IEnumerator IEDead()
    {
        isDead = true;
        anim.Play(CacheString.TAG_DEAD);
        trailEff.SetActive(false);
        yield return new WaitForSeconds(1f);
        UIManager.Ins.OpenUI<LooseCanvas>();
    }

    private void OnTriggerEnter(Collider other)
    {
        DieBox dieBox = Cache.GetDieBox(other);
        if (dieBox != null)
        {
            StartCoroutine(IEDead());
        }

        Chest chest = Cache.GetChest(other);
        if(chest != null)
        {
            Debug.Log("Win");
            AudioManager.Ins.PlaySFX(AudioManager.Ins.win);
            //SceneManager.LoadScene(0);
            transform.position = startPos;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(checkPos.position, Vector3.down * distance);
    }
}

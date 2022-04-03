using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MageBase : MonoBehaviour
{
    [Header("MoveBase:")]
    [SerializeField] Joystick movJoy;
    [SerializeField] Joystick attJoy;
    [SerializeField] float maxVelocity;
    [SerializeField] float acceleration;
    [SerializeField] float deceleration;
    [SerializeField] float rotSpeed;
    [SerializeField] protected Transform mageSprite;

    protected DashLoader dl;
    Rigidbody2D rb;

    protected MPointer mp;
    protected HPObject hp;

    float velocity = 1;

    [Header("Ult:")]
    [SerializeField] Button ultBut;
    Image ultImg;
    enum UltStates
    {
        Ready,
        Ult,
        CD
    }

    [SerializeField] UltStates ultState;
    [SerializeField] float ultTime;
    [SerializeField] float ultCD;

    protected Vector2 motionAxis;
    protected Vector2 attackAxis;

    Animator anim;

    protected virtual void Attack(Vector2 dir) { }

    private void Start()
    {
        dl = GetComponent<DashLoader>();
        rb = GetComponent<Rigidbody2D>();
        mp = GetComponent<MPointer>();
        hp = GetComponent<HPObject>();
        anim = GetComponentInChildren<Animator>();
        ultBut.onClick.AddListener(Ult);
    }
    protected virtual void Awake()
    {
        ultState = UltStates.CD;
        ultImg = ultBut.GetComponentInChildren<Image>();
        StartCoroutine(UltControll());
    }

    void  Update()
    {
        anim.SetBool("Move", rb.velocity.magnitude != 0);
        mp.ManaRegen();
        hp.HealthRegen();
    }
    
    void FixedUpdate()
    {
        Bullet.time -= Time.fixedDeltaTime;
        motionAxis = new Vector2(movJoy.Horizontal, movJoy.Vertical);
        attackAxis = new Vector2(attJoy.Horizontal, attJoy.Vertical);
        Move(motionAxis);
        Attack(attackAxis);
        Rot();
    }

    public void Rot()
    {
        Vector2 dir = attackAxis.magnitude != 0 ? attackAxis : motionAxis;
        if (dir.magnitude < 0.1f) return;
        mageSprite.rotation = Quaternion.RotateTowards(mageSprite.rotation, Quaternion.LookRotation(Vector3.forward, dir), rotSpeed * Time.deltaTime);
    }

    void Move(Vector2 dir)
    {
        velocity = Mathf.Clamp(velocity, 1, maxVelocity);
        rb.velocity = dir.normalized * velocity;
        if (dir.magnitude > 0.1f)
        {
            velocity *= acceleration;
        }
    }

    public void Ult()
    {
        if (ultState == UltStates.Ready)
            OnUltStart();

    }
    public virtual void OnUltStart()
    {
        ultState = UltStates.Ult;
    }
    public virtual void OnUltEnd()
    {
        ultState = UltStates.CD;
    }
    IEnumerator UltControll()
    {
        while (true)
        {
            switch (ultState)
            {
                case UltStates.CD:
                    for (short i = 0; i <= 100; i++)
                    {
                        ultImg.fillAmount = i / 100.0f;
                        yield return new WaitForSeconds(ultCD/100);
                    }
                    ultState = UltStates.Ready;
                    break;
                case UltStates.Ult:
                    yield return new WaitForSeconds(ultTime);
                    OnUltEnd();
                    break;
                default:
                    yield return new WaitUntil(()=>(ultState==UltStates.Ult|| ultState == UltStates.CD));
                    break;
            }
        }
    }
}
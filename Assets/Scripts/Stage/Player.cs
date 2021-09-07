using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Player : MonoBehaviour
{
    // player 이동관련
    Rigidbody2D rigid;
    public float Speed = 10f;
    float h;
    float v;
    bool isHorizonMove;
    Vector3 dirVec;
    Vector3 dirVec1;

    float IsRun = 1f;
    float Rate = 1;

    // Animation
    Animator anim;

    // 발걸음
    public AudioSource[] step = new AudioSource[4];
    public float soundRad = 5;
    public bool is_running = false;     // 좀비 탐지
    float interval =1f;
    int idx = 0;
    int doing = 0;
    int w;
    int r;

    // Light 관련
    [SerializeField] FieldView field;
    [SerializeField] FieldView field2;
    float Light_h;
    float Light_v;
    int Move_match;
    int Light_match;
    public bool is_battery;
    public bool is_light;   // 좀비탐지

    // plater attack
    public GameObject attack;
    public ParticleSystem light;
    public BatteryController Battery;

    // Player Hide
    HidePlace hp;
    SpriteRenderer sprite;
    RaycastHit2D hPlace;
    public bool is_Hide = false;     // 좀비탐지

    // Item 관련
    public GameObject ITEM;
    public int item_num = 0;
    public int[] item_found = new int[20];
    int item_ID;
    public float stealthS = 1;

    // Nitro
    bool IsNitro = false;
    float NitroRate = 1f;
    float NitroTime = 5f;
    float CurNitroTime;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        for(int i = 0; i < 4; i++)
        {
            step[i] = step[i].GetComponent<AudioSource>();
        }
    }
    void Update()
    {
        Hide();
        if (!is_Hide)
        {
            playerMove();
            LightingSet();
            Match();
            Running();
            LightOnOff();
            Attack();
            Speed = 10 * IsRun * Rate * NitroRate;
        }

        if(doing != r + w)
        {
            doing = r + w;
            CancelInvoke();
            WalkSound();
        }
    }
    void FixedUpdate()
    {
        // Move
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.velocity = moveVec * (Speed);

        // nowdoing
        if (h == 0 && v == 0) w = 0;
        else w = 1;
    }

    void playerMove()
    {
        // Move Value
        h = Input.GetAxisRaw("Horizontal2");
        v = Input.GetAxisRaw("Vertical2");

        // Check Button Down & Up
        bool hDown = Input.GetButtonDown("Horizontal2");
        bool vDown = Input.GetButtonDown("Vertical2");
        bool hUp = Input.GetButtonUp("Horizontal2");
        bool vUp = Input.GetButtonUp("Vertical2");

        // Check Horizontal Move
        if (hDown)
        {
            isHorizonMove = true;
            WalkSound();
        }
        else if (vDown)
        {
            isHorizonMove = false;
            WalkSound();
        }
        else if (hUp || vUp)
            isHorizonMove = h != 0;

        // 아이템 줍기 or 사용
        int n = 0;
        for(int i = 0; i < item_found.Length; i++){
          if(item_found[i] == 1) break;
          n++;
          if(n >= item_found.Length){
            item_num = 0;
            n = 0;
          }
        }
        item_ID = n;
        if(item_num != 0){
          pickupItem();
        }

        // Nitro 사용
        NitroBoost();

        //Animation
        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
            anim.SetBool("isChange", false);

        // Check Player Rotation
        if (vDown && v == 1)
        {
            dirVec = Vector3.up;
            Move_match = 1;
        }
        else if (vDown && v == -1)
        {
            dirVec = Vector3.down;
            Move_match = 2;
        }
        else if (hDown && h == -1)
        {
            dirVec = Vector3.left;
            Move_match = 3;
        }
        else if (hDown && h == 1)
        {
            dirVec = Vector3.right;
            Move_match = 4;
        }
    }
    void Hide() // 숨는 작업
    {
        hPlace = Physics2D.Raycast(rigid.position, dirVec, 2f, LayerMask.GetMask("HideP"));
        if (hPlace.collider != null && Input.GetButtonDown("Jump")) // 임시 키보드
        {
            hp = hPlace.collider.GetComponent<HidePlace>();
            hp.Hide();
            is_Hide = !is_Hide;
            sprite.color = (sprite.color == new Color(1, 1, 1, 1)) ? new Color(1, 1, 1, 0) : new Color(1, 1, 1, 1);
        }
    }
    void Attack()
    {
      if (is_battery){
        if (Input.GetKeyDown(KeyCode.R))
        {
            attack.SetActive(true);
            light.Play();
            Battery.AttackUse();
            Invoke("End", 2f);
        }
      }
    }
    void End()
    {
        attack.SetActive(false);
    }
    void WalkSound()
    {
        CancelInvoke();
        if (r == 1 && w == 1)
        {
            interval = 0.2f;
            step[idx].Play();
            idx = (idx + 1) % 4;
            Invoke("WalkSound", interval);
        }
        else if (r == 1 || w == 1)
        {
            interval = 0.7f;
            step[idx].Play();
            idx = (idx + 1) % 4;
            Invoke("WalkSound", interval);
        }
        else
            CancelInvoke();

    }
    void LightingSet()
    {
        // Move Value
        Light_h = Input.GetAxisRaw("Horizontal");
        Light_v = Input.GetAxisRaw("Vertical");

        // Check Button Light Down & Up
        bool hDown_L = Input.GetButtonDown("Horizontal");
        bool vDown_L = Input.GetButtonDown("Vertical");
        bool hUp_L = Input.GetButtonUp("Horizontal");
        bool vUp_L = Input.GetButtonUp("Vertical");

        // Check Light Rotation
        if (vDown_L && Light_v == 1)
        {
            dirVec1 = Vector3.left;
            Light_match = 1;
        }
        else if (vDown_L && Light_v == -1)
        {
            dirVec1 = Vector3.right;
            Light_match = 2;
        }
        else if (hDown_L && Light_h == -1)
        {
            dirVec1 = Vector3.down;
            Light_match = 3;
        }
        else if (hDown_L && Light_h == 1)
        {
            dirVec1 = Vector3.up;
            Light_match = 4;
        }
        field.SetOrigin(rigid.transform.position);
        field.SetAimDirection(dirVec1);
    }
    void LightOnOff()
    {
        // if battery > 0
        if (is_battery)
        {
            // LightSet
            if (Input.GetKeyDown(KeyCode.F))
            {
                is_light = !is_light;
                field.SetLightOff();
            }
        }
        else
        {
            field.GetComponent<FieldView>().fov = 0;
            is_light = false;
        }
    }
    void Match() // 시선과 방향 관련 스피드
    {
        if (!is_light)
        { //불 켜져 있을 때만 속도가 바뀌어야 함.
            if (Light_match == Move_match)
                Rate = 1;
            else
                Rate = 0.6f;
        }
    }
    void Running()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            IsRun = 1.2f; r = 1; soundRad = 20 * stealthS;
            is_running = true;
        }
        else if (Input.GetButtonUp("Fire3"))
        {
            IsRun = 1;r = 0; soundRad = 10 * stealthS;
            is_running = false;
        }
    }

    void pickupItem()
    {
        if (Input.GetButtonDown("Jump"))
        {
            //Debug.Log(item_ID);
            ITEM.GetComponent<ItemController>().ItemControl(item_num, item_ID);
            item_num = 0;
        }
    }
    public void NitroOn(){
      IsNitro = true;
      CurNitroTime = NitroTime;
    }

    void NitroBoost()
    {
        if(IsNitro){
          CurNitroTime -= Time.deltaTime;
          if(CurNitroTime <= 0) IsNitro = false;
          else NitroRate = 1.2f;
        }
        else NitroRate = 1.0f;
    }
    // 탐지가능한 행위를 했는지
    public bool is_finding()
    {
        if (is_running || !is_light) return true;
        return false;
    }
}

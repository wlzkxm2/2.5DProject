using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NekoMove : MonoBehaviour
{
    public float maxSpeed;
    float JumpPower = 15f;
    float timer;
    public int waitingTime;
    float forceGravity = 1.2f;
    
    int moveStats = 0;          // 움직임 체크 0이면 Idle 상태
    bool JumpCheack = false;

    
    Vector3 unitPosition;

    public GameObject gameobject;
    //public GameObject gameguide = GameObject.FindGameObjectWithTag("GameGuide");
    public GameManager gamemanager;

    RaycastHit hit;
    Rigidbody rigid;
    SpriteRenderer spr;
    Transform tf;
    Animator ani;
    public AudioSource gameSFX;      // 점프, 죽었을때, 클리어 했을때 소리
    public AudioClip JumpSFX;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        spr = GetComponent<SpriteRenderer>();
        //gameobject = GetComponent<GameObject>();
        tf = GetComponent<Transform>();
        //ani = GetComponent<Animator>();
        ani = gameobject.GetComponent<Animator>();

    }

    void Start()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // 레이케스트를 위한 유닛의 위치 + y축으로-3 만큼 더한위치를 저장
        unitPosition = this.transform.position + new Vector3(0, 1.5f, 0);
        if (Input.GetKey("right"))
        {
            // 우측 화살표를 눌럿을때 이동
            moveStats = 1;
            //this.transform.rotation = Quaternion.Euler(0, 180, 0);
            gameobject.GetComponent<SpriteRenderer>().flipX = false;

        }
        else if (Input.GetKey("left"))
        {
            moveStats = -1;
            //this.transform.rotation = Quaternion.Euler(0, 0, 0);
            gameobject.GetComponent<SpriteRenderer>().flipX = true;

        }
        else
        {
            moveStats = 0;      // 움직이지 않고 있다는것
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveStats = 2;      // 점프 했음을 체크
            if(JumpCheack == false)
            {
                JumpCheack = true;
                CharacterJump();
                JumpSoundPlay();
            }
        }

        if(rigid.velocity.normalized.x == 0)
        {
            timer += Time.deltaTime;

            if (timer > waitingTime)
            {
                ani.SetBool("isWalk", false);
                timer = 0;
            }
            
        }
        else
        {
            ani.SetBool("isWalk", true);
        }

        

        Debug.DrawLine(this.transform.position, unitPosition, Color.red, 0.3f);

    }

    // 캐릭터 이동관련 함수
    void FixedUpdate()
    {
        // 캐릭터 상태를 최고속도 * 현재 상태로 전달
        // moveStats 가 - 일경우 되로가기 때문에 뒤로 이동
        // y축은 변함 없음
        rigid.velocity = new Vector2(maxSpeed * moveStats, rigid.velocity.y);
        ani.SetBool("isWalk", false);
        rigid.AddForce(Vector3.down * forceGravity);

        
    }
    
    // 캐릭터 점프 함수
    void CharacterJump()
    {
        //rigid.velocity = new Vector2(rigid.velocity.x, 10);
        rigid.AddForce(Vector2.up * JumpPower, ForceMode.Impulse);
        this.gameObject.layer = 9;
        // Debug.DrawLine(transform.position, transform.position + new Vector3(0, -3, 0), Color.red, 0.3f);        // 레이히트확인
    

        if(rigid.velocity.y == 0){
            CheakJump();
        }
    }
    
    void CheakJump(){

        // Raycast(발사할 위치, 발사할 방향, hit에 넘겨주는 코드 , 거리)
        if(Physics.Raycast(gameobject.transform.position, unitPosition, out hit)){
            Debug.Log("hit collider name : " + hit.collider.name);       // 레이캐스트에 닿인 오브젝트에 일므 출력
            // Debug.DrawRay(gameObject.transform.position, transform.position + new Vector3(0, -3, 0), Color.blue);
            // 발사할 위치, 발사항 방향과 거리, 발사 색)
        }
    }

    // 도착 후 씬 전환
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Finish")
        {
            Debug.Log("finish");
            gamemanager.soundPlay("gameClear");
            // Finish 블록에 트리거 작동시 씬 로드
            // SceneManager.LoadScene("Stage02");
            gamemanager.StageManager();
            
        }else if(col.gameObject.tag == "DeadBox")
        {
            //gamemanager.StageLevelSet();
            Debug.Log("낙사");

            // 만약 유저가 진행한 레벨이 5레벨 이상이면 죽어도 5스테이지를 불러옴
            string gotoStage = gamemanager.StageLevelSet().ToString("D2");

            SceneManager.LoadScene("Stage" + gotoStage);
            transform.position = new Vector3(0f, 0f, 2.33f);
        }else if(col.gameObject.tag == "GameGuide")
        {
            
        }else if(col.gameObject.tag == "Coin")
        {
            gamemanager.getCoin();
            col.gameObject.SetActive(false);
            Debug.Log("Coin!!");
        }
    }

    private void OnCollisionEnter(Collision col)
    {

        //Debug.Log("땅입니다 " + col.gameObject.tag);
        if (col.gameObject.tag == "Ground")
        {
            JumpCheack = false;
            this.gameObject.layer = 8;
        } else if(col.gameObject.layer == LayerMask.NameToLayer("JumpGround"))
        {
            this.gameObject.layer = 9;
            Debug.Log("레이어 변경");
        }
    }

    void JumpSoundPlay(){
        gameSFX.PlayOneShot(JumpSFX);
    }

}

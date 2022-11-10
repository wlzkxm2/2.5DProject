using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int StageCount;  
    public int StageLevel = 1;

    public static GameManager instance;
    public GameObject gameobject;

    public Text Score;
    public Text Scoreshadow;
    public Text DeathCount;
    public Text DeathCountshadow;
    int Score_int = 0;
    int DeathCount_int = -1;
    char pad = '0';

    public SoundManager soundManager;


    //public Button StartBtn, OptionBtn, ExitBtn;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        StageLevelSet();
    }

    private void Start()
    {
        //StageLevel = 1;
        soundManager.SoundAllMute();
    }

    public void StageManager()
    {
        StageLevel++;
        if (StageCount >= StageLevel)
        {
            SceneManager.LoadScene("Stage0" + StageLevel);
            gameobject.transform.position = new Vector3(0f, 0f, 2.33f);
        }
            
        else
            Debug.Log("더이상 스테이지가 없음");
    }

    public int StageLevelSet()
    {
        if (StageLevel > 5)
            StageLevel = 5;
        else
            StageLevel = 1; // 스테이지를 1스테이지부터 재시작
            
        Score_int = 0;      // 얻은 포인트 초기화

        soundPlay("dead");
        getCoinUI();        // 얻은 포인트 UI에 재 표시
        deathCountSet();        // 죽은 카운트를 UI에 올려주는 함수
        Debug.Log(DeathCount_int);      // 로그 확인

        return StageLevel;
    }
    
    public void getCoin()
    {
        Score_int++;
        soundPlay("getCoin");
        getCoinUI();
    }

    public void deathCountSet()
    {
        DeathCount_int++;
        deathCountUI();
    }

    void getCoinUI(){
        string Score_str = Score_int + "";
        Score.text = Score_str.PadLeft(3, pad).ToString();
        Scoreshadow.text = Score_str.PadLeft(3, pad).ToString();
    }

    void deathCountUI(){
        string DeathCount_str = DeathCount_int + "";

        DeathCount.text = DeathCount_str.PadLeft(3, pad).ToString();
        DeathCountshadow.text = DeathCount_str.PadLeft(3, pad).ToString();
    }

   public void soundPlay(string situation){
       switch (situation)
       {
           case "getCoin":
           soundManager.PlaySound("get_CoinSound");
           break;

           case "getkeyCoin":
           soundManager.PlaySound("get_SpecialCoin");
           break;

           case "dead":
           int randint = Random.Range(0, 2);
           if(randint == 1){
               soundManager.PlaySound("DeathSound01");
           }else{
               soundManager.PlaySound("DeathSound02");
           }
           break;

           case "gameClear":
           soundManager.PlaySound("GameClear");
           break;

           default:
           break;
       }

   }
}

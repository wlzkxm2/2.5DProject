using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCoinManager : MonoBehaviour
{
    public GameObject gameobject;

    void Awake()
    {
    }
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")            // 플레이어 와 충돌하게 되면
        {
            Debug.Log("Player!");
            this.gameObject.SetActive(false);       // 키 코인 비활성화
            gameobject.SetActive(true);             // 타일 무빙 활성화
            
        }
    }


}

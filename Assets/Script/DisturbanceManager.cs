using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisturbanceManager : MonoBehaviour
{
    public float limitTime;     // 직접 정하는 체크타임 
    public float objectSpeed;
    float cheakTime = 0f;
    bool movingcheak = false;

    // Update is called once per frame
    void Update()
    {
        cheakTime += Time.deltaTime;
    }

    void FixedUpdate(){
        if(movingcheak){
            transform.position += new Vector3(0, 0, (objectSpeed * -1) * Time.deltaTime);
            setbool();
        }else{
            transform.position += new Vector3(0, 0, objectSpeed * Time.deltaTime);
            setbool();
        }
    }

    void setbool()
    {
        if(cheakTime >= limitTime){
            if(movingcheak)
                movingcheak = false;
            else
                movingcheak = true;
            
            cheakTime = 0f;

        }
    }
}

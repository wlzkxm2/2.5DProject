using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMoveManager : MonoBehaviour
{
    float cheakTime = 0f;
    bool movingcheak = true;        // true 면 위로 false면 밑으로

    float cointSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cheakTime += Time.deltaTime;

        //Debug.Log(cheakTime);

       
    }

    void FixedUpdate()
    {
        if (movingcheak == true)
        {
            transform.position += new Vector3(0, cointSpeed * (Time.deltaTime / 10), 0);
            Setbool();
        }
        else if (movingcheak == false)
        {
            transform.position += new Vector3(0, (cointSpeed * -1) * (Time.deltaTime / 10), 0);
            Setbool();
        }
    }

    void Setbool()
    {

        //Debug.Log("setbool : " + cheakTime);

        if (cheakTime >= 0.7f)
        {
            // Debug.Log("if!!! ");
            if (movingcheak == true)
                movingcheak = false;
            else if (movingcheak == false)
                movingcheak = true;
            cheakTime = 0;
        }
    }

}

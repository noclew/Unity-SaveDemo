using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ncCharController : MonoBehaviour
{

    public GameObject myBall;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            //save
            ncGlobalController.Singleton.SaveData();

        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            //Load
            ncGlobalController.Singleton.loadData();
        }


        //make balls
        if (Input.GetKeyDown(KeyCode.E))
        {
            print("ball Created");
            float randx = Random.Range(-1f, 1f);
            float randz = Random.Range(-1f, 1f);

            GameObject ball = (GameObject)Instantiate(myBall);
            ball.transform.position = new Vector3(randx, 3, randz);

            ncGlobalController.Singleton.myBallList.Add(ball);
        }

        //delete all balls
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            print("delete all balls");

            ncGlobalController.Singleton.deleteAll();
           
        }

    }
}

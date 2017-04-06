using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//make sure to include these
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ncGlobalController : MonoBehaviour {

    //singleton
    public static ncGlobalController Singleton;
    private static bool isSaving = false;

    //Event and Delegate
    public delegate void SaveDelegate(object sender, EventArgs args);
    public static event SaveDelegate SaveEvent;

    public List<GameObject> myBallList; 
    public List<ncCuteBall> allBallsData;
    public GameObject ballPreFab;

    void Awake()
    {
        if (Singleton == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Singleton = this;
            print("global singleton is created");
        }
        else if(Singleton != this)
        {
            Destroy(gameObject);
        }
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SaveData()
    {
        if (!Directory.Exists("Saves")) Directory.CreateDirectory("Saves");

        if (isSaving) return;

        isSaving = true;
        
        
        BinaryFormatter ff = new BinaryFormatter();
        FileStream file = File.Create("Saves/mySaveFile.woongki");

        //create serialized balls
        myBallList.ForEach( ball =>
       {
           ncCuteBall ballData = new ncCuteBall();
           ballData.x = ball.transform.position.x;
           ballData.y = ball.transform.position.y;
           ballData.z = ball.transform.position.z;
           allBallsData.Add(ballData);
       });

        ff.Serialize(file, allBallsData);
        file.Close();
        print("SaveDone");
        isSaving = false;
    }

    public void loadData()
    {
        deleteAll();
        BinaryFormatter ff = new BinaryFormatter();
        FileStream file = File.Open("Saves/mySaveFile.woongki", FileMode.Open);

        allBallsData = (List<ncCuteBall>)ff.Deserialize(file);
        file.Close();
        print("loaded");

        foreach (ncCuteBall ball in allBallsData)
        {
            GameObject spawnedBall = (GameObject)Instantiate(ballPreFab);
            ballPreFab.transform.position = new Vector3(ball.x, ball.y, ball.z);
            myBallList.Add(spawnedBall);
            print(ball.x +",  "+ ball.y + ",  " + ball.z);
        }

    }

    public void deleteAll()
    {
        foreach (GameObject ball in myBallList)
        {
            Destroy(ball);
        }
        myBallList = new List<GameObject>();
        allBallsData = new List<ncCuteBall>();

    }
}

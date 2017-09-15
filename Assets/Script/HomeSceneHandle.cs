using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeSceneHandle : MonoBehaviour {

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void moveToPrivateKey()
    {
        SceneManager.LoadScene(2);
    }

    public void moveToFile()
    {
        SceneManager.LoadScene(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoControl : MonoBehaviour {
    float timeD;
	// Use this for initialization
	void Start () {
        timeD = 0;
	}
	
	// Update is called once per frame
	void Update () {
        timeD += Time.deltaTime;
        
        if(transform.localScale.x== 0)
            SceneManager.LoadScene("Game");

        float scale = (-(Mathf.Pow((timeD * 2 )- 3, 2)) + 9) / 30f ;
        if (scale < 0)
        {
            transform.localScale = new Vector2(0, 0);
            //SceneManager.LoadScene("Game");
        }
        else {
        Debug.Log(scale);
        transform.localScale = new Vector2(scale, scale);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class determineHeight : MonoBehaviour {
    int currentLevel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        currentLevel = 0;
        RaycastHit hit;
        for (int x = -3; x < 4; x++)
        {
            for (int y = -3; x < 4; y++)
            {

                if(Physics.Raycast(new Vector3(x, 10, y), new Vector3(0, -1, 0), out hit))
                {
                    
                }
            }
        }

	}
}

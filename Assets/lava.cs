using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lava : MonoBehaviour {

    public PlayerController player;
    float offsetX;
    float offsetY;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float playerdiffer = player.transform.position.y - transform.position.y;
        if(BlockSpawner.begin)
            transform.Translate(0, 0.001f , 0,Space.World);
        offsetX += Time.deltaTime * 0.005f;
        offsetY += Time.deltaTime * 0.005f;
        Shader.SetGlobalFloat("offsetX", offsetX);
        Shader.SetGlobalFloat("offsetY", offsetY);

    }
}

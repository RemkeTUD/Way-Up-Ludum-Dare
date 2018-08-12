using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BlockController : MonoBehaviour {

    Rigidbody rb;
    float speed;
    Material mat;
    public GameObject player;
    Color col;
    Component copyOfRB;
    public bool isExploded = false;
    public long TimeExploded;
    bool done;
    public GameObject lava;
    
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 0;

        speed = -5f;
        col = UnityEngine.Random.ColorHSV(0,1,0,1,0,1,1,1) + new Color(0.1f, 0.1f, 0.1f) ;

        foreach(Renderer renderer in GetComponentsInChildren<Renderer>())
        renderer.material.color = col;
        lava = GameObject.Find("Lava");

	}
    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag=="Lava" && isGrounded())
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.Sleep();
            Destroy(GetComponent<Rigidbody>());
            done = true;
            gameObject.tag = "Untagged";
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Lava" && isGrounded())
        {
        }
    }
    // Update is called once per frame
    void Update () {
        
        if(!done && ((isGrounded() && lava.transform.position.y > transform.position.y) || lava.transform.position.y-5 > transform.position.y) )
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.Sleep();
            Destroy(GetComponent<Rigidbody>());
            done = true;
            SetLayerRecursively(this.gameObject, 0);
            this.enabled = false;
            gameObject.tag = "Untagged";
        }
        if (lava.transform.position.y - 5 > transform.position.y)
        {
            foreach (MeshRenderer ren in GetComponentsInChildren<MeshRenderer>())
                ren.enabled = false;
            gameObject.tag = "Untagged";
            this.enabled = false;
        }

        if (isExploded)
        {
            

            if (TimeExploded == 0)
                TimeExploded = Time.frameCount;
            if (Time.frameCount > TimeExploded + 120)
                Destroy(gameObject);

            foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
                renderer.material.color = Color.Lerp(renderer.material.color, new Color(0, 0, 0), 0.1f);//new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f-((Time.frameCount - TimeExploded)/180f));
        }
            

        if (transform.parent != null && transform.parent.gameObject.tag == "Player") {
            SetLayerRecursively(this.gameObject, 2);
            //rb.velocity = new Vector3(0, 0, 0);
            //rb.detectCollisions = false;
            if (GetComponent<Rigidbody>() != null)
            {
                Destroy(GetComponent<Rigidbody>());
                
            }
            setEmission(true);
        }
        else { 
            SetLayerRecursively(this.gameObject, 0);
            if (GetComponent<Rigidbody>() == null && !done)
            {
                Rigidbody rbNew;
                rbNew= gameObject.AddComponent<Rigidbody>();
                rbNew.constraints = RigidbodyConstraints.FreezeAll ^ RigidbodyConstraints.FreezePositionY;

                rb = GetComponent<Rigidbody>();
                rb.detectCollisions = true;
                
            }
            if(!isExploded && !isGrounded())
                rb.velocity = new Vector3(0, speed, 0);
            setEmission(false);
        }

        if (speed == 0)
            rb.velocity = new Vector3(0, 0, 0);

        
    }

    public bool isGrounded()
    {
        SetLayerRecursively(this.gameObject, 2);
        for (int i = 0; i < transform.childCount; i++)
            if (Physics.Raycast(transform.position, Vector3.down, 0.61f)) {
                SetLayerRecursively(this.gameObject, 0);
                return true;
            
        }
        SetLayerRecursively(this.gameObject, 0);
        return false;
    }

    public static void SetLayerRecursively(GameObject go, int layerNumber)
    {
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }

    public void setEmission(bool active)
    {
        if(!isExploded)
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>()) {
            if (active)
                renderer.material.color = Color.Lerp(renderer.material.color, col * 2f + new Color(0.1f,0.1f,0.1f), 0.3f) ;
            else
            {
                renderer.material.color = Color.Lerp(renderer.material.color, col * 1f, 0.3f);
            }

        }
    }

    
}

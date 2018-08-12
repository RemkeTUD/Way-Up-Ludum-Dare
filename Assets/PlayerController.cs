using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour {

    int x, y;
    int moveX, moveZ;
    Vector3 currentPosition;
    Vector3 target;
    Rigidbody rb;
    public bool dead;


    Vector3Int directionLooking;

    // Use this for initialization
    void Start() {
        x = 0;
        y = 0;
        currentPosition = transform.position;
        target = transform.position;
        directionLooking = new Vector3Int(0, 0, 1);

        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag=="Lava") {
            GetComponent<AudioSource>().Play();
            CameraShaker.Instance.ShakeOnce(7, 7, 0, 1);
            var exp = GetComponent<ParticleSystem>();
            exp.Play();
            foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
                mr.enabled = false;
            GetComponent<Rigidbody>().detectCollisions = false;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody>().useGravity = false;
            dead = true;
            //Destroy(this.gameObject, exp.main.duration);
        }
    }

    // Update is called once per frame
    void Update() {

        if(dead && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Game");
        }


        transform.GetChild(0).rotation = Quaternion.RotateTowards(transform.GetChild(0).rotation, Quaternion.LookRotation(directionLooking), 5f * Time.deltaTime * 60f);

        if (Vector3.Distance(transform.position,target) < 0.1f) {
            transform.position = new Vector3(target.x, transform.position.y, target.z);
            currentPosition.x = transform.position.x;
            currentPosition.z = transform.position.z;
            moveX = 0; moveZ = 0;
            
            if (Input.GetKey(KeyCode.W))
            {
                if (!hasInDir(directionLooking.x, directionLooking.z)) {
                    moveX = directionLooking.x;
                    moveZ = directionLooking.z;
                }
            }
            else if(Input.GetKey(KeyCode.S)) {

                Vector3Int back = directionLooking * -1;

                if(!hasInDir(back.x, back.z)) {
                    moveX = back.x;
                    moveZ = back.z;
                }
            }
            else if (Input.GetKey(KeyCode.A))
            {
                if (transform.GetChild(0).forward.normalized == directionLooking) {
                    moveZ = directionLooking.x;
                    moveX = -directionLooking.z;
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (transform.GetChild(0).forward.normalized == directionLooking)
                {
                    moveZ = -directionLooking.x;
                    moveX = directionLooking.z;
                }
            }

            
            Vector3Int moveVec = new Vector3Int(moveX, 0, moveZ);
            if (moveVec.magnitude > 0 && moveVec != directionLooking * -1 && directionLooking != moveVec) {
                directionLooking = new Vector3Int(moveX, 0, moveZ);
                if(transform.childCount > 1)
                    transform.GetChild(1).parent = null;
            }

            if (directionLooking == transform.GetChild(0).forward)
                target = currentPosition + new Vector3(moveX, 0, moveZ);

            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                if(transform.childCount == 1) {
                    if(getObjectInDir(directionLooking.x, directionLooking.z) != null)
                        getObjectInDir(directionLooking.x, directionLooking.z).transform.parent = gameObject.transform;
                }
                
            }
            if(!Input.GetKey(KeyCode.LeftShift))
            {
                if (transform.childCount == 2)
                {
                    transform.GetChild(1).transform.parent = null;
                }
            }
            

        }
        target.y = transform.position.y;
        rb.MovePosition(Vector3.MoveTowards(transform.position, target, 0.1f * Time.deltaTime * 60f));
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (isGrounded()) {
                Debug.Log("JUMP");
                rb.AddForce(new Vector3(0, 6, 0), ForceMode.Impulse);

            }
        }


    }

    public bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.61f);
    }

    bool hasInDir(int x, int z)
    {
        if(transform.childCount > 1)
        for(int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            if (Physics.Raycast(transform.GetChild(1).GetChild(i).position + new Vector3(0, 0.45f, 0), new Vector3(x, 0, z), 0.61f) || Physics.Raycast(transform.GetChild(1).GetChild(i).position + new Vector3(0, -0.45f, 0), new Vector3(x, 0, z), 0.61f))
                return true;
        }

        return Physics.Raycast(transform.position + new Vector3(0, 0.45f, 0), new Vector3(x, 0, z), 0.61f) || Physics.Raycast(transform.position + new Vector3(0, -0.45f, 0), new Vector3(x, 0, z), 0.61f);
    }

    GameObject getObjectInDir(int x, int z)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, 0, 0), new Vector3(x, 0, z), out hit, 0.7f))
            return hit.rigidbody.gameObject;
        else
            return null;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class MineController : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            CameraShaker.Instance.ShakeOnce(7, 7, 0, 1);
            var exp = GetComponent<ParticleSystem>();
            exp.Play();
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Rigidbody>().detectCollisions = false;
            Debug.Log(GameObject.FindGameObjectsWithTag("Block").Length);
            foreach (GameObject other in GameObject.FindGameObjectsWithTag("Block"))
            {
                Debug.Log(Vector3.Distance(transform.position, other.transform.position));
                if (other.transform.parent == null && other.GetComponent<Rigidbody>() != null)
                if(Vector3.Distance(transform.position, other.transform.position) < 3f)
                {
                    other.GetComponent<BlockController>().isExploded = true;
                    other.GetComponent<Rigidbody>().useGravity = true;
                    other.GetComponent<Rigidbody>().drag = 0.05f;
                    other.GetComponent<Rigidbody>().angularDrag = 0.05f;

                    other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                   other.GetComponent<Rigidbody>().AddExplosionForce(1000, transform.position - new Vector3(0, 1.5f,0) + new Vector3(Random.value * 0.1f, Random.value * 0.1f, Random.value * 0.1f), 10);
                    
                }
            }
            Destroy(this.gameObject, exp.main.duration);
        }
    }

        // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

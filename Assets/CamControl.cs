using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour {

    Quaternion targetRotation;
    Vector3 targetPosition;
    public PlayerController player;

    // Use this for initialization
    void Start () {
        targetRotation = transform.rotation;
        targetPosition = new Vector3(0, 0, 0);
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        if(targetRotation == transform.rotation) {
		    if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                targetRotation *= Quaternion.Euler(Vector3.up * 90);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                targetRotation *= Quaternion.Euler(Vector3.up * -90);
            }
        }

        if(player.isGrounded())
        {
            targetPosition.y = player.transform.position.y - 0.5f;
        }
        
        transform.Rotate(0, Input.GetAxis("Mouse X") * 2,0 * Time.deltaTime * 60f,Space.World);
        //transform.Rotate(Input.GetAxis("Mouse Y") * 2, 0, 0, Space.Self);
        //transform.rotation = Quaternion.Euler(Mathf.Clamp(transform.rotation.eulerAngles.x, 10, 80), transform.rotation.eulerAngles.y, 0);



        //targetRotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 1, Vector3.up);
        //targetRotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * 1, Vector3.right);

        //Debug.Log(targetRotation.eulerAngles);


        //transform.rotation=Quaternion.RotateTowards(transform.rotation, targetRotation, 5);
        targetPosition = player.transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPosition, 1);
    }
}

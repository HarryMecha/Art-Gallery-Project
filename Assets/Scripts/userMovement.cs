using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userMovement : MonoBehaviour {

    public CharacterController controller; //This will reference the CharacterContoller object attach to the GameObject

    public float speed = 12f; //This will set the default speed at which the user will move at

    public Crosshair ch;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (ch.movement == true)
        {
            float x = Input.GetAxis("Horizontal"); //This will get the horizontal input
            float z = Input.GetAxis("Vertical"); //This will get the vertical input

            Vector3 move = transform.right * x + transform.forward * z; //This will move the user by taking the direction the user is facing and multiplying that by the input of the arrow keys

            controller.Move(move * speed * Time.deltaTime); //This will allow the CharacterController to move using the input and at the speed we set, time being used to move independent to the framerate
        }
	}
}

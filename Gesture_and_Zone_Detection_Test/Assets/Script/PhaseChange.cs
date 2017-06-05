using UnityEngine;
using System.Collections;


//This method PhaseChange gets data from KinectManager.cs and set up different phase values, which is used by other scripts.
public class PhaseChange : MonoBehaviour {

    //PhaseInput1 and PhaseInput2 are used to determine if the Kinect is getting data that switch phases
    public int PhaseInput1;
    public int PhaseInput2;
    //Phase is the actual value that determine the phase
    public int Phase;
    //Not used now
    public float test;
    //timer1 and timer2 are used to make sure that phase changes under the condition that the data getting from Kinect are consistent for over a certain amount of time    
    private float timer1;
    private float timer2;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate() {
        
        //Start the timer when getting data from Kinect
        if (PhaseInput1 == 1)
        {
            timer1 = timer1 + Time.deltaTime;
        }

        if (PhaseInput2 == 1)
        {
            timer2 = timer2 + Time.deltaTime;
        }

        //Dump the timer when no phase changing is happening
        if (PhaseInput1 == 0)
        {
            timer1 = 0;
        }

        if (PhaseInput2 == 0)
        {
            timer2 = 0;
        }

        //Trigger change on value of Phase if the Kinect streams consistent data over 1 second
        if (timer1 > 1)
        {
            Phase = 1;
        }

        if (timer2 > 1)
        {
            Phase = 2;
        }

	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZoneReaction : MonoBehaviour {

    public DetectPresence detectpresence;
    public Transformation transformation;
    public bool malegesture;
    public bool femalegesture;
    public float timer;
    public float timerb;
    public int timerTrigger;
    public PhaseChange PhaseChange;
    public GameObject player1;
    public GameObject player2;
    public GameObject player1translate;
    public GameObject player2translate;   
         
    private int Zone1_Trigger;
    private int Zone2_Trigger;
    private int Stoptrigger;
    private Animator anim1;
    private Animator anim2;
    private int Phase;
    private float timer1;
    private float timer2;
   

	// Use this for initialization
	void Start () {
        
        anim1 = player1.GetComponent<Animator>();
        anim2 = player2.GetComponent<Animator>();

    }	
	// Update is called once per frame
	void FixedUpdate() {
        
        //this tells the gameobject to face towards the person that is standing still in the kinect sensor range
        player1translate.transform.eulerAngles = new Vector3(0, detectpresence.FloatangleRadians, 0);
        player2translate.transform.eulerAngles = new Vector3(0, detectpresence.FloatangleRadians, 0);

        //Zone1_Trigger = detectpresence.Zone1_presence;
        //Zone2_Trigger = detectpresence.Zone2_presence;
        Stoptrigger = detectpresence.Stoptrigger;

        malegesture = transformation.MaleGesture;
        femalegesture = transformation.FemaleGesture;
        Phase = PhaseChange.Phase;
        // if viewer holds "male gesture" correctly and over 1 second, then the model plays the "maleReaction"

        if (Phase == 1)
        {
            if (malegesture)
            {
                timer1 = timer1 + Time.deltaTime;
            }

            if (malegesture != true)
            {
                timer1 = 0;
            }

            if (timer1 > 1)
            {
                if (timer == 0)
                {
                    anim1.Play("MaleReaction");
                    anim2.Play("MaleReaction");
                    timerTrigger = 1;
                }
            }

            if (femalegesture)
            {
                timer2 = timer2 + Time.deltaTime;
            }

            if (femalegesture != true)
            {
                timer2 = 0;
            }

            if (timer2 > 1)
            {
                if (timer == 0)
                {
                    anim1.Play("FemaleReaction");
                    anim2.Play("FemaleReaction");
                    timerTrigger = 1;
                }
            }
            // player will activate and play "scare" if viewer is standing still, DetectPresence.cs timer is defined
            if (Stoptrigger == 1)
            {
                if (timerb == 0)
                {
                    anim1.Play("Scare_L");
                    anim2.Play("Scare_L");
                    timerTrigger = 2;
                    detectpresence.Stoptrigger = 0;
                }
            }
            
            if (timerTrigger == 1)
            {
                if (timerb != 0)
                {
                    timer = timer + Time.deltaTime;
                    timerb = timerb + Time.deltaTime;
                }

                if (timerb == 0)
                {
                    timer = timer + Time.deltaTime;
                }


            }

            if (timerTrigger == 2)
            {
                if (timer != 0)
                {
                    timer = timer + Time.deltaTime;
                    timerb = timerb + Time.deltaTime;
                }

                if (timer == 0)
                {
                    timerb = timerb + Time.deltaTime;
                }
            }


            if (timerb > 2)
            {
                timerTrigger = 0;
                timerb = 0;
            }

            // timer causes a pause before the nexct reaction will activate
            if (timer > 5)
            {
                if (timerb != 0)
                {
                    timerTrigger = 2;
                    timer = 0;
                }

                if (timerb == 0)
                {
                    timerTrigger = 0;
                    timer = 0;
                }

            }
            // if once the leap forward animation has played, it is now returned to the 0 0 0 
            // state once its completed. Otherwise, it would loop, building the rotational radians turn
            if (anim1.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                player1.transform.position = new Vector3(0, 0, 0);
                player2.transform.position = new Vector3(0, 0, 0);
                player1.transform.rotation = new Quaternion(0, 0, 0, 0);
                player2.transform.rotation = new Quaternion(0, 0, 0, 0);
            }

        }

        if (Phase == 2)
        {
            if (malegesture)
            {
                timer1 = timer1 + Time.deltaTime;
            }

            if (malegesture != true)
            {
                timer1 = 0;
            }

            if (timer1 > 1)
            {
                if (timer == 0)
                {
                    anim1.Play("MaleReaction");
                    anim2.Play("MaleReaction");
                    timerTrigger = 1;
                }
            }
        }
        

    }


}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;
using System;

public class DetectPresence : MonoBehaviour
{
    public GameObject BodySrcManager;
    private BodySourceManager bodymanager;
    private Body[] bodies;
    public float Xdistance;
    public float Zdistance;
    public double Xposition;
    public double Zposition;
    public double oldXposition;
    public double oldZposition;
    public int Stoptrigger;
    public float FloatangleRadians;

    // Old method for square zone presence detection
    // if used, the min and max distances will need to be input based on set-up of kinect in the space
    //public float Zone1_timer;
    //public float Zone1_ZminDistance;
    //public float Zone1_ZmaxDistance;    
    //public float Zone1_XminDistance;
    //public float Zone1_XmaxDistance;
    //public int Zone1_trigger;
    //public int Zone1_presence;
    //public float Zone2_timer;
    //public float Zone2_ZminDistance;
    //public float Zone2_ZmaxDistance;
    //public float Zone2_XminDistance;
    //public float Zone2_XmaxDistance;
    //public int Zone2_trigger;
    //public int Zone2_presence;

    public double angleRadians;
    public GameObject player1;
    public GameObject player2;
    public float stoptimer;
    public float stoptimer2;
    public float stoptimer3;
    

    // Use this for initialization
    void Start()
    {
        if (BodySrcManager == null)
        {
            Debug.Log("Assign Gameobject with body source manager");
        }
        else
        {
            bodymanager = BodySrcManager.GetComponent<BodySourceManager>();
        }

        InvokeRepeating("Presence", 0.5f, 0.5f);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        angleRadians = Math.Atan2(Xdistance, Zdistance) * 100;
        FloatangleRadians = Convert.ToSingle(angleRadians) * -1;
 

        if (bodymanager == null)
        {
            return;
        }

        bodies = bodymanager.GetData();

        if (bodies == null)
        {
            // Old method for square zone presence detection
            //Zone1_trigger = 0;
            return;
        }
           

        foreach (var body in bodies)
        {
            if (bodies == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                Zdistance = body.Joints[JointType.Neck].Position.Z;
                Xdistance = body.Joints[JointType.Neck].Position.X;
                
                Zposition = Math.Round(Zdistance, 1);
                Xposition = Math.Round(Xdistance, 1);
                //if person is still more than 2 seconds
                if ((Zposition == oldZposition) & (Xposition == oldXposition))
                {
                    stoptimer = stoptimer + Time.deltaTime;
                    stoptimer3 = stoptimer3 + Time.deltaTime;
                }
                //if person is moving
                if ((Zposition != oldZposition) || (Xposition != oldXposition))
                {
                    stoptimer2 = stoptimer2 + Time.deltaTime;
                }
                // this script is a temporary fix, to have 2 players movements not interfere with each other's stop timer reads             
                if (stoptimer >2)
                {
                    Stoptrigger = 1;
                    stoptimer = 0;
                }

                if (stoptimer3 > 0.5)
                {
                    stoptimer2 = 0;
                    stoptimer3 = 0;
                }

                if (stoptimer2 > 1)
                {
                    stoptimer = 0;
                    stoptimer2 = 0;
                    stoptimer3 = 0;
                } 

                // Old method for square zone presence detection

                //if ((Zdistance > Zone1_ZminDistance) & (Zdistance < Zone1_ZmaxDistance) & (Xdistance > Zone1_XminDistance) & (Xdistance < Zone1_XmaxDistance))
                //{
                //    Zone1_trigger = 1;
                //}

                //if ((Zdistance > Zone2_ZminDistance) & (Zdistance < Zone2_ZmaxDistance) & (Xdistance > Zone2_XminDistance) & (Xdistance < Zone2_XmaxDistance))
                //{
                //    Zone2_trigger = 2;
                //}

                //if ((Zdistance < Zone1_ZminDistance) || (Zdistance > Zone1_ZmaxDistance))
                //{
                //    Zone1_trigger = 0;
                //}
                //if ((Xdistance < Zone1_XminDistance) || (Xdistance > Zone1_XmaxDistance))
                //{
                //    Zone1_trigger = 0;
                //}
                //if ((Zdistance < Zone2_ZminDistance) || (Zdistance > Zone2_ZmaxDistance))
                //{
                //    Zone2_trigger = 0;
                //}
                //if ((Xdistance < Zone2_XminDistance) || (Xdistance > Zone2_XmaxDistance))
                //{
                //    Zone2_trigger = 0;
                //}

                //if (Zone1_trigger == 1)
                //{
                //    Zone1_timer = Zone1_timer + Time.deltaTime;
                //}

                //if (Zone1_trigger == 0)
                //{
                //    Zone1_timer = 0;
                //}

                //if (Zone1_timer > 1)
                //{
                //    Zone1_presence = 1;
                //}

                //if (Zone1_timer == 0)
                //{
                //    Zone1_presence = 0;
                //}

                //if (Zone1_timer > 10)
                //{
                //    Zone1_trigger = 0;
                //}

                //if (Zone2_trigger == 2)
                //{
                //    Zone2_timer = Zone2_timer + Time.deltaTime;
                //}

                //if (Zone2_trigger == 0)
                //{
                //    Zone2_timer = 0;
                //}

                //if (Zone2_timer > 1)
                //{
                //    Zone2_presence = 1;
                //}

                //if (Zone2_timer == 0)
                //{
                //    Zone2_presence = 0;
                //}

                //if (Zone2_timer > 10)
                //{
                //    Zone2_trigger = 0;
                //}
            }
        }
                
    }    

    void Presence()
    {
        oldXposition = Xposition;
        oldZposition = Zposition;
    }

}


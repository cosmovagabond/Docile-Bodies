using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;
using System;

//The function of the method DetectPresence is to determine whether if there is people standing infront of the Kinect radar camera using the feedback data provided by Kinect
public class DetectPresence : MonoBehaviour
{
    //Variables for accessing Kinect
    public GameObject BodySrcManager;
    private BodySourceManager bodymanager;
    private Body[] bodies;
    //Variables to determine the distance between the recognized sketlon and Kinect
    public float Xdistance;
    public float Zdistance;
    public double Xposition;
    public double Zposition;
    public double oldXposition;
    public double oldZposition;
    //Variables used as timers to determine whether if a skelton is recognized long enough to be register as a presence, this is to prevent false positive
    public int Stoptrigger;
    public float stoptimer;
    public float stoptimer2;
    public float stoptimer3;
    //The two players were to test the functionality of Kinect tracking two different skeltons, they are not being used in this script right now, but they are being used in ZoneReaction.cs
    public GameObject player1;
    public GameObject player2;
    //These two variables are to calculate the angle between the person standing infront of Kinect and Kinect, they are accessed by ZoneReaction.cs
    public float FloatangleRadians;
    public double angleRadians;

    // Old method for square zone presence detection, bascially the skelton position needs to be inside of a square area inorder to be register as a presence
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




    // Use this for initialization
    void Start()
    {
        //if Kinect does not feed any data, debug log
        if (BodySrcManager == null)
        {
            Debug.Log("Assign Gameobject with body source manager");
        }
        //if Kinect is live then initiat BodySourceManager script
        else
        {
            bodymanager = BodySrcManager.GetComponent<BodySourceManager>();
        }
        //!!!I forgot why i put this here...
        InvokeRepeating("Presence", 0.5f, 0.5f);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //calculate the angle between the person standing infront of Kinect and Kinect
        angleRadians = Math.Atan2(Xdistance, Zdistance) * 100;
        FloatangleRadians = Convert.ToSingle(angleRadians) * -1;
 
        //if Kinect fails to provide any data, return and check again until it provides data
        if (bodymanager == null)
        {
            return;
        }

        //bodies is the number of how many skelton is being tracked by Kinect
        bodies = bodymanager.GetData();

        //if no skelton is being tracked, check again until one is tracked
        if (bodies == null)
        {
            // Old method for square zone presence detection
            //Zone1_trigger = 0;
            return;
        }
           
        //cycling through all the skeltons tracked by Kinect
        foreach (var body in bodies)
        {
            //if there's no skelton being tracked this happen, right now nothing happens
            if (bodies == null)
            {
                continue;
            }

            //if there are skeltons being tracked
            if (body.IsTracked)
            {
                //get and calculate position from Kinect data
                Zdistance = body.Joints[JointType.Neck].Position.Z;
                Xdistance = body.Joints[JointType.Neck].Position.X;
                
                Zposition = Math.Round(Zdistance, 1);
                Xposition = Math.Round(Xdistance, 1);

                //if person is still more than 2 seconds, stoptimer and stoptimer 3 starts
                if ((Zposition == oldZposition) & (Xposition == oldXposition))
                {
                    stoptimer = stoptimer + Time.deltaTime;
                    stoptimer3 = stoptimer3 + Time.deltaTime;
                }

                //if person is moving, stoptimer2 starts
                if ((Zposition != oldZposition) || (Xposition != oldXposition))
                {               
                    stoptimer2 = stoptimer2 + Time.deltaTime;
                }

                // the following is a temporary fix for that Kinect constantly stream two different sets of data when having 2 players recognized, with this fix the two data sets should not interfere with each other's stop timer reads             
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

    //I forgot what is this for
    void Presence()
    {
        oldXposition = Xposition;
        oldZposition = Zposition;
    }

}


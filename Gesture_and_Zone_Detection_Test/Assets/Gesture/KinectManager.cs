﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Windows.Kinect;
using System.Text;

public class KinectManager : MonoBehaviour
{
    public Text GestureTextGameObject;
    public Text ConfidenceTextGameObject;
    public Transformation Transformation;
    public PhaseChange phasechange;

    // Kinect 
    private KinectSensor kinectSensor;

    // color frame and data 
    private ColorFrameReader colorFrameReader;
    private byte[] colorData;
    private Texture2D colorTexture;

    private BodyFrameReader bodyFrameReader;
    private int bodyCount;
    private Body[] bodies;

    private string MaleGestureName = "Male_Gesture";
    private string FemaleGestureName = "Female_Gesture";
    private string Phase1Name = "Phase1";
    private string Phase2Name = "Phase2";



    // GUI output
    private UnityEngine.Color[] bodyColors;
    //private string[] bodyText;

    /// <summary> List of gesture detectors, there will be one detector created for each potential body (max of 6) </summary>
    private List<GestureDetector> gestureDetectorList = null;

    // Use this for initialization
    void Start()
    {
        // get the sensor object

        this.kinectSensor = KinectSensor.GetDefault();

        if (this.kinectSensor != null)
        {
            this.bodyCount = this.kinectSensor.BodyFrameSource.BodyCount;

            // color reader
            this.colorFrameReader = this.kinectSensor.ColorFrameSource.OpenReader();

            // create buffer from RGBA frame description
            var desc = this.kinectSensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Rgba);


            // body data
            this.bodyFrameReader = this.kinectSensor.BodyFrameSource.OpenReader();

            // body frame to use
            this.bodies = new Body[this.bodyCount];

            // initialize the gesture detection objects for our gestures
            this.gestureDetectorList = new List<GestureDetector>();
            for (int bodyIndex = 0; bodyIndex < this.bodyCount; bodyIndex++)
            {
                //PUT UPDATED UI STUFF HERE FOR NO GESTURE
                GestureTextGameObject.text = "none";
                //this.bodyText[bodyIndex] = "none";
                this.gestureDetectorList.Add(new GestureDetector(this.kinectSensor));
            }

            // start getting data from runtime
            this.kinectSensor.Open();
        }
        else
        {
            ;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // process bodies
        bool newBodyData = false;
        using (BodyFrame bodyFrame = this.bodyFrameReader.AcquireLatestFrame())
        {
            if (bodyFrame != null)
            {
                bodyFrame.GetAndRefreshBodyData(this.bodies);
                newBodyData = true;
            }
        }

        if (newBodyData)
        {

            // update gesture detectors with the correct tracking id
            for (int bodyIndex = 0; bodyIndex < this.bodyCount; bodyIndex++)
            {
                var body = this.bodies[bodyIndex];
                if (body != null)
                {
                    var trackingId = body.TrackingId;

                    // if the current body TrackingId changed, update the corresponding gesture detector with the new value
                    if (trackingId != this.gestureDetectorList[bodyIndex].TrackingId)
                    {
                        GestureTextGameObject.text = "none";
                        //this.bodyText[bodyIndex] = "none";
                        this.gestureDetectorList[bodyIndex].TrackingId = trackingId;

                        // if the current body is tracked, unpause its detector to get VisualGestureBuilderFrameArrived events
                        // if the current body is not tracked, pause its detector so we don't waste resources trying to get invalid gesture results
                        this.gestureDetectorList[bodyIndex].IsPaused = (trackingId == 0);
                        this.gestureDetectorList[bodyIndex].OnGestureDetected += CreateOnGestureHandler(bodyIndex);
                    }
                }
            }
        }

    }

    private EventHandler<GestureEventArgs> CreateOnGestureHandler(int bodyIndex)
    {
        return (object sender, GestureEventArgs e) => OnGestureDetected(sender, e, bodyIndex);
    }

    // this establishes the confidence level for recognizing the gesture
    private void OnGestureDetected(object sender, GestureEventArgs e, int bodyIndex)
    {
        var isDetected = e.IsBodyTrackingIdValid && e.IsGestureDetected;

        if (e.GestureID == MaleGestureName)
        {
            //NEW UI FOR GESTURE DETECTed
            GestureTextGameObject.text = "Gesture Detected: " + isDetected;
            //StringBuilder text = new StringBuilder(string.Format("Gesture Detected? {0}\n", isDetected));
            ConfidenceTextGameObject.text = "Confidence: " + e.DetectionConfidence;
            //text.Append(string.Format("Confidence: {0}\n", e.DetectionConfidence));
            // 0.65f = f is "float"  this number was put in by Cosmo, to establish the "wiggle room" for gesture confidence
            if (e.DetectionConfidence > 0.65f)
            {
                Transformation.MaleGesture = true;
            }
            else
            {
                Transformation.MaleGesture = false;
            }
        }

        if (e.GestureID == FemaleGestureName)
        {
            //NEW UI FOR GESTURE DETECTed
            GestureTextGameObject.text = "Gesture Detected: " + isDetected;
            //StringBuilder text = new StringBuilder(string.Format("Gesture Detected? {0}\n", isDetected));
            ConfidenceTextGameObject.text = "Confidence: " + e.DetectionConfidence;
            //text.Append(string.Format("Confidence: {0}\n", e.DetectionConfidence));
            if (e.DetectionConfidence > 0.65f)
            {
                Transformation.FemaleGesture = true;
            }
            else
            {
                Transformation.FemaleGesture = false;
            }
        }

        if (e.GestureID == Phase1Name)
        {
            //NEW UI FOR GESTURE DETECTed
            GestureTextGameObject.text = "Gesture Detected: " + isDetected;
            //StringBuilder text = new StringBuilder(string.Format("Gesture Detected? {0}\n", isDetected));
            ConfidenceTextGameObject.text = "Confidence: " + e.DetectionConfidence;
            //text.Append(string.Format("Confidence: {0}\n", e.DetectionConfidence));
            if (e.DetectionConfidence > 0.65f)
            {
                //phasechange.PhaseInput1 = 1;
            }
            else
            {
                //phasechange.PhaseInput1 = 0;
            }
        }
//if more gestures are added, this following paragraph can be copy/pasted
        if (e.GestureID == Phase2Name)
        {
            //NEW UI FOR GESTURE DETECTed
            GestureTextGameObject.text = "Gesture Detected: " + isDetected;
            //StringBuilder text = new StringBuilder(string.Format("Gesture Detected? {0}\n", isDetected));
            ConfidenceTextGameObject.text = "Confidence: " + e.DetectionConfidence;
            //text.Append(string.Format("Confidence: {0}\n", e.DetectionConfidence));
            if (e.DetectionConfidence > 0.65f)
            {
                phasechange.PhaseInput2 = 1;
            }
            else
            {
                phasechange.PhaseInput2 = 0;
            }
        }


        //this.bodyText[bodyIndex] = text.ToString();
    }

    //private void OnRightLeanGestureDetected(object sender, GestureEventArgs e, int bodyIndex)
    //{
    //    var isDetected = e.IsBodyTrackingIdValid && e.IsGestureDetected;

    //NEW UI FOR GESTURE DETECTed
    //    GestureTextGameObject.text = "Gesture Detected: " + isDetected;
    //StringBuilder text = new StringBuilder(string.Format("Gesture Detected? {0}\n", isDetected));
    //    ConfidenceTextGameObject.text = "Confidence: " + e.DetectionConfidence;
    //text.Append(string.Format("Confidence: {0}\n", e.DetectionConfidence));
    //    if (e.DetectionConfidence > 0.65f)
    //    {
    //        Transformation.FemaleGesture = true;
    //    }
    //    else
    //   {
    //        Transformation.FemaleGesture = false;
    //   }

    //this.bodyText[bodyIndex] = text.ToString();
    //}

    void OnApplicationQuit()
    {
        if (this.colorFrameReader != null)
        {
            this.colorFrameReader.Dispose();
            this.colorFrameReader = null;
        }

        if (this.bodyFrameReader != null)
        {
            this.bodyFrameReader.Dispose();
            this.bodyFrameReader = null;
        }

        if (this.kinectSensor != null)
        {
            if (this.kinectSensor.IsOpen)
            {
                this.kinectSensor.Close();
            }

            this.kinectSensor = null;
        }
    }

}


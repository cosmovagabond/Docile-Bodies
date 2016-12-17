using UnityEngine;
using System.Collections;

public class Transformation : MonoBehaviour
{

    public bool MaleGesture;
    public bool FemaleGesture;
    public DoubleSideShaderMorph DoubleSideShaderMorph_lower;
    public DoubleSideShaderMorph DoubleSideShaderMorph_upper;
    public DoubleSideShaderMorph1 DoubleSideShaderMorph1_lower;
    public DoubleSideShaderMorph1 DoubleSideShaderMorph1_upper;
    public PhaseChange PhaseChange;

    private float KeyCheck;
    public float timer1;
    public float dumptimer1;
    public float timer2;
    public float dumptimer2;
    private int Phase;

    // Use this for initialization
    void Start()
    {    

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Phase = PhaseChange.Phase;

        if (Phase == 1)
        {
            if (MaleGesture)
            {
                timer1 = timer1 + Time.deltaTime;
                dumptimer1 = 0;
            }
            //timer for how long the gesture must be held to trigger a "1"
            if (MaleGesture != true)
            {
                dumptimer1 = dumptimer1 + Time.deltaTime;
            }

            if (dumptimer1 > 0.5)
            {
                timer1 = 0;
            }
            // if timer is more than 1, then check to see if model is in "male state", if no, then...
            if (timer1 > 1)
            {
                if ((DoubleSideShaderMorph_lower.blendshape < 100) & (DoubleSideShaderMorph_lower.blendshape > 0) & (DoubleSideShaderMorph_upper.blendshape < 100) & (DoubleSideShaderMorph_upper.blendshape > 0))
                {
                    return;
                }

                else
                {
                    DoubleSideShaderMorph_lower.KeyCheck = 1;
                    DoubleSideShaderMorph_upper.KeyCheck = 1;
                    DoubleSideShaderMorph1_lower.KeyCheck = 1;
                    DoubleSideShaderMorph1_upper.KeyCheck = 1;
                }
            }

            if (FemaleGesture)
            {
                timer2 = timer2 + Time.deltaTime;
                dumptimer2 = 0;
            }

            if (FemaleGesture != true)
            {
                dumptimer2 = dumptimer2 + Time.deltaTime;
            }

            if (dumptimer2 > 0.5)
            {
                timer2 = 0;
            }

            // if timer is more than 1, then check to see if model is in "female state", if no, then...
            if (timer2 > 1)
            {
                if ((DoubleSideShaderMorph_lower.blendshape < 100) & (DoubleSideShaderMorph_lower.blendshape > 0) & (DoubleSideShaderMorph_upper.blendshape < 100) & (DoubleSideShaderMorph_upper.blendshape > 0))
                {
                    return;
                }

                {
                    DoubleSideShaderMorph_lower.KeyCheck = 2;
                    DoubleSideShaderMorph_upper.KeyCheck = 2;
                    DoubleSideShaderMorph1_lower.KeyCheck = 2;
                    DoubleSideShaderMorph1_upper.KeyCheck = 2;
                }
            }
        }


        if (Phase == 2)
        {
            DoubleSideShaderMorph1_lower.KeyCheck = 1;
            DoubleSideShaderMorph1_upper.KeyCheck = 1;
        }
        
    }
}

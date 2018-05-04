using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//The method of DoubleSideShaderMorph1 is modified from DoubleSideShaderMorph to achieve the function of a male model transforming into a female model
public class DoubleSideShaderMorph1 : MonoBehaviour {
    //KeyCheck is used in DoubleSideShaderMorph.cs and DoubleSideShaderMorph1.cs to control model transformation between male version and female version, 0 is default/initiate value, 1 is female to male process, 2 is male to female process
    public int KeyCheck;
    //speedcoefficient determines how fast the metamorphosis happens, speedcoefficient>=0
    public float speedcoefficient;
    //blendshape is the blendshape value accessed from the model, 0-(0-1)<=blendshape<=100+(0~1)
    public float blendshape;
    //Access PhhaseChange.cs
    public PhaseChange PhaseChange;

    //texturemorph is the transparency of the shader, 0<=texturemorph<=1
    private float texturemorph;
    //Access the shader of the object
    private Renderer rend;
    //blendshapespeed is the final value of how fast the metamorphosis happens after speedcoefficient is multiplied by 100 times so that the change speed is fast
    private float blendshapespeed;
    //Access the model's blendshape value
    private SkinnedMeshRenderer skinnedMeshRenderer;
    //textureprogress is used to calculate the value of texturemorph
    private float textureprogress;
    //Phase is the value that determine the what event to happen under different values, its value is extracted from PhaseChange.cs
    private int Phase;

    //Set default values to variables and assign variables to accessed scripts
    void Start () {
        //Access PhaseChange.cs to extract the value of its Phase to local
        Phase = PhaseChange.Phase;
        //Assign default value to Keycheck
        KeyCheck = 0;
        //Access the value of user input changespeed and multiply it by 100 times to make it work to the script
        blendshapespeed = 100 * speedcoefficient;
        //Assign rend to access the shader of the object
        rend = GetComponent<Renderer>();
        //This was used to make sure that the transparent value of the shader was assigned, but is no longer needed
        //rend.material.shader = Shader.Find("Ciconia Studio/Double Sided/Transparent/Diffuse Bump");
        //Assign skinnedMeshRenderer to access the model's blendshape value
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    // Check the value of KeyCheck every frame to confirm the transformation (blendshapes and texture)
    void FixedUpdate() {
        //For debug purpose, allows keyboard input to change the value of KeyCheck
        if (Input.GetKeyDown("1"))
        {
            KeyCheck = 1;
        }

        if (Input.GetKeyDown("2"))
        {
            KeyCheck = 2;
        }

        //The defalut Phase, right now there's only onw phase
        if (Phase == 1)
        {
            //KeyCheck is 1 which means the process is to transform to male model and texture from female ones
            if (KeyCheck == 1)
            {
                //If the blendshape value of the model is over 100, which means the model is already in male state, then reset the KeyCheck and textureprogress
                //It should be blendshape >= 0
                if (blendshape > 100)
                {
                    KeyCheck = 0;
                    textureprogress = 0;
                }
                //If it's not in male state, then this happens
                else
                {
                    //Change the blendshape value based on deltaTime from 0 to 100
                    blendshape = blendshape + blendshapespeed * Time.deltaTime;
                    //Assign the value to the model
                    skinnedMeshRenderer.SetBlendShapeWeight(0, blendshape);
                    //Change the textureprogress value based on deltaTime
                    textureprogress = textureprogress + speedcoefficient * Time.deltaTime;
                    //Transparency value goes from 0 to 1, transparency value of Female_Torso_Texture_2's shader should be on 0 when the model is in female states, it should be on 1 when the model is in male states
                    //This is the part that is different from DoubleSideShaderMorph.cs
                    texturemorph = Mathf.Lerp(0f, 1f, textureprogress);
                    //Assign the value of shader transparency value to the shader
                    rend.material.SetFloat("_Transparency", texturemorph);
                }
            }
            //KeyCheck is 2 which means the process is to transform to female model and texture from male ones
            if (KeyCheck == 2)
            {
                //If the blendshape value of the model is below 100, which means the model is already in female state, then reset the KeyCheck and textureprogress
                //It should be blendshape <= 0
                if (blendshape < 0)
                {
                    KeyCheck = 0;
                    textureprogress = 0;
                }
                //If it's not in male state, then this happens
                else
                {
                    //Change the blendshape value based on deltaTime from 100 to 0
                    blendshape = blendshape - blendshapespeed * Time.deltaTime;
                    //Assign the value to the model
                    skinnedMeshRenderer.SetBlendShapeWeight(0, blendshape);
                    //Change the textureprogress value based on deltaTime
                    textureprogress = textureprogress + speedcoefficient * Time.deltaTime;
                    //Transparency value goes from 1 to 0, transparency value of Female_Torso_Texture_2's shader should be on 0 when the model is in female states, it should be on 1 when the model is in male states
                    //This is the part that is different from DoubleSideShaderMorph.cs
                    texturemorph = Mathf.Lerp(1f, 0f, textureprogress);
                    //Assign the value of shader transparency value to the shader
                    rend.material.SetFloat("_Transparency", texturemorph);
                }
            }
        }
        // not filled in yet, waiting for determined activity to be put into production
        if (Phase == 2)
        {

        }  
	
	}
}

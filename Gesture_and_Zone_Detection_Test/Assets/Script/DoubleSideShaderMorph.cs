using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoubleSideShaderMorph : MonoBehaviour {

    public int KeyCheck;
    public float speedcoefficient;
    public float blendshape;
    public PhaseChange PhaseChange;

    private float texturemorph;
    private Renderer rend;
    private float blendshapespeed;
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private float textureprogress;
    private int Phase;

    // Use this for initialization
    void Start () {
        
        Phase = PhaseChange.Phase;
        KeyCheck = 0;
        blendshapespeed = 100 * speedcoefficient;
        rend = GetComponent<Renderer>();
        //rend.material.shader = Shader.Find("Ciconia Studio/Double Sided/Transparent/Diffuse Bump");
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        
	}
	
	// Update is called once per frame
    // use keystrokes 1 or 2 to test the transformation (blendshapes and texture)
	void FixedUpdate() {

        if (Input.GetKeyDown("1"))
        {
            KeyCheck = 1;
        }

        if (Input.GetKeyDown("2"))
        {
            KeyCheck = 2;
        }



        if (Phase == 1)
        {
            if (KeyCheck == 1)
            {
                if (blendshape > 100)
                {
                    KeyCheck = 0;
                    textureprogress = 0;
                }

                else
                {
                    blendshape = blendshape + blendshapespeed * Time.deltaTime;
                    skinnedMeshRenderer.SetBlendShapeWeight(0, blendshape);

                    textureprogress = textureprogress + speedcoefficient * Time.deltaTime;
                    texturemorph = Mathf.Lerp(1f, 0f, textureprogress);
                    rend.material.SetFloat("_Transparency", texturemorph);
                }
            }

            if (KeyCheck == 2)
            {
                if (blendshape < 0)
                {
                    KeyCheck = 0;
                    textureprogress = 0;
                }

                else
                {
                    blendshape = blendshape - blendshapespeed * Time.deltaTime;
                    skinnedMeshRenderer.SetBlendShapeWeight(0, blendshape);

                    textureprogress = textureprogress + speedcoefficient * Time.deltaTime;
                    texturemorph = Mathf.Lerp(0f, 1f, textureprogress);
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

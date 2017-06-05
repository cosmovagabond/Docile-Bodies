using UnityEngine;
using System.Collections;

public class PhaseChange : MonoBehaviour {

    public int PhaseInput1;
    public int PhaseInput2;
    public int Phase;
    public float test;

    private float timer1;
    private float timer2;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate() {

        if (PhaseInput1 == 1)
        {
            timer1 = timer1 + Time.deltaTime;
        }

        if (PhaseInput2 == 1)
        {
            timer2 = timer2 + Time.deltaTime;
        }

        if (PhaseInput1 == 0)
        {
            timer1 = 0;
        }

        if (PhaseInput2 == 0)
        {
            timer2 = 0;
        }

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

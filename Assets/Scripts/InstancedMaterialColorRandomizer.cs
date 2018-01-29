using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstancedMaterialColorRandomizer : MonoBehaviour {
    public Gradient colorRange;

	// Use this for initialization
	void Start () {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        MaterialPropertyBlock mpb = new MaterialPropertyBlock();

        mpb.SetColor("_Color", Random.ColorHSV(0.0f,1.0f,0.0f, 1f, 0.25f, 0.75f));

        //mpb.SetColor("_Color", colorRange.Evaluate(Random.value));

        meshRenderer.SetPropertyBlock(mpb);
	}
	
}

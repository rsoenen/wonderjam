using UnityEngine;
using System.Collections;



public class HeadOnBeat : MonoBehaviour {

	public SpectrumComputer spectrum;
	private float pb = 0.0f;

	// Use this for initialization
	void Start () {
	
	}



	// Update is called once per frame
	void Update () {
		double ticklen = 2.5/70.0;

		double row_unit = ticklen * 2.0;
		double poffset = 6*row_unit;
		double psize = row_unit * 64.0;

		int pattern;
		int row;


		if (spectrum != null)
		{
			if (spectrum.trackTime < poffset)
			{
				pattern = 0;
				row = 0;
			}
			else
			{
				pattern = Mathf.FloorToInt((float)((spectrum.trackTime - poffset) / psize));
				row = Mathf.FloorToInt((float)((spectrum.trackTime - poffset - pattern*psize) /row_unit));
				pattern += 1; 
			}
			Vector3 p = transform.localPosition;
			Vector3 r = transform.localRotation.eulerAngles;
			//int u = 12;
			//float b = spectrum.spectrumL[u] + spectrum.spectrumR[u];
			//b = Mathf.Lerp(pb,b,0.5f);

			//p.y = b*2.0f;
		
			if (row==48 && ((pattern>0 && pattern<5) || (pattern>17 && pattern<22)))
			{
				p.y = .2f;
				r.y = -90.0f;
			}
			else
			{
				p.y *= 0.9f * (1.0f - Time.deltaTime);
				r.y *= 0.9f * (1.0f - Time.deltaTime);


			}
			Debug.Log("pattern " + pattern + " row " + row);
			transform.localPosition = p;
			transform.localRotation = Quaternion.Euler(r);
			//pb = b;

		}
		else
		{
		}
	}
}

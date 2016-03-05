using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class SpectrumComputer : MonoBehaviour {

	public float[] spectrumL = new float[512];
	public float[] spectrumR = new float[512];
	public double trackTime=0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<AudioSource>().GetSpectrumData(spectrumL,0,FFTWindow.Triangle);
		GetComponent<AudioSource>().GetSpectrumData(spectrumR,1,FFTWindow.Triangle);
		trackTime = GetComponent<AudioSource>().time;
	}
}

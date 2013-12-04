using UnityEngine;
using System.Collections;

public class AxisFollow : MonoBehaviour {
	
	public GameObject followTarget;	//object to be followed
	
	public bool xFollow;	//whether to follow target along x axis
	public bool yFollow;	//whether to follow target along y axis
	
	private float xPrevious;	//target object's penultimate x value
	private float yPrevious;	//target object's penultimate y value
	
	private float xTarget;	//target object's current x value
	private float yTarget;	//target object's current y value
	
	Vector3 pos;	//location of the self object open for editing
	
	// runs once at start
	void Awake () {
		xPrevious = followTarget.transform.position.x;	//get initial target x
		yPrevious = followTarget.transform.position.y;	//get initial target ys
		
	}
	
	// Update is called once per frame
	void Update () {
		pos = transform.position; //get current self object position
		
		//following on x axis , moves if target object changed x
		xTarget = followTarget.transform.position.x;
		if (xFollow && (xTarget != xPrevious)) {
			pos.x = xTarget;	//set self x to target object's x value
			xPrevious = xTarget;	//log current target x value for later comparisons
		}
		
		//following on y axis
		yTarget = followTarget.transform.position.y;
		if (yFollow && (yTarget != yPrevious)) {
			pos.y = yTarget;	//set self y to target object's y value
			yPrevious = yTarget;	//log current target y value for later comparisons
		}
		
		transform.position = pos;	//write changes in position to object
	}

	//accessors
	void setYFollow(bool followState){
		yFollow = followState;
	}
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {

	public LayerMask collisionMask;

	private BoxCollider collider;
	private Vector3 s;	//collider's size
	private Vector3 c;	//collider's center
	
	private float skin = .005f;

	[HideInInspector]
	public bool grounded;	//whether the cube is on the ground or not
	[HideInInspector]
	public bool movementStopped;	//whether the cube is touching something on the side or not

	//Ray for raycasting
	Ray ray;
	RaycastHit hit;

	void Start() {
		collider = GetComponent<BoxCollider> ();	//find the collider

		//quick abbreviations for calculations later
		s = collider.size;
		c = collider.center;
	}

	public void Move(Vector2 moveAmount){

		float deltaY = moveAmount.y;
		float deltaX = moveAmount.x;
		Vector2 p = transform.position;

		//Check for collisions above and below
		grounded = false;

		for (int i = 0; i < 3; i ++) {
			float dir = Mathf.Sign(deltaY);
			float x = (p.x + c.x - s.x/2) + s.x/2 * i; //left, center and then right point of collider
			float y = p.y + c.y + s.y/2 * dir; //Bottom of collider

			ray = new Ray(new Vector2(x,y), new Vector2(0,dir));

			if (Physics.Raycast(ray , out hit, Mathf.Abs(deltaY) + skin, collisionMask))	{
				// Get distance between player and ground
				float dst = Vector3.Distance(ray.origin, hit.point);

				// Stop player's downwards movement after coming within skin width of a collider
				if (dst > skin){
					deltaY = dst * dir - skin * dir;
				}else{
					deltaY = 0;
				}
				grounded = true;
				break;
			}
		}

		//Check for collisions to the right and left
		movementStopped = false;

		for (int i = 0; i < 3; i ++) {
			float dir = Mathf.Sign(deltaX);
			float x = p.x + c.x + s.x/2 * dir; //low, mid and top points of cube
			float y = p.y + c.y - s.y/2 + s.y/2 * i;
			
			ray = new Ray(new Vector2(x,y), new Vector2(dir,0));
			
			if (Physics.Raycast(ray , out hit, Mathf.Abs(deltaX) + skin, collisionMask))	{
				// Get distance between player and side object
				float dst = Vector3.Distance(ray.origin, hit.point);
				
				// Stop player's sideways movement after coming within skin width of a collider
				if (dst > skin){
					deltaX = dst * dir - skin * dir;
				}else{
					deltaX = 0;
				}
				movementStopped = true;
				break;
			}
		}

		Vector2 finalTransform = new Vector2 (deltaX, deltaY);	//make a vector2 from transformations

		transform.Translate (finalTransform);	//apply the translation
	}
}

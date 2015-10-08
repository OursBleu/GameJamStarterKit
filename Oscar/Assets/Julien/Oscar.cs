using UnityEngine;
using System.Collections;

public class Oscar : MonoBehaviour {
	public Animator anim;
	public GameObject visage;
	public GameObject FireBall;
	public GameObject placeToFire;
	public float speed = 2;
	public float jumpForce=100;
	public float ratioRun=2;
	public float fireballPower=100;
	private float speedRun;
	private int Horizontal=0; 
	private int Xmove;
	private int Jump=0;
	private bool onGrounded = true;
	private bool fireball=false;
	
	// Use this for initialization
	void Start () {
		Cursor.visible=false;
	}
	
	// Update is called once per frame
	void Update () {
		//gestion de la marche
		Horizontal = (int)Input.GetAxisRaw("Horizontal");
		GetComponent<Rigidbody2D>().AddForce(Vector2.right*Horizontal*speed);
		//gestion du saut
		if(Input.GetButtonDown("Jump") && onGrounded==true)
		{
			GetComponent<Rigidbody2D>().AddForce(Vector2.up*jumpForce);
		}

		//gestion de la course
		if(Input.GetButtonDown("Run")){
			speedRun*=ratioRun;
		}
		if(Input.GetButtonUp("Run")){
			speedRun/=ratioRun;
		}
		//se retourner automatiquement quand on change de direction
		if(Horizontal<0){transform.localScale=new Vector3(-Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);}
		if(Horizontal>0){transform.localScale=new Vector3(Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);}
		//if(Horizontal==0){GetComponent<Rigidbody2D>().velocity=Vector2.zero;} // aucune glissade autorise!


		//quand le personange tombe
		if(GetComponent<Rigidbody2D>().velocity.y<0){Jump=-1;}

		//fireball
		fireball = Input.GetButtonDown("Fire");
		if(fireball){
			GameObject fb = (GameObject)Instantiate(FireBall,placeToFire.transform.position,visage.transform.rotation);
			if(transform.localScale.x>0){
				fb.GetComponent<Rigidbody2D>().AddRelativeForce(Vector3.right*fireballPower);
			}
			if(transform.localScale.x<0){
				fb.GetComponent<Rigidbody2D>().AddRelativeForce(Vector3.left*fireballPower);
			}
		
		}
		//code que je n'ai pas fait et que donc par consequent je ne comprend rien (sert entre autre a  suivre la souris de la tete)

		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		Vector3 dir = Input.mousePosition - pos;
		float angle = Mathf.Atan2(dir.y, 100) * Mathf.Rad2Deg;

		visage.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 


		//incorporations des variable dans le controller d'animation
		anim.SetInteger("Xmove",Horizontal);
		anim.SetInteger("Jump",Jump);
		anim.SetFloat("speedRun",speedRun);
		anim.SetBool("fireball",fireball);
		
	}


	void OnCollisionStay2D(Collision2D c){
		if (c.gameObject.tag=="sol"){
			onGrounded=true;
			Jump=0;
		}

	}

	void OnCollisionExit2D(Collision2D c){
		if (c.gameObject.tag=="sol"){
			onGrounded=false;
			Jump=1;
		}
		
	}


	
}

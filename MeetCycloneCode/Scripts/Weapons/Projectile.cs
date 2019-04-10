using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public LayerMask collisionMask; 
	protected enum Target{ship,player,enemy};
	[SerializeField]
	protected Target target; 

	public float damage = 0.1f;
	public float energyCost;

	protected float speed = 10f;	
	protected float lifetime = 2f; 
	private float skinWidth = 0.2f;
	protected GameObject closestGameObject;

    List<GameObject> Enemies = new List<GameObject>();

	protected Ship ship;
	protected Player player;

    protected void Awake()
    {
        ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<Ship>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    protected virtual void Start() {}

    private void OnEnable()
    {
        Enemies.Clear();
        Enemies.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        closestGameObject = StaticMethods.ClosestTargetToPlayerWithinRange(Enemies, player.transform.position);
        Invoke("HideProjectile", lifetime);

        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, 1f, collisionMask);
        if (initialCollisions.Length > 0)
        {
            OnHitObject(initialCollisions[0], transform.position); // first thing it collides with
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void SetSpeed(float _newSpeed){
		speed = _newSpeed;
	}

	void Update () {
		LaunchProjectile();
	}

	protected void CheckCollisions (float _moveDistance) {
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, _moveDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide)) {
			OnHitObject(hit.collider, hit.point); // hit.point changed in Enemy to be the transform.position - so hit.point does nothing
		}
	}

	public virtual void LaunchProjectile(){

		float moveDistance = speed * Time.deltaTime;
		CheckCollisions (moveDistance);
			
		if(closestGameObject != null){
			transform.position = Vector3.MoveTowards(transform.position, closestGameObject.transform.position, moveDistance);

			Vector3 relativePos = closestGameObject.transform.position - transform.position;

			if (relativePos != Vector3.zero) {
				Quaternion rotation = Quaternion.LookRotation(relativePos);
				transform.rotation = rotation;
			}
		} else {
            HideProjectile();
		}
	}

    void OnHitObject(Collider _c, Vector3 _hitPoint) { // _hitpoint changed in Enemy Script to be the transform.position - so _hitpoint does nothing anymore
		IDamageable damageableObject = _c.GetComponent<IDamageable>();

		if (damageableObject != null){
			damageableObject.TakeHit(damage, _hitPoint, transform.forward);
		}

        HideProjectile();
	}

    void HideProjectile(){
        gameObject.SetActive(false);
    }
}
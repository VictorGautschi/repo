using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : LivingEntity {	

	public enum State {Idle, Chasing, Attacking}; // Do you need this?

	private float lastDamageTime;

	public float speed;
	[Tooltip("percentage of ship's shield or health that is deducted when this enemy attacks it")]
	public float damagePerc = 1;
	public int destroyValue = 50;
	public int destroyPoints = 50;
	[Tooltip("distance from target that enemy will start the close combat attack")]
	public float attackDistanceThreshold = 2f;
	[Tooltip("The length of the attack animation after slowing it down or speeding it up, not to be confused with the throwing animation for ranged units")]
	public float timeBetweenAttacks; 

	[Header("Boss Details")]
	public bool isBoss;
	public float healthRegenerateTimeGap;
	public float healthRegenerateRate;

	[Header("Walkable Regions")]
	public bool ignoreUnwalkable = false; // future development for flying creatures

	[Header("Speed Change When Hit")]
	public bool increaseSpeed = false;
	public float speedChangeFactor = 1f;
	public float speedUpTime = 1f;

	[Header("Particle Systems")]
	public ParticleSystem deathEffect;

    [Header("Audio Systems")]
    public AudioClip enemyDieAudio;

	const float minPathUpdateTime = 0.2f;
	const float pathUpdateMoveThreshold = 0.5f;

	[HideInInspector]
	public Transform target;
	[HideInInspector]
	public bool isBabyEnemy;

	protected State currentState;
	protected Player player;
	protected Ship ship;
	protected Path path;
	protected Material skinMaterial;
	protected LivingEntity targetEntity;  
	protected Color originalColor;
	protected Animator anim;
	protected bool hasTarget; 
	protected bool followingPath;
	protected float nextAttackTime;
	protected float turnSpeed = 1000f; // for future projects
	protected float turnDst = 0f; // for future projects
	protected float sqrDstToTarget;
    protected CreditManager creditManager;
    protected SoundEffectsManager soundEffectsManager;

	[Header("Health Bar")]
	public Image healthBar;

	[SerializeField]
	private Color fullColor;

	[SerializeField]
	private Color lowColor;

	[SerializeField]
	private bool lerpColors;

	protected virtual void Awake(){
		player = FindObjectOfType<Player>();
		ship = FindObjectOfType<Ship>();
        creditManager = CreditManager.Instance();
        soundEffectsManager = SoundEffectsManager.Instance();
	}

	protected override void Start () {}

    protected virtual void Update () {}

	public override void TakeHit(float _damage, Vector3 _hitPoint, Vector3 _hitDirection) { 
		if(_damage >= currentHealth){
			//Destroy(Instantiate(deathEffect.gameObject, _hitPoint, Quaternion.FromToRotation(Vector3.forward, _hitDirection)) as GameObject, deathEffect.main.startLifetimeMultiplier); // changed from deathEffect.startLifetime
            Instantiate(deathEffect.gameObject, transform.position, Quaternion.identity); // changed from deathEffect.startLifetime
            
            creditManager.AddCredit(destroyValue);

		}
		base.TakeHit(_damage,_hitPoint,_hitDirection);
	}

	public override void TakeDamage(float _damage) { 

		if (currentHealth >= startingHealth && increaseSpeed == true) {
			StartCoroutine(SpeedUp());
		}

		currentHealth -= _damage; 

		healthBar.fillAmount = currentHealth / startingHealth;

		if (lerpColors) {
			healthBar.color = Color.Lerp(lowColor, fullColor, healthBar.fillAmount);
		}

		if (currentHealth <= 0 && !dead) { // not already dead
			Die();
            soundEffectsManager.audioSource.PlayOneShot(enemyDieAudio);
		}

		lastDamageTime = Time.time;
	}

	IEnumerator SpeedUp(){
		speed *= speedChangeFactor;
		/* To do: Activate some sort of particle effect to make the speed up look good */
		yield return new WaitForSeconds(speedUpTime);
		speed /= speedChangeFactor;
	}

	public IEnumerator RegenerateHealthOverTime() {
		while(true){
			if(Time.time >= (lastDamageTime + healthRegenerateTimeGap) && currentHealth < startingHealth){
				
				currentHealth += healthRegenerateRate; 

				if(currentHealth > startingHealth)
					currentHealth = startingHealth;
				
				healthBar.fillAmount = currentHealth / startingHealth;

				yield return new WaitForSeconds(1f);
			} else {
				yield return null;
			}
		}
	}

	protected void OnTargetDeath(){ 
		hasTarget = false; // when on death fires on target/player, then no target exists, so false
		currentState = State.Idle;
		// lose condition 
	}

	public IEnumerator UpdatePath() {
		if(Time.timeSinceLevelLoad < .3f){
			yield return new WaitForSeconds (.3f);
		}
		PathRequestManager.RequestPath(new PathRequest(transform.position,target.position,OnPathFound), ignoreUnwalkable);

		float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
		Vector3 targetPosOld = target.position;
        Vector3 transformPosOld = transform.position;
        //float positionCheckTime = 1f;

		while (hasTarget) {
			if (target != null){
				if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold) {
					PathRequestManager.RequestPath(new PathRequest(transform.position,target.position,OnPathFound), ignoreUnwalkable);
					targetPosOld = target.position;
				}

                //// Added to try stop the Enemy A's from getting stuck on the map
                //if (Time.time > positionCheckTime){
                    
                //    if ((transform.position == transformPosOld))
                //    {
                //        PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound), ignoreUnwalkable);
                //        transformPosOld = transform.position;
                //    }

                //    positionCheckTime = Time.time + 1f;
                //}
			}
			yield return null;
		}
	}

	public void OnPathFound (Vector3[] _waypoints, bool _pathSuccessful) {
		if (_pathSuccessful && transform != null) {
			path = new Path(_waypoints, transform.position, turnDst);
			if(gameObject != null) {
				StopCoroutine("FollowPath");
				StartCoroutine("FollowPath");
			}
		} 
	}

	protected virtual IEnumerator FollowPath() {
		followingPath = true;
		int pathIndex = 0;

		if(path != null && path.lookPoints.Length > 0) {

			transform.LookAt(path.lookPoints[0]);
			float speedPercent = 1f;

			while (followingPath) {
				Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
				while (path.turnBoundaries[pathIndex].HasCrossedLine (pos2D)) {
					if (pathIndex == path.finishLineIndex){
						followingPath = false;
						break;
					} else {
						pathIndex++;
					}
				}

				if (followingPath) {
					Vector3 direction = path.lookPoints[pathIndex] - transform.position;
					transform.rotation = Quaternion.identity; // Rotation is dealt with in EnemyLook - identity = no rotation.
					transform.Translate(Vector3.Normalize(direction) * Time.deltaTime * speed * speedPercent, Space.Self);
				} 
				yield return null;
			}
		}
		yield return null;
	}

	protected virtual IEnumerator Attack() {
		yield return null;
	}

	public void OnDrawGizmos() {
		if (path != null && displayUnitGizmos) {
			path.DrawWithGizmos();
		}
	}
}


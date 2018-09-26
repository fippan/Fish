using UnityEngine;

public class Submarine : Health
{

    private bool hasEnabled = false;
    public bool diveBack = false;
    private float health = 150f;

    public Transform playerPos;
    public Transform spawnpos;
    public GameObject explosionFX;
	// Use this for initialization
	protected override void Start () {
        base.Start();
        spawnpos = FindObjectOfType<DiverManager>().transform;
        Vector3 targetdir = transform.position - spawnpos.position;
        //Vector3 newDir = Vector3.RotateTowards(transform.right, targetdir, 100f, 0f);
        //transform.rotation = Quaternion.LookRotation(newDir);
        //transform.rotation = Quaternion.Euler(0, targetdir.x * targetdir.z, 0);
        transform.LookAt(new Vector3(spawnpos.position.x, transform.position.y, spawnpos.position.z));
    }
	
	// Update is called once per frame
	void Update () {
        if(transform.position.y < -1f)
        {
            if(!diveBack)
            {
                transform.position += new Vector3(0, 1 * Time.deltaTime, 0);
            }
        }
        else
        {
            if(!hasEnabled)
            {
                var tempEnemy = GetComponentInChildren<SubmarineEnemy>().canFire = true;
                GetComponent<AudioController>().PlayOneShot("Ping", transform.position);
                hasEnabled = true;
            }
        }

        if(diveBack)
        {
            transform.position += new Vector3(0, 1 * -Time.deltaTime, 0);
            Destroy(gameObject, 4f);
        }
	}

    public override void TakeDamage(float amount, Vector3 point)
    {
        health -= amount;
        audioController.Play("Hit", point);

        if (health <= 0)
        {
            diveBack = true;
            KillCountManager.Instance.AddKill();
        }
    }


}

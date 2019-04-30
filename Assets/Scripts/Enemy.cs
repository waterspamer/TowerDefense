using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public float startSpeed = 10f;

    [HideInInspector]
    public float speed = 10f;

    private Transform target;
    private int wpIndex = 0;

    public int valueForKill = 30;

    private GameObject GameMaster;

    public GameObject DestroyEffect;

    public bool IsBoss = false;

    private float _health;

    public Image healthBar;

    private float startHealth = 1;
        
    public float GetHealth()
    {
        return _health;
    }

    public void SetHealth(float health)
    {
        startHealth = health;
        _health = health;
    }

	void Start ()
	{
        GameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        target = Waypoints.points[0];
		speed = startSpeed;
	}
    void GetNextWayPoint()
    {
        if (wpIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        wpIndex++;
        target = Waypoints.points[wpIndex];
    }

    public void HealthLogic(float damage)
    {
        
        _health -= damage;
        healthBar.fillAmount = _health / startHealth;
        healthBar.color = new Color(1 - _health/startHealth, _health/startHealth, 0);
        if (_health <= 0) OnDeath();

    }

    private void OnDeath()
    {
        ShopManager.instance.EarnLogic(valueForKill);
        if (IsBoss) GameManager.instance.WinLogic();
        Instantiate(DestroyEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void Update()
    {
        
        var dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
    }

}

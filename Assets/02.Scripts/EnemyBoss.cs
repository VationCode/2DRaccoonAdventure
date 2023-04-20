using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBoss : MonoBehaviour
{
	public enum State
    {
		Idle,
		Chase,
		Die
    }

	public State state;

	public float speed;
	public AudioClip hitSound;
	public AudioClip dieSound;
	public Slider slider_hp;
	public ParticleSystem PS_die;
	public ParticleSystem PS_hit;
    public SpriteRenderer image_exclamation;
	public Canvas canvas_boss;

    Rigidbody2D rigidbody2d;
	Vector2 direction = Vector2.right;
	Vector2 originPos;

	Animator animator;
	AudioSource audioSource;

	public float maxHP;
	float hp;

	public float chaseStartDistance;
	public float chaseEndDistance;

	IEnumerator Start()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();

		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();

		hp = maxHP;
		slider_hp.maxValue = maxHP;
		slider_hp.value = hp;

		originPos = transform.position;

		ChangeState(State.Chase);

		canvas_boss.enabled = true;
		yield return new WaitForSeconds(5f);
		canvas_boss.enabled = false;
	}

	void ChangeState(State newState)
    {
		switch (newState)
        {
			case State.Idle:
				image_exclamation.enabled = false;
				break;
			case State.Chase:
				image_exclamation.enabled = true;
				break;
			case State.Die:
				image_exclamation.enabled = false;
				slider_hp.transform.parent.gameObject.SetActive(false);
				Instantiate(PS_die, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
				Destroy(gameObject, 5f);
				break;
		}

		state = newState;
    }

	void FixedUpdate()
	{
		float playerDistance = Vector3.Distance(transform.position, Player.Instance.transform.position);

		switch (state)
        {
			case State.Idle:
				animator.SetTrigger("Idle");

				if (playerDistance < chaseStartDistance)
                {
					ChangeState(State.Chase);
                }
				break;
			case State.Chase:
				animator.SetTrigger("Walk");
				direction = (Player.Instance.transform.position - transform.position).normalized;

				if (direction.x > 0)
                {
					transform.localEulerAngles = new Vector3(0, 0, 0);
                } else
                {
					transform.localEulerAngles = new Vector3(0, 180, 0);
				}

				rigidbody2d.MovePosition(rigidbody2d.position + direction * speed * Time.deltaTime);
				break;
			case State.Die:
				animator.SetTrigger("Die");
				rigidbody2d.simulated = false;

				break;
        }

		slider_hp.transform.rotation = Quaternion.identity;
	}

	public void OnDamaged(float damage, Vector2 pos)
    {
		hp -= damage;

		if (hp <= 0)
        {
			ChangeState(State.Die);
			hp = 0;
        } else
        {
			Instantiate(PS_hit, pos, Quaternion.identity);
		}

		audioSource.PlayOneShot(hitSound);

		slider_hp.value = hp;
    }

	void OnCollisionStay2D(Collision2D other)
	{
		Player controller = other.collider.GetComponent<Player>();

		if (controller != null)
			controller.ChangeHealth(-1);
	}
}

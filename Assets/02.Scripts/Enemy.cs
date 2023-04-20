using System;
using UnityEngine;

/// <summary>
/// This class handle Enemy behaviour. It make them walk back & forth as long as they aren't fixed, and then just idle
/// without being able to interact with the player anymore once fixed.
/// </summary>

public enum ENEMYTYPE
{
	Robot,
	Robot3,
	Boss
}

public class Enemy : MonoBehaviour
{
	public ENEMYTYPE enemyType;

	public float speed;
	public float timeToChange;
	public bool horizontal;

	public GameObject smokeParticleEffect;
	public ParticleSystem fixedParticleEffect;

	public AudioClip hitSound;
	public AudioClip fixedSound;

	Rigidbody2D rigidbody2d;
	float remainingTimeToChange;
	Vector2 direction = Vector2.right;
	bool repaired = false;

	Animator animator;
	AudioSource audioSource;

	void Start ()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
		remainingTimeToChange = timeToChange;

		direction = horizontal ? Vector2.right : Vector2.down;

		animator = GetComponent<Animator>();

		audioSource = GetComponent<AudioSource>();
	}
	
	void Update()
	{
		if(repaired)
			return;
		
		remainingTimeToChange -= Time.deltaTime;
		int rand = UnityEngine.Random.Range(0, 4);

		if (remainingTimeToChange <= 0)
		{
			remainingTimeToChange += timeToChange;

			if (enemyType == ENEMYTYPE.Robot3)
			{
				switch (rand)
				{
					case 0:
						direction = Vector2.right;
						break;
					case 1:
						direction = Vector2.left;
						break;
					case 2:
						direction = Vector2.up;
						break;
					case 3:
						direction = Vector2.down;
						break;
				}
			}
			else
				direction *= -1;
		}

		animator.SetFloat("ForwardX", direction.x);
		animator.SetFloat("ForwardY", direction.y);

		//안좋은 방법이지만, 잔머리 사용. 꼭 플레이어가 죽인다 해서, 플레이어의 공격=>에너미일 필요는 없음
		//특수 키를 누르면, 해당 스크립트를 가지고 있는 것들이 죽으면, 이론상 동일/유사한 기능을 가짐.
		if (Input.GetKeyDown(KeyCode.K))
		{
			Destroy(gameObject);
		}

		
	}

	void FixedUpdate()
	{
		rigidbody2d.MovePosition(rigidbody2d.position + direction * speed * Time.deltaTime);
	}

	void OnCollisionStay2D(Collision2D other)
	{
		if(repaired)
			return;
		
		Player controller = other.collider.GetComponent<Player>();
		
		if(controller != null)
			controller.ChangeHealth(-1);
	}

	public void Fix()
	{
		animator.SetTrigger("Fixed");
		repaired = true;
		
		smokeParticleEffect.SetActive(false);

		Instantiate(fixedParticleEffect, transform.position + Vector3.up * 0.5f, Quaternion.identity);

		rigidbody2d.simulated = false;
		
		audioSource.Stop();
		audioSource.PlayOneShot(hitSound);
		audioSource.PlayOneShot(fixedSound);
	}
}

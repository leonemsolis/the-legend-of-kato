﻿#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum JumpType { FIRST, SECOND, NONE };

public class PlayerController : MonoBehaviour 
{
	[SerializeField] Sprite rightSprite;
	[SerializeField] Sprite leftSprite;

	Rigidbody2D rb;
	SpriteRenderer spriteRenderer;
	BoxCollider2D boxCollider;

	JumpType jumpType = JumpType.FIRST;
	const float jumpForce = 1600f;

	bool facingRight = true;

	const float legDistance = 25f;
	const float minJumpAllowDistance = 60f;
	const float moveForce = 29200f;
	const float maxSpeed = 80f;

	private void Awake ()
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();
		rb = GetComponent<Rigidbody2D> ();
		boxCollider = GetComponent<BoxCollider2D> ();
		//Time.timeScale = .2f;
	}

	private void FixedUpdate ()
	{
		if (facingRight) {
			rb.AddForce (Vector2.right * moveForce);
		} else {
			rb.AddForce (Vector2.left * moveForce);
		}
		if (Mathf.Abs (rb.velocity.x) > maxSpeed) {
			rb.velocity = new Vector2 (Mathf.Sign (rb.velocity.x) * maxSpeed, rb.velocity.y);
		}

		Vector2 originLeft = new Vector2 (transform.position.x - legDistance, transform.position.y - .5f);
		Vector2 originRight = new Vector2 (transform.position.x + legDistance, transform.position.y - .5f);
		Vector2 direction = Vector2.down;
		LayerMask collisionMask = 1 << LayerMask.NameToLayer ("Ground");

		RaycastHit2D leftHit = Physics2D.Raycast (originLeft, direction, minJumpAllowDistance, collisionMask);
		RaycastHit2D rightHit = Physics2D.Raycast (originRight, direction, minJumpAllowDistance, collisionMask);
		//Debug.DrawLine(originLeft, originLeft + direction * minJumpAllowDistance, Color.red);
		//Debug.DrawLine(originRight, originRight + direction * minJumpAllowDistance, Color.red);

		if ((leftHit || rightHit) && rb.velocity.y <= 0) {
			jumpType = JumpType.FIRST;
		}
	}

	private void MoveLeft ()
	{
		if (facingRight) {
			facingRight = false;
			spriteRenderer.sprite = leftSprite;
		}
	}

	private void MoveRight ()
	{
		if (!facingRight) {
			facingRight = true;
			spriteRenderer.sprite = rightSprite;
		}
	}

	public void ChangeDirection ()
	{
		if (facingRight) {
			MoveLeft ();
		} else {
			MoveRight ();
		}
	}

	public void Jump ()
	{
		switch (jumpType) {
		case JumpType.SECOND:
			jumpType = JumpType.NONE;
			rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
			break;
		case JumpType.FIRST:
			jumpType = JumpType.SECOND;
			rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
			break;
		case JumpType.NONE:
			break;
		}
	}

	public bool IsFacingRight ()
	{
		return facingRight;
	}
}
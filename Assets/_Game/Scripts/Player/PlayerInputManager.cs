using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
	[Header("Character Input Values")]
	public Vector2 move;
	public bool jump;
	public bool attack;
	public bool shoot;

	public void OnMove(InputValue value)
	{
		MoveInput(value.Get<Vector2>());
	}

	public void OnJump(InputValue value)
	{
		JumpInput(value.isPressed);
	}

	public void OnAttack(InputValue value)
	{
		AttackInput(value.isPressed);
	}

	public void OnShoot(InputValue value)
    {
		ShootInput(value.isPressed);
    }


	public void MoveInput(Vector2 newMoveDirection)
	{
		move = newMoveDirection;
	}
	public void JumpInput(bool newJumpState)
	{
		jump = newJumpState;
	}
	public void AttackInput(bool newAttackState)
	{
		attack = newAttackState;
	}

	public void ShootInput(bool newShootState)
    {
		shoot = newShootState;
    }
}


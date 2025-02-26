using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(CharacterController))]
public class PointClickMovement : MonoBehaviour {
	[SerializeField] private Transform target;
	public float rotSpeed = 15.0f;
	public float moveSpeed = 6.0f;
	public float jumpSpeed = 15.0f;
	public float gravity = -9.8f;
	public float terminalVelocity = -10.0f;
	public float minFall = -1.5f;
	public float deceleration = 20.0f;
	public float targetBuffer = 1.5f;

	private float _curSpeed = 0f;
	private float _vertSpeed;

	private Vector3 _targetPos = Vector3.one;
	private CharacterController _charController;
	private ControllerColliderHit _contact;
	//private Animator _animator;

	// Use this for initialization
	void Start () {
		_vertSpeed = minFall;
		_charController = GetComponent<CharacterController> ();
		//_animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 movement = Vector3.zero;
		if (Input.GetMouseButton (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit mouseHit;
			if (Physics.Raycast (ray, out mouseHit)) {
				GameObject hitObject = mouseHit.transform.gameObject;
				if (hitObject.layer == LayerMask.NameToLayer ("Ground")) {
					_targetPos = mouseHit.point;
					_curSpeed = moveSpeed;
				}
			}
		}
			if (_targetPos != Vector3.one) {
				Vector3 adjustedPos = new Vector3 (_targetPos.x,
					                     transform.position.y, _targetPos.z);
				Quaternion targetRot = Quaternion.LookRotation (
					                      adjustedPos - transform.position);
				transform.rotation = Quaternion.Slerp (transform.rotation,
					targetRot, rotSpeed * Time.deltaTime);

				movement = _curSpeed * Vector3.forward;
				movement = transform.TransformDirection (movement);
				if (Vector3.Distance (_targetPos, transform.position) < targetBuffer) {
					_curSpeed -= deceleration * Time.deltaTime;
					if (_curSpeed <= 0) {
						_targetPos = Vector3.one;
					}
				}
			}
			//_animator.SetFloat ("Speed", movement.sqrMagnitude);

			bool hitGround = false;
			RaycastHit hit;
			if (_vertSpeed < 0 &&
			   Physics.Raycast (transform.position, Vector3.down, out hit)) {
				float check =
					(_charController.height + _charController.radius) / 1.9f;
				hitGround = hit.distance <= check;
			}

			if (hitGround) {
				if (Input.GetButtonDown ("Jump")) {
					_vertSpeed = 0f;
				} else {
					_vertSpeed = minFall;
				}
			} else {
				_vertSpeed += gravity * 5 * Time.deltaTime;
				if (_vertSpeed < terminalVelocity) {
					_vertSpeed = terminalVelocity;
				}

				if (_charController.isGrounded) {
					if (Vector3.Dot (movement, _contact.normal) < 0) {
						movement = _contact.normal * moveSpeed;
					} else {
						movement += _contact.normal * moveSpeed;
					}
				}
			}
			movement.y = _vertSpeed;

			movement *= Time.deltaTime;
			_charController.Move (movement);
		}
	void OnControllerColliderHit (ControllerColliderHit hit) {
		_contact = hit;
	}
}

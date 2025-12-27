using Darkmatter.Core;
using Darkmatter.Domain;
using System;
using UnityEngine;
using VContainer;

namespace Darkmatter.Presentation
{
    public class PlayerMotor : MonoBehaviour, IPlayerPawn
    {

        [Header("LookSetting")]
        public Camera mainCamera { get; private set; }
        public Transform cinemachineFollowTarget;

        [Header("MoveSetting")]
        public CharacterController characterController;

        [Header("GravitySetting")]
        private float verticalVelocity;
        public bool isGrounded => IsOnGround();

        [Header("GroundCheckSensorSetting")]
        public float groundOffset;
        public float groundCheckRadius;
        public LayerMask groundLayer;

        [Header("TurnSetting")]
        public float turnSpeed = 5f;
        public float smoothing = 10f;
        [Header("AnimationSetting")]
        public Animator animator;

        [Header("AimSetting")]
        public Transform aim;
        Vector3 mouseWorldPos = Vector3.zero;
        public Transform muzzlePos;
        public Gun gun;
        public float fireRate = 0.1f;
        float nextFiretime=0;


        [Inject]  private IInputReader inputReader;
        [Inject] private PlayerConfigSO playerConfig;


        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            if(animator==null) animator = GetComponent<Animator>();
            mainCamera = Camera.main;

        }
        private void Update()
        {
            //HandleAim();
          HandleShooting();
        }

        private void LateUpdate()
        {
            
        }
        private void HandleShooting()
        {
            float shootingWeight = animator.GetLayerWeight(1);
            float targetWeight = inputReader.isShooting ? 1 : 0;
            float setshootingWeight = Mathf.Lerp(shootingWeight, targetWeight, Time.deltaTime * 5);
            animator.SetLayerWeight(1, setshootingWeight);
            animator.SetBool("IsShooting", inputReader.isShooting);

            if (!inputReader.isShooting) return;
            if(Time.time>=nextFiretime)
            {
                nextFiretime = Time.time + fireRate;
                gun.Init(muzzlePos.position, aim.position);
            }
        }

        private void HandleAim()
        {
            Vector2 screenPoint = new Vector2(Screen.width/2, Screen.height/2);
            Ray ray = mainCamera.ScreenPointToRay(screenPoint);
            if(Physics.Raycast(ray,out RaycastHit hitPoint, 100f,groundLayer))
            {
                mouseWorldPos= hitPoint.point;
               aim.position = mouseWorldPos;
              //aim.position = Vector3.Lerp(aim.position, hitPoint.point, Time.deltaTime * smoothing);
            }

            Vector3 aimDir = (mouseWorldPos - transform.position).normalized;
            aimDir.y = 0; //
            Quaternion targetRot = Quaternion.LookRotation(aimDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * turnSpeed);
        }


        //state based functions
        public void Move(Vector3 motion)
        {
            ApplyGravity(); //apply gravity before moving
            Vector3 finalMove = motion;
            finalMove.y = verticalVelocity;
            characterController.Move(finalMove * Time.deltaTime);
        }

        public void SetCameraRotation(float pitch, float yaw)
        {
            cinemachineFollowTarget.rotation = Quaternion.Euler(pitch, yaw, 0);
            HandleAim();
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,yaw,0), Time.deltaTime*turnSpeed);
        }

        public void Jump(float jumpForce)
        {
            verticalVelocity = jumpForce;
        }

        public bool IsOnGround()
        {
            Vector3 groundPos = transform.position + Vector3.down * groundOffset;

            return Physics.CheckSphere(
                groundPos,
                groundCheckRadius,
                groundLayer,
                QueryTriggerInteraction.Ignore
            );

        }
        public void ApplyGravity()
        {
            if (isGrounded && verticalVelocity < 0f)
            {
                verticalVelocity = -2f; // snap to ground
            }
            verticalVelocity += playerConfig.gravity * Time.deltaTime;
        }
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Vector3 groundPos = transform.position + Vector3.down * groundOffset;
            Gizmos.DrawWireSphere(groundPos, groundCheckRadius);
        }
    }


}

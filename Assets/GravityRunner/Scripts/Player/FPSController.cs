using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    [Header("Move Parameters")]
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public Transform playerFeetPosition;
    public float feetSkinRadius = 0.05f;
    public LayerMask ignoredLayerMask;

    [Header("Look Parameters")]
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    [Header("Player Interaction")]
    public float maxInteractDistance = 1.5f; 
    public LayerMask interactableLayers; // Max interaction distance
    public GameObject interactionUI;
    public KeyCode interactKey = KeyCode.F;

    [Header("Flags")]
    public bool canMove = true;
    private bool isFreeze = false;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private Camera lastCam;


    CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ignoredLayerMask = ~ignoredLayerMask;
    }

    void Update()
    {
        //Debug.Log(moveDirection.y);
        if(isFreeze)
        {
            return;
        }

        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion

        #region Handles Jumping
        bool isTouchingGroud = Physics.CheckSphere(playerFeetPosition.position, feetSkinRadius, ignoredLayerMask, QueryTriggerInteraction.Ignore);// make sure to ignore triggers
        /*
        if (!canMove)
            return;
        if (isTouchingGroud)
        {
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpPower * -1 * Mathf.Sign(gravity);
                print("jump, " + moveDirection.y.ToString());
            }
            else
            {
                moveDirection.y = movementDirectionY;
                print("move, " + moveDirection.y.ToString());
            }
        }
        else
        {
            moveDirection.y += gravity * Time.deltaTime;
            print("fall, " + moveDirection.y.ToString());
        }
        */
        
        if (Input.GetButton("Jump") && canMove && isTouchingGroud)
        {           
            moveDirection.y = jumpPower * -1 * Mathf.Sign(gravity);
            //print("jump, " + moveDirection.y.ToString());
        }
        else
        {
            if (Mathf.Abs(movementDirectionY) > Mathf.Abs(gravity))
            {
                movementDirectionY = gravity;
            }
            moveDirection.y = movementDirectionY;
            //print("move, " + moveDirection.y.ToString());
        }

        if (!isTouchingGroud)
        {       
            moveDirection.y += gravity * Time.deltaTime;
            //print("fall, " + moveDirection.y.ToString());
        }
        

        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        if (Camera.main) // Making sure it is active
        {
            Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0); // Apply camera rotation
        }
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0); // Apply body rotation including camera

        #endregion

        /*
        #region PlayerInteraction
        IInteractable tempInteractable;
        if (CanPlayerInteract(out tempInteractable))
        {
            interactionUI.SetActive(true); // Show interaction UI
            if (Input.GetKeyDown(interactKey)) // Press key to interact
            {
                if (tempInteractable != null)
                {
                    tempInteractable.Interact(this);
                }
            }
        }
        else
        {
            interactionUI.SetActive(false);
        }  
        #endregion
        */
    }

    bool CanPlayerInteract(out IInteractable result)
    {
        Ray ray;
        result = null;
        if (Camera.main) 
        {
            ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        }
        else 
        {
            
            return false; // Can't interact during cutscene
        }
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxInteractDistance, interactableLayers))
        {

            GameObject hitGameObject = hit.collider.gameObject;
            IInteractable interactable;
            hitGameObject.TryGetComponent<IInteractable>(out interactable);
            if (interactable != null)
            {
                result = interactable;
                return true;
            }                  
        }
        return false;
    }

    public void PlayerFreeze()
    {
        isFreeze = true;
        interactionUI.SetActive(false);
    }

    public void PlayerUnfreeze()
    {
        isFreeze = false;
    }
}
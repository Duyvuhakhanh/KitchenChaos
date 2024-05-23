using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    public static Player Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    public BaseCounter selectedClearCounter { get; private set; }
    public event EventHandler<OnSelectedCounterChangeArg> OnSelectedCounterChange;
    public class OnSelectedCounterChangeArg : EventArgs
    {
        public BaseCounter clearCounter;
    }
    private Vector3 lastMoveDir;
    private bool isWalking;
    private KitchenObject kitchenObject;
    // Update is called once per frame
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selectedClearCounter != null) selectedClearCounter.InteractAlternate(this);
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedClearCounter != null) selectedClearCounter.Interact(this);

    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }
    private void HandleInteraction()
    {
        float raycastDistance = 2f;
        if(Physics.Raycast(transform.position, lastMoveDir, out RaycastHit hitInfo, raycastDistance, interactLayer))
        {
            if (hitInfo.transform.TryGetComponent(out BaseCounter clearCounter))
            {
                SetSelectedCounter(clearCounter);
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }

    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetInputVectorNomarlized();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        isWalking = moveDir != Vector3.zero;
        if (moveDir != Vector3.zero)
        {
            lastMoveDir = moveDir;
        }
        float playerSize = .7f;
        float playerRadius = transform.localScale.x / 2;
        float moveDistance = Time.deltaTime * moveSpeed;

        transform.forward = Vector3.Slerp(transform.forward, lastMoveDir, Time.deltaTime * rotateSpeed);

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerSize, playerRadius, moveDir, moveDistance);
        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerSize, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerSize, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    moveDir = Vector3.zero;
                }

            }
        }

        transform.position += moveDir * moveDistance;
    }
    private void SetSelectedCounter(BaseCounter baseCounter)
    {
        if(selectedClearCounter != baseCounter)
        {
            OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangeArg { clearCounter = baseCounter });
            selectedClearCounter = baseCounter;
        }
    }
    public bool IsWalking()
    {
        return isWalking;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public void ClearKitchenObject()
    {
        if(kitchenObject != null) kitchenObject = null;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public Transform GetFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }
}

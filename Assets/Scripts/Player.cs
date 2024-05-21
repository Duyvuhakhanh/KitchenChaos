using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask interactLayer;
    public static Player Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    public ClearCounter selectedClearCounter { get; private set; }
    public event EventHandler<OnSelectedCounterChangeArg> OnSelectedCounterChange;
    public class OnSelectedCounterChangeArg : EventArgs
    {
        public ClearCounter clearCounter;
    }
    private Vector3 lastMoveDir;
    private bool isWalking;
    // Update is called once per frame
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedClearCounter != null) selectedClearCounter.Interact();

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
            if (hitInfo.transform.TryGetComponent(out ClearCounter clearCounter))
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
    private void SetSelectedCounter(ClearCounter clearCounter)
    {
        if(selectedClearCounter != clearCounter)
        {
            OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangeArg { clearCounter = clearCounter });
            selectedClearCounter = clearCounter;
        }
    }
    public bool IsWalking()
    {
        return isWalking;
    }
}

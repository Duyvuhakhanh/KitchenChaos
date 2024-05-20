using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask interactLayer;
    private bool isWalking;
    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }
    private void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GetInputVectorNomarlized();
        float raycastDistance = 2f;
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        if(Physics.Raycast(transform.position, moveDir, out RaycastHit hitInfo, raycastDistance, interactLayer))
        {
            if (hitInfo.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        }
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetInputVectorNomarlized();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        isWalking = moveDir != Vector3.zero;

        float playerSize = .7f;
        float playerRadius = transform.localScale.x / 2;
        float moveDistance = Time.deltaTime * moveSpeed;

        transform.forward = Vector3.Lerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

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
                    //moveDir = Vector3.zero;
                }

            }
        }

        transform.position += moveDir * moveDistance;
    }
    public bool IsWalking()
    {
        return isWalking;
    }
}

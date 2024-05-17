using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Player player;
    private static string IS_WALKING = "IsWalking";
    private Animator playerAnimator;
    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (playerAnimator != null)
        {
            playerAnimator.SetBool(IS_WALKING, player.IsWalking());
        }
    }
}

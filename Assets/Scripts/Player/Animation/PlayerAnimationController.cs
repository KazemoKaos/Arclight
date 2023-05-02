using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimationController : MonoBehaviour
{
    Animator playerAnimations;  // This is swapped out depending on the equipped weapon
    public float returnSpeed;
    public float aimReturnSpeed;
    public float dampMovement;

    int actionLayer;

    bool aiming;


    private void Awake()
    {
        playerAnimations = GetComponent<Animator>();

        actionLayer = playerAnimations.GetLayerIndex("Action Layer");
    }

    private void Update()
    {
        playerAnimations.SetFloat("VertRecoil", 0, returnSpeed, Time.deltaTime);
        playerAnimations.SetFloat("HorzRecoil", 0, returnSpeed, Time.deltaTime);

        if (aiming) { playerAnimations.SetFloat("Aiming", 1, aimReturnSpeed, Time.deltaTime); }
        else if(playerAnimations.GetFloat("Aiming") != 0) { playerAnimations.SetFloat("Aiming", 0, aimReturnSpeed, Time.deltaTime); }
    }

    // Walk Animation
    public void PlayWalkAnimation(Vector3 movement)
    {
        playerAnimations.SetFloat("Movement", Mathf.Clamp01(Mathf.Abs(movement.x) + Mathf.Abs(movement.z)), dampMovement, Time.deltaTime);
    }
    
    // Jump Animation
    public void PlayJumpAnimation()
    {
        playerAnimations.Play("Jump", actionLayer);
    }

    // Attack Animation. (x = vertical, y = horizontal)
    public void PlayAttackAnimation(Vector2 recoil)
    {
        playerAnimations.SetFloat("VertRecoil", recoil.x);
        playerAnimations.SetFloat("HorzRecoil", Mathf.Clamp(recoil.y, -15, 15));
    }

    // Reload Animation
    public void PlayReloadAnimation(float reloadSpeed)
    {
        playerAnimations.SetFloat("ReloadMult", reloadSpeed);
        playerAnimations.Play("Reload");
    }

    // ADS Animation
    public void PlayAimAnimation(float aimSpeed)
    {
        aiming = !aiming;
        aimReturnSpeed = aimSpeed;
    }

    // Land Animation
    public void PlayLandAnimation()
    {
        playerAnimations.Play("Land", actionLayer);
    }

    // Holster Animation
    public void PlayHolsterAnimation(float readyTime)
    {
        playerAnimations.SetFloat("ReadyMult", readyTime);
        playerAnimations.Play("Holster");
    }

    // Draw Animation
    public void PlayDrawAnimation(float readyTime)
    {
        playerAnimations.SetFloat("ReadyMult", readyTime);
        playerAnimations.Play("Draw");
    }

    void ChangeAnimator(RuntimeAnimatorController anim)
    {
        playerAnimations.runtimeAnimatorController = anim;
    }

    private void OnEnable()
    {
        PlayerWeaponInventory.Draw += PlayDrawAnimation;
        PlayerWeaponInventory.Holster += PlayHolsterAnimation;
        PlayerMovement.movement += PlayWalkAnimation;
        PlayerJump.jumpAction += PlayJumpAnimation;
        AbstractWeapon.attack += PlayAttackAnimation;
        AbstractWeapon.reload += PlayReloadAnimation;
        AbstractWeapon.animatorChange += ChangeAnimator;
        AbstractWeapon.aim += PlayAimAnimation;
        PlayerStates.Landed += PlayLandAnimation;
    }

    private void OnDisable()
    {
        PlayerWeaponInventory.Draw -= PlayDrawAnimation;
        PlayerWeaponInventory.Holster -= PlayHolsterAnimation;
        PlayerMovement.movement -= PlayWalkAnimation;
        PlayerJump.jumpAction -= PlayJumpAnimation;
        AbstractWeapon.attack -= PlayAttackAnimation;
        AbstractWeapon.reload -= PlayReloadAnimation;
        AbstractWeapon.animatorChange -= ChangeAnimator;
        AbstractWeapon.aim -= PlayAimAnimation;
        PlayerStates.Landed -= PlayLandAnimation;
    }
}

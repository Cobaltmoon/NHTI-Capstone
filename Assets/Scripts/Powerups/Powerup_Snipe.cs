﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///summary
 /*
Developers and Contributors: Ian Cahoon
    
Information
    Name: Snipe
    Type: Active
    Effect: Zoom in for AbilityTime(float), for the duration of the ability, your basic projectiles become hitscan
            Lower fire rate, higher damage, and a cool lightning zappy zap effect
    Tier: Rare
    Description: Basic fire turns into hitscan lightning bolts with increase damage but decreased fire rate
  */
///summary

namespace Powerups
{
    public class Powerup_Snipe : ActiveAbility
    {
        private float CDstart;
        bool onCooldown = false, CurrentlyActive = false;
        PlayerShoot pShoot;


        public override void OnAbilityAdd()
        {
            // Set name
            Name = "Snipe";
            Debug.Log(Name + " Added");

            // Add new shoot function to delegate 
            pShoot = GetComponent<PlayerShoot>();                                        
            if (pShoot)
            {
                Debug.Log("Snipe Added to Shoot Delegate");
                pShoot.shoot += OnShoot;
            }
        }
        public override void OnUpdate()
        {

        }
        public override void OnAbilityRemove()
        {
            // Remove shoot delegate
            if (pShoot)
            {
                pShoot.shoot -= OnShoot;
            }
            pShoot = null;

            // Call base function
            base.OnAbilityRemove();
        }
        public override void Activate()
        {
            throw new NotImplementedException();
        }
        public void OnShoot()
        {
            if (onCooldown)
            {
                Debug.Log("This ability is on cooldown" + "Start Time: " + CDstart);
                return;
            }
            //Debug.Log("Raycast Shot");
            GameObject rayOrigin = GameObject.Find("BasicPlayer/Gun"); //Needs to be changed to local player when networked
            Vector3 mp = Input.mousePosition;
            mp.z = 10;
            Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(mp);
            Vector3 targetVector = mouseLocation;

            Ray snipeRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit endPoint;

            if (Physics.Raycast(snipeRay, out endPoint))
            {
                Debug.Log(endPoint.transform.gameObject.name);
                targetVector = endPoint.point;

            }
            else
            {
                Debug.Log("no object was hit");
            }


            StartCoroutine(VisualizeRaycast(rayOrigin, targetVector));
        }
        IEnumerator VisualizeRaycast(GameObject Origin, Vector3 targetLocation)
        {
            StartCoroutine(TriggerCoolDown());

            LineRenderer snipeLaser = Origin.GetComponent<LineRenderer>();
            snipeLaser.SetPosition(0, Origin.transform.position);
            snipeLaser.SetPosition(1, targetLocation);

            snipeLaser.enabled = true;
            yield return new WaitForSeconds(.2f);
            snipeLaser.enabled = false;

        }
        IEnumerator TriggerCoolDown()
        {
            onCooldown = true;
            CDstart = Time.fixedTime;
            yield return new WaitForSeconds(5);
            onCooldown = false;
        }

    }
}

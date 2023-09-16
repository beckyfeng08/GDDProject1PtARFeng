using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : Ability
{
    public override void Use(Vector3 spawnPos)
    {
        RaycastHit hit;
        //we want to scale the particle system by the siz e and length of the laser in how far its reaching to hit the enemy
        float newLength = m_Info.Range;
        if (Physics.SphereCast(spawnPos, 0.5f, transform.forward, out hit, m_Info.Range))
        {
            newLength = (hit.point - spawnPos).magnitude;
            if (hit.collider.CompareTag("Enemy")) {
                hit.collider.GetComponent<EnemyController>().DecreaseHealth(m_Info.Power);
            }
                
        }
        var emitterShape = cc_PS.shape;
        emitterShape.length = newLength;
        cc_PS.Play();

    }
}

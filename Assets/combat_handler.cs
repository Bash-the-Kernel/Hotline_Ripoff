using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combat_handler : MonoBehaviour
{
    public static bool Melee_combat_handler(Transform attacker, Transform victim, string mask_name)
    {

        int mask = 1 << LayerMask.NameToLayer(mask_name);
        mask |= 1 << LayerMask.NameToLayer("Default");
        RaycastHit2D hit = Physics2D.Raycast(attacker.position, attacker.up, 1.5f, mask);
        if (victim.tag != null)
        {
            if (hit.collider.tag == victim.tag)
            {
                //Debug.Log(hit.collider.tag);
                return true;
            }
            else
            {
                //Debug.Log(hit.collider.tag);
                return false;
            }
        }
        else
        {
            return false;
        }

        
    }  
}

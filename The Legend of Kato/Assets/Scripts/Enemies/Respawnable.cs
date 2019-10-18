using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnable: MonoBehaviour
{

    protected bool running = true;

    public virtual void Respawn()
    {
        running = true;
    }

    public void Die()
    {
        running = false;
    }
}

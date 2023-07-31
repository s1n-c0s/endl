using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnParticleToPool : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}

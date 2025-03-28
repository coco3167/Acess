using System;
using UnityEngine;

namespace Platformer
{
    public class PlatformerObstacle : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!Options.Instance.godMode && other.CompareTag("Player"))
                other.GetComponent<PlatformerController>().HitObstacle();
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class EndLine : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Audio_Manager.instance.FinishDinoRun();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

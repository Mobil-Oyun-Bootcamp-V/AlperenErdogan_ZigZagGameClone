using System;
using UnityEngine;

namespace Player
{
    public class PlayerColliderController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem collectableCollisionParticle;
        [SerializeField]
        private Transform particlePoint;
        private void OnTriggerEnter(Collider other)
        {
            // Ball and diamonds collision detect
            if (other.GetComponent<CollectableController>() != null)
            {
                GameManager.Instance.ONCollectableCollision?.Invoke();
                //instantiating particle effect when collided
                var instantiated = Instantiate(collectableCollisionParticle, particlePoint.transform.position, Quaternion.identity);
                Destroy(instantiated.gameObject,collectableCollisionParticle.main.duration);
                other.GetComponent<CollectableController>().Die();
            }
        }
    }
}

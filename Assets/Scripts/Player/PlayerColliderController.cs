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
            if (other.GetComponent<CollectableController>() != null)
            {
                GameManager.Instance.ONCollectableCollision?.Invoke();
                var instantiated = Instantiate(collectableCollisionParticle, particlePoint.transform.position, Quaternion.identity);
                Destroy(instantiated.gameObject,collectableCollisionParticle.main.duration);
                other.GetComponent<CollectableController>().Die();
            }
        }
    }
}

using UnityEngine;

namespace TestScene
{
    public abstract class TestScript : MonoBehaviour
    {
        [SerializeField] protected float limit = 0.3f; 
        private void Start()
        {
            Debug.Log("Started");
        }

        protected abstract void Update();
    }
}
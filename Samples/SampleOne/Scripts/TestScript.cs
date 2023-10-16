using UnityEngine;

namespace TestScene
{
	public abstract class TestScript : MonoBehaviour
	{
		[SerializeField] protected float limit = 0.3f;
		protected void Start()
		{
			Debug.LogError("Started");
		}

		protected abstract void Update();
	}
}
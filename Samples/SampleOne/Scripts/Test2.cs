using UnityEngine;

namespace TestScene
{
	public class Test2 : TestScript
	{
		protected override void Update()
		{
			if (Random.value <= limit)
				Debug.LogError("Update..");
		}
	}
}
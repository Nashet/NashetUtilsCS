using UnityEngine;

namespace TestScene
{
	public class Test4 : TestScript
	{
		protected override void Update()
		{
			if (Random.value <= limit)
				Debug.LogError("Update..");
		}
	}
}
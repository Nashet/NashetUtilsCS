using Assets.NashUtilsCs.NullChecker;
using UnityEngine;

namespace TestScene
{
	public class Test1 : TestScript
	{
		[SerializeField][IsNotNull] private GameObject hru;
		protected override void Update()
		{
			if (Random.value <= limit)
				Debug.LogError("Update..");
		}
	}
}
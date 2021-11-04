using UnityEngine;

namespace TestScene
{
    public class Test3 : TestScript
    {
        protected override void Update()
        {
            if (Random.value <= limit)
                Debug.Log("Update..");
        }
    }
}
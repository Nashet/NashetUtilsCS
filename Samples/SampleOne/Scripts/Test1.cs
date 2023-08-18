using UnityEngine;

namespace TestScene
{
    public class Test1 : TestScript
    {
        protected override void Update()
        {
            if (Random.value <= limit)
                Debug.Log("Update..");
        }
    }
}
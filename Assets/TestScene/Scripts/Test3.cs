using UnityEngine;

public class Test3 : TestScript
{
    protected override void Update()
    {
        if (Random.value <= limit)
            Log("Update..");
    }
}
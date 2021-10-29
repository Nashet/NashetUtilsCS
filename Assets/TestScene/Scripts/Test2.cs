using UnityEngine;

public class Test2 : TestScript
{
    protected override void Update()
    {
        if (Random.value <= limit)
            Log("Update..");
    }
}
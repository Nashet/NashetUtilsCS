using UnityEngine;

public class Test4 : TestScript
{
    protected override void Update()
    {
        if (Random.value <= limit)
            Log("Update..");
    }
}
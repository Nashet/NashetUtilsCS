using UnityEngine;

public class Test1 : TestScript
{
    protected override void Update()
    {
        if (Random.value <= limit)
            Log("Update..");
    }
}
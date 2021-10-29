using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class TestScript : MonoBehaviour
{
    [SerializeField] protected float limit = 0.3f; 
    private void Start()
    {
        Log("Started");
    }

    protected void Log(string text, [CallerFilePath] string name = "")
    {
        Debug.Log(name+" "+text);
    }

    protected abstract void Update();
}
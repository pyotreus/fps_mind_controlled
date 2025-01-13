using UnityEngine;

public class GlobalReferences : MonoBehaviour
{

    public static GlobalReferences Instance { set; get; }

    public bool BCI;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        } else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}

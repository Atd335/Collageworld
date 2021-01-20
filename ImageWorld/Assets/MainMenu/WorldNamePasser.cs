using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldNamePasser : MonoBehaviour
{
    public static string worldName;
    public static bool isLoading;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(test());
        StartCoroutine(test());
        StartCoroutine(test());
        isLoading = false;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator test()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        print("hi");
        yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Space));
        StartCoroutine(test());
    }
}

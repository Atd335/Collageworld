using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingAnim : MonoBehaviour
{
    public Sprite[] loadingSprites;
    public SpriteRenderer SR;
    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    int frame;

    private void Update()
    {
        if (!LoadingJoke.doneLoading)
        {
            transform.position = Vector3.Lerp(transform.position, Vector3.zero, Time.deltaTime * 6);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0,-11,0), Time.deltaTime * 6);
            if (transform.position.y <= -10) { SceneManager.LoadScene(2); }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        frame++;
        if (frame==loadingSprites.Length)
        {
            frame = 0;
        }
        SR.sprite = loadingSprites[frame];
    }
}

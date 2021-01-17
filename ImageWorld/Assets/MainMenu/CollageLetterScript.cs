using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollageLetterScript : MonoBehaviour
{
    public Sprite[] letterVariants;
    public AudioClip[] Sounds;
    // Start is called before the first frame update
    void Start()
    {
        switchTime = Random.Range(1f,2f);
    }

    float switchTimer;
    public float switchTime;

    // Update is called once per frame
    void Update()
    {
        transform.parent.localScale = Vector3.Lerp(transform.parent.localScale, Vector3.one, Time.deltaTime * 15);
        switchTimer += Time.deltaTime;
        if (switchTimer>=switchTime)
        {
            transform.parent.localScale = new Vector3(1.3f,0.3f,1);
            GetComponent<SpriteRenderer>().sprite = letterVariants[Random.Range(0,letterVariants.Length)];
            switchTime = Random.Range(1f, 2f);           
            switchTimer = 0;
            float rand = Random.Range(0,1f);
            if (rand<.5f || MainMenuMouseInputs.activeScreenPos!=0) { return; }
            GetComponent<AudioSource>().PlayOneShot(Sounds[Random.Range(0, Sounds.Length)], .1f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CharacterScript : MonoBehaviour
{
    private SpriteRenderer sprite;
    public TechSheet techSheet;
    [Range(60,0)]public int fps;
    public int currFrame;

    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

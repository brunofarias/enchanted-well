using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ReSkin : MonoBehaviour
{

    private SpriteRenderer sRender;
    public Sprite[] sprites;
    public string spriteSheetName;
    public string loadedSpriteSheetName;
    private static ReSkin instance;
    

    private Dictionary<string, Sprite> spriteSheet;

    public static ReSkin Instance
  {
    get
    {
      if (instance == null) instance = GameObject.FindObjectOfType<ReSkin>();
      return instance;
    }
  }


    // Start is called before the first frame update
    void Start()
    {
        sRender = GetComponent<SpriteRenderer>();
        LoadSpriteSheet();
    }
    

    // Update is called once per frame
    public void LateUpdate()
    {
        if(loadedSpriteSheetName != spriteSheetName)
        {
            LoadSpriteSheet();
        }
        
        sRender.sprite = spriteSheet[sRender.sprite.name];
        
    }

    public void LoadSpriteSheet()
    {
        sprites = Resources.LoadAll<Sprite>(spriteSheetName);
        spriteSheet = sprites.ToDictionary(x => x.name,x => x);
        loadedSpriteSheetName = spriteSheetName;

    }
   
    
}

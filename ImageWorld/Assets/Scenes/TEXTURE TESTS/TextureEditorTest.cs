using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class TextureEditorTest : MonoBehaviour
{
    Color[] cBlock;
    

    // Start is called before the first frame update
    void Start()
    {
        
        cBlock = new Color[60 * 60];
        for (int i = 0; i < cBlock.Length; i++)
        {
            cBlock[i] = Color.clear;
        }
    }

    RaycastHit hit;
    public int brushSize = 36;
    // Update is called once per frame
    void Update()
    {
        if (PlayerMovement.inMenu || PauseMenuScript.imPaused) { return; }
        if (Input.GetKeyDown(KeyCode.RightBracket)) { brushSize += 5; }
        if (Input.GetKeyDown(KeyCode.LeftBracket)) { brushSize -= 5; }

        brushSize = Mathf.Clamp(brushSize,10,60);

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit))
        {
            if (hit.collider!=null && hit.collider.tag == "interactable")
            {
                Vector2Int texCoords = new Vector2Int(Mathf.RoundToInt(hit.collider.GetComponentInParent<TextureHavenScript>().editTex.width*hit.textureCoord.x), 
                                                      Mathf.RoundToInt(hit.collider.GetComponentInParent<TextureHavenScript>().editTex.height * hit.textureCoord.y));

                Vector2Int texCoordsNeg = new Vector2Int(texCoords.x-(brushSize/2),texCoords.y- (brushSize / 2));
                texCoordsNeg.x = Mathf.Clamp(texCoordsNeg.x,0, hit.collider.GetComponentInParent<TextureHavenScript>().editTex.width);
                texCoordsNeg.y = Mathf.Clamp(texCoordsNeg.y,0, hit.collider.GetComponentInParent<TextureHavenScript>().editTex.height);

                Vector2Int texCoordsPos = new Vector2Int(texCoords.x + (brushSize / 2), texCoords.y + (brushSize / 2));
                texCoordsPos.x = Mathf.Clamp(texCoordsPos.x, 0, hit.collider.GetComponentInParent<TextureHavenScript>().editTex.width);
                texCoordsPos.y = Mathf.Clamp(texCoordsPos.y, 0, hit.collider.GetComponentInParent<TextureHavenScript>().editTex.height);

                int blockSizex = Mathf.Abs(texCoordsPos.x - texCoordsNeg.x);
                int blockSizey = Mathf.Abs(texCoordsPos.y - texCoordsNeg.y);

                if (Input.GetKey(KeyCode.Mouse0))
                {
                    hit.collider.GetComponentInParent<TextureHavenScript>().editTex.SetPixels(texCoordsNeg.x, texCoordsNeg.y, blockSizex, blockSizey, cBlock);
                    hit.collider.GetComponentInParent<TextureHavenScript>().editTex.Apply();
                    hit.collider.GetComponentInParent<TextureHavenScript>().UpdateTex();
                }
                else if(Input.GetKey(KeyCode.Mouse1))
                {
                    hit.collider.GetComponentInParent<TextureHavenScript>().editTex.SetPixels(texCoordsNeg.x, texCoordsNeg.y, blockSizex, blockSizey, hit.collider.GetComponentInParent<TextureHavenScript>().refTex.GetPixels(texCoordsNeg.x, texCoordsNeg.y, blockSizex, blockSizey));
                    hit.collider.GetComponentInParent<TextureHavenScript>().editTex.Apply();
                    hit.collider.GetComponentInParent<TextureHavenScript>().UpdateTex();
                }

                if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse1))
                {
                    hit.collider.GetComponentInParent<TextureHavenScript>().UpdateColliders();
                }
            }
        }
    }

    Mesh SpriteToMesh(Sprite sprite)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = Array.ConvertAll(sprite.vertices, i => (Vector3)i);
        mesh.uv = sprite.uv;
        mesh.triangles = Array.ConvertAll(sprite.triangles, i => (int)i);

        return mesh;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hit.point, .5f);
    }
}

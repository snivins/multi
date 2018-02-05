using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

	// Use this for initialization
	GUIStyle healthStyle;
	GUIStyle backStyle;
	GUIStyle teamStyle;
    Health combat;
	public Color teamC;
	public bool isfollower;
	public int vida;
    void Awake()
    {
        combat = GetComponent<Health>();
    }

    void OnGUI()
    {
        InitStyles();

        // Draw a Health Bar


        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
		if (pos.z < 0)
			return;
		if (!isfollower) {
			vida = Health.maxHealth / 4;
		} else {
			vida = Health.maxHealthFollower / 4;
		}
		// draw health bar background
		GUI.color = Color.blue;
		GUI.backgroundColor = Color.blue;
		GUI.Box(new Rect(pos.x-28, Screen.height - pos.y + 18, vida + 6f, 11f), ".", teamStyle);
        GUI.color = Color.red;
        GUI.backgroundColor = Color.red;
        GUI.Box(new Rect(pos.x-25, Screen.height - pos.y + 21, vida, 5), ".", backStyle);
        
        // draw health bar amount
		if (combat.health < 1)
			return;
        GUI.color = Color.green;
        GUI.backgroundColor = Color.green;
        GUI.Box(new Rect(pos.x-25, Screen.height - pos.y + 21, combat.health/2, 5), ".", healthStyle);

    }

    void InitStyles()
    {
        if( healthStyle == null )
        {
            healthStyle = new GUIStyle( GUI.skin.box );
            healthStyle.normal.background = MakeTex( 2, 2, new Color( 0f, 1f, 0f, 1.0f ) );
        }

		if( backStyle == null )
		{
			backStyle = new GUIStyle( GUI.skin.box );
			backStyle.normal.background = MakeTex( 2, 2, new Color( 1f, 0f, 0f, 1.0f ) );
		}
		if( teamStyle == null )
		{
			teamStyle = new GUIStyle( GUI.skin.box );
			teamStyle.normal.background = MakeTex( 2, 2, new Color( 1f, 0.5f, 0.5f, 1.0f ) );
		}
    }
    
    Texture2D MakeTex( int width, int height, Color col )
    {
        Color[] pix = new Color[width * height];
        for( int i = 0; i < pix.Length; ++i )
        {
            pix[ i ] = col;
        }
        Texture2D result = new Texture2D( width, height );
        result.SetPixels( pix );
        result.Apply();
        return result;
    }
}

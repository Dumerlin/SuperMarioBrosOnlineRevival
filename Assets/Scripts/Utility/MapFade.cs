using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used in map transitions. Fades into black, then back into clear after a while.
/// <para>This is a Special Instance to avoid having duplicates.</para>
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
[DisallowMultipleComponent]
public class MapFade : MonoBehaviour
{
    #region Special Instance Fields

    public static MapFade Instance { get { return instance; } }

    public static bool HasInstance { get { return (instance != null); } }

    private static MapFade instance = null;

    #endregion

    public float FadeTime = .7f;
    public bool FadeIn = true;
    public bool DestroyOnFinish = false;

    private readonly Color StartColor = Color.clear;
    private readonly Color EndColor = Color.black;

    private SpriteRenderer SpriteRender = null;

    private Color SColor;
    private Color EColor;

    private float CurFadeTime = 0f;

    private void Awake()
    {
        //Avoid duplicates
        if (instance == null)
        {
            instance = this;
            SpriteRender = GetComponent<SpriteRenderer>();
            SpriteRender.color = StartColor;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ResetFade(true);
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    private void Update()
    {
        //Lerp the fade colors
        SpriteRender.color = Color.Lerp(SColor, EColor, CurFadeTime);
        CurFadeTime += (Time.deltaTime / FadeTime);

        //The fade is finished
        if (SpriteRender.color == EColor)
        {
            enabled = false;

            if (DestroyOnFinish == true)
            {
                Destroy(gameObject);
            }
        }
    }

    public void ResetFade(bool setEnabled)
    {
        if (FadeIn == true)
        {
            SColor = StartColor;
            EColor = EndColor;
        }
        else
        {
            SColor = EndColor;
            EColor = StartColor;
        }

        SpriteRender.color = SColor;
        CurFadeTime = 0f;
        enabled = setEnabled;
    }

    /// <summary>
    /// Creates and returns a MapFade. If one doesn't exist, the current one is returned.
    /// </summary>
    /// <param name="fadeIn">Whether the MapFade starts fading in or not. Whether the instance exists or not, the fade value will be changed.</param>
    /// <param name="destroyOnFinish">Whether the MapFade object is destroyed when finished or not.</param>
    /// <returns></returns>
    public static MapFade Create(bool fadeIn, bool destroyOnFinish)
    {
        if (HasInstance == true)
        {
            Instance.FadeIn = fadeIn;
            Instance.DestroyOnFinish = destroyOnFinish;
            Instance.ResetFade(true);
            return Instance;
        }

        MapFade fade = Resources.Load<MapFade>(ResourcePath.VFX.MapFade);
        if (fade == null)
        {
            Debug.LogError("MapFade object not found at " + ResourcePath.VFX.MapFade);
            return null;
        }

        MapFade instantiatedFade = Instantiate<MapFade>(fade);
        instantiatedFade.FadeIn = fadeIn;
        instantiatedFade.DestroyOnFinish = destroyOnFinish;
        return instantiatedFade;
    }
}

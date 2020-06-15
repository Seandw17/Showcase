using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static PauseMenu;

/// <summary>
/// Class to manage fading in/out assets
/// </summary>
static public class FadeIn
{
    /// <summary>
    /// Takes 3D object and fades in / out
    /// </summary>
    /// <param name="_renderer">Renderer component</param>
    /// <param name="_time">How long should it take to complete</param>
    /// <param name="_in">is it fading in or out?</param>
    /// <returns></returns>
    static public IEnumerator FadeAsset(Renderer _renderer, float _time,
        bool _in)
    {
        Material material = _renderer.material;
        Color newColour = new Color(
            material.color.r,
            material.color.g,
            material.color.b,
            material.color.a);

        float fade = (0.4f * _time) * Time.deltaTime;
        if (!_in) { fade *= -1; }

        while (CheckAlpha(_in, newColour.a))
        {
            if (!IsPaused())
            {
                newColour.a += fade;
                material.color = newColour;
            }
            yield return null;
        }
    }

    /// <summary>
    /// Takes TMP asset and fades in / out text
    /// </summary>
    /// <param name="_tmp">asset</param>
    /// <param name="_in">is it fading in?</param>
    /// <param name="_time">how long should it take to complete</param>
    /// <returns></returns>
    static public IEnumerator FadeAsset(TextMeshProUGUI _tmp, float _time,
        bool _in)
    {
        Color newColour = new Color(
            _tmp.color.r,
            _tmp.color.g,
            _tmp.color.b,
            _tmp.color.a
            );

        float fade = (0.4f * _time) * Time.deltaTime;
        if (!_in) { fade *= -1; }

        while (CheckAlpha(_in, newColour.a))
        {
            if (!IsPaused())
            {
                Debug.Log(newColour.a);
                newColour.a += fade;
                _tmp.color = newColour;
            }
            yield return null;
        }
    }

    /// <summary>
    /// Fade in / Fade out text
    /// </summary>
    /// <param name="_tmp">tmp object</param>
    /// <param name="_time">time to fade out</param>
    /// <param name="_in">if it is fading in / out</param>
    /// <returns></returns>
    static public IEnumerator FadeAsset(TextMeshPro _tmp, float _time,
        bool _in)
    {
        Color newColour = new Color(
            _tmp.color.r,
            _tmp.color.g,
            _tmp.color.b,
            _tmp.color.a
            );

        float fade = (0.4f * _time) * Time.deltaTime;
        if (!_in){ fade *= -1; }

        while (CheckAlpha(_in, newColour.a))
        {
            if (!IsPaused())
            {
                newColour.a += fade;
                _tmp.color = newColour;
            }
            yield return null;
        }
    }

    /// <summary>
    /// Function to fade out a sprite
    /// </summary>
    /// <param name="_spriteRenderer">the rendrer</param>
    /// <param name="_time">time to fade out</param>
    /// <param name="_in">fade in?</param>
    /// <returns></returns>
    static public IEnumerator FadeAsset(SpriteRenderer _spriteRenderer,
        float _time, bool _in)
    {
        Color newColour = new Color(
            _spriteRenderer.color.r,
            _spriteRenderer.color.g,
            _spriteRenderer.color.b,
            _spriteRenderer.color.a
            );

        float fade = 0.02f * _time;
        if (!_in) { fade *= -1; }

        while (CheckAlpha(_in, newColour.a))
        {
            if (!IsPaused())
            {
                newColour.a += fade;
                _spriteRenderer.color = newColour;
            }
            yield return null;
        }
    }

    /// <summary>
    /// Fade out image asset
    /// </summary>
    /// <param name="_image">the image</param>
    /// <param name="_time">time to fade out completly</param>
    /// <param name="_in">fade in?</param>
    /// <returns></returns>
    static public IEnumerator FadeAsset(Image _image, float _time, bool _in)
    {
        Color newColour = new Color(
            _image.color.r,
            _image.color.g,
            _image.color.b,
            _image.color.a
            );

        float fade = 0.02f * _time;
        if (!_in) { fade *= -1; }

        while (CheckAlpha(_in, newColour.a))
        {
            if (!IsPaused())
            {
                newColour.a += fade;
                _image.color = newColour;
            }
            yield return null;
        }
    }

    /// <summary>
    /// Function to check if alpha value falls within certain bounds
    /// </summary>
    /// <param name="_up"> is alpha going up</param>
    /// <param name="_currentAlpha"> the current alpha</param>
    /// <returns></returns>
    static bool CheckAlpha(bool _up, float _currentAlpha)
    {
        if (_up) { return _currentAlpha <= 1; }
        else { return _currentAlpha >= 0; }
    }

    /// <summary>
    /// force alpha of a material to 0
    /// </summary>
    /// <param name="_material">the given material</param>
    static public void SetAlphaToZero(Material _material)
    {
        _material.color = new Color(
            _material.color.r,
            _material.color.g,
            _material.color.b,
            0);
    }

    /// <summary>
    /// Function to force a tmpproUGUI object alpha to 0
    /// </summary>
    /// <param name="_tmp">the TMPPROUGUI object</param>
    static public void SetAlphaToZero(TextMeshProUGUI _tmp)
    {
        _tmp.color = new Color(
            _tmp.color.r,
            _tmp.color.g,
            _tmp.color.b,
            0);
    }

    /// <summary>
    /// Function to force a tmpproUGUI object alpha to 0
    /// </summary>
    /// <param name="_tmp">the TMPPRO object</param>
    static public void SetAlphaToZero(TextMeshPro _tmp)
    {
        _tmp.color = new Color(
            _tmp.color.r,
            _tmp.color.g,
            _tmp.color.b,
            0);
    }

    /// <summary>
    /// Function to set sprite alpha to 0
    /// </summary>
    /// <param name="_spriteRenderer">the sprite renderer</param>
    static public void SetAlphaToZero(SpriteRenderer _spriteRenderer)
    {
        _spriteRenderer.color = new Color(
            _spriteRenderer.color.r,
            _spriteRenderer.color.g,
            _spriteRenderer.color.b,
            0);
    }

    /// <summary>
    /// Set image alpha to 0
    /// </summary>
    /// <param name="_image">the image to set</param>
    static public void SetAlphaToZero(Image _image)
    {
        _image.color = new Color(
           _image.color.r,
           _image.color.g,
           _image.color.b,
           0);
    }

    /// <summary>
    /// Fade out the screen and unload the scene
    /// </summary>
    /// <param name="_image">the image to load out</param>
    /// <returns>yield return null</returns>
    static public IEnumerator FadeOutLoadingScreen(Image
        _image)
    {
        Color newColour = new Color(
            _image.color.r,
            _image.color.g,
            _image.color.b,
            _image.color.a
            );

        float fade = -0.02f;

        while (CheckAlpha(false, newColour.a))
        {
            newColour.a += fade;
            _image.color = newColour;
            yield return null;
        }

        SceneManager.UnloadSceneAsync("Loading");
    }
}

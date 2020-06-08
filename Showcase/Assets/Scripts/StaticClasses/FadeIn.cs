using System.Collections;
using TMPro;
using UnityEngine;

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

        bool goal;
        float fade = 0.02f * _time;
        if (_in)
        {
            goal = material.color.a <= 1;
        }
        else
        {
            goal = material.color.a >= 0;
            fade *= -1;
        }

        while (goal)
        {
            newColour.a += fade;
            material.color = newColour;
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

        bool goal;
        float fade = 0.02f * _time;
        if (_in)
        {
            goal = newColour.a <= 1;
        }
        else
        {
            goal = newColour.a <= 0;
            fade *= -1;
        }

        while (goal)
        {
            newColour.a += fade;
            _tmp.color = newColour;
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

        bool goal;
        float fade = 0.02f * _time;
        if (_in)
        {
            goal = newColour.a <= 1;
        }
        else
        {
            goal = newColour.a <= 0;
            fade *= -1;
        }

        while (goal)
        {
            newColour.a += fade;
            _tmp.color = newColour;
            yield return null;
        }
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
}

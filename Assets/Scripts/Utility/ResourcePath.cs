using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines strings for easily referencing asset locations. Most commonly used to load from Resources.
/// </summary>
public static class ResourcePath
{
    public static class AnimationStrings
    {
        //Overworld
        public const string IdleAnim = "Idle";
        public const string WalkingAnim = "Walking";
        public const string JumpAnim = "Jump";
        public const string JumpFallAnim = "JumpFall";
        public const string GetItemAnim = "GetItem";
        public const string DizzyAnim = "Dizzy";
    }

    public static class Sprites
    {
        public const string SpritePath = "Sprites/";
    }

    public static class Audio
    {
        public const string MusicPath = "Audio/Music/";
        public const string SFXPath = "Audio/SFX/";
    }

    public static class VFX
    {
        public const string MapFade = "Prefabs/MapFade";
    }
}

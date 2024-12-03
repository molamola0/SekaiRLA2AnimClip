/*
 * Generated code file by Il2CppInspector - http://www.djkaty.com - https://github.com/djkaty
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Image 0: Assembly-CSharp.dll - Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null - Types 0-13335

namespace Sekai.Streaming
{
    [CreateAssetMenu(fileName = "0000", menuName = "Sekai/StreamingLiveData")]
    public class StreamingLiveData : ScriptableObject // TypeDefIndex: 9695
    {
        // Fields
        [SerializeField] private string stageName; // 0x18
        [SerializeField] private CharacterData[] characters; // 0x20
        [SerializeField] private StageDecorationData[] stageDecorations; // 0x28
        [SerializeField] private MusicData[] musics; // 0x30
        [SerializeField] private SeData[] ses; // 0x38
        [SerializeField] private VideoData[] videos; // 0x40
        [SerializeField] private ImageSequenceData[] imageSequences; // 0x48
        [SerializeField] private CharacterMonitorData characterMonitor; // 0x50
        [SerializeField] private TimelineData[] timelines; // 0x60
        [SerializeField] private PenlightColorMap[] colorMaps; // 0x68
        [SerializeField] private McPenlightColorMap[] mcPenlightColorMaps; // 0x70
        [SerializeField] private PlayerAvatarData playerAvatarData; // 0x78
        [SerializeField] private CharacterHairSpringBoneGravityData characterHairSpringBoneGravity; // 0x88
        [SerializeField] private Vector3 cheerMessagePositionOffset; // 0x98
        [SerializeField] private Vector3 playerCheerMessagePositionOffset; // 0xA4
        [SerializeField] private bool enableMoviePreloadInTimeline; // 0xB0
        [SerializeField] private int freeLiveSequenceNo; // 0xB4
        [SerializeField] private AfterEventData afterEvent; // 0xB8

        // Properties
        public string StageName => stageName;

        public CharacterData[] Characters => characters;

        public StageDecorationData[] StageDecorations => stageDecorations;

        public MusicData[] Musics => musics;

        public SeData[] Ses => ses;

        public VideoData[] Videos => videos;

        public ImageSequenceData[] ImageSequences => imageSequences;

        public CharacterMonitorData CharacterMonitor => characterMonitor;

        public TimelineData[] Timelines => timelines;

        public PenlightColorMap[] ColorMaps => colorMaps;

        public McPenlightColorMap[] McPenlightColorMaps => mcPenlightColorMaps;

        public PlayerAvatarData PlayerAvatar => playerAvatarData;

        public CharacterHairSpringBoneGravityData CharacterHairSpringBoneGravity => characterHairSpringBoneGravity;

        public Vector3 CheerMessagePositionOffset => cheerMessagePositionOffset;

        public Vector3 PlayerCheerMessagePositionOffset => playerCheerMessagePositionOffset;

        public bool EnableMoviePreloadInTimeline => enableMoviePreloadInTimeline;

        public int FreeLiveSequenceNo => freeLiveSequenceNo;

        public AfterEventData AfterEvent => afterEvent;

        // Nested types
        [Serializable]
        public struct CharacterCostumeData // TypeDefIndex: 9678
        {
            // Fields
            public string Name; // 0x00
            public int CharacterDataId; // 0x08
            public int UnitType; // 0x0C
            public string FaceCostumeModelName; // 0x10
            public string BodyCostumeModelName; // 0x18
            public string BodyColorVariationName; // 0x20
            public string AccessoryCostmeModelName; // 0x28
            public string AccessoryColorVariationName; // 0x30
            public int MusicItemId; // 0x38
        }

        [Serializable]
        public struct CharacterData // TypeDefIndex: 9679
        {
            // Fields
            public string Name; // 0x00
            public CharacterCostumeData[] CostumeData; // 0x08
        }

        [Serializable]
        public struct StageDecorationData // TypeDefIndex: 9680
        {
            // Fields
            public string Name; // 0x00
            public string DecorationAssetName; // 0x08
        }

        [Serializable]
        public struct MusicCharacterData // TypeDefIndex: 9681
        {
            // Fields
            public int CharacterIndex; // 0x00
            public int CostumeIndex; // 0x04
        }

        [Serializable]
        public struct MusicData // TypeDefIndex: 9682
        {
            // Fields
            public string Name; // 0x00
            public string CueAssetName; // 0x08
            public int MusicId; // 0x10
            public string TimelineAssetName; // 0x18
            public StageDecorationData[] DecorationDatas; // 0x20
            public MusicCharacterData[] CharacterDatas; // 0x28
        }

        [Serializable]
        public struct SeData // TypeDefIndex: 9683
        {
            // Fields
            public int Id; // 0x00
            public string CueAssetName; // 0x08
            public bool Loop; // 0x10
            public float FadeOutTime; // 0x14
        }

        [Serializable]
        public struct VideoData // TypeDefIndex: 9684
        {
            // Fields
            public string Name; // 0x00
            public MovieType MovieType; // 0x08
            public string VideoAssetName; // 0x10
        }

        [Serializable]
        public struct ImageSequenceData // TypeDefIndex: 9685
        {
            // Fields
            public string Name; // 0x00
            public string ImageSequenceAssetName; // 0x08
        }

        [Serializable]
        public struct CharacterMonitorData // TypeDefIndex: 9686
        {
            // Fields
            public int UseMonitorCount; // 0x00
            public int RenderTargetWidth; // 0x04
            public int RenderTargetHeight; // 0x08
        }

        [Serializable]
        public struct TimelineData // TypeDefIndex: 9687
        {
            // Fields
            public string Name; // 0x00
            public string TimelineAssetName; // 0x08
            public int McPenlightColorMapIndex; // 0x10
            public bool Loop; // 0x14
        }

        [Serializable]
        public struct PenlightColorMap // TypeDefIndex: 9688
        {
            // Fields
            public Color Color; // 0x00
            [Range(0f, 1f)] public float Rate; // 0x10
        }

        [Serializable]
        public struct McPenlightColorMap // TypeDefIndex: 9689
        {
            // Fields
            public PenlightColorMap[] PenlightColorMaps; // 0x00
        }

        [Serializable]
        public struct PlayerAvatarData // TypeDefIndex: 9690
        {
            // Fields
            public bool IsOverrideCameraPositionOffset; // 0x00
            public Vector3 CameraPositionOffset; // 0x04
        }

        [Serializable]
        public struct CharacterHairSpringBoneGravityData // TypeDefIndex: 9691
        {
            // Fields
            public bool ForceGravity; // 0x00
            public Vector3 Gravity; // 0x04
        }

        [Serializable]
        public struct AfterEventData // TypeDefIndex: 9692
        {
            // Fields
            public string AvatarMapAssetName; // 0x00
        }

        private class StageDecorationDataComparer : IEqualityComparer<StageDecorationData> // TypeDefIndex: 9693
        {
            // Constructors
            public StageDecorationDataComparer()
            {
            } // 0x0325EC30-0x0325EC38

            // Methods
            public bool Equals(StageDecorationData x, StageDecorationData y) => default; // 0x0325ECD0-0x0325ECE0
            public int GetHashCode(StageDecorationData data) => default; // 0x0325ECE0-0x0325ED00
        }

        // Constructors
        public StreamingLiveData()
        {
        } // 0x0325ECC8-0x0325ECD0
    }
}
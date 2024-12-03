using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using EasyButtons;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sekai.Core;
using Sekai.Streaming;
using UnityEditor;
using UnityEngine.Serialization;

namespace SekaiRLA
{
    [System.Serializable]
    public class KeyframeData
    {
        public float time;
        public Vector3 position;
        public Vector3 rotation; // Euler angles as Vector3
    }

    public class SekaiRLA2AnimClip : MonoBehaviour
    {
        private const string _prskFolderPath = "/Volumes/T7/prsk/{0}";
        // private const string _assetBasePath = "assets/sekai/assetbundle/resources/ondemand/{0}";

        public string streamingDataName = "1st_live_vbs";
        public int liveIndex = 1;
        public string blockName = "威風堂々";

        public string jsonFolderPath = "/Volumes/T7/prsk/streaming_live/archive/1st_live_vbs-1_anim/威風堂々";
        public string jsonFilePath = "/Volumes/T7/prsk/streaming_live/archive/1st_live_vbs-1_anim/威風堂々/bone_2.json";
        private const string _outputFolderBase = "Assets/connect_live";
        public float height;
        private const float _deltaTime = 1 / 60f;

        // characters - CostumeData -
        // CharacterDataId for height
        // BodyCostumeModelName
        // musics - Name - CharacterDatas - CharacterIndex & CostumeIndex
        // musics - MusicId (log)


        private string streamingDataPath = "/Volumes/T7/prsk/streaming_live/data/1st_live_vbs";
        private string archivePath = "/Volumes/T7/prsk/streaming_live/archive/1st_live_vbs-1";

        private StreamingLiveData streamingLiveData;

        private AssetBundle currentAb;
        // private Dictionary<string, AssetBundle> loadedBodyAbs = new Dictionary<string, AssetBundle>();
        // private Dictionary<string, GameObject> instantiatedBodies = new Dictionary<string, GameObject>();

        private Dictionary<string, float> characterHeights = new Dictionary<string, float>();
        private string _animPath;
        private string _outputFolder;


        [Button]
        public void ImportAnimationFromFolderAndExport()
        {
            AssetBundle.UnloadAllAssetBundles(true);

            LoadStreamingLiveData();

            // foreach (var kvp in loadedBodyAbs)
            // {
            //     kvp.Value.Unload(true);
            // }

            // loadedBodyAbs = new Dictionary<string, AssetBundle>();
            // instantiatedBodies = new Dictionary<string, GameObject>();
            characterHeights = new Dictionary<string, float>();

            LoadCharaBodyAndGetHeight();

            archivePath = string.Format(_prskFolderPath,
                AssetBundleNames.GetStreamingLiveArchiveName($"{streamingDataName}-{liveIndex}"));
            jsonFolderPath = $"{archivePath}_anim/{blockName}";

            var files = Directory.GetFiles(jsonFolderPath, "*.json", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                if (!file.Contains("bone")) continue;
                jsonFilePath = file;
                var charaIndex = Path.GetFileNameWithoutExtension(jsonFilePath).Split('_')[1];
                height = characterHeights[charaIndex];
                ImportAnimationFromJson();
            }
        }

        private void LoadCharaBodyAndGetHeight()
        {
            var musicDatas = streamingLiveData.Musics;
            var characterDatas = streamingLiveData.Characters;
            foreach (var musicData in musicDatas)
            {
                if (musicData.Name == blockName)
                {
                    foreach (var musicDataCharacterData in musicData.CharacterDatas)
                    {
                        var characterIndex = musicDataCharacterData.CharacterIndex;
                        var costumeIndex = musicDataCharacterData.CostumeIndex;
                        var characterData = characterDatas[characterIndex];
                        var costumeData = characterData.CostumeData[costumeIndex];
                        var id = costumeData.CharacterDataId;
                        var body = id.ToString("D2") + "/0001";

                        if (id > 31)
                        {
                            Debug.LogError("Character ID is out of range: " + id);
                            return;
                        }


                        height = 0;
                        string charaBodyFigure = null;
                        string charaBodyModelName = null;
                        SetFromJson(id, ref height, ref charaBodyFigure, ref charaBodyModelName);
                        if (height == 0 || charaBodyFigure == null || charaBodyModelName == null)
                        {
                            Debug.LogError("Failed to load character info from JSON");
                            return;
                        }

                        characterHeights[characterIndex.ToString()] = height;

                        // var bodyAbName = AssetBundleNames.GetLiveCharacterBodyModelName(body, charaBodyFigure);
                        // var bodyAbPath = string.Format(_prskFolderPath, bodyAbName);
                        // var bodyAb = AssetBundle.LoadFromFile(bodyAbPath);
                        // if (!bodyAb)
                        // {
                        //     Debug.LogError("Failed to load AssetBundle: " + bodyAbPath);
                        //     return;
                        // }
                        //
                        // loadedBodyAbs[characterIndex.ToString()] = bodyAb;
                        //
                        // var bodyAssetName = bodyAbName + "/body.prefab";
                        // var bodyAssetPath = string.Format(_assetBasePath, bodyAssetName);
                        // var asset = bodyAb.LoadAsset<GameObject>(bodyAssetPath);
                        // var instantiatedAsset = Instantiate(asset);
                        // instantiatedAsset.name = bodyAssetName.Replace("/", "_").Split(".")[0];
                        // instantiatedBodies[characterIndex.ToString()] = instantiatedAsset;
                        //
                        // // streaming models don't use heeloffset
                        // var instantiatedAssetTransform = instantiatedAsset.transform;
                        // var transforms = instantiatedAssetTransform.GetComponentsInChildren<Transform>();
                        // var position1 = transforms.FirstOrDefault(x => x.name == "Position");
                        // if (position1)
                        // {
                        //     position1.localScale = Vector3.one * height;
                        // }
                        // else
                        // {
                        //     Debug.LogError("Position not found");
                        // }
                    }
                }
            }
        }

        private void SetFromJson(int id, ref float charaBodyHeight, ref string charaBodyFigure,
            ref string charaBodyModelName)
        {
            if (id > 26)
            {
                id = 21;
            }

            var gamecharactersJson = "Assets/Scripts/gameCharacters.json";
            if (!File.Exists(gamecharactersJson))
            {
                Debug.LogError("JSON file not found: " + gamecharactersJson);
                return;
            }

            var jsonData = File.ReadAllText(gamecharactersJson);
            var characters = JArray.Parse(jsonData);

            var character = characters.FirstOrDefault(c => (int)c["id"] == id);
            if (character == null)
            {
                Debug.LogError("Character with ID " + id + " not found.");
                return;
            }

            charaBodyHeight = (float)character["height"] / 100;
            charaBodyFigure = (string)character["figure"];
            if ((string)character["breastSize"] != "none")
            {
                charaBodyFigure += "_" + (string)character["breastSize"];
            }

            charaBodyModelName = (string)character["modelName"];
        }

        private void LoadStreamingLiveData()
        {
            streamingDataPath =
                string.Format(_prskFolderPath, AssetBundleNames.GetStreamingLiveDataName(streamingDataName));
            // load streaming live data
            if (currentAb)
            {
                if (currentAb.name.Contains(streamingDataName)) return;
                currentAb.Unload(true);
            }

            currentAb = AssetBundle.LoadFromFile(streamingDataPath);
            streamingLiveData = currentAb.LoadAllAssets<StreamingLiveData>()[0];
        }

        [Button]
        public void GetCharacterIndex()
        {
            LoadStreamingLiveData();
            var musicDatas = streamingLiveData.Musics;
            var characterDatas = streamingLiveData.Characters;
            var ids = new List<int>();
            var names = new List<string>();
            foreach (var musicData in musicDatas)
            {
                if (musicData.Name == blockName)
                {
                    foreach (var musicDataCharacterData in musicData.CharacterDatas)
                    {
                        var characterIndex = musicDataCharacterData.CharacterIndex;
                        var costumeIndex = musicDataCharacterData.CostumeIndex;
                        var characterData = characterDatas[characterIndex];
                        var costumeData = characterData.CostumeData[costumeIndex];
                        ids.Add(characterIndex);
                        names.Add(costumeData.Name);
                        // Debug.Log(
                        //     $"CharacterIndex: {musicDataCharacterData.CharacterIndex}, CostumeIndex: {musicDataCharacterData.CostumeIndex}");
                    }
                }
            }

            var debugText = string.Join(", ", ids);
            var debugText2 = string.Join(", ", names);
            Debug.Log($"CharacterIndex: {debugText}");
            Debug.Log($"CharacterName: {debugText2}");
        }

        [Button]
        public void DumpStreamingLiveData()
        {
            LoadStreamingLiveData();
            var outputPath = streamingDataPath + ".json";
            string json = JsonUtility.ToJson(streamingLiveData, true);
            File.WriteAllText(outputPath, json);
            Debug.Log($"StreamingLiveData exported to {outputPath}");
        }

        [Button]
        private void ImportAnimationFromJson()
        {
            _outputFolder = Path.Combine(_outputFolderBase, jsonFilePath.Split('/')[6], jsonFilePath.Split('/')[7]);
            if (!AssetDatabase.IsValidFolder(_outputFolder))
            {
                Directory.CreateDirectory(_outputFolder);
            }

            string json = File.ReadAllText(jsonFilePath);
            var clipName = Path.GetFileNameWithoutExtension(jsonFilePath);
            _animPath = Path.Combine(_outputFolder, clipName + ".anim");

            if (File.Exists(_animPath))
            {
                Debug.Log("Animation clip already exists: " + _animPath);
                return;
            }

            // Parse the JSON to a dictionary (Key: path, Value: keyframes)
            var animationData = JsonConvert.DeserializeObject<Dictionary<string, List<KeyframeData>>>(json);
            AnimationClip clip = new AnimationClip();
            foreach (KeyValuePair<string, List<KeyframeData>> anim in animationData)
            {
                CreateAnimationCurves(clip, anim.Key, anim.Value);
            }

            clip.EnsureQuaternionContinuity();

            AssetDatabase.CreateAsset(clip, _animPath);
            AssetDatabase.SaveAssets();
            Debug.Log("Animation clip saved to: " + _animPath);
        }

        private void CreateAnimationCurves(AnimationClip clip, string path, List<KeyframeData> keyframes)
        {
            bool hasposition = path == "Position/PositionOffset/Hip";

            AnimationCurve curvePosX = new AnimationCurve();
            AnimationCurve curvePosY = new AnimationCurve();
            AnimationCurve curvePosZ = new AnimationCurve();

            AnimationCurve curveRotX = new AnimationCurve();
            AnimationCurve curveRotY = new AnimationCurve();
            AnimationCurve curveRotZ = new AnimationCurve();
            AnimationCurve curveRotW = new AnimationCurve();

            var previousTime = 0f;

            foreach (var keyframe in keyframes)
            {
                var time = keyframe.time;
                var dt = time - previousTime;
                previousTime = time;
                if (dt < _deltaTime)
                {
                    continue;
                }

                if (hasposition)
                {
                    var position = keyframe.position / height;
                    curvePosX.AddKey(time, position.x);
                    curvePosY.AddKey(time, position.y);
                    curvePosZ.AddKey(time, position.z);
                }

                Quaternion currentRotation = Quaternion.Euler(keyframe.rotation);
                curveRotX.AddKey(time, currentRotation.x);
                curveRotY.AddKey(time, currentRotation.y);
                curveRotZ.AddKey(time, currentRotation.z);
                curveRotW.AddKey(time, currentRotation.w);
            }

            if (hasposition)
            {
                clip.SetCurve(path, typeof(Transform), "localPosition.x", curvePosX);
                clip.SetCurve(path, typeof(Transform), "localPosition.y", curvePosY);
                clip.SetCurve(path, typeof(Transform), "localPosition.z", curvePosZ);
            }

            clip.SetCurve(path, typeof(Transform), "localRotation.x", curveRotX);
            clip.SetCurve(path, typeof(Transform), "localRotation.y", curveRotY);
            clip.SetCurve(path, typeof(Transform), "localRotation.z", curveRotZ);
            clip.SetCurve(path, typeof(Transform), "localRotation.w", curveRotW);

            // clip.EnsureQuaternionContinuity();
        }
    }
}
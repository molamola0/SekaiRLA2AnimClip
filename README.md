# Sekai RLA to Animation Clip

a Unity script for converting Project Sekai connect live PoseData to animation clip

> [!NOTE]
> Originally written on 2024-09-12.
> This version is extracted from my actual scripts, and I am too lazy to refactor it.

## Background

I created this script because I do almost everything in Unity Editor. I didn’t plan to share it because nobody would
have the same workflow as mine. However, I came across some connect live mmd videos recently and noticed that the motions
are quite shaky. This script should help fix that. The result animation clip works the same as the animation clip in
regular mv timelines so you can convert them the same way for mmd. The only difference is that hip is used instead of
position. Since bodyPosition in PoseData is in world space, it needs to be divided by character height for animation clip.
(I kept some code as comments in case anyone want to know how character bodies are loaded according to height.)

## Dependencies

### Tools
- sssekai
- Unity 2022.3

### Packages
- `com.unity.nuget.newtonsoft-json`
- `https://github.com/madsbangh/EasyButtons.git#upm`

## Usage

### sssekai
- create json from rla
- the structure needs to be the same as the one in example folder

### Unity Editor
- copy to "Assets/Scripts" in your Unity project
- edit `const` paths
- edit line 258 (`_outputFolder` in `ImportAnimationFromJson()`) according to your paths
- create a new game object in the scene and attach the script to it
    - use `ImportAnimationFromFolderAndExport` with `streamingDataName`, `liveIndex`, `blockName`
    - use `ImportAnimationFromJson` with `jsonFilePath`, `height`
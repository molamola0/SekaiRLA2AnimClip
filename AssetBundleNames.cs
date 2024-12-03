namespace Sekai.Core
{
    public class AssetBundleNames
    {
        private const string STREAMING_LIVE_DATA_BUNDLE_NAME_BASE = "streaming_live/data/{0}"; // Metadata: 0x00939500
        private const string STREAMING_LIVE_ARCHIVE_NAME_BASE = "streaming_live/archive/{0}"; // Metadata: 0x009395BA

        private const string
            LIVE_CHARACTER_BODY_MODEL_BUNDLE_NAME_BASE = "live_pv/model/character/body/{0}/{1}"; // Metadata: 0x00938E6B

        public static string GetLiveCharacterBodyModelName(string bundleName, string figure) =>
            string.Format(LIVE_CHARACTER_BODY_MODEL_BUNDLE_NAME_BASE, bundleName, figure)
                .Replace("character", "characterv2");

        public static string GetStreamingLiveArchiveName(string bundleName) =>
            string.Format(STREAMING_LIVE_ARCHIVE_NAME_BASE, bundleName); // 0x03A7DDC4-0x03A7DE10

        public static string GetStreamingLiveDataName(string bundleName) =>
            string.Format(STREAMING_LIVE_DATA_BUNDLE_NAME_BASE, bundleName); // 0x03A7DBB0-0x03A7DBFC
    }
}
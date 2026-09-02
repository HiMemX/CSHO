

namespace Asset{
    public static class AvailableAssetsTEMP{
        public static HoArchive.wmlTypeID[] available = { // Only put in object types that are implemented for every, in AssetCaster specified, game.
            HoArchive.wmlTypeID.SimpleObject,
            HoArchive.wmlTypeID.Platform, // Needs work
            HoArchive.wmlTypeID.NPCGeneric,
            HoArchive.wmlTypeID.Tiki,
            HoArchive.wmlTypeID.Texture,
            HoArchive.wmlTypeID.BSP,
            HoArchive.wmlTypeID.SoundFX,
            HoArchive.wmlTypeID.Counter,
            HoArchive.wmlTypeID.Timer,
            HoArchive.wmlTypeID.SoundBankWrap,
            HoArchive.wmlTypeID.Direction,
            HoArchive.wmlTypeID.Env,
            HoArchive.wmlTypeID.Fog,
            HoArchive.wmlTypeID.Script,
            HoArchive.wmlTypeID.FloatingCollectible,
            HoArchive.wmlTypeID.Trampoline,
            HoArchive.wmlTypeID.Group,
            HoArchive.wmlTypeID.Conditional,
            HoArchive.wmlTypeID.PuckReflector,

            HoArchive.wmlTypeID.TriggerOG,
            HoArchive.wmlTypeID.Curve,

            HoArchive.wmlTypeID.LightKitScene,
            HoArchive.wmlTypeID.LightKit,
            HoArchive.wmlTypeID.Camera,
            HoArchive.wmlTypeID.Camera_Tweak,


            HoArchive.wmlTypeID.Model,
            HoArchive.wmlTypeID.GenericShader,
            HoArchive.wmlTypeID.Material,
            HoArchive.wmlTypeID.Effect,
            HoArchive.wmlTypeID.StaticGeometry,
            HoArchive.wmlTypeID.SkinGeometry, // Needs work
            HoArchive.wmlTypeID.RawBlob,

            HoArchive.wmlTypeID.UVMovementSettings,

            HoArchive.wmlTypeID.ScaleformAsset, // Bit of a stretch calling it supported but still

        };
    }
}
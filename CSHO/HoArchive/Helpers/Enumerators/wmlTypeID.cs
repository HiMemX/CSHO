using System.ComponentModel;
using System;
using System.Globalization;

namespace HoArchive{

    public class wmlTypeIDConverter : EnumConverter{
        private Type enumType;

        public wmlTypeIDConverter(Type type) : base(type){
            enumType = type;
        }


        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(wmlTypeID);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return (wmlTypeID)Enum.Parse(typeof(wmlTypeID), value as string);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return ((wmlTypeID)value).ToString();
        }

    }

    public enum wmlTypeID : uint{
        Accomplishment = 0x60AA5B15,
        ActionSet = 0x159D5F88,
        AmbientHuman = 0xC1B5C5D5,
        AmmunitionAsset = 0x26162815,
        AnimationSet = 0x6188B5F2,
        AnimBlendNode = 0xC8579886,
        AnimList = 0x69DCEB73,
        AnimPackage = 0xAD8343CD,
        AxiomHuman = 0xD8AECCB1,
        BadgePoint = 0x20E30D75,
        Beacon = 0x9C14FFEC,
        BehaviorSet = 0xDBE14A38,
        BehaviorTree = 0x846EA850,
        Binary_POI = 0xBD1100EC,
        BiplaneCurve = 0xB402075E,
        BoneMask = 0xF33D4358,
        Boulder = 0x122DFB5D,
        BoulderGenerator = 0x5D16D8B2,
        BranchSwitch = 0xEB59717A,
        BreakawayPlatform = 0x60B1E628,
        BSP = 0x0011731B,
        BungeeBall = 0xFD5F1EC7,
        BuyScreen = 0x1C5F23C6,
        Camera = 0x52FE9CED,
        CameraD = 0x784A4D8B,
        Camera_Param_Asset = 0x9CE1EF64,
        Camera_Tweak = 0x67F2E8CE,
        CamFirstPersonParamWrap = 0xA0EA0155,
        CameraFly = 0xACE8C832,
        CamLockedOnTargetParamWrap = 0x544233AE,
        CamThirdPersonParamWrap = 0xE596E9BA,
        CarlLedgeHoist = 0x53DEF75A,
        CarryableDropZone = 0x3FF8AD14,
        CARRYABLEOBJECT = 0xACA1C5B4,
        CarryableType = 0x16872A07,
        Carryable_Object = 0xDBE80CB5,
        Catapult = 0x282D8AEC,
        CaveWorm = 0x665B8B28,
        CharacterAssets = 0x098E112C,
        CharacterParametersTrapMimic = 0x0361BFCB,
        CharacterParametersTrapMouse = 0x0431AED5,
        CharacterParametersTrapProjectile = 0xB0D71DA1,
        CharacterParametersTrapRadial =	0x16F8306B,
        Charger = 0x87FEE37A,
        Chaser = 0xCC4699C8,
        ChaserProp = 0xD9A0BA57,
        Checkpoint = 0x2DE7AB98,
        Cinematic = 0xC5087DAD,
        CinematicPlayer = 0x06CB20BE,
        Clip = 0x090E5BE8,
        CollectibleAsset = 0xB25B6370,
        CollectibleSpawner = 0xB8B01678,
        CollisionMesh = 0xBA0466EB,
        ComboAnimBlob = 0x9B14DD74,
        Conditional = 0xBD93E39A,
        ConsoleCommand = 0x2BCC8468,
        ControlPoint = 0x431786EB,
        Counter = 0xC72962FC,
        CoverSpot = 0x912CD831,
        Crank = 0xA324C057,
        Crawlspace = 0x993F2EB3,
        CurrencyAsset = 0xCD13D9A7,
        Current = 0x781F523D,
        Curve = 0xA39020D3,
        CurveCamera = 0x6AC030C8,
        CylinderAsset = 0x2D181AEA,
        DamageOverTimeEffect = 0xD2C5F2C5,
        DamageVFXTable = 0xEBD5DBF9,
        Decal = 0xB2F50535,
        DecalEmitter = 0x8D0742E5,
        Destructible = 0xAAB39C14,
        DetectorHuman = 0xDA162F5F,
        DetectorHumanProp = 0x37D3691E,
        DifficultyAdjuster = 0xEBC6684B,
        Direction = 0x03E3F7C3,
        Dispatcher = 0x40E1F917,
        DragRope = 0x5E335BBA,
        DTRMovieSettings = 0xBE29D785,
        DugLever = 0xB76D6258,
        DynaLightSpawnPtAsset = 0x00EE1F83,
        Effect = 0xA0DB9C91,
        ElectricArc = 0x81BD116D,
        ElectricPoint = 0x121DA437,
        Env = 0x001239AD,
        Eve = 0x00123DB4,
        EventSpy = 0x119A5C9A,
        Exercise = 0xBEECA960,
        Exercise2 = 0xB31AAC52,
        ExercisePeripheralScreens = 0x810D395B,
        ExerciseProgram = 0xA2988934,
        ExerciseSession = 0x42C8B04A,
        ExplosionAsset = 0x3FC7CC17,
        ExplosiveObject = 0x225C7788,
        ExplosiveProperties = 0xD6FAD258,
        FGUYOneLiners = 0x097DF73E,
        FGUYPlayerTemplate = 0x49C84BB8,
        FloatingCollectible = 0xC333072E,
        FloatingCollectibleGroup = 0x5A1DFB5D,
        FloatingCollectibleSpawner = 0x8FB224E2,
        Flythrough = 0x9211AB9E,
        FMVReference = 0x950FFE9C,
        Fog = 0x00127D2A,
        Font = 0x09761067,
        FontAsset = 0xC923DEA5,
        Fountain = 0x505DD6A4,
        FXBlend = 0x6F1AD15F,
        FXInstance = 0xFC3DDC2F,
        FXObjectSystem = 0x78B4396C,
        FXParticleSystem = 0x7E6B3037,
        FXScreenWarp = 0x2452D4E2,
        FXScriptAsset = 0x9CFC6E83,
        FXScriptSpawnPtAsset = 0x172D9652,
        FXSpawn = 0x9A0C1BAD,
        Game = 0x0994B2F2,
        GenericCurve = 0x4AB0908C,
        GenericShader = 0x0A208F1C,
        GenericSpawner = 0x0D59AE69,
        GenericSwarm = 0x63CC1B35,
        Generic_Use_Property = 0x1D121A95,
        GEN_Effect = 0x92527C4C,
        GEN_GenericShader = 0xC3CF6C25,
        GEN_ImmediateGeometry = 0x9A73A824,
        GEN_ImmediatePrototype = 0x8B1E3DDC,
        GEN_ModelPrototype = 0xA580A148,
        GEN_TextureResource = 0xE9AA3502,
        GEN_Material = 0xB25B2452,
        GEN_RawBlob = 0x4DB6973E,
        GEN_RenderMode = 0xFFF701AC,
        GEN_ShaderCodeBlob = 0x14F0B536,
        GEN_SkeletonBlob = 0x517F2609,
        GEN_SkinGeometry = 0x1A2BE19E,
        GEN_StaticGeometry = 0x8B6A4513,
        GEN_VertexDecl = 0xE02D3191,
        GestureCombo = 0x9C96F291,
        GestureOrientation = 0x6010A737,
        GesturePiece = 0x7FF94B3D,
        GesturePieceGroup = 0x09DAF31A,
        GestureSubChain = 0x6227A6A6,
        Geyser = 0x88E5EB29,
        GlobalFX = 0xEF649009,
        GradientCurve = 0xB899A9FF,
        GrenadeAmmunitionAsset = 0x1730CA0F,
        Group = 0xE95F47B3,
        Gust = 0x0999F2C7,
        Gusteau = 0x5CBC0C82,
        Hammer = 0x3C9627DE,
        HazardAsset = 0x06BB987E,
        HazardPlaced = 0xB175759F,
        HealthAsset = 0x5C3BF748,
        HideyHole = 0x06F820BF,
        HintSphere = 0x77CB6428,
        HitButton = 0x1AFB16B5,
        House = 0xFA87A658,
        HouseBalloonBounce = 0x70CE290F,
        HouseFloat = 0x2D0F3710,
        HudFGUY = 0x08414B74,
        HudSB = 0xFB51045E,
        HudUFCT = 0x0A438B45,
        HudUp = 0xFB510572,
        HUD_Compass_Object = 0x50B5E94C,
        HUD_Compass_System = 0xD3BB2158,
        Icon = 0x09D9D549,
        ImmediateGeometry = 0x3206524B,
        ImmediateModel = 0xCF187818,
        InflatablePlatform = 0x63378FE3,
        InstaKill = 0x2359F315,
        Interaction = 0xA753FD36,
        InteractRopeTie = 0x3B0D78B4,
        InWorldIconWidget = 0x8765B7D6,
        KevinDanglePoint = 0x56A0230C,
        KevinSpringboard = 0x8464B338,
        LaserTurret = 0xE7606CC7,
        Launch = 0x2BBA047B,
        Ledge = 0x3F62FDD1,
        LedgeGrab = 0x257B0585,
        LedgeShimmy = 0x9F84E0E2,
        LensFlare = 0x10DD74D8,
        LensFlareSpawnPt = 0x025660D3,
        Lever = 0x3F67B37A,
        Light = 0x3FECFDEA,
        LightGradient = 0x5989E14E,
        LightKit = 0xB604E800,
        LightKitScene = 0x8E2ADD70,
        Light_Effect_Flicker = 0x376A6C63,
        Light_Effect_Strobe = 0xCCF2B67E,
        LinguiniCooking = 0xA6789A1D,
        LinguiniCookingStation = 0x80D6B00F,
        LoadingScreen = 0x65393854,
        Locator = 0xE3BC202A,
        Material = 0x189F55BF,
        Melee = 0x50F2CB64,
        MissionOG = 0xC4163794,
        MissionRibbon = 0x65A08FD2,
        MissionRibbonFX = 0x9849288C,
        Model = 0x5247BB31,
        MovePoint = 0x683317AB,
        Movie = 0x524C73D8,
        Mowbot = 0x1D3FC556,
        NatalMaskEvaluator = 0xF5E0412F,
        NatalPose = 0x5C2A8041,
        NavLink = 0xDAE03351,
        NavMarker = 0xCEEFF3D1,
        Notes = 0x63D9A219,
        NPCCounterPattern = 0xBC3D00E1,
        NPCGeneric = 0xEB1FD67E,
        NPCGenericPool = 0x93C38ACE,
        NPCGroupCentipede = 0xD41D581D,
        NPCRegulator = 0x0A01E034,
        NPCRegulatorRule = 0x7BB42D40,
        NPCRegulatorRuleGroup = 0x59915B73,
        NPCSpawner = 0x50648743,
        NPCSpawnerRulePopulate = 0x5C5FC029,
        NPCSpawnerRuleQueue = 0x503FCED6,
        NPCSpawnerRuleSet = 0xFFBCF2F3,
        NPCSpawnerRuleWave = 0xDE387DEC,
        NPCSpawnerSpawnPointFilter = 0x53D39C70,
        NPCTemplate = 0x26382C1F,
        NPC_Combat = 0xF771CA82,
        NPC_Detector_View_Blocker = 0xFB064AE7,
        NPC_FighterBrain = 0x5640C78B,
        NPC_Perception = 0xE0AB669F,
        NuiSpeechGrammarAsset = 0xAD2E9721,
        NuiSpeechGrammarList = 0xA8E481CD,
        Objective = 0x2E44DA25,
        Object_Generator = 0x21DCFDC7,
        ObjGenSpawnPtAsset = 0x3683588A,
        Option = 0x2570FEF5,
        OverlayAnimationSet = 0x9E60F08E,
        PartGenSpawnPtAsset = 0x563B06EE,
        ParticleSink = 0x9780C6D1,
        Particle_Generator = 0xB024C2B2,
        Pipe = 0x0ACB8736,
        Plant = 0x868930DB,
        PlantTrap = 0x991A389C,
        Platform = 0x9D5F550F,
        Player = 0xD836DA19,
        PlayerAICommand = 0xAC40EF6A,
        PlayerAICommandGroup = 0xB156E251,
        PlayerAnimSeq = 0xA843E279,
        PlayerCustomizationMaster = 0xF56AF480,
        PlayerCustomizationTrainer = 0xFCF890B3,
        PlayerLocker = 0xD4FDC229,
        PlayerRespawnPoint = 0x4677BC6D,
        PlayerTemplate = 0xBECBD147,
        Player_Location_Ent_Connector = 0x6A973375,
        Player_Location_Platform_Connector = 0x8C0DF1D1,
        Pointer = 0x2196C135,
        PointJump = 0xA38A1EB6,
        PolygonalVolume = 0xEA9365BB,
        Portal = 0x0F25D318,
        POWGroup = 0x763F072F,
        POWObject = 0x44D7E8AB,
        ProjectileAsset = 0x8888F48B,
        ProjectileLauncher = 0x569000FD,
        ProjectileSpawner = 0x2D44416B,
        PropSwapUFCT = 0x466ECDCC,
        Pry = 0x00151D1F,
        PuckReflector = 0xF3DA1F7F,
        Puppet = 0x783273B8,
        PushPull = 0xD5A8292B,
        QuickGesture = 0xF9110D24,
        RainControl = 0x8D9AFFF5,
        Ranger = 0x0FE1D5C1,
        RawAsset = 0x1A170D14,
        RawBlob = 0xBDE21A8D,
        Reference = 0x122BE3E3,
        Reflection = 0x555407E3,
        RenderMode = 0x714C4581,
        Ribbon = 0x9AB29AF2,
        RibbonEmitter = 0xA10B7204,
        RopeCut = 0x04A2CF44,
        RubberBand = 0xFEB96C51,
        Rumble = 0x6ED07AD5,
        Rumble_Spherical_Emitter = 0x91C941A6,
        RuntimeEntityAsset = 0x906B1CE5,
        RussellDanglePoint = 0xC6265267,
        RussellRope = 0xBD809914,
        SaveLoad = 0xDB3E2D73,
        ScaleformAsset = 0x21AA26F6,
        ScaleformPlayer = 0x798E1FDB,
        ScaleformTextures = 0xA84D6202,
        Scene = 0xB9FEA570,
        ScreenFade = 0x5E1F54A8,
        ScreenWarp = 0x60668328,
        Script = 0x2F0B4FF7,
        ShaderCodeBlob = 0x1075999B,
        ShadowMapSettings = 0xD349AF5B,
        ShadowSettings = 0xFC13350B,
        ShrapnelAssetDef = 0x394A1682,
        ShrapnelLauncher = 0x38D8465D,
        ShrapnelSpawnPtAsset = 0xC3F58C0A,
        SimpleObject = 0xAF4D6221,
        SkeletonBlob = 0x9E3F4786,
        SkinGeometry = 0x66EC031B,
        Skydome = 0x692078D4,
        Slope = 0xBB35FF43,
        Smoke_Emitter = 0xFB3E9462,
        Snifflist = 0xA5B75568,
        SoundAlertSettings = 0x90D2EE7C,
        SoundBankWrap = 0xC9F2C7F9,
        SoundCue = 0x5E6449B0,
        SoundFX = 0x124EF085,
        SoundFXMultiple = 0x20DA12E5,
        SoundMask = 0x4EA387DF,
        SoundsNamed = 0x57325F65,
        SoundWiimoteSpeaker = 0x99F85C98,
        SoundWiimoteSpeakerList = 0x6B63AE7A,
        Spinner = 0x3B314DFD,
        Springboard = 0xFED791AD,
        SSAOSettings = 0x15AEFF75,
        StaticGeometry = 0x86EF2978,
        StepUp = 0x57B85243,
        Steward = 0xE43CFC1C,
        StringReference = 0x95B46192,
        StringsAsset = 0xC414A526,
        Subtitles = 0xEF95C4CB,
        SurfaceGamePlay = 0x6037C81F,
        SwarmMean = 0x77EAF4D9,
        SwingVine = 0xC7522D24,
        TaskOG = 0x079701E5,
        TemplateInst = 0xFE17E3AC,
        Texture = 0x2952081F,
        TextureAnimationSettings = 0x0A7BD900,
        Text_Box = 0x264D04D1,
        Thief = 0xCC38E752,
        Tightrope = 0xEDFE94B4,
        Tiki = 0x0B54BB17,
        TikiScreen = 0x5921C1AB,
        Timer = 0xCC5C411D,
        ToonSettings = 0x95D0CC15,
        TortureChamber = 0x3EE41423,
        TracerAsset = 0xFA393D9F,
        Trampoline = 0x42D90BD1,
        Transition_Time = 0x60C7EC1B,
        Trap = 0x0B571151,
        TrapMimic = 0x446FC946,
        TrapMouse = 0x453FB850,
        TrapProjectile = 0x835868E2,
        TrapRadial = 0x61270A5C,
        TriggerBSPVisibility = 0xF64936D9,
        TriggerEnt = 0x3B74646B,
        TriggerOG = 0xA883D6F4,
        TriggerPhantom = 0x94BF0A61,
        Trip = 0x0B571569,
        TunnelCurve = 0x1B42F42F,
        UFCTGestureHUD = 0x5A66F1EC,
        UIDModelReference = 0xFAC61192,
        UIDReference = 0x62671137,
        UI_Controller = 0xC52EDD1B,
        UI_Flash_On_Screen_Text = 0x7891F972,
        UI_Group = 0x6B2E7BE8,
        UI_Hint_Box = 0x675A0E4A,
        UI_Image = 0x8D9AB2D0,
        UI_Model = 0xD416EF66,
        UI_Motion = 0x89E268DD,
        UI_Text = 0x46F1A08C,
        UPBiplane = 0x77D7046E,
        UpFloatingBridge = 0x60B875AA,
        UpFloatingObjectGenerator = 0x25802403,
        UPGeneric = 0xED3BC708,
        UpQuestCard = 0xF67BB513,
        Use_Property_Attract = 0xCB998CF7,
        Use_Property_Bomb = 0x9CEF538C,
        Use_Property_Glide = 0xA5D45C8F,
        Use_Property_Hide = 0x9DBB8E60,
        Use_Property_Repel = 0x65FCE896,
        Use_Property_Roll = 0x9F142CC3,
        Use_Property_Swipe = 0x79F24428,
        UVMovementSettings = 0x2F2F3873,
        Vent = 0x0B984BBD,
        VentType = 0x407A42FF,
        VertexDecl = 0x51827566,
        WallNet = 0x859FD963,
        WallNetGroup = 0xECE9F02C,
        Water = 0xFFF4CC77,
        WaterWheel = 0xCE1BB49C,
        WeaponAsset = 0x9DE475D0,
        Whirlpool = 0x71AA8182,
        zFGUYChallengeAsset = 0x48C4D9B4,
        zOneLiner = 0xC17B61BA}
}
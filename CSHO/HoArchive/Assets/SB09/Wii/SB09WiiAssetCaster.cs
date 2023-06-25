
namespace SB09WiiAsset{
    public static class SB09WiiAssetCaster{
        public static Asset.AssetEntity ReadAsset(HoArchive.MemoryStreamEndian file, HoArchive.wmlTypeID wmlTypeID){
            if(wmlTypeID == HoArchive.wmlTypeID.AnimationSet){return new AnimationSet(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.AnimBlendNode){return new AnimBlendNode(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.AnimList){return new AnimList(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.BehaviorSet){return new BehaviorSet(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.BehaviorTree){return new BehaviorTree(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Binary_POI){return new Binary_POI(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.BreakawayPlatform){return new BreakawayPlatform(file);}  
            if(wmlTypeID == HoArchive.wmlTypeID.BSP){return new BSP(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.BungeeBall){return new BungeeBall(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.BuyScreen){return new BuyScreen(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Camera){return new Camera(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.CameraD){return new CameraD(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Camera_Tweak){return new Camera_Tweak(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.CharacterAssets){return new CharacterAssets(file);}      
            if(wmlTypeID == HoArchive.wmlTypeID.Checkpoint){return new Checkpoint(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.CollectibleSpawner){return new CollectibleSpawner(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.CollisionMesh){return new CollisionMesh(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.ComboAnimBlob){return new ComboAnimBlob(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Conditional){return new Conditional(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Counter){return new Counter(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Curve){return new Curve(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.CurveCamera){return new CurveCamera(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.CylinderAsset){return new CylinderAsset(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Decal){return new Decal(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Destructible){return new Destructible(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Direction){return new Direction(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Dispatcher){return new Dispatcher(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Effect){return new Effect(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.ElectricArc){return new ElectricArc(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.ElectricPoint){return new ElectricPoint(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Env){return new Env(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.FloatingCollectible){return new FloatingCollectible(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.FMVReference){return new FMVReference(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Fog){return new Fog(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Font){return new Font(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.FontAsset){return new FontAsset(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Fountain){return new Fountain(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.FXInstance){return new FXInstance(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.FXObjectSystem){return new FXObjectSystem(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.FXParticleSystem){return new FXParticleSystem(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.FXScreenWarp){return new FXScreenWarp(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.FXSpawn){return new FXSpawn(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.GenericShader){return new GenericShader(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.GenericSpawner){return new GenericSpawner(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.GlobalFX){return new GlobalFX(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.GradientCurve){return new GradientCurve(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Group){return new Group(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Hammer){return new Hammer(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.HintSphere){return new HintSphere(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.HitButton){return new HitButton(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.HudSB){return new HudSB(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.ImmediateGeometry){return new ImmediateGeometry(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.ImmediateModel){return new ImmediateModel(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.InflatablePlatform){return new InflatablePlatform(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.InWorldIconWidget){return new InWorldIconWidget(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Light){return new Light(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.LightKit){return new LightKit(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.LightKitScene){return new LightKitScene(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Light_Effect_Flicker){return new Light_Effect_Flicker(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Light_Effect_Strobe){return new Light_Effect_Strobe(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.LoadingScreen){return new LoadingScreen(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Material){return new Material(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Model){return new Model(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.MovePoint){return new MovePoint(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.NPCGeneric){return new NPCGeneric(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.NPCGenericPool){return new NPCGenericPool(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.NPCTemplate){return new NPCTemplate(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.NPC_Combat){return new NPC_Combat(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.NPC_Perception){return new NPC_Perception(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.PlantTrap){return new PlantTrap(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Platform){return new Platform(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Player){return new Player(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.PlayerAnimSeq){return new PlayerAnimSeq(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Player_Location_Ent_Connector){return new Player_Location_Ent_Connector(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.PolygonalVolume){return new PolygonalVolume(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Portal){return new Portal(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.ProjectileAsset){return new ProjectileAsset(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.PuckReflector){return new PuckReflector(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.RawAsset){return new RawAsset(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.RawBlob){return new RawBlob(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Reference){return new Reference(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.RenderMode){return new RenderMode(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Ribbon){return new Ribbon(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.RubberBand){return new RubberBand(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Rumble){return new Rumble(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.ScaleformAsset){return new ScaleformAsset(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.ScaleformPlayer){return new ScaleformPlayer(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Scene){return new Scene(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.ScreenFade){return new ScreenFade(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.ScreenWarp){return new ScreenWarp(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Script){return new Script(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.ShaderCodeBlob){return new ShaderCodeBlob(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.ShrapnelAssetDef){return new ShrapnelAssetDef(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.SimpleObject){return new SimpleObject(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.SkeletonBlob){return new SkeletonBlob(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.SkinGeometry){return new SkinGeometry(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.SoundBankWrap){return new SoundBankWrap(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.SoundCue){return new SoundCue(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.SoundFX){return new SoundFX(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.SoundFXMultiple){return new SoundFXMultiple(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.SoundsNamed){return new SoundsNamed(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.SoundWiimoteSpeakerList){return new SoundWiimoteSpeakerList(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Spinner){return new Spinner(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.StaticGeometry){return new StaticGeometry(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.StringsAsset){return new StringsAsset(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Subtitles){return new Subtitles(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.SurfaceGamePlay){return new SurfaceGamePlay(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.TemplateInst){return new TemplateInst(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Texture){return new Texture(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.TextureAnimationSettings){return new TextureAnimationSettings(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Tiki){return new Tiki(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.TikiScreen){return new TikiScreen(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Timer){return new Timer(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Trampoline){return new Trampoline(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.Transition_Time){return new Transition_Time(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.TriggerEnt){return new TriggerEnt(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.TriggerOG){return new TriggerOG(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.TriggerPhantom){return new TriggerPhantom(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.UIDModelReference){return new UIDModelReference(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.UIDReference){return new UIDReference(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.UI_Controller){return new UI_Controller(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.UI_Flash_On_Screen_Text){return new UI_Flash_On_Screen_Text(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.UI_Image){return new UI_Image(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.UI_Motion){return new UI_Motion(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.UVMovementSettings){return new UVMovementSettings(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.VertexDecl){return new VertexDecl(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.WallNet){return new WallNet(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.WallNetGroup){return new WallNetGroup(file);}
            if(wmlTypeID == HoArchive.wmlTypeID.WaterWheel){return new WaterWheel(file);}
            return null;
        }
    }
}
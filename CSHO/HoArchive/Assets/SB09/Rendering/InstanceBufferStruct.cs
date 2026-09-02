using System.Collections.Generic;

namespace RenderingInternal;

public class InstanceBuffer
{
    public GenericBuffer buffer = new();
    public List<InstanceInfo> CulledInstanceInfos = new(); // Temporary storage space for culled instance infos
    public List<InstanceInfo> InstanceInfos = new(); // Contains instancing data
        
}
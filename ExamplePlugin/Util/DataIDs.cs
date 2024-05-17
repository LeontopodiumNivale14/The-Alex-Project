using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ExamplePlugin.Util;

public static class DataIDs
{

    //////////// Alexander - The Burden of the father IDs ////////////
    public const uint LeftLegId = 4108;
    public const uint RightLegId = 4107;
    public const uint ManipulatorId = 3902;
    public const uint PanzerDollId = 3906;

    public static readonly Vector3 LeftLegLocation = new(6.4599f, 10.544f, 7.385f);
    public static readonly Vector3 RightLegLocation = new(6.3969f, 10.544f, -7.476f);
    public static readonly Vector3 ManipulatorLocation = new(-0.015f, 10.594f, 13.862f);

    // 
    public static readonly HashSet<uint> A4NIDs = [4108, 4107, 3902, 3906];

    //////////// Limsa IDs ////////////
    public const ushort LimsaMapId = 129;
    public const uint LimsaAetherytId = 8;
    public const uint RetainerBellDataId = 2000401;
    public static readonly Vector3 RetainerBellLocation = new(-124.1676f, 18f, 19.9041f);

    public const ushort A4NMapId = 445;

    public static readonly HashSet<uint> ChestIDs =
        [1036, 1037, 1038, 1039, 1040, 1041, 1042, 1043, 1044, 1045, 1046, 1047, 1048, 1049, 2007357, 2007358];
}

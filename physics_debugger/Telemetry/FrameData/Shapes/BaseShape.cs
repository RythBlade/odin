using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Telemetry.FrameData.Shapes
{
    public enum ShapeType : uint
    {
        eObb
        , eSphere
        , eCone
        , eConvexHull
    };

    public class BaseShape
    {
        public int Id = -1;
        public bool HasLocalMatrix = false;
        public Matrix4x4 LocalMatrix = new Matrix4x4();
        public ShapeType ShapeType = ShapeType.eObb;
    }
}

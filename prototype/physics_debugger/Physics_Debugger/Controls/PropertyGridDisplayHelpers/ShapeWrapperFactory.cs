using Telemetry.FrameData.Shapes;

namespace physics_debugger.Controls.PropertyGridDisplayHelpers
{
    public class ShapeWrapperFactory
    {
        public static BaseShapePropertyWrapper BuildPropertyWrapperForShape(BaseShape baseShape)
        {
            BaseShapePropertyWrapper wrapperToReturn = null;

            switch (baseShape.ShapeType)
            {
                case ShapeType.eObb:
                    wrapperToReturn = new ObbPropertyWrapper((ObbShape)baseShape);
                    break;
                case ShapeType.eSphere:
                    break;
                case ShapeType.eCone:
                    break;
                case ShapeType.eConvexHull:
                    wrapperToReturn = new ConvexHullPropertyWrapper((ConvexHullShape)baseShape);
                    break;
                case ShapeType.eTetrahedron:
                    wrapperToReturn = new TetrahedronPropertyWrapper((TetrahedronShape)baseShape);
                    break;
                default:
                    wrapperToReturn = new BaseShapePropertyWrapper(baseShape);
                    break;
            }

            return wrapperToReturn;
        }
    }
}

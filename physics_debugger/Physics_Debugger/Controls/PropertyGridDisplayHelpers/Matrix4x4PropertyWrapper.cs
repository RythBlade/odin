using System.ComponentModel;
using System.Numerics;

namespace physics_debugger.Controls.PropertyGridDisplayHelpers
{
    // tag the class with it's type converter so it can be expanded in a property grid view
    [TypeConverter(typeof(Matrix4x4PropertyWrapperTypeConverter))]
    public class Matrix4x4PropertyWrapper
    {
        [BrowsableAttribute(false)]
        private Matrix4x4 MatrixToWrap { get; }

        public float M11 { get { return MatrixToWrap.M11; } }
        public float M12 { get { return MatrixToWrap.M12; } }
        public float M13 { get { return MatrixToWrap.M13; } }
        public float M14 { get { return MatrixToWrap.M14; } }
        public float M21 { get { return MatrixToWrap.M21; } }
        public float M22 { get { return MatrixToWrap.M22; } }
        public float M23 { get { return MatrixToWrap.M23; } }
        public float M24 { get { return MatrixToWrap.M24; } }
        public float M31 { get { return MatrixToWrap.M31; } }
        public float M32 { get { return MatrixToWrap.M32; } }
        public float M33 { get { return MatrixToWrap.M33; } }
        public float M34 { get { return MatrixToWrap.M34; } }
        public float M41 { get { return MatrixToWrap.M41; } }
        public float M42 { get { return MatrixToWrap.M42; } }
        public float M43 { get { return MatrixToWrap.M43; } }
        public float M44 { get { return MatrixToWrap.M44; } }

        public Matrix4x4PropertyWrapper(Matrix4x4 toWrap)
        {
            MatrixToWrap = toWrap;
        }

        // Override ToString so it appears nicely in the property grid
        public override string ToString()
        {
            return $"({ MatrixToWrap.M11 }, { MatrixToWrap.M12 }, { MatrixToWrap.M13 }, { MatrixToWrap.M14 }), "
                 + $"({ MatrixToWrap.M21 }, { MatrixToWrap.M22 }, { MatrixToWrap.M23 }, { MatrixToWrap.M24 }), "
                 + $"({ MatrixToWrap.M31 }, { MatrixToWrap.M32 }, { MatrixToWrap.M33 }, { MatrixToWrap.M34 }), "
                 + $"({ MatrixToWrap.M41 }, { MatrixToWrap.M42 }, { MatrixToWrap.M43 }, { MatrixToWrap.M44 })";
        }
    }
}

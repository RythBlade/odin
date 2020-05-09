namespace physics_debugger.FrameData.Properties
{
    public class FloatProperty : IBaseProperty
    {
        public float Value = 0.0f;

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}

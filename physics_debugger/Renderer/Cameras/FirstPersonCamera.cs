using SharpDX;

namespace Renderer.Cameras
{
    public class FirstPersonCamera : ICamera
    {
        private float pitch;
        private float roll;
        private float yaw;

        private Matrix viewMatrix;
        private Matrix projectionMatrix;

        private Vector3 cameraPosition;
        private Vector3 cameraLongitudinal;
        private Vector3 cameraUp;
        private Vector3 cameraLateral;

        private float fieldOfView;
        private Vector2 backBufferResolution;

        public Matrix ViewMatrix { get { return viewMatrix; } }

        public Matrix ProjectionMatrix { get { return projectionMatrix; } }

        public Vector3 CameraPosition
        {
            get { return cameraPosition; }
            set
            {
                cameraPosition = value;
                UpdateCameraVectors();
            }
        }

        public Vector2 BackBufferResolution
        {
            get { return backBufferResolution; }
            set
            {
                backBufferResolution = value;
                UpdateCameraVectors();
            }
        }

        public float NearClipPlane { get; set; }
        public float FarClipPlane { get; set; }
        public float FieldOfView { get; set; }

        public float Pitch
        {
            get { return pitch; }
            set
            {
                pitch = value;
                
                float piOverTwo = MathUtil.Pi / 2.0f;

                if (pitch > piOverTwo)
                {
                    pitch = piOverTwo;
                }
                else if (pitch < -piOverTwo)
                {
                    pitch = -piOverTwo;
                }

                UpdateCameraVectors();
            }
        }

        public float Roll
        {
            get { return roll; }
            set
            {
                roll = value;
                UpdateCameraVectors();
            }
        }

        public float Yaw
        {
            get { return yaw; }
            set
            {
                yaw = value;
                UpdateCameraVectors();
            }
        }

        public FirstPersonCamera()
        {
            cameraPosition = new Vector3(0.0f, 0.0f, 0.0f);
            cameraLongitudinal = new Vector3(0.0f, 0.0f, -1.0f);
            cameraUp = new Vector3(0.0f, 1.0f, 0.0f);
            cameraLateral = new Vector3(-1.0f, 0.0f, 0.0f);

            pitch = 0.0f;
            yaw = 0.0f;
            roll = 0.0f;

            NearClipPlane = 1.0f;
            FarClipPlane = 1000.0f;
            fieldOfView = MathUtil.Pi / 4.0f;

            backBufferResolution.X = 1.0f;
            backBufferResolution.Y = 1.0f;
        }

        public void SetMatrices()
        {
            SetViewMatrix();
            SetProjectionMatrix();
        }

        private void SetViewMatrix()
        {
            Vector3 cameraTarget = cameraPosition + cameraLongitudinal;

            viewMatrix = Matrix.LookAtLH(cameraPosition, cameraTarget, cameraUp);
        }

        /*
          Sets the projection transformation matrix.
          */
        private void SetProjectionMatrix()
        {
            projectionMatrix = Matrix.PerspectiveFovLH(fieldOfView, backBufferResolution.X / backBufferResolution.Y, NearClipPlane, FarClipPlane);
        }

        private void UpdateCameraVectors()
        {
            Matrix pitchYawRoll = Matrix.RotationYawPitchRoll(yaw, pitch, roll);

            Vector4 cameraTarget = new Vector4(0.0f, 0.0f, -1.0f, 0.0f);
            Vector4 newCameraUp = new Vector4(0.0f, 1.0f, 0.0f, 0.0f);
            Vector4 cameraRight = new Vector4(-1.0f, 0.0f, 0.0f, 0.0f);

            cameraTarget = Vector4.Transform(cameraTarget, pitchYawRoll);
            newCameraUp = Vector4.Transform(newCameraUp, pitchYawRoll);
            cameraRight = Vector4.Transform(cameraRight, pitchYawRoll);

            cameraTarget.Normalize();
            newCameraUp.Normalize();
            cameraRight.Normalize();

            cameraLongitudinal = new Vector3(cameraTarget.X, cameraTarget.Y, cameraTarget.Z);
            cameraUp = new Vector3(newCameraUp.X, newCameraUp.Y, newCameraUp.Z);
            cameraLateral = new Vector3(cameraRight.X, cameraRight.Y, cameraRight.Z);
        }

        public void MoveCameraLongitudinal(float distance)
        {
            if (distance != 0.0f)
            {
                cameraPosition += distance * cameraLongitudinal;
            }
        }

        public void MoveCameraLateral(float distance)
        {
            if (distance != 0.0f)
            {
                cameraPosition += distance * cameraLateral;
            }
        }

        public void MoveCameraUp(float distance)
        {
            if (distance != 0.0f)
            {
                cameraPosition += distance * cameraUp;
            }
        }
    }
}

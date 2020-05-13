using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace physics_debugger
{
    public enum PlayBackState
    {
        /// <summary>
        /// Display a single static frame (aka paused mode)
        /// </summary>
        eStaticFrame,

        /// <summary>
        /// Play the live telemetry as it comes in
        /// </summary>
        eLive,

        /// <summary>
        /// Play buffer forwards
        /// </summary>
        eForwards,

        /// <summary>
        /// Play buffer backwards
        /// </summary>
        eBackwards,
    }

    public class FrameController
    {
        private int maxFrameId;
        public int MaxFrameId 
        { 
            get { return maxFrameId; }
            set 
            {
                if (value < 0)
                {
                    maxFrameId = 0;
                }
                else
                {
                    maxFrameId = value;
                }
            }
        }

        public PlayBackState State { get; set; }

        private int currentFrameId;
        public int CurrentFrameId 
        {
            get { return currentFrameId; }
            set
            {
                int frameId = value;

                if(frameId > MaxFrameId)
                {
                    frameId = MaxFrameId;
                }

                if(frameId < 0)
                {
                    frameId = 0;
                }

                currentFrameId = frameId;
            }
        }

        public FrameController()
        {
            MaxFrameId = 0;
            State = PlayBackState.eLive;
            CurrentFrameId = 0;
        }

        public void Update()
        {
            switch (State)
            {
                case PlayBackState.eLive:
                    GoToLastFrame();
                    break;
                case PlayBackState.eForwards:
                    ++CurrentFrameId;
                    break;
                case PlayBackState.eBackwards:
                    --CurrentFrameId;
                    break;
                case PlayBackState.eStaticFrame:
                default:
                    // don't move frames if we're in static frame mode
                    break;
            }
        }

        public void GoToNextFrame()
        {
            ++CurrentFrameId;
            State = PlayBackState.eStaticFrame;
        }

        public void GoToPreviousFrame()
        {
            --CurrentFrameId;
            State = PlayBackState.eStaticFrame;
        }

        public void GoToLastFrame()
        {
            CurrentFrameId = MaxFrameId;
            State = PlayBackState.eLive;
        }

        public void GoToFirstFrame()
        {
            CurrentFrameId = 0;
            State = PlayBackState.eStaticFrame;
        }
    }
}

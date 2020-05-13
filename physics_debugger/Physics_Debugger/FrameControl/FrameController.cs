using System;

namespace physics_debugger.FrameControl
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
                int oldMaxFrame = maxFrameId;

                if (value < 0)
                {
                    maxFrameId = 0;
                }
                else
                {
                    maxFrameId = value;
                }

                if (maxFrameId != oldMaxFrame)
                {
                    MaxFrameChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        private PlayBackState state;
        public PlayBackState State 
        { 
            get { return state; }
            set 
            {
                PlayBackState oldState = state;
                state = value;

                if (state != oldState)
                {
                    StateChanged?.Invoke(this, new EventArgs());
                }
            }
        }

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

                int oldFrameId = currentFrameId;

                currentFrameId = frameId;

                if(oldFrameId != currentFrameId)
                {
                    FrameChanged?.Invoke(this, new EventArgs());
                }   
            }
        }

        public event ThresholdReachedEventHandler StateChanged;
        public event ThresholdReachedEventHandler MaxFrameChanged;
        public event ThresholdReachedEventHandler FrameChanged;

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

using System;
using System.Collections.Generic;
using System.Threading;
using Telemetry.FrameData;
using Telemetry.Network;

namespace physics_debugger
{
    public class TelemetryReceiver
    {
        private Thread receiverThread = null;
        public Telemetry.Network.DataStream DataStream = null;
        private Telemetry.Network.PacketTranslator Translator = null;

        public object LockObject { get; }

        public Queue<FrameSnapshot> ReceivedFrameSnapshots { get; }
        public Queue<PacketTranslator.CollectedFrameShapes> ReceivedShapes { get; }

        public bool StopThreadSwitch = false;

        public TelemetryReceiver(Telemetry.Network.DataStream dataStream, Telemetry.Network.PacketTranslator translator)
        {
            DataStream = dataStream;
            Translator = translator;

            LockObject = new object();
            ReceivedFrameSnapshots = new Queue<FrameSnapshot>();
            ReceivedShapes = new Queue<PacketTranslator.CollectedFrameShapes>();
        }

        public void StartReceiverThread()
        {
            lock (LockObject)
            {
                ReceivedFrameSnapshots.Clear();
                ReceivedShapes.Clear();
            }

            StopThreadSwitch = false;

            receiverThread = new Thread(recv);
            receiverThread.Start(this);
        }

        public void StopThread()
        {
            if (receiverThread != null)
            {
                StopThreadSwitch = true;

                // wait for the thread to see the "stop" flag and stop
                receiverThread.Join();
            }
        }

        private static void recv(object inputPayload)
        {
            TelemetryReceiver payload = (TelemetryReceiver)inputPayload;

            // while we're connected and no request has been made to stop processing
            while (payload.DataStream.Connected && !payload.StopThreadSwitch)
            {
                Telemetry.Network.BasePacketHeader basePacket = payload.DataStream.ReceiveData();

                if (basePacket != null)
                {
                    if (payload.Translator.TranslatePacket(basePacket))
                    {
                        // todo: error - don't pull out packets that aren't complete
                        foreach (Tuple<bool, FrameSnapshot> snapshot in payload.Translator.ConstructedSnaphots.Values)
                        {
                            lock (payload.LockObject)
                            {
                                payload.ReceivedFrameSnapshots.Enqueue(snapshot.Item2);
                            }
                        }

                        foreach (PacketTranslator.CollectedFrameShapes frameShapeList in payload.Translator.AddedShapes.Values)
                        {
                            lock (payload.LockObject)
                            {
                                payload.ReceivedShapes.Enqueue(frameShapeList);
                            }
                        }

                        payload.Translator.ConstructedSnaphots.Clear();
                        payload.Translator.AddedShapes.Clear();
                    }
                    else
                    {
                        Console.WriteLine($"Error: read unknown packet type: {basePacket.PacketBytes}");
                    }
                }
                else
                {
                    Console.WriteLine("No data to receive");
                    // todo - this number should be driven by the frame update time sent over or by a config option
                    Thread.Sleep(5); // we're running faster/as fast as the producer of the data - take a brief break
                }
            }
        }
    }
}

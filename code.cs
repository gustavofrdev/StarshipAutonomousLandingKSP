using KRPC.Client;
using KRPC.Client.Services.KRPC;
using KRPC.Client.Services.SpaceCenter;
using System;
using System.Net;

/// <summary>
/// 
/// </summary>
class Program
{
    public static void Main()
    {
        using (var connection = new Connection(
        name: "My Example Program",
        address: IPAddress.Parse("127.0.0.1"),
        rpcPort: 50000,
        streamPort: 50001))
        {
            var krpc = connection.KRPC();
            Console.WriteLine(krpc.GetStatus().Version);

        }
        bool reached_off_alture = false;
        bool reached_off_alture2 = false;
        bool pushed_last_burn = false;
        bool Engine1Shutdown = false;
        bool Engine2Shutdown = false;
        bool Engine3Shutdown = false;
        bool ManuverStarted = false;
        bool LandPitchSeq = false;
        bool StartLandMode = false;
        using (var connection = new Connection())
        {
            var spaceCenter = connection.SpaceCenter();
            var vessel = spaceCenter.ActiveVessel;
            var control = vessel.Control;
            vessel.Name = "SpaceFlightAcademy";
            var srfFrame = vessel.Orbit.Body.ReferenceFrame;
            var flightInfo = vessel.Flight();
            System.Threading.Thread.Sleep(5000);
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"c:\contagem.wav");
            player.Play();
            var flap1__ = vessel.Parts.ControlSurfaces[0];
            flap1__.Deployed = false;
            var flap2__ = vessel.Parts.ControlSurfaces[1];
            flap2__.Deployed = false;
            var flap3__ = vessel.Parts.ControlSurfaces[2];
            flap3__.Deployed = false;
            var flap4__ = vessel.Parts.ControlSurfaces[3];
            flap4__.Deployed = false;
            Console.WriteLine(flightInfo.MeanAltitude);
            Console.WriteLine($"10");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine($"9");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine($"8");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine($"7");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine($"6");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine($"5");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine($"4");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine($"3");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine($"2");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine($"1");
            Console.WriteLine($"GO FOR LAUNCH!");
            control.Throttle = 1f;
            Console.WriteLine(flightInfo.Roll);
            control.RCS = true;
            control.Gear = false;
            control.SAS = true;
            control.SASMode = 0;
            control.ActivateNextStage();
        
            vessel.Flight();
            void run()
            {
                Console.WriteLine("Landing Sequence Started");
                System.Threading.Thread.Sleep(500);
                vessel.AutoPilot.TargetPitch = 100;
                System.Threading.Thread.Sleep(1000);
                vessel.AutoPilot.TargetPitch = 84;
                System.Threading.Thread.Sleep(1000);
                vessel.AutoPilot.TargetPitch = 90;
            }
            bool acess_84 = false;
            System.Threading.Thread T1 = new System.Threading.Thread(new System.Threading.ThreadStart(run));
            void run2()
            {
                Console.WriteLine("Landing Sequence Started");

            }
            System.Threading.Thread tempo2 = new System.Threading.Thread(new System.Threading.ThreadStart(run2));
            while (true)
            {
                var srfSpeed = vessel.Flight(srfFrame).Speed;
                if (flightInfo.MeanAltitude >= 2000 && flightInfo.MeanAltitude <= 3000 && !Engine1Shutdown)
                {
                    // Desligar o primeiro motor
                    var Engine1 = vessel.Parts.Engines[2];
                    Engine1.Active = false;
                    Console.WriteLine("Desligando motor 2");
                    Engine1Shutdown = true;
         


                }
                if (flightInfo.MeanAltitude >= 6000 && flightInfo.MeanAltitude <= 7000 && !Engine2Shutdown)
                {
                    // Desligar o segundo motor
                    var Engine1 = vessel.Parts.Engines[0];
                    Engine1.Active = false;
                    Console.WriteLine("Desligando motor 0");
                    control.Throttle = 0.45f;
                    Engine2Shutdown = true;

                }

                if (flightInfo.MeanAltitude >= 9000 && flightInfo.MeanAltitude <= 10000 && !Engine3Shutdown)
                {
                    // Desligar o terceiro motor
                    control.Throttle = 0.1f;
                    Console.WriteLine("All Engines Shutdown/No throttle, only visual");
                    Engine3Shutdown = true;
                }
                if (Engine1Shutdown && Engine2Shutdown && Engine3Shutdown && !ManuverStarted && srfSpeed <= 25)
                {
                    var Engine1 = vessel.Parts.Engines[1];
                    Engine1.Active = false;
                    BeginFlipManuver();
                    ManuverStarted = true;
                    break;
                }
                System.Threading.Thread.Sleep(50);
            }

            void BeginFlipManuver()
            {
                // Reativar motores, porém deixar o THROTTLE em zero. O throttle será ativado dps em 1/1.5 de potencia, não é total
                control.Throttle = 0;

                // Retrair todos os flaps
                var flap1 = vessel.Parts.ControlSurfaces[0];
                flap1.Deployed = true;
                var flap2 = vessel.Parts.ControlSurfaces[1];
                flap2.Deployed = true;
                var flap3 = vessel.Parts.ControlSurfaces[2];
                flap3.Deployed = true;
                var flap4 = vessel.Parts.ControlSurfaces[3];
                flap4.Deployed = true;
                // Flip Manuver
                vessel.AutoPilot.Engage();

                vessel.AutoPilot.TargetPitch = -3;

            }
            while (true)
            {
                if (ManuverStarted == true && flightInfo.MeanAltitude < 1000)
                {
                     Random _random = new Random();
                    vessel.AutoPilot.TargetRoll = 0;
                    Console.WriteLine(flightInfo.Roll);
                    vessel.AutoPilot.TargetPitch = 120;

                    var flap3_ = vessel.Parts.ControlSurfaces[2];
                    var flap4_ = vessel.Parts.ControlSurfaces[3];
                    flap4_.Deployed = false;
                    flap3_.Deployed = false;
                    control.SAS = true;
                    control.Throttle = 0.85f;
                    var Engine1 = vessel.Parts.Engines[0];
                    var Engine2 = vessel.Parts.Engines[1];
                    var Engine3 = vessel.Parts.Engines[2];
                    Engine1.Active = true;
                    System.Threading.Thread.Sleep(_random.Next(500, 1000));
                    Engine2.Active = true;
                    System.Threading.Thread.Sleep(_random.Next(500, 1000));
                    Engine3.Active = true;
                    System.Threading.Thread.Sleep(4200);
                    if (flightInfo.HorizontalSpeed <= 0)
                    {
                        StartLandMode = true;
                        LandPitchSeq = true;
                        T1.Name = "Landing Pitch Sequence";
                        T1.Start();
                        break;
                    }
                    else
                    {
                        control.Throttle = 1f;
                    }
                }
                System.Threading.Thread.Sleep(5);

            }
            bool T2_Started = false;
            bool tempo2_pitch_seq_on = false;
            {
                while (true)
                {
                    if (StartLandMode == true)
                    {
                        var flap1_ = vessel.Parts.ControlSurfaces[0];
                        var flap2_ = vessel.Parts.ControlSurfaces[3];
                        var flap3_ = vessel.Parts.ControlSurfaces[2];
                        var flap4_ = vessel.Parts.ControlSurfaces[3];
                        flap4_.Deployed = true;
                        flap3_.Deployed = true;
                        flap1_.Deployed = true;
                        flap2_.Deployed = true;
                        if (flightInfo.MeanAltitude <= 115)
                        {
                            T2_Started = true;
                        }

                        var srfFrame2 = vessel.Orbit.Body.ReferenceFrame;
                        var srfSpeed2 = vessel.Flight(srfFrame).Speed;
                        var Engine1_ = vessel.Parts.Engines[2];
                        Engine1_.Active = false;
                        float increase = 0;
                        if (!T2_Started)
                        {
                            control.Throttle = (float)((1 * (Math.Sqrt(srfSpeed2 / 3)) / 1.6) / 2.2) + (increase);
                           
                            vessel.AutoPilot.TargetPitch = 88;
                        }
                        else
                        {


                            float Throttle = (float)((1 * (Math.Sqrt(srfSpeed2 / 4)) / 1.6) / 1.04) + (increase);
                            Console.WriteLine(Throttle);
                            if (Throttle <= 0)
                            {
                                Throttle = 0.1f;
                            }
                            control.Throttle = Throttle;
                            T2_Started = true;
                            vessel.AutoPilot.TargetPitch = 90;
                            control.Gear = true;

                        }

                        if ((float)((1 * (Math.Sqrt(srfSpeed2 / 4)) / 1.6) / 1.7) < 0.15 || flightInfo.SurfaceAltitude <= 5)

                        {
                            control.Throttle = 0;
                            control.Brakes = false;
                            break;
                        }
                    }
                    System.Threading.Thread.Sleep(5);
                }


            }

        }
    }
}

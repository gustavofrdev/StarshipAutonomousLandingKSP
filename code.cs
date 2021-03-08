using KRPC.Client;
using KRPC.Client.Services.KRPC;
using KRPC.Client.Services.SpaceCenter;
using System;
using System.Net;

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
            Console.WriteLine(flightInfo.Roll);
            control.RCS = true;
            control.Gear = false;
            control.SAS = true;
            control.SASMode = 0;
            control.Throttle = 1;
            control.ActivateNextStage();
            control.Throttle = 1;
            vessel.Flight();
            while (true)
            {
                var srfSpeed = vessel.Flight(srfFrame).Speed;
                if (flightInfo.MeanAltitude >= 2000 && flightInfo.MeanAltitude <= 3000 && !Engine1Shutdown)
                {
                    // Desligar o primeiro motor
                    var Engine1 = vessel.Parts.Engines[0];
                    Engine1.Active = false;
                    Console.WriteLine("Desligando motor 0");
                    Engine1Shutdown = true;

                }
                if (flightInfo.MeanAltitude >= 6000 && flightInfo.MeanAltitude <= 7000 && !Engine2Shutdown)
                {
                    // Desligar o segundo motor
                    var Engine1 = vessel.Parts.Engines[1];
                    Engine1.Active = false;
                    Console.WriteLine("Desligando motor 1");
                    control.Throttle = 0.45f;
                    Engine2Shutdown = true;

                }

                if (flightInfo.MeanAltitude >= 9000 && flightInfo.MeanAltitude <= 10000 && !Engine3Shutdown)
                {
                    // Desligar o terceiro motor
                    var Engine1 = vessel.Parts.Engines[2];
                    Engine1.Active = false;
                    Console.WriteLine("All Engines Shutdown");
                    Engine3Shutdown = true;
                }
                if (Engine1Shutdown && Engine2Shutdown && Engine3Shutdown && !ManuverStarted && srfSpeed <= 25)
                {
                    BeginFlipManuver();
                    ManuverStarted = true;
                    break;
                }
                System.Threading.Thread.Sleep(5);
            }

            void BeginFlipManuver()
            {
                // Reativar motores, porém deixar o THROTTLE em zero. O throttle será ativado dps em 1/1.5 de potencia, não é total
                control.Throttle = 0;
                var Engine1 = vessel.Parts.Engines[0];
                var Engine2 = vessel.Parts.Engines[1];
                var Engine3 = vessel.Parts.Engines[2];
                Engine1.Active = true;
                Engine2.Active = true;
                Engine3.Active = true;
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
                if (ManuverStarted == true && flightInfo.MeanAltitude < 425)
                {
                    Console.WriteLine(flightInfo.Roll);
                    vessel.AutoPilot.TargetPitch = 120;
                    vessel.AutoPilot.TargetRoll = 0;
                    var flap3_ = vessel.Parts.ControlSurfaces[0];
                    var flap4_ = vessel.Parts.ControlSurfaces[1];
                    flap4_.Deployed = false;
                    flap3_.Deployed = false;
                    control.SAS = true;
                    control.Throttle = 1f;
                    System.Threading.Thread.Sleep(5200);
                    if (flightInfo.HorizontalSpeed <= 0)
                    {
                        vessel.AutoPilot.TargetPitch = 90;
                        StartLandMode = true;
                        break;
                    }
                    else
                    {
                        control.Throttle = 1f;
                    }
                }
                System.Threading.Thread.Sleep(5);

            }

            {
                while (true)
                {
                    if (StartLandMode == true)
                    {
                       
                        var srfFrame2 = vessel.Orbit.Body.ReferenceFrame;
                        var srfSpeed2 = vessel.Flight(srfFrame).Speed;
                        var Engine1_ = vessel.Parts.Engines[1];
                        Engine1_.Active = false;
                        control.Roll = 6;
                        float increase = 0;
                        Console.WriteLine((float)((1 * (Math.Sqrt(srfSpeed2 / 4)) / 1.6) / 1.1));
                        control.Throttle = (float)((1 * (Math.Sqrt(srfSpeed2 / 4)) / 1.6) / 1.1);
                        control.Gear = true;
                    
                        if ((float)((1 * (Math.Sqrt(srfSpeed2 / 4)) / 1.6) / 1.7) < 0.35 || flightInfo.SurfaceAltitude <= 5)
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

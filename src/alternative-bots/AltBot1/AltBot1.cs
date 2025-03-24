using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class AltBot1 : Bot
{
    private bool movingForward;
    private double moveAmount;
    private bool peek;

    static void Main()
    {
        new AltBot1().Start();
    }

    AltBot1() : base(BotInfo.FromFile("AltBot1.json")) { }

    public override void Run()
    {
        BodyColor = Color.Blue;
        TurretColor = Color.Black;
        RadarColor = Color.Red;
        BulletColor = Color.Green;
        ScanColor = Color.Yellow;

        moveAmount = Math.Max(ArenaWidth, ArenaHeight);
        movingForward = true;
        peek = false;

        TurnRight(Direction % 90);
        Forward(moveAmount);
        peek = true;
        TurnGunRight(0);
        TurnRight(135);

        while (IsRunning)
        {
            peek = true;
            Forward(moveAmount);
            peek = false;
            TurnRight(135);
        }
    }

    public override void OnHitWall(HitWallEvent e)
    {
        Console.WriteLine("Nabrak dinding, ya belok");
        Back(50);
        TurnRight(135);
        movingForward = !movingForward;
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        double distance = DistanceTo(e.X, e.Y);
        double firepower = distance < 200 ? 3 : 1;
        Fire(firepower);
        if (peek)
            Rescan();
    }

    public override void OnHitBot(HitBotEvent e)
    {
        if (e.IsRammed)
        {
            Console.WriteLine("nabrak bot lain, mundur trus serang!");
            Back(50);
            TurnRight(45);
            Fire(2);
        }
    }
}
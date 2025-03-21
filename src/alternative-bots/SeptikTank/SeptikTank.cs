
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

// ------------------------------------------------------------------
// Septik Tank - Greedy berdasarkan energy bot musuh
// ------------------------------------------------------------------
// 1. Jika menemukan musuh dengan energi rendah, langsung tabrak
// 2. Jika menemukan musuh dengan energi tinggi, tembak dari jarak optimal
// 3. Arahkan gun ke target dan tembak dengan kekuatan sesuai jarak
// ------------------------------------------------------------------
public class SeptikTank : Bot
{
    int turnDirection = 1;

    static void Main(string[] args)
    {
        new SeptikTank().Start();
    }
    SeptikTank() : base(BotInfo.FromFile("SeptikTank.json")) { }

    public override void Run()
    {
        BodyColor = Color.Green;
        TurretColor = Color.DarkGreen;
        RadarColor = Color.LightGreen;
        BulletColor = Color.Yellow;
        while (IsRunning)
        {
            TurnLeft(5 * turnDirection);
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        TurnToFaceTarget(e.X, e.Y);
        var distance = DistanceTo(e.X, e.Y);
        if (e.Energy < 10)
        {
            Forward(distance + 5);
        }
        else if (distance > 200)
        {
            Forward(distance - 150);
        }
        else
        {
            double firepower;
            if (distance < 100) {
                firepower = 3;
            } else if (distance > 300) {
                firepower = 1;
            } else {
                firepower = 3 - (((distance - 100)/200) * 2);
            }
            if (Energy > 5) {
                Fire(firepower);
            }
        }
        Rescan();
    }

    public override void OnHitBot(HitBotEvent e)
    {
        TurnToFaceTarget(e.X, e.Y);
        if (e.Energy > 16)
            Fire(3);
        else if (e.Energy > 10)
            Fire(2);
        else if (e.Energy > 4)
            Fire(1);
        else if (e.Energy > 2)
            Fire(.5);
        else if (e.Energy > .4)
            Fire(.1);
            
        Forward(40);
    }
    
    public override void OnHitWall(HitWallEvent e)
    {
        Back(50);
        TurnRight(90);
    }
    private void TurnToFaceTarget(double x, double y)
    {
        var bearing = BearingTo(x, y);
        if (bearing >= 0)
            turnDirection = 1;
        else
            turnDirection = -1;
            
        TurnLeft(bearing);
    }
}

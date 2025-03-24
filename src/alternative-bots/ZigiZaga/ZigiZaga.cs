using System;
using System.Drawing;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

// -----------------------------------------------------------------------------------------
// ZigiZaga 
// ------------------------------------------------------------------------------------------
// Bot yg geraknya zigzag dinamis untuk menghindari tembakan dan menembak musuh secara efisien
// -------------------------------------------------------------------------------------------
public class ZigiZaga : Bot
{
  
    private int tickCounter = 0;
    private bool movingForward = true;
    private double lastEnemyEnergy = 100;
    static void Main(string[] args)
    {
        new ZigiZaga().Start();
    }
    ZigiZaga() : base(BotInfo.FromFile("ZigiZaga.json")) { } 
    public override void Run()
    {
       
        BodyColor = Color.Blue;
        TurretColor = Color.DarkBlue;
        RadarColor = Color.LightBlue;
        BulletColor = Color.Yellow;
        
       
        while (IsRunning)
        {
            tickCounter++;
            SetTurnRight(30 - (tickCounter % 60));
            if (movingForward)
            {
                SetForward(150 + (tickCounter % 100));
            }
            else
            {
                SetBack(150 + (tickCounter % 100));
            }
            
            if (tickCounter % 50 == 0)
            {
                SetTurnRadarRight(360);
            }
            if (tickCounter % 100 == 0)
            {
                movingForward = !movingForward;
            }
            Go();
        }
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {
        SetTurnRadarRight(RadarBearingTo(e.X, e.Y) * 1.5);
        SetTurnGunRight(GunBearingTo(e.X, e.Y));
        
        if (Math.Abs(GunBearingTo(e.X, e.Y)) < 10)
        {
            double distance = DistanceTo(e.X, e.Y);
            double firepower;

            if (distance < 100)
            {
                firepower = 3.0;
            }

            else if (distance < 200 && Energy > 30)
            {
                firepower = 2.0;
            }
            else
            {
                firepower = 1.0;
            }

            SetFire(firepower);
        }
        
        if (lastEnemyEnergy > e.Energy && lastEnemyEnergy - e.Energy <= 3.0)
        {

            Evade();
        }

        lastEnemyEnergy = e.Energy;
    }

    public override void OnHitByBullet(HitByBulletEvent e)
    {

        Evade();

        tickCounter += 25;
    }
    
    public override void OnHitWall(HitWallEvent e)
    {
        Stop();
        SetTurnRight(120);
        movingForward = !movingForward;

        if (movingForward)
        {
            SetForward(150);
        }
        else
        {
            SetBack(150);
        }
        
        Go();
    }

    public override void OnHitBot(HitBotEvent e)
    {
        if (Energy > e.Energy)
        {
            SetTurnGunRight(GunBearingTo(e.X, e.Y));
            SetFire(3.0);
            Evade();
        }
        else
        {
            Evade();
        }
    }
    
    private void Evade()
    {
        SetTurnRight(90 - (tickCounter % 180));
        
        if (tickCounter % 2 == 0)
        {
            movingForward = !movingForward;
        }
        if (movingForward)
        {
            SetForward(150 + (tickCounter % 50));
        }
        else
        {
            SetBack(150 + (tickCounter % 50));
        }
    }
}
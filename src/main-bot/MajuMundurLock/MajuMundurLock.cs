using System;
using System.Collections.Generic;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

public class MajuMundurLock : Bot
{
    private Random random = new Random();
    private const double WALL_MARGIN = 100;
    private bool movingForward = true;  
    private bool scanningActive = false;  

    // musuh yg terdeteksi
    private Dictionary<int, (double X, double Y)> enemies = new Dictionary<int, (double, double)>();

    static void Main()
    {
        new MajuMundurLock().Start();
    }

    public MajuMundurLock() : base(BotInfo.FromFile("MajuMundurLock.json")) { }

    public override void Run()
    {

        SetTurnGunLeft(Double.PositiveInfinity); 

        while (IsRunning)
        {
            // **Scanner hanya hidup ketika sedang mencari posisi baru**
            if (scanningActive)
            {
                SetTurnRadarRight(Double.PositiveInfinity);
            }

            // **Pastikan tidak dekat dinding sebelum bergerak**
            if (IsNearWall())
            {
                AvoidWall();
            }
            else
            {
                MoveBackAndForth();
            }
        }
    }

    private void MoveBackAndForth()
    {

        if (movingForward)
        {
            SetForward(100);
        }
        else
        {
            SetBack(100);
        }

        WaitFor(new MoveCompleteCondition(this));
        movingForward = !movingForward; // Balik arah setelah satu gerakan selesai
    }

    private bool IsNearWall()
    {
        return (X < WALL_MARGIN || X > ArenaWidth - WALL_MARGIN || Y < WALL_MARGIN || Y > ArenaHeight - WALL_MARGIN);
    }

    private void AvoidWall()
    {
        SetTurnRight(90 + random.Next(-30, 30)); 
        WaitFor(new TurnCompleteCondition(this));
        SetForward(200);
        WaitFor(new MoveCompleteCondition(this));
    }

    private void AvoidBot()
    {
        SetTurnRight(random.Next(90, 180));
        WaitFor(new TurnCompleteCondition(this));
        SetForward(200);
        WaitFor(new MoveCompleteCondition(this));
    }

    public override void OnScannedBot(ScannedBotEvent e)
    {

        if (Energy < 30)
        {
            return; // Keluar dari fungsi tanpa melakukan penembakan
        }
        double distance = DistanceTo(e.X, e.Y);
        enemies[e.ScannedBotId] = (e.X, e.Y);

        // mengunci & menembak musuh
        double bearingFromGun = GunBearingTo(e.X, e.Y);

        // Putar gun ke arah musuh
        SetTurnGunLeft(bearingFromGun); 

        // Jika sudah mengarah ke musuh, langsung tembak!
        if (Math.Abs(bearingFromGun) <= 3 && GunHeat == 0)
        {
            double firePower = Math.Min(3 - Math.Abs(bearingFromGun), Energy - 0.1);
            Fire(firePower);
        }

        // Rescan untuk mempertahankan tracking jika musuh tetap di depan
        if (bearingFromGun == 0)
        {
            Rescan();
        }
    }

    public override void OnHitByBullet(HitByBulletEvent e)
    {   
        scanningActive = true;

        SetTurnRight(random.Next(30, 60));
        WaitFor(new TurnCompleteCondition(this));
        SetForward(50);
        WaitFor(new MoveCompleteCondition(this));

        scanningActive = false;
    }

    public override void OnHitWall(HitWallEvent e)
    {
        AvoidWall();
    }

    public override void OnHitBot(HitBotEvent e)
    {
        AvoidBot();
    }

    private (double X, double Y)? FindNearestEnemy()
    {
        double minDistance = double.MaxValue;
        (double X, double Y)? nearestEnemy = null;

        foreach (var enemy in enemies.Values)
        {
            double distance = DistanceTo(enemy.X, enemy.Y);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }
}

// **Menunggu pergerakan selesai**
public class MoveCompleteCondition : Condition
{
    private readonly Bot bot;
    public MoveCompleteCondition(Bot bot) { this.bot = bot; }
    public override bool Test() { return bot.DistanceRemaining == 0; }
}

// **Menunggu belokan selesai**
public class TurnCompleteCondition : Condition
{
    private readonly Bot bot;
    public TurnCompleteCondition(Bot bot) { this.bot = bot; }
    public override bool Test() { return bot.TurnRemaining == 0; }
}

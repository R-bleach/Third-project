using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics.Contracts;
using System.Drawing;
using GXPEngine;

public class RotatingSpaceship : Game
{
    Player _spaceship;
    Ui ui;
    LevelManager manager;
    public bool dead;
    string[] levels = new string[2];
    public int currentLevel = 0;
    public int deathCounter = 120;
    Sound music;
    SoundChannel noise;
    public RotatingSpaceship() : base(1376, 768, true, false)
    {
        music = new Sound("Assets/Lift_Off_Soundtrack_with_delay.wav", true);
        //music.Play();

        noise = (SoundChannel)music.Play(false, 0, .2f);
        

        manager = new LevelManager();
        AddChild(manager);
        targetFps = 60;

        _spaceship = FindObjectOfType<Player>();

    }

    void Update()
    {
        if (manager.loadNumber >= 0 && !manager.onMenu)
        {
            ui = new Ui();
            LateAddChild(ui);
            manager.onMenu = true;
        }
        if (dead)
        {
            Dead();
        }

        string yay = GetDiagnostics();
        //Console.WriteLine(yay);
        //Console.WriteLine("current fps:" + currentFps);

    }

    public void Dead()
    {
        Background background = FindObjectOfType<Background>();
/*        if (background != null)
        {
            background.DeathEffect();
        }*/
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.pDead();

        }
        deathCounter--;
        //fbmanager.DeathEffect();
        /*AnimationSprite sprite = new AnimationSprite("Assets/Space Background.png", 1, 1, -1, false, false);
        sprite.width = width;
        sprite.height = height * 2;
        sprite.blendMode = BlendMode.MULTIPLY;
        int count = GetChildCount();
        SetChildIndex(sprite, count);
        AddChild(sprite);*/
        noise.Stop();
       
        if (deathCounter == 0)
        {
            deathCounter = 180;
            dead = false;
            manager.Destroy();
            manager = new LevelManager();
            LateAddChild(manager);
            if (ui != null)
            {
                ui.Destroy();
            }
            if (manager.loadNumber >= 0 && !manager.onMenu)
            {
                ui = new Ui();
                AddChild(ui);
            }
            noise = (SoundChannel)music.Play(false, 0, .2f);
        }
        }

    static void Main()
    {
        new RotatingSpaceship().Start();
    }

}
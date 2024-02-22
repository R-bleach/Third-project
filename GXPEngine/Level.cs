﻿using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
using static TiledMapParser.TiledLoader;

namespace GXPEngine { 
            

    internal class Level : GameObject
    {
        Player player;
        Spikes spike;
        EnemyTurn turn;
        TiledLoader loader;
        RotatingSpaceship _mygame;
        public string file;

        public Level(string thislevelName, bool addColliders = true, float defaultOriginX = 0.5f, float defaultOriginY = 0.5f)
        {
            loader = new TiledLoader(thislevelName, null, addColliders, defaultOriginX, defaultOriginY);
            loader.autoInstance = true;
            loader.rootObject = this;
            loader.addColliders = false;
            loader.LoadImageLayers();
            loader.LoadTileLayers(0);
            loader.addColliders = true;
            loader.LoadTileLayers(1);
            loader.LoadObjectGroups(); // player is made -> child of Level
            y -= defaultOriginY;
            player = FindObjectOfType<Player>();
            spike = FindObjectOfType<Spikes>();
            turn = FindObjectOfType<EnemyTurn>();
            file = thislevelName;
        }
        void Update()
        {
            /*    if (player != null)
                {
                    if (player.pInput)
                    {
                        scroll();
                    }
                } */
          //  Console.WriteLine(file);

            
               
            
        }

        void scroll()
        {
            int boundarySize = 600;

            if (player != null)
            {
                if (player.y + y < boundarySize)
                {
                    y = boundarySize - player.y;
                }
                if (player.y + this.y > game.height - boundarySize)
                {
                    this.y = game.height - boundarySize - player.y;
                }

                // Cap scrolling within the boundary limits


                if (y > -300)
                {
                    y = -300;
                }


                if (y < -(loader.map.Height * loader.map.TileHeight - boundarySize * 0.8f))
                {
                    y = -(loader.map.Height * loader.map.TileHeight) + boundarySize * 0.8f;
                }

                y += 300;

                //Console.WriteLine(y);
            }
            else
            {
                Console.WriteLine("Player not found, scrolling not possible");
            }

        }
    }
}
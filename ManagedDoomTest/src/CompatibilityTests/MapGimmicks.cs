﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ManagedDoom;

namespace ManagedDoomTest.CompatibilityTests
{
    [TestClass]
    public class MapGimmicks
    {
        [TestMethod]
        public void E1M8Boss()
        {
            using (var resource = CommonResource.CreateDummy(WadPath.Doom1))
            {
                var demo = new Demo(@"demos\e1m8_boss_test.lmp");
                demo.Options.GameMode = resource.Wad.GameMode;
                var players = DoomTest.GetDefaultPlayers(demo.Options);
                var cmds = Enumerable.Range(0, Player.MaxPlayerCount).Select(i => new TicCmd()).ToArray();
                var game = new DoomGame(players, resource, demo.Options);

                var lastMobjHash = 0;
                var aggMobjHash = 0;
                var lastSectorHash = 0;
                var aggSectorHash = 0;

                while (true)
                {
                    if (!demo.ReadCmd(cmds))
                    {
                        break;
                    }

                    game.Update(cmds);
                    lastMobjHash = DoomDebug.GetMobjHash(game.World);
                    aggMobjHash = DoomDebug.CombineHash(aggMobjHash, lastMobjHash);
                    lastSectorHash = DoomDebug.GetSectorHash(game.World);
                    aggSectorHash = DoomDebug.CombineHash(aggSectorHash, lastSectorHash);
                }

                Assert.AreEqual(0xb01c44f9u, (uint)lastMobjHash);
                Assert.AreEqual(0x1a4918bbu, (uint)aggMobjHash);
                Assert.AreEqual(0xa7bac3ceu, (uint)lastSectorHash);
                Assert.AreEqual(0xda0067d0u, (uint)aggSectorHash);
            }
        }

        [TestMethod]
        public void Map06Crusher()
        {
            using (var resource = CommonResource.CreateDummy(WadPath.Doom2))
            {
                var demo = new Demo(@"demos\map06_crusher_test.lmp");
                var players = DoomTest.GetDefaultPlayers(demo.Options);
                var cmds = Enumerable.Range(0, Player.MaxPlayerCount).Select(i => new TicCmd()).ToArray();
                var game = new DoomGame(players, resource, demo.Options);

                var lastMobjHash = 0;
                var aggMobjHash = 0;
                var lastSectorHash = 0;
                var aggSectorHash = 0;

                while (true)
                {
                    if (!demo.ReadCmd(cmds))
                    {
                        break;
                    }

                    game.Update(cmds);
                    lastMobjHash = DoomDebug.GetMobjHash(game.World);
                    aggMobjHash = DoomDebug.CombineHash(aggMobjHash, lastMobjHash);
                    lastSectorHash = DoomDebug.GetSectorHash(game.World);
                    aggSectorHash = DoomDebug.CombineHash(aggSectorHash, lastSectorHash);
                }

                Assert.AreEqual(0x302bc4e3u, (uint)lastMobjHash);
                Assert.AreEqual(0xe4050462u, (uint)aggMobjHash);
                Assert.AreEqual(0x3ce914d8u, (uint)lastSectorHash);
                Assert.AreEqual(0x549ea480u, (uint)aggSectorHash);
            }
        }

        [TestMethod]
        public void Map07Boss()
        {
            using (var resource = CommonResource.CreateDummy(WadPath.Doom2))
            {
                var demo = new Demo(@"demos\map07_boss_test.lmp");
                var players = DoomTest.GetDefaultPlayers(demo.Options);
                var cmds = Enumerable.Range(0, Player.MaxPlayerCount).Select(i => new TicCmd()).ToArray();
                var game = new DoomGame(players, resource, demo.Options);

                var lastMobjHash = 0;
                var aggMobjHash = 0;
                var lastSectorHash = 0;
                var aggSectorHash = 0;

                while (true)
                {
                    if (!demo.ReadCmd(cmds))
                    {
                        break;
                    }

                    game.Update(cmds);
                    lastMobjHash = DoomDebug.GetMobjHash(game.World);
                    aggMobjHash = DoomDebug.CombineHash(aggMobjHash, lastMobjHash);
                    lastSectorHash = DoomDebug.GetSectorHash(game.World);
                    aggSectorHash = DoomDebug.CombineHash(aggSectorHash, lastSectorHash);
                }

                Assert.AreEqual(0x4c4e952fu, (uint)lastMobjHash);
                Assert.AreEqual(0x56d1d836u, (uint)aggMobjHash);
                Assert.AreEqual(0x44469690u, (uint)lastSectorHash);
                Assert.AreEqual(0x1b989de0u, (uint)aggSectorHash);
            }
        }
    }
}

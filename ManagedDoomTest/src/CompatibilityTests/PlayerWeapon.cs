﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ManagedDoom;

namespace ManagedDoomTest.CompatibilityTests
{
    [TestClass]
    public class PlayerWeapon
    {
        [TestMethod]
        public void PunchTest()
        {
            using (var resource = CommonResource.CreateDummy(WadPath.Doom2, @"data\punch_test.wad"))
            {
                var demo = new Demo(@"data\punch_test.lmp");
                var players = DoomTest.GetDefaultPlayers();
                var cmds = players.Select(player => player.Cmd).ToArray();
                var world = new World(resource, demo.Options, players);

                var lastHash = 0;
                var aggHash = 0;

                while (true)
                {
                    if (!demo.ReadCmd(cmds))
                    {
                        break;
                    }

                    world.Update();
                    lastHash = DoomDebug.GetMobjHash(world);
                    aggHash = DoomDebug.CombineHash(aggHash, lastHash);
                }

                Assert.AreEqual(0x3d6c0f49u, (uint)lastHash);
                Assert.AreEqual(0x97d3aa02u, (uint)aggHash);
            }
        }

        [TestMethod]
        public void ChainsawTest()
        {
            using (var resource = CommonResource.CreateDummy(WadPath.Doom2, @"data\chainsaw_test.wad"))
            {
                var demo = new Demo(@"data\chainsaw_test.lmp");
                var players = DoomTest.GetDefaultPlayers();
                var cmds = players.Select(player => player.Cmd).ToArray();
                var world = new World(resource, demo.Options, players);

                var lastHash = 0;
                var aggHash = 0;

                while (true)
                {
                    if (!demo.ReadCmd(cmds))
                    {
                        break;
                    }

                    world.Update();
                    lastHash = DoomDebug.GetMobjHash(world);
                    aggHash = DoomDebug.CombineHash(aggHash, lastHash);
                }

                Assert.AreEqual(0x5db30e69u, (uint)lastHash);
                Assert.AreEqual(0xed598949u, (uint)aggHash);
            }
        }

        [TestMethod]
        public void ShotgunTest()
        {
            using (var resource = CommonResource.CreateDummy(WadPath.Doom2, @"data\shotgun_test.wad"))
            {
                var demo = new Demo(@"data\shotgun_test.lmp");
                var players = DoomTest.GetDefaultPlayers();
                var cmds = players.Select(player => player.Cmd).ToArray();
                var world = new World(resource, demo.Options, players);

                var lastHash = 0;
                var aggHash = 0;

                while (true)
                {
                    if (!demo.ReadCmd(cmds))
                    {
                        break;
                    }

                    world.Update();
                    lastHash = DoomDebug.GetMobjHash(world);
                    aggHash = DoomDebug.CombineHash(aggHash, lastHash);
                }

                Assert.AreEqual(0x3dd50799u, (uint)lastHash);
                Assert.AreEqual(0x4ddd814fu, (uint)aggHash);
            }
        }

        [TestMethod]
        public void SuperShotgunTest()
        {
            using (var resource = CommonResource.CreateDummy(WadPath.Doom2, @"data\supershotgun_test.wad"))
            {
                var demo = new Demo(@"data\supershotgun_test.lmp");
                var players = DoomTest.GetDefaultPlayers();
                var cmds = players.Select(player => player.Cmd).ToArray();
                var world = new World(resource, demo.Options, players);

                var lastHash = 0;
                var aggHash = 0;

                while (true)
                {
                    if (!demo.ReadCmd(cmds))
                    {
                        break;
                    }

                    world.Update();
                    lastHash = DoomDebug.GetMobjHash(world);
                    aggHash = DoomDebug.CombineHash(aggHash, lastHash);
                }

                Assert.AreEqual(0xe2f7936eu, (uint)lastHash);
                Assert.AreEqual(0x538061e4u, (uint)aggHash);
            }
        }

        [TestMethod]
        public void ChaingunTest()
        {
            using (var resource = CommonResource.CreateDummy(WadPath.Doom2, @"data\chaingun_test.wad"))
            {
                var demo = new Demo(@"data\chaingun_test.lmp");
                var players = DoomTest.GetDefaultPlayers();
                var cmds = players.Select(player => player.Cmd).ToArray();
                var world = new World(resource, demo.Options, players);

                var lastHash = 0;
                var aggHash = 0;

                while (true)
                {
                    if (!demo.ReadCmd(cmds))
                    {
                        break;
                    }

                    world.Update();
                    lastHash = DoomDebug.GetMobjHash(world);
                    aggHash = DoomDebug.CombineHash(aggHash, lastHash);
                }

                Assert.AreEqual(0x0b30e14bu, (uint)lastHash);
                Assert.AreEqual(0xb2104158u, (uint)aggHash);
            }
        }

        [TestMethod]
        public void RocketTest()
        {
            using (var resource = CommonResource.CreateDummy(WadPath.Doom2, @"data\rocket_test.wad"))
            {
                var demo = new Demo(@"data\rocket_test.lmp");
                var players = DoomTest.GetDefaultPlayers();
                var cmds = players.Select(player => player.Cmd).ToArray();
                var world = new World(resource, demo.Options, players);

                var lastHash = 0;
                var aggHash = 0;

                while (true)
                {
                    if (!demo.ReadCmd(cmds))
                    {
                        break;
                    }

                    world.Update();
                    lastHash = DoomDebug.GetMobjHash(world);
                    aggHash = DoomDebug.CombineHash(aggHash, lastHash);
                }

                Assert.AreEqual(0x8dce774bu, (uint)lastHash);
                Assert.AreEqual(0x87f45b5bu, (uint)aggHash);
            }
        }

        [TestMethod]
        public void PlasmaTest()
        {
            using (var resource = CommonResource.CreateDummy(WadPath.Doom2, @"data\plasma_test.wad"))
            {
                var demo = new Demo(@"data\plasma_test.lmp");
                var players = DoomTest.GetDefaultPlayers();
                var cmds = players.Select(player => player.Cmd).ToArray();
                var world = new World(resource, demo.Options, players);

                var lastHash = 0;
                var aggHash = 0;

                while (true)
                {
                    if (!demo.ReadCmd(cmds))
                    {
                        break;
                    }

                    world.Update();
                    lastHash = DoomDebug.GetMobjHash(world);
                    aggHash = DoomDebug.CombineHash(aggHash, lastHash);
                }

                Assert.AreEqual(0x116924d3u, (uint)lastHash);
                Assert.AreEqual(0x88fc9e68u, (uint)aggHash);
            }
        }

        [TestMethod]
        public void BfgTest()
        {
            using (var resource = CommonResource.CreateDummy(WadPath.Doom2, @"data\bfg_test.wad"))
            {
                var demo = new Demo(@"data\bfg_test.lmp");
                var players = DoomTest.GetDefaultPlayers();
                var cmds = players.Select(player => player.Cmd).ToArray();
                var world = new World(resource, demo.Options, players);

                var lastHash = 0;
                var aggHash = 0;

                while (true)
                {
                    if (!demo.ReadCmd(cmds))
                    {
                        break;
                    }

                    world.Update();
                    lastHash = DoomDebug.GetMobjHash(world);
                    aggHash = DoomDebug.CombineHash(aggHash, lastHash);
                }

                Assert.AreEqual(0xdeaf403fu, (uint)lastHash);
                Assert.AreEqual(0xb2c67368u, (uint)aggHash);
            }
        }

        [TestMethod]
        public void SkyShootTest()
        {
            using (var resource = CommonResource.CreateDummy(WadPath.Doom2, @"data\sky_shoot_test.wad"))
            {
                var demo = new Demo(@"data\sky_shoot_test.lmp");
                var players = DoomTest.GetDefaultPlayers();
                var cmds = players.Select(player => player.Cmd).ToArray();
                var world = new World(resource, demo.Options, players);

                var lastHash = 0;
                var aggHash = 0;

                while (true)
                {
                    if (!demo.ReadCmd(cmds))
                    {
                        break;
                    }

                    world.Update();
                    lastHash = DoomDebug.GetMobjHash(world);
                    aggHash = DoomDebug.CombineHash(aggHash, lastHash);
                }

                Assert.AreEqual(0xfe794466u, (uint)lastHash);
                Assert.AreEqual(0xc71f30b2u, (uint)aggHash);
            }
        }
    }
}

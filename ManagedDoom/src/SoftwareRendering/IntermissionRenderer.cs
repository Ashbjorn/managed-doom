﻿using System;
using System.Collections.Generic;

namespace ManagedDoom.SoftwareRendering
{
    public sealed class IntermissionRenderer
    {
        // GLOBAL LOCATIONS
        private static readonly int WI_TITLEY = 2;
        private static readonly int WI_SPACINGY = 33;

        // SINGPLE-PLAYER STUFF
        private static readonly int SP_STATSX = 50;
        private static readonly int SP_STATSY = 50;

        private static readonly int SP_TIMEX = 16;
        private static readonly int SP_TIMEY = (200 - 32);


        // NET GAME STUFF
        private static readonly int NG_STATSY = 50;
        //private static readonly int NG_STATSX = (32 + SHORT(star->width) / 2 + 32 * !dofrags);

        private static readonly int NG_SPACINGX = 64;


        // DEATHMATCH STUFF
        private static readonly int DM_MATRIXX = 42;
        private static readonly int DM_MATRIXY = 68;

        private static readonly int DM_SPACINGX = 40;

        private static readonly int DM_TOTALSX = 269;

        private static readonly int DM_KILLERSX = 10;
        private static readonly int DM_KILLERSY = 100;
        private static readonly int DM_VICTIMSX = 5;
        private static readonly int DM_VICTIMSY = 50;






        private DrawScreen screen;
        private Patches patches;

        private int scale;

        public IntermissionRenderer(Wad wad, DrawScreen screen)
        {
            this.screen = screen;

            patches = new Patches(wad);

            scale = screen.Width / 320;
        }

        private void DrawPatch(Patch patch, int x, int y)
        {
            screen.DrawPatch(patch, scale * x, scale * y, scale);
        }

        public void Render(Intermission intermission)
        {
            var im = intermission;
            switch (im.state)
            {
                case IntermissionState.StatCount:
                    if (im.Options.Deathmatch != 0)
                    {
                        // WI_drawDeathmatchStats();
                    }
                    else if (im.Options.NetGame)
                    {
                        // WI_drawNetgameStats();
                    }
                    else
                    {
                        DrawStats(im);
                    }
                    break;

                case IntermissionState.ShowNextLoc:
                    WI_drawShowNextLoc(im);
                    break;

                case IntermissionState.NoState:
                    WI_drawNoState(im);
                    break;
            }
        }

        private void DrawBackground(Intermission intermission)
        {
            if (intermission.Options.GameMode == GameMode.Commercial)
            {
                DrawPatch(patches.Background, 0, 0);
            }
            else
            {
                var e = intermission.Options.Episode - 1;
                if (e < patches.MapPictures.Count)
                {
                    DrawPatch(patches.MapPictures[e], 0, 0);
                }
                else
                {
                    DrawPatch(patches.Background, 0, 0);
                }
            }
        }

        private void DrawStats(Intermission intermission)
        {
            DrawBackground(intermission);

            var im = intermission;

            // line height
            var lh = (3 * patches.Numbers[0].Height) / 2;

            // draw animated background
            //WI_drawAnimatedBack();

            WI_drawLF(im);

            DrawPatch(patches.Kills, SP_STATSX, SP_STATSY);
            WI_drawPercent(320 - SP_STATSX, SP_STATSY, im.cnt_kills[0]);

            DrawPatch(patches.Items, SP_STATSX, SP_STATSY + lh);
            WI_drawPercent(320 - SP_STATSX, SP_STATSY + lh, im.cnt_items[0]);

            DrawPatch(patches.SP_Secret, SP_STATSX, SP_STATSY + 2 * lh);
            WI_drawPercent(320 - SP_STATSX, SP_STATSY + 2 * lh, im.cnt_secret[0]);

            DrawPatch(patches.Time, SP_TIMEX, SP_TIMEY);
            WI_drawTime(320 / 2 - SP_TIMEX, SP_TIMEY, im.cnt_time);

            if (im.Wbs.Epsd < 3)
            {
                //V_DrawPatch(SCREENWIDTH / 2 + SP_TIMEX, SP_TIMEY, FB, par);
                //WI_drawTime(SCREENWIDTH - SP_TIMEX, SP_TIMEY, cnt_par);
            }
        }

        private void WI_drawNoState(Intermission im)
        {
            //snl_pointeron = true;
            WI_drawShowNextLoc(im);
        }

        private void WI_drawShowNextLoc(Intermission im)
        {
            DrawBackground(im);

            // draw animated background
            //WI_drawAnimatedBack();

            if (im.Options.GameMode != GameMode.Commercial)
            {
                if (im.Wbs.Epsd > 2)
                {
                    //WI_drawEL();
                    return;
                }

                var last = (im.Wbs.Last == 8) ? im.Wbs.Next - 1 : im.Wbs.Last;

                // draw a splat on taken cities.
                for (var i = 0; i <= last; i++)
                {
                    //WI_drawOnLnode(i, &splat);
                }

                // splat the secret level?
                if (im.Wbs.DidSecret)
                {
                    //WI_drawOnLnode(8, &splat);
                }

                // draw flashing ptr
                //if (snl_pointeron)
                {
                    //WI_drawOnLnode(wbs->next, yah);
                }
            }

            // draws which level you are entering..
            if ((im.Options.GameMode != GameMode.Commercial) || im.Wbs.Next != 30)
            {
                WI_drawEL(im);
            }
        }

        // Draws "<Levelname> Finished!"
        private void WI_drawLF(Intermission intermission)
        {
            var wbs = intermission.Wbs;
            var y = WI_TITLEY;

            var e = 0;
            if (intermission.Options.GameMode != GameMode.Commercial)
            {
                e = intermission.Options.Episode - 1;
            }

            // draw <LevelName> 
            DrawPatch(
                patches.LevelNames[e][wbs.Last],
                (320 - patches.LevelNames[e][wbs.Last].Width) / 2, y);

            // draw "Finished!"
            y += (5 * patches.LevelNames[e][wbs.Last].Height) / 4;

            DrawPatch(
                patches.Finished,
                (320 - patches.Finished.Width) / 2, y);
        }

        // Draws "Entering <LevelName>"
        private void WI_drawEL(Intermission im)
        {
            int y = WI_TITLEY;

            var e = 0;
            if (im.Options.GameMode != GameMode.Commercial)
            {
                e = im.Options.Episode - 1;
            }

            // draw "Entering"
            DrawPatch(
                patches.Entering,
                (320 - patches.Entering.Width) / 2, y);

            // draw level
            y += (5 * patches.LevelNames[e][im.Wbs.Next].Height) / 4;

            DrawPatch(
                patches.LevelNames[e][im.Wbs.Next],
                (320 - patches.LevelNames[e][im.Wbs.Next].Width) / 2, y);
        }





        //
        // Draws a number.
        // If digits > 0, then use that many digits minimum,
        //  otherwise only use as many as necessary.
        // Returns new x position.
        //

        private int WI_drawNum(int x, int y, int n, int digits)
        {
            var fontwidth = patches.Numbers[0].Width;

            if (digits < 0)
            {
                if (n == 0)
                {
                    // make variable-length zeros 1 digit long
                    digits = 1;
                }
                else
                {
                    // figure out # of digits in #
                    digits = 0;
                    var temp = n;

                    while (temp != 0)
                    {
                        temp /= 10;
                        digits++;
                    }
                }
            }

            var neg = n < 0;

            if (neg)
            {
                n = -n;
            }

            // if non-number, do not draw it
            if (n == 1994)
            {
                return 0;
            }

            // draw the new number
            while (digits-- != 0)
            {
                x -= fontwidth;
                DrawPatch(patches.Numbers[n % 10], x, y);
                n /= 10;
            }

            // draw a minus sign if necessary
            if (neg)
            {
                DrawPatch(patches.Minus, x -= 8, y);
            }

            return x;

        }

        private void WI_drawPercent(int x, int y, int p)
        {
            if (p < 0)
            {
                return;
            }

            DrawPatch(patches.Percent, x, y);
            WI_drawNum(x, y, p, -1);
        }

        //
        // Display level completion time and par,
        //  or "sucks" message if overflow.
        //
        private void WI_drawTime(int x, int y, int t)
        {

            int div;
            int n;

            if (t < 0)
            {
                return;
            }

            if (t <= 61 * 59)
            {
                div = 1;

                do
                {
                    n = (t / div) % 60;
                    x = WI_drawNum(x, y, n, 2) - patches.Colon.Width;
                    div *= 60;

                    // draw
                    if (div == 60 || t / div != 0)
                    {
                        DrawPatch(patches.Colon, x, y);
                    }

                } while (t / div != 0);
            }
            else
            {
                // "sucks"
                DrawPatch(patches.Sucks, x - patches.Sucks.Width, y);
            }
        }






        private class Patches
        {
            // background (map of levels).
            private Patch background;
            private Patch[] mapPictures;

            // "Kills", "Scrt", "Items", "Frags"
            private Patch kills;
            private Patch secret;
            private Patch sp_secret;
            private Patch items;
            private Patch frags;

            // Time sucks.
            private Patch time;
            private Patch par;
            private Patch sucks;

            // minus sign
            private Patch wiminus;

            // 0-9 graphic
            private Patch[] numbers;

            // %, : graphics
            private Patch percent;
            private Patch colon;

            // "Finished!" graphics
            private Patch finished;

            // "Entering" graphic
            private Patch entering;

            // "killers", "victims"
            private Patch killers;
            private Patch victims;

            // "Total", your face, your dead face
            private Patch total;
            private Patch star;
            private Patch bstar;




            // You Are Here graphic
            private Patch youAreHere1;
            private Patch youAreHere2;

            // splat
            private Patch splat;

            // "red P[1..MAXPLAYERS]"
            private Patch[] p;

            // "gray P[1..MAXPLAYERS]"
            private Patch[] bp;

            // Name graphics of each level (centered)
            private Patch[][] lnames;

            public Patches(Wad wad)
            {
                if (wad.GameMode == GameMode.Commercial)
                {
                    background = Patch.FromWad("INTERPIC", wad);
                }
                else
                {
                    mapPictures = new Patch[3];
                    for (var e = 0; e < 3; e++)
                    {
                        var patchName = "WIMAP" + e;
                        if (wad.GetLumpNumber(patchName) != -1)
                        {
                            mapPictures[e] = Patch.FromWad(patchName, wad);
                        }
                    }
                    if (wad.GetLumpNumber("INTERPIC") != -1)
                    {
                        background = Patch.FromWad("INTERPIC", wad);
                    }
                }

                kills = Patch.FromWad("WIOSTK", wad);
                secret = Patch.FromWad("WIOSTS", wad);
                sp_secret = Patch.FromWad("WISCRT2", wad);
                items = Patch.FromWad("WIOSTI", wad);
                frags = Patch.FromWad("WIFRGS", wad);

                time = Patch.FromWad("WITIME", wad);
                par = Patch.FromWad("WIPAR", wad);
                sucks = Patch.FromWad("WISUCKS", wad);

                wiminus = Patch.FromWad("WIMINUS", wad);

                numbers = new Patch[10];
                for (var i = 0; i < 10; i++)
                {
                    numbers[i] = Patch.FromWad("WINUM" + i, wad);
                }

                percent = Patch.FromWad("WIPCNT", wad);
                colon = Patch.FromWad("WICOLON", wad);

                finished = Patch.FromWad("WIF", wad);

                entering = Patch.FromWad("WIENTER", wad);

                killers = Patch.FromWad("WIKILRS", wad);
                victims = Patch.FromWad("WIVCTMS", wad);

                total = Patch.FromWad("WIMSTT", wad);
                star = Patch.FromWad("STFST01", wad);
                bstar = Patch.FromWad("STFDEAD0", wad);


                if (wad.GameMode == GameMode.Commercial)
                {
                    var numMaps = 32;
                    lnames = new Patch[1][];
                    lnames[0] = new Patch[numMaps];
                    for (var i = 0; i < numMaps; i++)
                    {
                        lnames[0][i] = Patch.FromWad("CWILV" + i.ToString("00"), wad);
                    }
                }
                else
                {
                    var numEpisodes = 4;
                    var numMaps = 9;
                    lnames = new Patch[numEpisodes][];
                    for (var e = 0; e < numEpisodes; e++)
                    {
                        lnames[e] = new Patch[numMaps];
                        for (var m = 0; m < numMaps; m++)
                        {
                            var patchName = "WILV" + e + m;
                            if (wad.GetLumpNumber(patchName) != -1)
                            {
                                lnames[e][m] = Patch.FromWad(patchName, wad);
                            }
                        }
                    }

                    youAreHere1 = Patch.FromWad("WIURH0", wad);
                    youAreHere2 = Patch.FromWad("WIURH1", wad);
                    splat = Patch.FromWad("WISPLAT", wad);
                }

                Console.WriteLine("All intermission patches are OK.");
            }




            public Patch Background => background;
            public IReadOnlyList<Patch> MapPictures => mapPictures;

            public Patch Kills => kills;
            public Patch Secret => secret;
            public Patch SP_Secret => sp_secret;
            public Patch Items => items;
            public Patch Frags => frags;

            public Patch Time => time;
            public Patch Par => par;
            public Patch Sucks => sucks;

            public Patch Minus => wiminus;

            public IReadOnlyList<Patch> Numbers => numbers;

            public Patch Percent => percent;
            public Patch Colon => colon;

            public Patch Finished => finished;

            public Patch Entering => entering;

            public Patch Killers => killers;
            public Patch Victims => victims;

            public Patch Total => total;
            public Patch Star => star;
            public Patch BStar => BStar;

            public IReadOnlyList<IReadOnlyList<Patch>> LevelNames => lnames;
        }
    }
}

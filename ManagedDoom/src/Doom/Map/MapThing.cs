﻿using System;

namespace ManagedDoom
{
    public sealed class MapThing
    {
        public const int DataSize = 10;

        public static MapThing Empty = new MapThing(
            Fixed.Zero,
            Fixed.Zero,
            Angle.Ang0,
            0, 0);

        private Fixed x;
        private Fixed y;
        private Angle angle;
        private int type;
        private ThingFlags flags;

        public MapThing(
            Fixed x,
            Fixed y,
            Angle angle,
            int type,
            ThingFlags flags)
        {
            this.x = x;
            this.y = y;
            this.angle = angle;
            this.type = type;
            this.flags = flags;
        }

        public static MapThing FromData(byte[] data, int offset)
        {
            var x = BitConverter.ToInt16(data, offset);
            var y = BitConverter.ToInt16(data, offset + 2);
            var angle = BitConverter.ToInt16(data, offset + 4);
            var type = BitConverter.ToInt16(data, offset + 6);
            var flags = BitConverter.ToInt16(data, offset + 8);

            return new MapThing(
                Fixed.FromInt(x),
                Fixed.FromInt(y),
                new Angle(ManagedDoom.Angle.Ang45.Data * (uint)(angle / 45)),
                type,
                (ThingFlags)flags);
        }

        public static MapThing[] FromWad(Wad wad, int lump)
        {
            var length = wad.GetLumpSize(lump);
            if (length % DataSize != 0)
            {
                throw new Exception();
            }

            var data = wad.ReadLump(lump);
            var count = length / DataSize;
            var things = new MapThing[count];

            for (var i = 0; i < count; i++)
            {
                var offset = DataSize * i;
                things[i] = FromData(data, offset);
            }

            return things;
        }

        public Fixed X => x;
        public Fixed Y => y;
        public Angle Angle => angle;

        public int Type
        {
            get => type;
            set => type = value;
        }

        public ThingFlags Flags => flags;
    }
}

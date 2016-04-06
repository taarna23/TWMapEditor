using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;

namespace MapEditor
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct HeaderInfo
    {
        //[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.I1, SizeConst = 32)]

        public Byte UnknownByte1;
        public Int32 Width;
        public Int32 Height;
        public Int16 UnknownShort1;
        public Int16 ImgCount;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct ChunkInfo
    {
        public Int32 Pos;
        public Int32 Row;
        public Int32 ChunkSize;
    }

    public struct ImgEncodeRow
    {
        public ChunkInfo Info;
        public Byte[] Raw;
    }

    public struct ImgInfo
    {
        //--User
        //public Int32 Index;

        //--Format
        public Byte NumByte; //0 2 3
        public Int16 ImgIdx;
        public Int16 _Short2; //0
        public Int16 _Short3; //0
        public Int16 _Short4; //0

        public Int32 Width;
        public Int32 Height;

        public Byte[] PaletteData;

        public Int32 ChunkCount;
        public Int32 X2Size;
        public Int32 RelativeY;
        public Int32 RelativeX;

        public Bitmap Img;
        //public List<RLEPieceImg> nImgList;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct PlanInfo
    {
        public Int16 MiniBlockWidth;
        public Int16 MiniBlockHeight;
        public Int16 UnknownShort3;
        public Int16 UnknownShort4;
        public Int32 UnknownInt1;
        public Int32 UnknownInt2;
        public Int16 CountLayerTwo;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct MapAttr
    {
        public Int16 ImgIdx;
        public Int16 ImgIdx2;
        public Int16 ImgIdx3;
        public Int16 Unknow;
    }

    public struct Plan
    {
        //--User
        public Int32 HIdx;
        public Int32 WIdx;

        public PlanInfo Info;
        public MapAttr [] TileBlocks2;
        public Int16 CountUnkImage;
        //ReadSub ???
        public MapAttr[] TileBlocks;

        public List<ImgInfo> ImgLists;
    }

  
}

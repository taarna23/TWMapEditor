using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace MapEditor
{
    public unsafe struct EntryTable
    {
        public UInt32[] Offset;
        public EntryTable(UInt32 size)
        {
            Offset = new UInt32[size];
        }
    }

    //!--IF Have PAL read PAL
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct DataPal
    {
        public UInt32 Signature; //PAL_
        public UInt32 Must100; //0x64
        public fixed UInt32 dwUnknow[5]; //0 
        public UInt32 AllPALSize; //sizeOf DataPal
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct DataRaw
    {
        [MarshalAs(UnmanagedType.SafeArray)]
        public Byte[] RawData;
        public DataRaw(UInt32 size)
        {
            RawData = new byte[size];
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct XY
    {
        public Int32 X; //
        public Int32 Y; //
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Rect
    {
        public Int32 Left; //
        public Int32 Top; //
        public Int32 Right; //
        public Int32 Bottom; //
    }

}

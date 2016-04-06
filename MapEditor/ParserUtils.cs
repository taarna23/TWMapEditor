using System;
using System.Runtime.InteropServices;
using System.IO;

namespace MapEditor
{
    public class ParserBase
    {
        public  bool Read<T>(byte[] buffer, int index, ref T retval)
        {
            if (index == buffer.Length) return false;
            int size = Marshal.SizeOf(typeof(T));
            if (index + size > buffer.Length) throw new IndexOutOfRangeException();
            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                IntPtr addr = (IntPtr)((long)handle.AddrOfPinnedObject() + index);
                retval = (T)Marshal.PtrToStructure(addr, typeof(T));
            }
            finally
            {
                handle.Free();
            }
            return true;
        }

        public  bool Read<T>(Stream stream, ref T retval)
        {
            int size = Marshal.SizeOf(typeof(T));
            byte[] buffer = null;
            if (buffer == null || size > buffer.Length) buffer = new byte[size];
            int len = stream.Read(buffer, 0, size);
            if (len == 0) return false;
            if (len != size) throw new EndOfStreamException();
            var handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            try
            {
                retval = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
            return true;
        }

        public  bool ReadBytes(Stream stream, UInt32 readSize, ref Byte[] bArray)
        {
            int size = (int)readSize;
            int len = stream.Read(bArray, 0, size);
            if (len == 0) return false;
            if (len != size) throw new EndOfStreamException();
            return true;
        }

        public byte ReadByte(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);
            return br.ReadByte(); 
        }

        public Int16 ReadInt16(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);
            return br.ReadInt16();
        }

        public Int32 ReadInt32(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);
            return br.ReadInt32();
        }

        public  bool ReadUInt32s(Stream stream, UInt32 readSize, ref UInt32[] bArray)
        {
            int size = (int)readSize * sizeof(UInt32);
            byte[] buffer = null;
            if (buffer == null || size > buffer.Length) buffer = new byte[size];
            int len = stream.Read(buffer, 0, size);
            if (len == 0) return false;
            if (len != size) throw new EndOfStreamException();
            Buffer.BlockCopy(buffer, 0, bArray, 0, size);
            return true;
        }

        public void DecompressData(Stream pStream, out MemoryStream Outz)
        {
            MemoryStream msInner = new MemoryStream();
            using (System.IO.Compression.DeflateStream z = new System.IO.Compression.DeflateStream(pStream, System.IO.Compression.CompressionMode.Decompress))
            {
                MemoryStream _o = new MemoryStream();
                z.CopyTo(_o);
                _o.Seek(0, SeekOrigin.Begin);
                Outz = _o;
            }
        }

        public static bool Write<T>(ref byte[] buffer, T data, ref int writesize)
        {
            int size = Marshal.SizeOf(typeof(T));
            IntPtr marshal_buff = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(data, marshal_buff, true);
                Marshal.Copy(marshal_buff, buffer, 0, size);
                writesize = size;
            }
            finally
            {
                Marshal.FreeHGlobal(marshal_buff);
            }
            return true;
        }
    }
}

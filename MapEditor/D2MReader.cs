using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Text;
using System.Drawing.Imaging;
using System.Linq;

namespace MapEditor
{
    public partial class D2MFile
    {
        public class D2MReader : ParserBase
        {
            private D2MFile _D2MFile;

            public MapEditor.D2MFile D2MFile
            {
                get { return _D2MFile; }
                set { _D2MFile = value; }
            }

            public D2MReader(D2MFile pD2MFile)
            {
                D2MFile = pD2MFile;
            }

            private ImgInfo ReadImage(Stream _stream,Int32 pIndex)
            {
                ImgInfo _ImgInfo = new ImgInfo();

                _ImgInfo.NumByte = ReadByte(_stream);
                _ImgInfo.ImgIdx = 0;
                _ImgInfo._Short2 = 0;
                _ImgInfo._Short3 = 0;
                _ImgInfo._Short4 = 0;
                if (_ImgInfo.NumByte == 0)
                {
                    _ImgInfo.ImgIdx = ReadInt16(_stream);
                    _ImgInfo._Short2 = ReadInt16(_stream);
                    _ImgInfo._Short3 = ReadInt16(_stream);
                    _ImgInfo._Short4 = ReadInt16(_stream);
                }
                else if (_ImgInfo.NumByte == 3)
                {
                    _ImgInfo.ImgIdx = ReadInt16(_stream);
                }
                else if (_ImgInfo.NumByte == 4)
                {
                    _ImgInfo.ImgIdx = ReadInt16(_stream);
                    _ImgInfo._Short2 = ReadInt16(_stream);
                    _ImgInfo._Short3 = ReadInt16(_stream);
                }
                else
                {
                    //!--Error
                }

                _ImgInfo.Width = ReadInt32(_stream);
                _ImgInfo.Height = ReadInt32(_stream);

                if (_ImgInfo.NumByte != 0)
                {
                    _ImgInfo.PaletteData = new Byte[512];
                    ReadBytes(_stream, 512, ref _ImgInfo.PaletteData);

                    List<System.Windows.Media.Color> colors = new List<System.Windows.Media.Color>();
                    Bitmap _bp2 = BitmapExtensions.BitmapSourceFromArray(_ImgInfo.PaletteData, 16, 16, 16);
                    Bitmap bm2 = new Bitmap(_bp2);
                    int FuchsiaIdx = 0;
                    bool ff = false;
                    for (int i = 0; i < 16; ++i)
                    {
                        for (int j = 0; j < 16; ++j)
                        {
                            Color c = bm2.GetPixel(j, i);
                            if (ff == false)
                            {
                                if (c.ToArgb() == Color.Fuchsia.ToArgb())
                                {
                                    ff = true;
                                }
                                else
                                {
                                    FuchsiaIdx++;
                                }
                            }
                          
                            colors.Add(System.Windows.Media.Color.FromRgb(c.R, c.G, c.B));
                        }
                    }
                    //bm2.Save("C:\\b" + string.Format("\\{0}_Palette.png", pIndex), ImageFormat.Png);
                    BitmapPalette _Pl = new BitmapPalette(colors);

                    _ImgInfo.ChunkCount = ReadInt32(_stream);

                    if (_ImgInfo.ChunkCount - 1 >= 0)
                    {
                        _ImgInfo.X2Size = ReadInt32(_stream);
                        Byte[] ImgD =  Enumerable.Repeat((byte)FuchsiaIdx, _ImgInfo.Width * _ImgInfo.Height * _ImgInfo.NumByte).ToArray();//new Byte[_ImgInfo.Width * _ImgInfo.Height * _ImgInfo.NumByte];
                        for (int ChkIdx = 0; ChkIdx  < _ImgInfo.ChunkCount; ++ChkIdx)
                        {
                            ImgEncodeRow nImg = new ImgEncodeRow();
                            Read<ChunkInfo>(_stream, ref nImg.Info);

                            nImg.Raw = new Byte[nImg.Info.ChunkSize];
                            ReadBytes(_stream, (uint)nImg.Info.ChunkSize, ref nImg.Raw);
                            int _Row = nImg.Info.Row;
                            int _Pos = nImg.Info.Pos;
                            nImg.Raw.CopyTo(ImgD, _Row * _ImgInfo.Width + _Pos);
                        }

                        Bitmap _bp = BitmapExtensions.BitmapSourceFromArray(ImgD, _ImgInfo.Width, _ImgInfo.Height, _ImgInfo.NumByte * 8,_Pl);
                        Bitmap bm = new Bitmap(_bp);
                        bm.MakeTransparent(Color.Fuchsia);
                        _ImgInfo.Img = bm;
                        //bm.Save("C:\\b" + string.Format("\\{0}.png", _ImgInfo.ImgIdx), ImageFormat.Png);
                    }
                    _ImgInfo.RelativeY = ReadInt32(_stream);
                    _ImgInfo.RelativeX = ReadInt32(_stream);

                    Trace.WriteLine(string.Format("Idx {0}\tINT [{1},{2}]\t\tW:{3}\tH:{4}",  _ImgInfo.ImgIdx, _ImgInfo.RelativeY, _ImgInfo.RelativeX, _ImgInfo.Width, _ImgInfo.Height));
                }

                return _ImgInfo;
            }

            public bool Open(Stream _stream)
            {
                D2MFile.ImgLists = new List<ImgInfo>();
                D2MFile.PlanLists = new List<Plan>();

                BinaryReader _br = new BinaryReader(_stream);
                UInt32 _ZipSize = _br.ReadUInt32();
                _br.ReadInt16();
                MemoryStream _decompress;
                DecompressData(_stream, out _decompress);
                _stream = _decompress;

                Read<HeaderInfo>(_stream, ref D2MFile.Header);
                if (D2MFile.Header.ImgCount - 1 >= 0)
                {
                    for (int imgidx = 0; imgidx < D2MFile.Header.ImgCount; ++imgidx)
                    {
                        D2MFile.ImgLists.Add(ReadImage(_stream, imgidx));
                    }
                }

                D2MFile.WidthPlan = D2MFile.Header.Width / 13;
                D2MFile.HeightPlan = D2MFile.Header.Height / 13;
                if (D2MFile.Header.Width % 13 > 0)
                {
                    ++D2MFile.WidthPlan;
                }
                if (D2MFile.Header.Height % 13 > 0)
                {
                    ++D2MFile.HeightPlan;
                }

                for (int i = 0; i < D2MFile.HeightPlan; ++i)
                {
                    for (int j = 0; j < D2MFile.WidthPlan; ++j)
                    {
                        Plan P = new Plan();
                        P.ImgLists = new List<ImgInfo>();
                        P.HIdx = i;
                        P.WIdx = j;

                        Read<PlanInfo>(_stream, ref P.Info);

                        if (P.Info.CountLayerTwo > 0)
                        {
                            P.TileBlocks2 = new MapAttr[P.Info.CountLayerTwo];
                            for (int cidx = 0; cidx < P.Info.CountLayerTwo; ++cidx)
                            {
                                Read<MapAttr>(_stream, ref P.TileBlocks2[cidx]);
                                Trace.WriteLine(string.Format("C1 ImgIdx1:{0}  ImgIdx2:{1}  ImgIdx3:{2}  Unk:{3}", P.TileBlocks2[cidx].ImgIdx, P.TileBlocks2[cidx].ImgIdx2, P.TileBlocks2[cidx].ImgIdx3, P.TileBlocks2[cidx].Unknow));
                            }
                        }
                        //!--
                        P.CountUnkImage = ReadInt16(_stream);
                        if (P.CountUnkImage > 0)
                        {
                            for (int cidx = 0; cidx < P.CountUnkImage; ++cidx)
                            {
                                P.ImgLists.Add( ReadImage(_stream, cidx));
                            }
                        }
                        //!--
                        int ACount = P.Info.MiniBlockHeight;
                        int BCount = P.Info.MiniBlockWidth;
                        P.TileBlocks = new MapAttr[ACount * BCount];
                        for (int m_BlkHIdx = 0; m_BlkHIdx < P.Info.MiniBlockHeight; ++m_BlkHIdx)
                        {
                            for (int m_BlkWIdx = 0; m_BlkWIdx < P.Info.MiniBlockWidth; ++m_BlkWIdx)
                            {
                                int idx = 0;
                                if (m_BlkHIdx == 0)
                                {
                                    idx = m_BlkWIdx;
                                }
                                else
                                {
                                    idx = (m_BlkHIdx * P.Info.MiniBlockWidth) + m_BlkWIdx;
                                }

                                Read<MapAttr>(_stream, ref P.TileBlocks[idx]);
                                if(P.TileBlocks[idx].ImgIdx2 != 0)
                                Trace.WriteLine(string.Format("C2 ImgIdx1:{0}  ImgIdx2:{1}  ImgIdx3:{2}  Unk:{3}", P.TileBlocks[idx].ImgIdx, P.TileBlocks[idx].ImgIdx2, P.TileBlocks[idx].ImgIdx3, P.TileBlocks[idx].Unknow));
                            }
                        }

                        D2MFile.PlanLists.Add(P);
                    }
                }

                for (int i = 0; i < D2MFile.HeightPlan; ++i)
                {
                    for (int j = 0; j < D2MFile.WidthPlan; ++j)
                    {
                        Int32 Ukn = ReadInt32(_stream);
                        //Trace.WriteLine(Ukn.ToString());
                    }
                }
                return true;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Drawing;

namespace MapEditor
{
    public partial class D2MFile
    {
        HeaderInfo Header = new HeaderInfo();
        public List<ImgInfo> ImgLists;
        public List<Plan> PlanLists;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Int32 _WidthPlan;
        public System.Int32 WidthPlan
        {
            get { return _WidthPlan; }
            set { _WidthPlan = value; }
        }
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Int32 _HeightPlan;
        public System.Int32 HeightPlan
        {
            get { return _HeightPlan; }
            set { _HeightPlan = value; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Int32 _MapWidth;
        public System.Int32 MapWidth
        {
            get { return _MapWidth; }
            set { _MapWidth = value; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Int32 _MapHeight;
        public System.Int32 MapHeight
        {
            get { return _MapHeight; }
            set { _MapHeight = value; }
        }
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        D2MReader _D2MReader;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Bitmap _MapBuffer;
        private System.Drawing.Bitmap MapBuffer
        {
            get { return _MapBuffer; }
            set { _MapBuffer = value; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Bitmap _MapBuffer2;
        private System.Drawing.Bitmap MapBuffer2
        {
            get { return _MapBuffer2; }
            set { _MapBuffer2 = value; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected string _FilePath;
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }

        public D2MFile()
        {
            MapBuffer = null;
            MapBuffer2 = null;
        }

        public bool ReadFromFile(string path)
        {
            Stream _D2MStreamSource = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            _D2MReader = new D2MReader(this);
            bool _ret = _D2MReader.Open(_D2MStreamSource);
            if( _ret )
            {
                CalculateSize();
                FilePath = path;
            }
            _D2MStreamSource.Dispose();
            return _ret;
        }

        public void CalculateSize()
        {
            this.MapWidth = 0;
            this.MapHeight = 0;
            for (int HeightIdx = 0; HeightIdx < this.HeightPlan; ++HeightIdx)
            {
                for (int WidthIdx = 0; WidthIdx < this.WidthPlan; ++WidthIdx)
                {
                    int idx = 0;
                    if (HeightIdx == 0)
                    {
                        idx = WidthIdx;
                    }
                    else
                    {
                        idx = (HeightIdx * this.WidthPlan) + WidthIdx;
                    }

                    Plan _P = this.PlanLists[idx];

                    int _X = 624 * (WidthIdx + HeightIdx);
                    int _Y = 312 * (HeightIdx + this.HeightPlan - WidthIdx) - (_P.Info.UnknownShort4 >> 1);

                    for (int m_Hidx = 0; m_Hidx < _P.Info.MiniBlockHeight; ++m_Hidx)
                    {
                        for (int m_Widx = 0; m_Widx < _P.Info.MiniBlockWidth; ++m_Widx)
                        {
                            int idx2 = 0;
                            if (m_Hidx == 0)
                            {
                                idx2 = m_Widx;
                            }
                            else
                            {
                                idx2 = (m_Hidx * _P.Info.MiniBlockWidth) + m_Widx;
                            }

                            MapAttr _Ma = _P.TileBlocks[idx2];

                            if (_P.ImgLists.Count > 0)
                            {
                                for (int imgidx = 0; imgidx < _P.ImgLists.Count; ++imgidx)
                                {
                                    ImgInfo m = _P.ImgLists[imgidx];
                                    if (m.Img != null)
                                    {
                                        if (_Ma.ImgIdx == m.ImgIdx)
                                        {
                                            int _Y2 = _Y + (_P.Info.UnknownShort4 >> 1) - (24 * (m_Widx - m_Hidx) - 24) + m.RelativeY + m.Img.Height;
                                            int _X2 = _X + (48 * (m_Hidx + m_Widx)) + m.RelativeX + m.Img.Width;
                                            if (this.MapWidth < _X2)
                                            {
                                                this.MapWidth = _X2;
                                            }
                                            if (this.MapHeight < _Y2)
                                            {
                                                this.MapHeight = _Y2;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int imgidx = 0; imgidx < this.ImgLists.Count; ++imgidx)
                                {
                                    ImgInfo m = this.ImgLists[imgidx];
                                    if (m.Img != null)
                                    {
                                        if (_Ma.ImgIdx == m.ImgIdx)
                                        {
                                            int _Y2 = _Y + (_P.Info.UnknownShort4 >> 1) - (24 * (m_Widx - m_Hidx) - 24) + m.RelativeY + m.Img.Height;
                                            int _X2 = _X + (48 * (m_Hidx + m_Widx)) + m.RelativeX + m.Img.Width;
                                            if (this.MapWidth < _X2)
                                            {
                                                this.MapWidth = _X2;
                                            }
                                            if (this.MapHeight < _Y2)
                                            {
                                                this.MapHeight = _Y2;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public Bitmap GetLayerImage()
        {
            if(this.MapBuffer != null )
            {
                return this.MapBuffer;
            }
            this.MapBuffer = new Bitmap(this.MapWidth, this.MapHeight);
            var g = Graphics.FromImage(this.MapBuffer);
            for (int HeightIdx = 0; HeightIdx < this.HeightPlan; ++HeightIdx)
            {
                for (int WidthIdx = 0; WidthIdx < this.WidthPlan; ++WidthIdx)
                {
                    int idx = 0;
                    if (HeightIdx == 0)
                    {
                        idx = WidthIdx;
                    }
                    else
                    {
                        idx = (HeightIdx * this.WidthPlan) + WidthIdx;
                    }

                    Plan _P = this.PlanLists[idx];

                    int _X = 624 * (WidthIdx + HeightIdx);
                    int _Y = 312 * (HeightIdx + this.HeightPlan - WidthIdx) - (_P.Info.UnknownShort4 >> 1);

                    for (int m_Hidx = 0; m_Hidx < _P.Info.MiniBlockHeight; ++m_Hidx)
                    {
                        for (int m_Widx = 0; m_Widx < _P.Info.MiniBlockWidth; ++m_Widx)
                        {
                            int idx2 = 0;
                            if (m_Hidx == 0)
                            {
                                idx2 = m_Widx;
                            }
                            else
                            {
                                idx2 = (m_Hidx * _P.Info.MiniBlockWidth) + m_Widx;
                            }

                            MapAttr _Ma = _P.TileBlocks[idx2];

                            if (_P.ImgLists.Count > 0)
                            {
                                for (int imgidx = 0; imgidx < _P.ImgLists.Count; ++imgidx)
                                {
                                    ImgInfo m = _P.ImgLists[imgidx];
                                    if (m.Img != null)
                                    {
                                        if (_Ma.ImgIdx == m.ImgIdx)
                                        {
                                            int _Y2 = _Y + (_P.Info.UnknownShort4 >> 1) - (24 * (m_Widx - m_Hidx) - 24);
                                            int _X2 = _X + (48 * (m_Hidx + m_Widx));
                                            g.DrawImage(m.Img, new Point(_X2 + m.RelativeY, _Y2 + m.RelativeX));
                                            Rectangle r = new Rectangle(new Point(_X2 + m.RelativeY, _Y2 + m.RelativeX), new Size(m.Img.Width, m.Img.Height));
                                            g.DrawRectangle(Pens.AliceBlue, r);
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int imgidx = 0; imgidx < this.ImgLists.Count; ++imgidx)
                                {
                                    ImgInfo m = this.ImgLists[imgidx];
                                    if (m.Img != null)
                                    {
                                        if (_Ma.ImgIdx == m.ImgIdx)
                                        {
                                            int _Y2 = _Y + (_P.Info.UnknownShort4 >> 1) - (24 * (m_Widx - m_Hidx) - 24);
                                            int _X2 = _X + (48 * (m_Hidx + m_Widx));
                                            g.DrawImage(m.Img, new Point(_X2 + m.RelativeY, _Y2 + m.RelativeX));
                                            Rectangle r = new Rectangle(new Point(_X2 + m.RelativeY, _Y2 + m.RelativeX), new Size(m.Img.Width, m.Img.Height));
                                            g.DrawRectangle(Pens.AliceBlue,r);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return this.MapBuffer;
        }

        public Bitmap GetLayer2Image()
        {
            if (this.MapBuffer2 != null )
            {
                return this.MapBuffer2;
            }

            this.MapBuffer2 = new Bitmap(this.MapWidth, this.MapHeight);
            var g2 = Graphics.FromImage(this.MapBuffer2);
            for (int HeightIdx = 0; HeightIdx < this.HeightPlan; ++HeightIdx)
            {
                for (int WidthIdx = 0; WidthIdx < this.WidthPlan; ++WidthIdx)
                {
                    int idx = 0;
                    if (HeightIdx == 0)
                    {
                        idx = WidthIdx;
                    }
                    else
                    {
                        idx = (HeightIdx * this.WidthPlan) + WidthIdx;
                    }


                    Plan _P = this.PlanLists[idx];
                    int _X = 624 * (WidthIdx + HeightIdx);
                    int _Y = 312 * (HeightIdx + this.HeightPlan - WidthIdx) - (_P.Info.UnknownShort4 >> 1);

                    if (_P.TileBlocks2 != null)
                    {
                        for (int m_Hidx = 0; m_Hidx < _P.TileBlocks2.Length; ++m_Hidx)
                        {
                            MapAttr _Ma = _P.TileBlocks2[m_Hidx];

                            for (int imgidx = 0; imgidx < this.ImgLists.Count; ++imgidx)
                            {
                                ImgInfo m = this.ImgLists[imgidx];
                                if (m.Img != null)
                                {
                                    if (_Ma.ImgIdx3 == m.ImgIdx)
                                    {
                                        int _Y2 = _Y + (_P.Info.UnknownShort4 >> 1) - (24 * (_Ma.ImgIdx - _Ma.ImgIdx2) - 24);
                                        int _X2 = _X + (48 * (_Ma.ImgIdx2 + _Ma.ImgIdx));
                                        g2.DrawImage(m.Img, new Point(_X2 + m.RelativeY, _Y2 + m.RelativeX));
                                        //Trace.WriteLine(string.Format("IMG: {0},{1} Idx {2} ", _X2, _Y2, idx));
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
            return this.MapBuffer2;
        }
    }
}

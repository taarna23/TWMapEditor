using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace MapEditor
{
    public partial class frmEditor : Form
    {
        MapDocument Working_MAP = null;
        private Bitmap _MapBuffer = new Bitmap(128, 128);

        private bool trackzoomMouseDown = false;
        private bool trackzoomScrolling = false;

        public frmEditor()
        {
            InitializeComponent();

            splitEditor.Panel1.AutoScroll = true;
            splitEditor.Panel1.AutoScrollMinSize = new Size(0, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void frmEditor_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                foreach (string fileLoc in filePaths)
                {
                    MapDocument m = new MapDocument();
                    m.Open(fileLoc);
                    RefreshWorkSpace(m);
                    break;
                }
            }
        }

        private void frmEditor_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        public void RefreshWorkSpace(MapDocument pMAP)
        {
            Working_MAP = pMAP;
            trackZoom.Value = 100;
            trackRenderlayer.Enabled = false;
            ReefreshMapView();
        }

        private void trackZoom_Scroll(object sender, EventArgs e)
        {
            trackzoomScrolling = true;
        }

        private void trackZoom_MouseDown(object sender, MouseEventArgs e)
        {
            trackzoomMouseDown = true;
        }

        private void trackZoom_MouseUp(object sender, MouseEventArgs e)
        {
            if (trackzoomMouseDown == true && trackzoomScrolling == true)
                ReefreshMapView();
            trackzoomScrolling = false;
            trackzoomMouseDown = false;
        }

        public void ReefreshMapView()
        {
            if (Working_MAP != null)
            {
                picMap.Width = Working_MAP.D2M.MapWidth;
                picMap.Height = Working_MAP.D2M.MapHeight;

                if (trackZoom.Value < 100)
                {
                    _MapBuffer = BitmapExtensions.ResizeImage(Working_MAP.D2M.GetLayerImage(), new Size(Working_MAP.D2M.MapWidth / 100 * trackZoom.Value, Working_MAP.D2M.MapHeight / 100 * trackZoom.Value));
                }
                else
                {
                    _MapBuffer = Working_MAP.D2M.GetLayerImage();
                }
                picMap.Invalidate();
            }
        }

        private void picMap_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(_MapBuffer, new Point(0, 0));
        }
    }
}

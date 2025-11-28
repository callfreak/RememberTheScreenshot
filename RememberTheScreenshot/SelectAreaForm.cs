using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace RememberTheScreenshot
{
    public partial class SelectAreaForm : Form
    {
        #region Resize Constants
        private const int
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17;

        const int BORDER_THICKNESS = 10;
        #endregion

        #region Resize Rectangles
        Rectangle Top { get { return new Rectangle(0, 0, this.ClientSize.Width, BORDER_THICKNESS); } }
        Rectangle Left { get { return new Rectangle(0, 0, BORDER_THICKNESS, this.ClientSize.Height); } }
        Rectangle Bottom { get { return new Rectangle(0, this.ClientSize.Height - BORDER_THICKNESS, this.ClientSize.Width, BORDER_THICKNESS); } }
        Rectangle Right { get { return new Rectangle(this.ClientSize.Width - BORDER_THICKNESS, 0, BORDER_THICKNESS, this.ClientSize.Height); } }

        Rectangle TopLeft { get { return new Rectangle(0, 0, BORDER_THICKNESS, BORDER_THICKNESS); } }
        Rectangle TopRight { get { return new Rectangle(this.ClientSize.Width - BORDER_THICKNESS, 0, BORDER_THICKNESS, BORDER_THICKNESS); } }
        Rectangle BottomLeft { get { return new Rectangle(0, this.ClientSize.Height - BORDER_THICKNESS, BORDER_THICKNESS, BORDER_THICKNESS); } }
        Rectangle BottomRight { get { return new Rectangle(this.ClientSize.Width - BORDER_THICKNESS, this.ClientSize.Height - BORDER_THICKNESS, BORDER_THICKNESS, BORDER_THICKNESS); } }
        #endregion

        #region Dragging Imports
        public const int WM_NCLBUTTONDOWN = 0xA1;

        // TODO: Captured Size end width/height - Border thickness
        // TODO: Minimum Size
        // TODO: Aspect Ratio Lock (Shift key)
        // TODO: Show dimensions while resizing
        // TODO: Remember last position and size

        private void btnCapture_Click(object sender, EventArgs e)
        {
            // take screenshot of selected area (DPI-aware when app manifest sets PerMonitorV2)
            var captureRect = this.RectangleToScreen(this.ClientRectangle);

            // hide the selector so underlying content is visible
            this.Hide();
            Application.DoEvents();
            System.Threading.Thread.Sleep(50);

            using (var bitmap = new Bitmap(captureRect.Width, captureRect.Height, PixelFormat.Format32bppArgb))
            using (var g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(captureRect.Location, Point.Empty, captureRect.Size, CopyPixelOperation.SourceCopy);

                var sfd = new SaveFileDialog
                {
                    Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp",
                    Title = "Save the screenshot",
                    FileName = "screenshot.png"
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    switch (sfd.FilterIndex)
                    {
                        case 1: bitmap.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png); break;
                        case 2: bitmap.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                        case 3: bitmap.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Bmp); break;
                    }
                }
            }

            this.Show();
        }

        public const int HTCAPTION = 0x2;

        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        #endregion

        public SelectAreaForm()
        {
            InitializeComponent();

            this.Opacity = .5D; // make trasparent
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true); // this is to avoid visual artifacts

            this.panelDrag.Cursor = Cursors.SizeAll; // change cursor to move
        }

        private void panelDrag_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        protected override void OnPaint(PaintEventArgs e) // you can safely omit this method if you want
        {
            e.Graphics.FillRectangle(Brushes.Green, Top);
            e.Graphics.FillRectangle(Brushes.Green, Left);
            e.Graphics.FillRectangle(Brushes.Green, Right);
            e.Graphics.FillRectangle(Brushes.Green, Bottom);
        }

        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == 0x84) // WM_NCHITTEST
            {
                var cursor = this.PointToClient(Cursor.Position);

                if (TopLeft.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
                else if (TopRight.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
                else if (BottomLeft.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
                else if (BottomRight.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;

                else if (Top.Contains(cursor)) message.Result = (IntPtr)HTTOP;
                else if (Left.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
                else if (Right.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
                else if (Bottom.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;
            }
        }
    }
}

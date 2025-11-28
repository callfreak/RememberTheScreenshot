using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

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

        const int BORDER_THICKNESS = 4;
        #endregion

        #region Resize Rectangles
#pragma warning disable CS0108 // Element blendet vererbte Element aus; fehlendes 'new'-Schlüsselwort
        Rectangle Top { get { return new Rectangle(0, 0, this.ClientSize.Width, BORDER_THICKNESS); } }
        Rectangle Left { get { return new Rectangle(0, 0, BORDER_THICKNESS, this.ClientSize.Height); } }
        Rectangle Bottom { get { return new Rectangle(0, this.ClientSize.Height - BORDER_THICKNESS, this.ClientSize.Width, BORDER_THICKNESS); } }
        Rectangle Right { get { return new Rectangle(this.ClientSize.Width - BORDER_THICKNESS, 0, BORDER_THICKNESS, this.ClientSize.Height); } }
#pragma warning restore CS0108 // Element blendet vererbte Element aus; fehlendes 'new'-Schlüsselwort

        Rectangle TopLeft { get { return new Rectangle(0, 0, BORDER_THICKNESS, BORDER_THICKNESS); } }
        Rectangle TopRight { get { return new Rectangle(this.ClientSize.Width - BORDER_THICKNESS, 0, BORDER_THICKNESS, BORDER_THICKNESS); } }
        Rectangle BottomLeft { get { return new Rectangle(0, this.ClientSize.Height - BORDER_THICKNESS, BORDER_THICKNESS, BORDER_THICKNESS); } }
        Rectangle BottomRight { get { return new Rectangle(this.ClientSize.Width - BORDER_THICKNESS, this.ClientSize.Height - BORDER_THICKNESS, BORDER_THICKNESS, BORDER_THICKNESS); } }
        #endregion

        #region Dragging Imports
        public const int WM_NCLBUTTONDOWN = 0xA1;

        public const int HTCAPTION = 0x2;

        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        #endregion

        private void panelDrag_Resize(object sender, EventArgs e)
        {
            LblAreaSize.Text = $"{this.Width} x {this.Height}\n" +
                $"Strg -> Aspect Ratio 16:9\n" +
                $"Shift -> 1:1\n" +
                $"Esc -> Close";
        }

        public SelectAreaForm()
        {
            InitializeComponent();

            // make trasparent
            this.Opacity = .5D; 
            this.DoubleBuffered = true;

            // this is to avoid visual artifacts
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            // change cursor to move
            this.panelDrag.Cursor = Cursors.SizeAll; 

            this.Resize += SelectAreaForm_Resize;
            this.KeyPreview = true;
            this.KeyDown += SelectAreaForm_KeyDown;
        }

        private void SelectAreaForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        // TODO: Captured Size end width/height - Border thickness
        // TODO: Minimum Size
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
                        case 1: bitmap.Save(sfd.FileName, ImageFormat.Png); break;
                        case 2: bitmap.Save(sfd.FileName, ImageFormat.Jpeg); break;
                        case 3: bitmap.Save(sfd.FileName, ImageFormat.Bmp); break;
                    }
                }
            }

            this.Show();
        }

        private void SelectAreaForm_Resize(object sender, EventArgs e)
        {
            // Save the aspect ratio on resize start
            //aspectRatio = (double)this.Width / this.Height;

            // Check for modifier keys
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                // 16:9 Ratio
                double aspectRatio = 16.0 / 9.0;
                int newWidth = this.Width;
                int newHeight = (int)(newWidth / aspectRatio);

                this.Resize -= SelectAreaForm_Resize;
                this.Size = new Size(newWidth, newHeight);
                this.Resize += SelectAreaForm_Resize;
            }
            else if ((ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                // 1:1 Ratio
                int side = Math.Min(this.Width, this.Height);

                this.Resize -= SelectAreaForm_Resize;
                this.Size = new Size(side, side);
                this.Resize += SelectAreaForm_Resize;
            }
            else
            {
                // Frei skalierbar
            }

        }

        private void panelDrag_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Orange, Top);
            e.Graphics.FillRectangle(Brushes.Orange, Left);
            e.Graphics.FillRectangle(Brushes.Orange, Right);
            e.Graphics.FillRectangle(Brushes.Orange, Bottom);
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

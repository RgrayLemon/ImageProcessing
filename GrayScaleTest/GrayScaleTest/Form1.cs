using GrayscaleTest;

namespace GrayScaleTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var img = ImageProcessing.GrayScale();
            pictureBox1.Image = img;
        }

        private void WindowForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 謎のバックグラウンド処理が残ってしまうので、プロセスを殺す
            ImageProcessing.KillProcess();
        }
    }
}
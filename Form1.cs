using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.DataVisualization.Charting;
using System.Windows.Forms;

namespace Copy
{
    public partial class Form1 : Form
    {
        String mode = null;
        Bitmap bmp;
        Bitmap ImageB;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           /* OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                if (mode == null)
                {
                    MessageBox.Show("No mode selected yet", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string filePath = openFileDialog.FileName;

                bmp = new Bitmap(filePath);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Size = new Size(200, 200);

                pictureBox1.Image = bmp;

         *//*       switch (mode)
                {
                    case "copy":
                        basicCopyFN(bmp);
                        break;
                    case "greyscale":
                        greyScaleFN(bmp); break;
                    case "color inversion":
                        inversionFN(bmp); break;
                    case "histogram":
                        int[] histogram = CalculateHistogram(bmp);
                        DrawHistogram(histogram);
                        break;
                    case "sepia":
                        ApplySepiaEffect(bmp); break;

                }*//*
            }*/

        }

        private void basicCopyFN(Bitmap bmp)
        {
            Bitmap basicCopyTMP = new Bitmap(bmp.Width, bmp.Height);

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color clr = bmp.GetPixel(i, j);
                    basicCopyTMP.SetPixel(i, j, clr);
                }
            }
            pictureBox2.Image = basicCopyTMP;
        }

        private void inversionFN(Bitmap bmp)
        {
            Bitmap newImage = new Bitmap(bmp.Width, bmp.Height);

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color clr = bmp.GetPixel(i, j);
                    newImage.SetPixel(i, j, Color.FromArgb(255 - clr.R, 255 - clr.G, 255 - clr.B));
                }
            }
            pictureBox2.Image = newImage;
        }

        private void greyScaleFN(Bitmap bmp)
        {
            Bitmap greyScaleTMP = new Bitmap(bmp.Width, bmp.Height);

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color clr = bmp.GetPixel(i, j);
                    int greyValue = (int)((clr.R + clr.G + clr.B) / 3);
                    Color greyColor = Color.FromArgb(greyValue, greyValue, greyValue);
                    greyScaleTMP.SetPixel(i, j, greyColor);
                }
            }
            pictureBox2.Image = greyScaleTMP;
        }



        private void ApplySepiaEffect(Bitmap originalImage)
        {
            Bitmap sepiaImage = new Bitmap(originalImage.Width, originalImage.Height);

            for (int i = 0; i < originalImage.Width; i++)
            {
                for (int j = 0; j < originalImage.Height; j++)
                {
                    Color originalColor = originalImage.GetPixel(i, j);

                    int sepiaR = (int)(originalColor.R * 0.393 + originalColor.G * 0.769 + originalColor.B * 0.189);
                    int sepiaG = (int)(originalColor.R * 0.349 + originalColor.G * 0.686 + originalColor.B * 0.168);
                    int sepiaB = (int)(originalColor.R * 0.272 + originalColor.G * 0.534 + originalColor.B * 0.131);


                    sepiaR = Math.Min(255, Math.Max(0, sepiaR));
                    sepiaG = Math.Min(255, Math.Max(0, sepiaG));
                    sepiaB = Math.Min(255, Math.Max(0, sepiaB));

                    Color sepiaColor = Color.FromArgb(sepiaR, sepiaG, sepiaB);
                    sepiaImage.SetPixel(i, j, sepiaColor);
                }
            }

            pictureBox2.Image = sepiaImage;
        }

        public int[] CalculateHistogram(Bitmap image)
        {
            int[] histogram = new int[256]; // Assuming 8-bit greyscale image

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color pixel = image.GetPixel(i, j);
                    int greyValue = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                    histogram[greyValue]++;
                }
            }

            return histogram;
        }

        private void DrawHistogram(int[] histogram)
        {
            int barWidth = 2;
            int maxHeight = 150;
            int offsetX = 50;
            int offsetY = 200;

            Bitmap histogramImage = new Bitmap(256 * barWidth + offsetX * 2, maxHeight + offsetY * 2);
            using (Graphics g = Graphics.FromImage(histogramImage))
            {
                g.Clear(Color.White);

                for (int i = 0; i < histogram.Length; i++)
                {
                    int barHeight = Math.Min(histogram[i], maxHeight);
                    g.FillRectangle(Brushes.Black, offsetX + i * barWidth, offsetY + maxHeight - barHeight, barWidth, barHeight);

                    if (i % 20 == 0) 
                    {
                        g.DrawString(i.ToString(), Font, Brushes.Black, offsetX + i * barWidth, offsetY + maxHeight + 5);
                    }
                }
            }

            pictureBox2.Image = histogramImage;
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void basicCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mode = "copy";
            label4.Text = mode;
            if (pictureBox1.Image != null)
            {
                basicCopyFN(bmp);
            }
        }

        private void greyScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mode = "greyscale";
            label4.Text = mode;
            if (pictureBox1.Image != null)
            {
                greyScaleFN(bmp);
            }
        }

        private void colorInversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mode = "color inversion";
            label4.Text = mode;
            if (pictureBox1.Image != null)
            {
                inversionFN(bmp);
            }
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mode = "histogram";
            label4.Text = mode;
            if (pictureBox1.Image != null)
            {
                int[] histogram = CalculateHistogram(bmp);
                DrawHistogram(histogram);
            }
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mode = "sepia";
            label4.Text = mode;
            if (pictureBox1.Image != null)
            {
                ApplySepiaEffect(bmp);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PNG Image|*.png";
                saveFileDialog.Title = "Save Sepia Image";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                {
                    pictureBox2.Image.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
            else
            {
                MessageBox.Show("No image to save.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;

                ImageB = new Bitmap(filePath);
                

                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox3.Size = new Size(200, 200);
                pictureBox3.Image = ImageB;

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Color mygreen = Color.FromArgb(0, 255, 0);
            int greygreen = (mygreen.R + mygreen.G + mygreen.B) / 3;
            int threshold = 5;

            Bitmap resultImage = new Bitmap(bmp.Width, bmp.Height);

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    Color pixel = bmp.GetPixel(x, y);
                    Color backPixel = ImageB.GetPixel(x, y);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    int subtractvalue = Math.Abs(grey - greygreen);
                    if (subtractvalue > threshold)
                    {
                        resultImage.SetPixel(x, y, pixel);
                    } else
                    {
                        resultImage.SetPixel(x, y, backPixel);
                    }
                }
            }
            pictureBox2.Image = resultImage;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                bmp = new Bitmap(filePath);


                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Size = new Size(200, 200);
                pictureBox1.Image = bmp;

            }
        }
    }
}

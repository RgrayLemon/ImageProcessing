using Numpy;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrayscaleTest
{
    public class ImageProcessing
    {
        public static Bitmap GrayScale()
        {

            var imagePath = @"test.npy";
            var imageArray = np.load(imagePath);
            var type = imageArray.dtype; // uint8
            // カラー画像を.npyにしたので、3次元の方で変換する
            var inputMat = NDArray3ToMat(imageArray, MatType.CV_8UC3);
            var mat = new Mat();
            Cv2.CvtColor(inputMat, mat, ColorConversionCodes.BGR2GRAY);
            return BitmapConverter.ToBitmap(mat);
        }

        static Mat NDArrayToMat(NDarray array, MatType type)
        {
            try
            {
                int rows = array.shape[0];
                int cols = array.shape[1];

                Mat mat = new Mat(rows, cols, type);
                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < cols; x++)
                    {
                        mat.Set<int>(y, x, (int)(array[y, x]));
                    }
                }
                return mat;
            }
            catch (Exception ex)
            {
                return new Mat();
            }
        }

        static Mat NDArray3ToMat(NDarray array, MatType type)
        {
            try
            {
                int rows = array.shape[0];
                int cols = array.shape[1];

                Mat mat = new Mat(rows, cols, type);
                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < cols; x++)
                    {
                        Vec3b pic = new Vec3b();
                        pic[0] = (byte)array[y, x, 0];
                        pic[1] = (byte)array[y, x, 1];
                        pic[2] = (byte)array[y, x, 2];
                        mat.Set<Vec3b>(y, x, pic);
                    }
                }
                return mat;
            }
            catch (Exception ex)
            {
                return new Mat();
            }
        }

        static NDarray MatToNDArray(Mat mat)
        {
            try
            {
                int width = mat.Width;
                int height = mat.Height;
                int[,] data = new int[height, width];

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        data[y, x] = mat.Get<int>(y, x);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                return (NDarray)0;
            }
        }

        static NDarray MatToNDArray3(Mat mat)
        {
            try
            {
                int width = mat.Width;
                int height = mat.Height;
                int[,,] data = new int[height, width, 3];

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        data[y, x, 0] = mat.At<Vec3b>(y, x)[0];
                        data[y, x, 1] = mat.At<Vec3b>(y, x)[1];
                        data[y, x, 2] = mat.At<Vec3b>(y, x)[2];
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                return (NDarray)0;
            }
        }

        public static void KillProcess()
        {

            // 自身のプロセス名で検索して、そいつを殺す
            System.Diagnostics.Process[] ps = System.Diagnostics.Process.GetProcessesByName("GrayScaleTest");
            foreach (System.Diagnostics.Process p in ps)
            {
                p.Kill();
            }
        }
    }
}

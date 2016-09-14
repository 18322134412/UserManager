using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System;
using System.Drawing.Drawing2D;

namespace ConsoleApplication3 {
    /// <summary>
    /// 图片处理类
    /// 1、生成缩略图片或按照比例改变图片的大小和画质
    /// 2、将生成的缩略图放到指定的目录下
    /// </summary>
    public class PhotoHelper
    {
        #region 缩略图和剪切图片

        /// <summary> 
        /// 剪切图片
        /// </summary> 
        /// <param name="originalImagePath">源图路径（物理路径）</param> 
        /// <param name="thumbnailPath">剪切图路径（物理路径）</param> 
        /// <param name="fromWidth">原图起始地址</param> 
        /// <param name="fromHeight">原图起始地址</param> 
        /// <param name="width">剪切图宽度</param> 
        /// <param name="height">剪切图高度</param>  
        public static void MakeThumbnailByCut(string originalImagePath, string thumbnailPath, int fromWidth, int fromHeight, int width, int height)
        {
            Image originalImage = Image.FromFile(originalImagePath);
            //新建一个bmp图片 
            Image bitmap = new System.Drawing.Bitmap(width, height);

            //新建一个画板 
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充 
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分 
            g.DrawImage(originalImage, new Rectangle(0, 0, width, height),
                new Rectangle(fromWidth, fromHeight, width, height),
                GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图 
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        /// <summary> 
        /// 剪切成比例图片
        /// </summary> 
        /// <param name="originalImagePath">源图路径（物理路径）</param> 
        /// <param name="thumbnailPath">剪切图路径（物理路径）</param> 
        /// <param name="fromWidth">原图起始地址</param> 
        /// <param name="fromHeight">原图起始地址</param> 
        /// <param name="width">剪切图宽度</param> 
        /// <param name="height">剪切图高度</param> 
        /// <param name="rate">剪切比例</param> 
        public static void MakeThumbnailByCutRate(string originalImagePath, string thumbnailPath, int fromWidth, int fromHeight, int width, int height, int rate)
        {
            Image originalImage = Image.FromFile(originalImagePath);
            //新建一个bmp图片 
            Image bitmap = new System.Drawing.Bitmap(width * rate, height * rate);

            //新建一个画板 
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充 
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分 
            g.DrawImage(originalImage, new Rectangle(0, 0, width * rate, height * rate),
                new Rectangle(fromWidth, fromHeight, width, height),
                GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图 
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        /// <summary> 
        /// 通过高度生成缩略图 
        /// </summary> 
        /// <param name="originalImagePath">源图路径（物理路径）</param> 
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param> 
        /// <param name="height">缩略图高度</param>    
        public static void MakeThumbnailByHeight(string originalImagePath, string thumbnailPath, int height)
        {

            Image originalImage = Image.FromFile(originalImagePath);

            int width = originalImage.Width * height / originalImage.Height;


            //新建一个bmp图片 
            Image bitmap = new System.Drawing.Bitmap(width, height);

            //新建一个画板 
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充 
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分 
            g.DrawImage(originalImage, new Rectangle(0, 0, width, height),
                new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图 
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        /// <summary> 
        /// 通过宽度生成缩略图 
        /// </summary> 
        /// <param name="originalImagePath">源图路径（物理路径）</param> 
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param> 
        /// <param name="width">缩略图宽度</param>    
        public static void MakeThumbnailByWidth(string originalImagePath, string thumbnailPath, int width)
        {

            Image originalImage = Image.FromFile(originalImagePath);

            int height = originalImage.Height * width / originalImage.Width;


            //新建一个bmp图片 
            Image bitmap = new System.Drawing.Bitmap(width, height);

            //新建一个画板 
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充 
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分 
            g.DrawImage(originalImage, new Rectangle(0, 0, width, height),
                new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图 
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        /// <summary> 
        /// 成比例压缩图片
        /// </summary> 
        /// <param name="originalImagePath">源图路径（物理路径）</param> 
        /// <param name="thumbnailPath">剪切图路径（物理路径）</param> 
        /// <param name="rate">剪切比例</param> 
        public static void MakeThumbnailByRate(string originalImagePath, string thumbnailPath, int rate)
        {
            Image originalImage = Image.FromFile(originalImagePath);

            int width = originalImage.Width;
            int height = originalImage.Height;

            //新建一个bmp图片 
            Image bitmap = new System.Drawing.Bitmap(width * rate, height * rate);

            //新建一个画板 
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充 
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分 
            g.DrawImage(originalImage, new Rectangle(0, 0, width * rate, height * rate),
                new Rectangle(0, 0, width, height),
                GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图 
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
        #endregion


        #region 给图片加水印

        /// <summary>
        /// 添加水印(分图片水印与文字水印两种)
        /// </summary>
        /// <param name="oldpath">原图片绝对地址</param>
        /// <param name="newpath">新图片放置的绝对地址</param>
        /// <param name="wmtType">要添加的水印的类型</param>
        /// <param name="sWaterMarkContent">水印内容，若添加文字水印，此即为要添加的文字；
        /// 若要添加图片水印，此为图片的路径</param>
        public void addWaterMark(string oldpath, string newpath,
         WaterMarkType wmtType, string sWaterMarkContent, WaterMarkPosition _waterMarkPosition)
        {
            try
            {
                Image image = Image.FromFile(oldpath);

                Bitmap b = new Bitmap(image.Width, image.Height,
                 PixelFormat.Format24bppRgb);

                Graphics g = Graphics.FromImage(b);
                g.Clear(Color.Transparent);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.High;

                g.DrawImage(image, 0, 0, image.Width, image.Height);

                switch (wmtType)
                {

                    case WaterMarkType.TextMark:
                        //文字水印
                        this.addWatermarkText(g, sWaterMarkContent, _waterMarkPosition,
                         image.Width, image.Height);
                        break;
                }

                b.Save(newpath);
                b.Dispose();
                image.Dispose();
            }
            catch { }
        }

        /// <summary>
        ///   加水印文字
        /// </summary>
        /// <param name="picture">imge 对象</param>
        /// <param name="_watermarkText">水印文字内容</param>
        /// <param name="_watermarkPosition">水印位置</param>
        /// <param name="_width">被加水印图片的宽</param>
        /// <param name="_height">被加水印图片的高</param>
        private void addWatermarkText(Graphics picture, string _watermarkText,
         WaterMarkPosition _watermarkPosition, int _width, int _height)
        {
            // 确定水印文字的字体大小
            int[] sizes = new int[] { 32, 30, 28, 26, 24, 22, 20, 18, 16, 14, 12, 10, 8, 6, 4 };
            Font crFont = null;
            SizeF crSize = new SizeF();
            for (int i = 0; i < sizes.Length; i++)
            {
                crFont = new Font("Arial Black", sizes[i], FontStyle.Bold);
                crSize = picture.MeasureString(_watermarkText, crFont);

                if ((ushort)crSize.Width < (ushort)_width)
                {
                    break;
                }
            }

            // 生成水印图片（将文字写到图片中）
            Bitmap floatBmp = new Bitmap((int)crSize.Width + 3,
                  (int)crSize.Height + 3, PixelFormat.Format32bppArgb);
            Graphics fg = Graphics.FromImage(floatBmp);
            PointF pt = new PointF(0, 0);

            // 画阴影文字
            Brush TransparentBrush0 = new SolidBrush(Color.FromArgb(255, Color.Black));
            Brush TransparentBrush1 = new SolidBrush(Color.FromArgb(255, Color.Black));
            fg.DrawString(_watermarkText, crFont, TransparentBrush0, pt.X, pt.Y + 1);
            fg.DrawString(_watermarkText, crFont, TransparentBrush0, pt.X + 1, pt.Y);

            fg.DrawString(_watermarkText, crFont, TransparentBrush1, pt.X + 1, pt.Y + 1);
            fg.DrawString(_watermarkText, crFont, TransparentBrush1, pt.X, pt.Y + 2);
            fg.DrawString(_watermarkText, crFont, TransparentBrush1, pt.X + 2, pt.Y);

            TransparentBrush0.Dispose();
            TransparentBrush1.Dispose();

            // 画文字
            fg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            fg.DrawString(_watermarkText,
             crFont, new SolidBrush(Color.White),
             pt.X, pt.Y, StringFormat.GenericDefault);

            // 保存刚才的操作
            fg.Save();
            fg.Dispose();
            // floatBmp.Save("d:\\WebSite\\DIGITALKM\\ttt.jpg");

            // 将水印图片加到原图中
            this.addWatermarkImage(
             picture,
             new Bitmap(floatBmp),
             _watermarkPosition,
             _width,
             _height);
        }

        /// <summary>
        ///   加水印图片
        /// </summary>
        /// <param name="picture">imge 对象</param>
        /// <param name="iTheImage">Image对象（以此图片为水印）</param>
        /// <param name="_watermarkPosition">水印位置</param>
        /// <param name="_width">被加水印图片的宽</param>
        /// <param name="_height">被加水印图片的高</param>
        private void addWatermarkImage(Graphics picture, Image iTheImage,
          WaterMarkPosition _watermarkPosition, int _width, int _height)
        {
            Image watermark = new Bitmap(iTheImage);

            ImageAttributes imageAttributes = new ImageAttributes();
            ColorMap colorMap = new ColorMap();
            //设置背景颜色的转换
            colorMap.OldColor = Color.FromArgb(255, 255, 255, 255);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
            ColorMap[] remapTable = { colorMap };

            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);
            //设置图片颜色的转化
            float[][] colorMatrixElements = {
             new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
             new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
             new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
             new float[] {0.0f, 0.0f, 0.0f, 0.3f, 0.0f},
             new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
            };

            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            int xpos = 0;
            int ypos = 0;
            int WatermarkWidth = 0;
            int WatermarkHeight = 0;
            double bl = 1d;

            //计算水印图片的比率
            //取背景的1/4宽度来比较
            if ((_width > watermark.Width * 4) && (_height > watermark.Height * 4))
            {
                bl = 1;
            }
            else if ((_width > watermark.Width * 4) && (_height < watermark.Height * 4))
            {
                bl = Convert.ToDouble(_height / 4) / Convert.ToDouble(watermark.Height);

            }
            else if ((_width < watermark.Width * 4) && (_height > watermark.Height * 4))
            {
                bl = Convert.ToDouble(_width / 4) / Convert.ToDouble(watermark.Width);
            }
            else
            {
                if ((_width * watermark.Height) > (_height * watermark.Width))
                {
                    bl = Convert.ToDouble(_height / 4) / Convert.ToDouble(watermark.Height);

                }
                else
                {
                    bl = Convert.ToDouble(_width / 4) / Convert.ToDouble(watermark.Width);

                }

            }

            WatermarkWidth = Convert.ToInt32(watermark.Width * bl);
            WatermarkHeight = Convert.ToInt32(watermark.Height * bl);

            switch (_watermarkPosition)
            {
                case WaterMarkPosition.WMP_Left_Top:
                    xpos = 10;
                    ypos = 10;
                    break;
                case WaterMarkPosition.WMP_Right_Top:
                    xpos = _width - WatermarkWidth - 10;
                    ypos = 10;
                    break;
                case WaterMarkPosition.WMP_Right_Bottom:
                    xpos = _width - WatermarkWidth - 10;
                    ypos = _height - WatermarkHeight - 10;
                    break;
                case WaterMarkPosition.WMP_Left_Bottom:
                    xpos = 10;
                    ypos = _height - WatermarkHeight - 10;
                    break;
            }

            picture.DrawImage(
             watermark,
             new Rectangle(xpos, ypos, WatermarkWidth, WatermarkHeight),
             0,
             0,
             watermark.Width,
             watermark.Height,
             GraphicsUnit.Pixel,
             imageAttributes);

            watermark.Dispose();
            imageAttributes.Dispose();
        }

        /// <summary>
        ///   加水印图片
        /// </summary>
        /// <param name="picture">imge 对象</param>
        /// <param name="WaterMarkPicPath">水印图片的地址</param>
        /// <param name="_watermarkPosition">水印位置</param>
        /// <param name="_width">被加水印图片的宽</param>
        /// <param name="_height">被加水印图片的高</param>
        private void addWatermarkImage(Graphics picture, string WaterMarkPicPath,
         WaterMarkPosition _watermarkPosition, int _width, int _height)
        {
            Image watermark = new Bitmap(WaterMarkPicPath);

            this.addWatermarkImage(picture, watermark, _watermarkPosition, _width,
             _height);
        }
        #endregion
    }
    /// <summary>
    /// 水印的类型
    /// </summary>
    public enum WaterMarkType
    {
        /// <summary>
        /// 文字水印
        /// </summary>
        TextMark,
        /// <summary>
        /// 图片水印
        /// </summary>
        ImageMark
    };

    /// <summary>
    /// 水印的位置
    /// </summary>
    public enum WaterMarkPosition
    {
        /// <summary>
        /// 左上角
        /// </summary>
        WMP_Left_Top,
        /// <summary>
        /// 左下角
        /// </summary>
        WMP_Left_Bottom,
        /// <summary>
        /// 右上角
        /// </summary>
        WMP_Right_Top,
        /// <summary>
        /// 右下角
        /// </summary>
        WMP_Right_Bottom
    };
}


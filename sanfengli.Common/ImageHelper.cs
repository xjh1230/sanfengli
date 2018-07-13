﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace sanfengli.Common
{
    public class ImageHelper
    {
        #region 配置: 图片上传路径 & 图片所属类别


        /// <summary>
        /// 上传图片的类型,用于决定存放目录
        /// (如果类型不够,请联系底层负责人添加)
        /// </summary>
        public class ImgDirType
        {
            public static string 活动图片 = "ActImg";
            public static string 活动专题 = "ActProject";

        }

        #endregion

        #region 方法：通过文件后缀判断是否问图片文件

        /// <summary>
        /// 判断文件类型是否为WEB格式图片
        /// (注：".jpg", ".JPG", ".png", ".gif", ".GIF", ".bmp", ".BMP")
        /// </summary>
        /// <param name="fileExt">文件后缀,例如 .jpg</param>
        /// <returns></returns>
        public static bool IsImgType(string fileExt)
        {
            List<string> imgType = new List<string>() { ".jpg", ".JPG", ".png", ".gif", ".GIF", ".bmp", ".BMP" };

            return (imgType.Contains(fileExt)) ? true : false;
        }

        #endregion


        #region 方法：UpLoadImg() 上传图片

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="dirType">此参数请输入 ImageHelper.ImgDirType...点出来的东西.   此方法同类下的枚举, 表名此图片要上传到那种文件夹下</param>
        /// <param name="requestKey">控件fileUpload的 ID</param>
        /// <param name="allowEmpty">是否允许调用此方法但不上传图片</param>
        /// <returns>string[0] true 或 false , string[1]图片相对路径(存数据库) 或 失败信息</returns>
        public static string[] UpLoadImg(string dirType, string requestKey, bool allowEmpty = true, bool isFtpTrans = true, bool isPc = true)
        {
            string[] result = { "false", "上传失败" };
            string cfg_upImgApp = "";

            cfg_upImgApp = BaseClass.cfg_dir_upImg+"upload";
            try
            {
                requestKey = (requestKey == "") ? "ImgUrl" : requestKey;
                var file = HttpContext.Current.Request.Files[0];

                //获取文件流
                Stream bigImgIO = file.InputStream;
                //1.检测是否有此控件(传入参数是否正常)
                if (file == null)
                {
                    result = new string[] { "false", "传输参数异常,无此(" + requestKey + ")控件ID" };
                }
                //2.检测是否有文件(检测是否选中要上传的文件)
                else if (file.ContentLength == 0)
                {
                    result = (allowEmpty == true) ? new string[] { "true", "" } : new string[] { "false", "未检测到文件" };
                }

                //3检测文件格式是否符合要求
                else if (!IsImgType(Path.GetExtension(file.FileName)))
                {
                    result = new string[] { "false", "文件格式( " + Path.GetExtension(file.FileName) + " )错误,请选择图片格式上传" };
                }
                //4.检测文件大小是否符合要求(<=500kb)
                else if (file.ContentLength > 524288)
                {
                    string fileExt = Path.GetExtension(file.FileName);

                    //bigImgIO = MakeSmallImg(file.InputStream, fileExt,50);
                    bigImgIO = MakeSmallImg(file.InputStream, fileExt, 80);
                    if (bigImgIO.Length > 524288)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            bigImgIO = MakeSmallImg(file.InputStream, fileExt, 80 - i * 10);
                            if (bigImgIO.Length < 524288)
                                break;
                        }
                    }
                    if (bigImgIO == null)
                    {
                        result = new string[] { "false", file.FileName + " 上传失败,上传文件不允许超过500K,请更改后重新上传" };
                    }
                    else if (bigImgIO.Length < 524288)
                    {
                        #region 代码块: 执行上传

                        //string fileName = Path.GetFileNameWithoutExtension(file.FileName);

                        string dirTypePath = dirType;

                        string absolutDir = Path.Combine(cfg_upImgApp, dirTypePath, DateTime.Now.ToString("yyyy-MM-dd"));

                        if (!Directory.Exists(absolutDir))
                        {
                            Directory.CreateDirectory(absolutDir);
                        }
                        string newImgName = CalculateHelper.CreateNumCode() + fileExt;
                        //Stream bigImgIO = file.InputStream;//移动到try  处

                        //保存小图
                        if (SaveImage(bigImgIO, absolutDir, newImgName))
                        {
                            absolutDir += @"/" + newImgName;
                            string relationPath = ReturnCrmDataBasePath(absolutDir, isPc);

                            result = new string[] { "true", relationPath };
                        }
                        else
                        {
                            result = new string[] { "false", "图片上传失败,请重试" };
                        }
                        #endregion
                    }
                    else
                    {
                        result = new string[] { "false", file.FileName + " 上传失败,上传文件不允许超过500K,请更改后重新上传" };
                    }

                }
                else
                {
                    #region 代码块: 执行上传

                    string fileExt = Path.GetExtension(file.FileName);

                    string dirTypePath = dirType;


                    string absolutDir = Path.Combine(cfg_upImgApp, dirTypePath, DateTime.Now.ToString("yyyy-MM-dd"));

                    if (!Directory.Exists(absolutDir))
                    {
                        Directory.CreateDirectory(absolutDir);
                    }
                    string newImgName = CalculateHelper.CreateNumCode() + fileExt;

                    //保存小图
                    if (SaveImage(bigImgIO, absolutDir, newImgName))
                    {
                        absolutDir += @"/" + newImgName;
                        string relationPath = ReturnCrmDataBasePath(absolutDir, isPc);

                        result = new string[] { "true", relationPath };
                    }
                    else
                    {
                        result = new string[] { "false", "图片上传失败,请重试" };
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                result = new string[] { "false", "发生异常,请联系管理员,异常信息: " + ex.Message };
            }
            return result;
        }

        private static string ReturnCrmDataBasePath(string absolutDir, bool isPc)
        {
            return absolutDir.Replace(BaseClass.cfg_dir_upImg, BaseClass.CurrentDomin);
        }
        #endregion

        #region  生成压缩后的流
        /// <summary>
        /// 生成压缩后的流
        /// </summary>
        /// <param name="fromFile">输入流</param>
        /// <param name="fileExt">源文件类型</param>
        /// <param name="quality">质量</param>
        /// <returns></returns>
        public static Stream MakeSmallImg(Stream fromFile, string fileExt, int quality)
        {
            Stream resultStream = new MemoryStream();
            //从文件取得图片对象，并使用流中嵌入的颜色管理信息   
            //System.Drawing.Image myImage = System.Drawing.Image.FromStream(fromFileStream, true);
            System.Drawing.Image myImage = System.Drawing.Image.FromStream(fromFile);
            //缩略图宽、高   
            System.Double newWidth = myImage.Width, newHeight = myImage.Height;

            #region  最大宽高处理
            //模版的宽高比例
            double WidthRate = 1;
            //原图片的宽高比例
            double HeightRate = 1;
            //最终比例  取宽高最下
            double Rate = 1;
            if (newWidth > 1300 || newHeight > 800)
            {
                WidthRate = 1300 / newWidth;
                HeightRate = 800 / newHeight;
                Rate = WidthRate > HeightRate ? HeightRate : WidthRate;
            }
            newWidth = Rate * newWidth;
            newHeight = Rate * newHeight;
            #endregion
            //取得图片大小   
            System.Drawing.Size mySize = new Size((int)newWidth, (int)newHeight);
            //新建一个bmp图片   
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(mySize.Width, mySize.Height);
            //新建一个画板   
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            try
            {
                //设置高质量插值法   
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //设置高质量,低速度呈现平滑程度   
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //清空一下画布   
                g.Clear(System.Drawing.Color.Transparent);
                //在指定位置画图   
                g.DrawImage(myImage, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                new System.Drawing.Rectangle(0, 0, myImage.Width, myImage.Height),
                System.Drawing.GraphicsUnit.Pixel);

                ImageFormat format = null;
                fileExt = fileExt.ToUpper();
                switch (fileExt)
                {
                    //".jpg", ".JPG", ".png", ".gif", ".GIF", ".bmp", ".BMP" 
                    case ".JPG":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".PNG":
                        format = ImageFormat.Png;
                        break;
                    case ".GIF":
                        format = ImageFormat.Gif;
                        break;
                    case ".BMP":
                        format = ImageFormat.Bmp;
                        break;
                    default:
                        format = ImageFormat.Jpeg;
                        break;
                }
                bitmap.Save(resultStream, format);
                return resultStream;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 方法: 生成缩略图(常用)

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="fromFileStream">原图片的物理路径</param>
        /// <param name="fileSaveUrl">缩略图保存路径(建议为物理路径)</param>
        /// <param name="templateWidth">缩略图宽度</param>
        /// <param name="templateHeight">缩略图高度</param>
        public static bool MakeSmallImg(string fromFileStream, string fileSaveUrl, System.Double templateWidth, System.Double templateHeight)
        {

            bool result = false;

            //从文件取得图片对象，并使用流中嵌入的颜色管理信息   
            //System.Drawing.Image myImage = System.Drawing.Image.FromStream(fromFileStream, true);
            System.Drawing.Image myImage = System.Drawing.Image.FromFile(fromFileStream);
            //缩略图宽、高   
            System.Double newWidth = myImage.Width, newHeight = myImage.Height;
            //宽大于模版的横图   
            if (myImage.Width > myImage.Height || myImage.Width == myImage.Height)
            {
                if (myImage.Width > templateWidth)
                {
                    //宽按模版，高按比例缩放   
                    newWidth = templateWidth;
                    newHeight = myImage.Height * (newWidth / myImage.Width);
                }
            }
            //高大于模版的竖图   
            else
            {
                if (myImage.Height > templateHeight)
                {
                    //高按模版，宽按比例缩放   
                    newHeight = templateHeight;
                    newWidth = myImage.Width * (newHeight / myImage.Height);
                }
            }

            //取得图片大小   
            System.Drawing.Size mySize = new Size((int)newWidth, (int)newHeight);
            //新建一个bmp图片   
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(mySize.Width, mySize.Height);
            //新建一个画板   
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            try
            {
                //设置高质量插值法   
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //设置高质量,低速度呈现平滑程度   
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //清空一下画布   
                g.Clear(System.Drawing.Color.Transparent);
                //在指定位置画图   
                g.DrawImage(myImage, new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                new System.Drawing.Rectangle(0, 0, myImage.Width, myImage.Height),
                System.Drawing.GraphicsUnit.Pixel);

                ///文字水印   
                //System.Drawing.Graphics G = System.Drawing.Graphics.FromImage(bitmap);
                //System.Drawing.Font f = new Font("宋体", 10);
                //System.Drawing.Brush b = new SolidBrush(Color.Black);
                //G.DrawString("myohmine", f, b, 10, 10);
                //G.Dispose();  

                ///图片水印   
                //System.Drawing.Image copyImage = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath("1.jpg"));
                //Graphics a = Graphics.FromImage(bitmap);
                //a.DrawImage(copyImage, new Rectangle(bitmap.Width - copyImage.Width, bitmap.Height - copyImage.Height, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, GraphicsUnit.Pixel);

                //copyImage.Dispose();
                //a.Dispose();
                //copyImage.Dispose();  

                //保存缩略图   
                string savaDir = fileSaveUrl.Contains(":") ? fileSaveUrl : HttpContext.Current.Server.MapPath(fileSaveUrl);
                bitmap.Save(savaDir, System.Drawing.Imaging.ImageFormat.Jpeg);


                g.Dispose();
                myImage.Dispose();
                bitmap.Dispose();

                result = true;

            }
            catch (Exception ex)
            {
                g.Dispose();
                myImage.Dispose();
                bitmap.Dispose();

                throw ex;
            }

            return result;
        }
        #endregion


        #region 方法：自定义裁剪并缩放（指定长宽裁剪，按模版比例最大范围的裁剪图片并缩放至模版尺寸）--此方法暂时没有被用到过,若用到,则需要修改其逻辑代码 去 适配UploadImg()

        /// <summary>
        /// 指定长宽裁剪
        /// 按模版比例最大范围的裁剪图片并缩放至模版尺寸
        /// </summary>
        /// <remarks>谭光洪 2015-05-09</remarks>
        /// <param name="fromFile">原图Stream对象</param>
        /// <param name="fileSaveUrl">保存路径</param>
        /// <param name="maxWidth">最大宽(单位:px)</param>
        /// <param name="maxHeight">最大高(单位:px)</param>
        /// <param name="quality">质量（范围0-100）</param>
        public static void CutForCustom(System.IO.Stream fromFile, string fileSaveUrl, int maxWidth, int maxHeight, int quality)
        {
            //从文件获取原始图片，并使用流中嵌入的颜色管理信息
            System.Drawing.Image initImage = System.Drawing.Image.FromStream(fromFile, true);

            //原图宽高均小于模版，不作处理，直接保存
            if (initImage.Width <= maxWidth && initImage.Height <= maxHeight)
            {
                initImage.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                //模版的宽高比例
                double templateRate = (double)maxWidth / maxHeight;
                //原图片的宽高比例
                double initRate = (double)initImage.Width / initImage.Height;

                //原图与模版比例相等，直接缩放
                if (templateRate == initRate)
                {
                    //按模版大小生成最终图片
                    System.Drawing.Image templateImage = new System.Drawing.Bitmap(maxWidth, maxHeight);
                    System.Drawing.Graphics templateG = System.Drawing.Graphics.FromImage(templateImage);
                    templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    templateG.Clear(Color.White);
                    templateG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, maxWidth, maxHeight), new System.Drawing.Rectangle(0, 0, initImage.Width, initImage.Height), System.Drawing.GraphicsUnit.Pixel);
                    templateImage.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                //原图与模版比例不等，裁剪后缩放
                else
                {
                    //裁剪对象
                    System.Drawing.Image pickedImage = null;
                    System.Drawing.Graphics pickedG = null;

                    //定位
                    Rectangle fromR = new Rectangle(0, 0, 0, 0);//原图裁剪定位
                    Rectangle toR = new Rectangle(0, 0, 0, 0);//目标定位

                    //宽为标准进行裁剪
                    if (templateRate > initRate)
                    {
                        //裁剪对象实例化
                        pickedImage = new System.Drawing.Bitmap(initImage.Width, (int)System.Math.Floor(initImage.Width / templateRate));
                        pickedG = System.Drawing.Graphics.FromImage(pickedImage);

                        //裁剪源定位
                        fromR.X = 0;
                        fromR.Y = (int)System.Math.Floor((initImage.Height - initImage.Width / templateRate) / 2);
                        fromR.Width = initImage.Width;
                        fromR.Height = (int)System.Math.Floor(initImage.Width / templateRate);

                        //裁剪目标定位
                        toR.X = 0;
                        toR.Y = 0;
                        toR.Width = initImage.Width;
                        toR.Height = (int)System.Math.Floor(initImage.Width / templateRate);
                    }
                    //高为标准进行裁剪
                    else
                    {
                        pickedImage = new System.Drawing.Bitmap((int)System.Math.Floor(initImage.Height * templateRate), initImage.Height);
                        pickedG = System.Drawing.Graphics.FromImage(pickedImage);

                        fromR.X = (int)System.Math.Floor((initImage.Width - initImage.Height * templateRate) / 2);
                        fromR.Y = 0;
                        fromR.Width = (int)System.Math.Floor(initImage.Height * templateRate);
                        fromR.Height = initImage.Height;

                        toR.X = 0;
                        toR.Y = 0;
                        toR.Width = (int)System.Math.Floor(initImage.Height * templateRate);
                        toR.Height = initImage.Height;
                    }

                    //设置质量
                    pickedG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    pickedG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    //裁剪
                    pickedG.DrawImage(initImage, toR, fromR, System.Drawing.GraphicsUnit.Pixel);

                    //按模版大小生成最终图片
                    System.Drawing.Image templateImage = new System.Drawing.Bitmap(maxWidth, maxHeight);
                    System.Drawing.Graphics templateG = System.Drawing.Graphics.FromImage(templateImage);
                    templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    templateG.Clear(Color.White);
                    templateG.DrawImage(pickedImage, new System.Drawing.Rectangle(0, 0, maxWidth, maxHeight), new System.Drawing.Rectangle(0, 0, pickedImage.Width, pickedImage.Height), System.Drawing.GraphicsUnit.Pixel);

                    //关键质量控制
                    //获取系统编码类型数组,包含了jpeg,bmp,png,gif,tiff
                    ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
                    ImageCodecInfo ici = null;
                    foreach (ImageCodecInfo i in icis)
                    {
                        if (i.MimeType == "image/jpeg" || i.MimeType == "image/bmp" || i.MimeType == "image/png" || i.MimeType == "image/gif")
                        {
                            ici = i;
                        }
                    }
                    EncoderParameters ep = new EncoderParameters(1);
                    ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality);

                    //保存缩略图
                    templateImage.Save(fileSaveUrl, ici, ep);
                    //templateImage.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);

                    //释放资源
                    templateG.Dispose();
                    templateImage.Dispose();

                    pickedG.Dispose();
                    pickedImage.Dispose();
                }
            }

            //释放资源
            initImage.Dispose();
        }
        #endregion

        #region 方法：图片等比缩放

        /// <summary>
        /// 图片等比缩放
        /// </summary>
        /// <remarks>谭光洪 2015-05-09</remarks>
        /// <param name="fromFile">原图Stream对象</param>
        /// <param name="savePath">缩略图存放地址</param>
        /// <param name="targetWidth">指定的最大宽度</param>
        /// <param name="targetHeight">指定的最大高度</param>
        /// <param name="watermarkText">水印文字(为""表示不使用水印)</param>
        /// <param name="watermarkImage">水印图片路径(为""表示不使用水印)</param>
        public static void ZoomAuto(System.IO.Stream fromFile, string savePath, System.Double targetWidth, System.Double targetHeight, string watermarkText, string watermarkImage)
        {
            //创建目录
            string dir = Path.GetDirectoryName(savePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            //原始图片（获取原始图片创建对象，并使用流中嵌入的颜色管理信息）
            System.Drawing.Image initImage = System.Drawing.Image.FromStream(fromFile, true);

            //原图宽高均小于模版，不作处理，直接保存
            if (initImage.Width <= targetWidth && initImage.Height <= targetHeight)
            {
                //文字水印
                if (watermarkText != "")
                {
                    using (System.Drawing.Graphics gWater = System.Drawing.Graphics.FromImage(initImage))
                    {
                        System.Drawing.Font fontWater = new Font("黑体", 10);
                        System.Drawing.Brush brushWater = new SolidBrush(Color.White);
                        gWater.DrawString(watermarkText, fontWater, brushWater, 10, 10);
                        gWater.Dispose();
                    }
                }

                //透明图片水印
                if (watermarkImage != "")
                {
                    if (File.Exists(watermarkImage))
                    {
                        //获取水印图片
                        using (System.Drawing.Image wrImage = System.Drawing.Image.FromFile(watermarkImage))
                        {
                            //水印绘制条件：原始图片宽高均大于或等于水印图片
                            if (initImage.Width >= wrImage.Width && initImage.Height >= wrImage.Height)
                            {
                                Graphics gWater = Graphics.FromImage(initImage);

                                //透明属性
                                ImageAttributes imgAttributes = new ImageAttributes();
                                ColorMap colorMap = new ColorMap();
                                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                                ColorMap[] remapTable = { colorMap };
                                imgAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                                float[][] colorMatrixElements = {
                                   new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  0.0f,  0.0f,  0.5f, 0.0f},//透明度:0.5
                                   new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                                };

                                ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
                                imgAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                                gWater.DrawImage(wrImage, new Rectangle(initImage.Width - wrImage.Width, initImage.Height - wrImage.Height, wrImage.Width, wrImage.Height), 0, 0, wrImage.Width, wrImage.Height, GraphicsUnit.Pixel, imgAttributes);

                                gWater.Dispose();
                            }
                            wrImage.Dispose();
                        }
                    }
                }

                //保存
                initImage.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                //缩略图宽、高计算
                double newWidth = initImage.Width;
                double newHeight = initImage.Height;

                //宽大于高或宽等于高（横图或正方）
                if (initImage.Width > initImage.Height || initImage.Width == initImage.Height)
                {
                    //如果宽大于模版
                    if (initImage.Width > targetWidth)
                    {
                        //宽按模版，高按比例缩放
                        newWidth = targetWidth;
                        newHeight = initImage.Height * (targetWidth / initImage.Width);
                    }
                }
                //高大于宽（竖图）
                else
                {
                    //如果高大于模版
                    if (initImage.Height > targetHeight)
                    {
                        //高按模版，宽按比例缩放
                        newHeight = targetHeight;
                        newWidth = initImage.Width * (targetHeight / initImage.Height);
                    }
                }

                //生成新图
                //新建一个bmp图片
                System.Drawing.Image newImage = new System.Drawing.Bitmap((int)newWidth, (int)newHeight);
                //新建一个画板
                System.Drawing.Graphics newG = System.Drawing.Graphics.FromImage(newImage);

                //设置质量
                newG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                newG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //置背景色
                newG.Clear(Color.White);
                //画图
                newG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, newImage.Width, newImage.Height), new System.Drawing.Rectangle(0, 0, initImage.Width, initImage.Height), System.Drawing.GraphicsUnit.Pixel);

                //文字水印
                if (watermarkText != "")
                {
                    using (System.Drawing.Graphics gWater = System.Drawing.Graphics.FromImage(newImage))
                    {
                        System.Drawing.Font fontWater = new Font("宋体", 10);
                        System.Drawing.Brush brushWater = new SolidBrush(Color.White);
                        gWater.DrawString(watermarkText, fontWater, brushWater, 10, 10);
                        gWater.Dispose();
                    }
                }

                //透明图片水印
                if (watermarkImage != "")
                {
                    if (File.Exists(watermarkImage))
                    {
                        //获取水印图片
                        using (System.Drawing.Image wrImage = System.Drawing.Image.FromFile(watermarkImage))
                        {
                            //水印绘制条件：原始图片宽高均大于或等于水印图片
                            if (newImage.Width >= wrImage.Width && newImage.Height >= wrImage.Height)
                            {
                                Graphics gWater = Graphics.FromImage(newImage);

                                //透明属性
                                ImageAttributes imgAttributes = new ImageAttributes();
                                ColorMap colorMap = new ColorMap();
                                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                                ColorMap[] remapTable = { colorMap };
                                imgAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                                float[][] colorMatrixElements = {
                                   new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                                   new float[] {0.0f,  0.0f,  0.0f,  0.5f, 0.0f},//透明度:0.5
                                   new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                                };

                                ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
                                imgAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                                gWater.DrawImage(wrImage, new Rectangle(newImage.Width - wrImage.Width, newImage.Height - wrImage.Height, wrImage.Width, wrImage.Height), 0, 0, wrImage.Width, wrImage.Height, GraphicsUnit.Pixel, imgAttributes);
                                gWater.Dispose();
                            }
                            wrImage.Dispose();
                        }
                    }
                }

                //保存缩略图
                newImage.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);

                //释放资源
                newG.Dispose();
                newImage.Dispose();
                initImage.Dispose();
            }
        }

        #endregion

        #region 方法：保存图片
        /// <summary>
        /// 
        /// </summary>
        /// <param name="smallImage"></param>
        /// <param name="imagePath"></param>
        /// <param name="newImageName"></param>
        /// <returns></returns>
        public static bool SaveImage(Stream smallImage, string imagePath, string newImageName)
        {
            try
            {
                Bitmap bitmap = new Bitmap(smallImage);
                string filePath = Path.Combine(imagePath, newImageName);
                bitmap.Save(filePath);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion



    }
}

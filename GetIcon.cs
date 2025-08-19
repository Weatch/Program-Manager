using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PM
{
    internal class GetIcon
    {
        public const uint SHGFI_ICON = 0x000000100;
        public const uint SHGFI_LARGEICON = 0x000000000;
        public const uint SHGFI_SMALLICON = 0x000000001;
        public const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;
        public const uint SHGFI_TYPENAME = 0x000000400;
        public const uint SHGFI_DISPLAYNAME = 0x000000200;
        
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        public void GetFileIcon(string filePath, string output)
        {
            //获取目录图标，指定目录即可
            SHFILEINFO shinfo = new SHFILEINFO();
            uint flags = SHGFI_ICON | SHGFI_LARGEICON;

            SHGetFileInfo(filePath, 0, out shinfo, (uint)Marshal.SizeOf(shinfo), flags);

            if (shinfo.hIcon != IntPtr.Zero)
            {
                Icon icon = (Icon)Icon.FromHandle(shinfo.hIcon).Clone();
                DestroyIcon(shinfo.hIcon);
                IconToIcon(icon, output);
                icon.Dispose();
            }
        }

        public void FileToIcon(string file, string output)
        {
            Icon icon = Icon.ExtractAssociatedIcon(file);
            Bitmap bitmap = icon.ToBitmap();
            using (MemoryStream memoryStream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(memoryStream))
            {
                // ICO文件头
                writer.Write((short)0); // 保留
                writer.Write((short)1); // 1=ICO, 2=CUR
                writer.Write((short)1); // 图像数量

                // 图像信息
                writer.Write((byte)bitmap.Width); // 宽度
                writer.Write((byte)bitmap.Height); // 高度
                writer.Write((byte)0); // 颜色数
                writer.Write((byte)0); // 保留
                writer.Write((short)0); // 调色板
                writer.Write((short)32); // 位每像素
                writer.Write((int)bitmap.Width * bitmap.Height * 4 + 40); // 图像数据大小
                writer.Write((int)22); // 图像数据偏移

                // 位图信息头
                writer.Write((int)40); // 信息头大小
                writer.Write((int)bitmap.Width); // 宽度
                writer.Write((int)bitmap.Height * 2); // 高度*2
                writer.Write((short)1); // 平面数
                writer.Write((short)32); // 位每像素
                writer.Write((int)0); // 压缩方式
                writer.Write((int)bitmap.Width * bitmap.Height * 4); // 图像大小
                writer.Write((int)0); // 水平分辨率
                writer.Write((int)0); // 垂直分辨率
                writer.Write((int)0); // 使用的颜色数
                writer.Write((int)0); // 重要颜色数

                // 图像数据 (ARGB转换为BGRA)
                for (int y = bitmap.Height - 1; y >= 0; y--)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        Color pixel = bitmap.GetPixel(x, y);
                        writer.Write(pixel.B);
                        writer.Write(pixel.G);
                        writer.Write(pixel.R);
                        writer.Write(pixel.A);
                    }
                }
                File.WriteAllBytes(output, memoryStream.ToArray());
                icon.Dispose();
                bitmap.Dispose();
            }
        }
        public void IconToIcon(Icon icon, string output)
        {
            Bitmap bitmap = icon.ToBitmap();
            using (MemoryStream memoryStream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(memoryStream))
            {
                // ICO文件头
                writer.Write((short)0); // 保留
                writer.Write((short)1); // 1=ICO, 2=CUR
                writer.Write((short)1); // 图像数量

                // 图像信息
                writer.Write((byte)bitmap.Width); // 宽度
                writer.Write((byte)bitmap.Height); // 高度
                writer.Write((byte)0); // 颜色数
                writer.Write((byte)0); // 保留
                writer.Write((short)0); // 调色板
                writer.Write((short)32); // 位每像素
                writer.Write((int)bitmap.Width * bitmap.Height * 4 + 40); // 图像数据大小
                writer.Write((int)22); // 图像数据偏移

                // 位图信息头
                writer.Write((int)40); // 信息头大小
                writer.Write((int)bitmap.Width); // 宽度
                writer.Write((int)bitmap.Height * 2); // 高度*2
                writer.Write((short)1); // 平面数
                writer.Write((short)32); // 位每像素
                writer.Write((int)0); // 压缩方式
                writer.Write((int)bitmap.Width * bitmap.Height * 4); // 图像大小
                writer.Write((int)0); // 水平分辨率
                writer.Write((int)0); // 垂直分辨率
                writer.Write((int)0); // 使用的颜色数
                writer.Write((int)0); // 重要颜色数

                // 图像数据 (ARGB转换为BGRA)
                for (int y = bitmap.Height - 1; y >= 0; y--)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        Color pixel = bitmap.GetPixel(x, y);
                        writer.Write(pixel.B);
                        writer.Write(pixel.G);
                        writer.Write(pixel.R);
                        writer.Write(pixel.A);
                    }
                }

                File.WriteAllBytes(output, memoryStream.ToArray());
                bitmap.Dispose();
            }
        }
        public void BmpToIcon(Bitmap bitmap, string output)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(memoryStream))
            {
                // ICO文件头
                writer.Write((short)0); // 保留
                writer.Write((short)1); // 1=ICO, 2=CUR
                writer.Write((short)1); // 图像数量

                // 图像信息
                writer.Write((byte)bitmap.Width); // 宽度
                writer.Write((byte)bitmap.Height); // 高度
                writer.Write((byte)0); // 颜色数
                writer.Write((byte)0); // 保留
                writer.Write((short)0); // 调色板
                writer.Write((short)32); // 位每像素
                writer.Write((int)bitmap.Width * bitmap.Height * 4 + 40); // 图像数据大小
                writer.Write((int)22); // 图像数据偏移

                // 位图信息头
                writer.Write((int)40); // 信息头大小
                writer.Write((int)bitmap.Width); // 宽度
                writer.Write((int)bitmap.Height * 2); // 高度*2
                writer.Write((short)1); // 平面数
                writer.Write((short)32); // 位每像素
                writer.Write((int)0); // 压缩方式
                writer.Write((int)bitmap.Width * bitmap.Height * 4); // 图像大小
                writer.Write((int)0); // 水平分辨率
                writer.Write((int)0); // 垂直分辨率
                writer.Write((int)0); // 使用的颜色数
                writer.Write((int)0); // 重要颜色数

                // 图像数据 (ARGB转换为BGRA)
                for (int y = bitmap.Height - 1; y >= 0; y--)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        Color pixel = bitmap.GetPixel(x, y);
                        writer.Write(pixel.B);
                        writer.Write(pixel.G);
                        writer.Write(pixel.R);
                        writer.Write(pixel.A);
                    }
                }

                File.WriteAllBytes(output, memoryStream.ToArray());
            }
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, out SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        [DllImport("user32.dll")]
        static extern bool DestroyIcon(IntPtr hIcon);
    }
}

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Data.SqlClient;
namespace VatLid
{
	/// <summary>
	/// Summary description for Ultil.
	/// </summary>
	public class ImgUtil
	{
		public ImgUtil()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		public static System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(string mimeType)
		{
			System.Drawing.Imaging.ImageCodecInfo [] myEncoders =
				System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();

			foreach (System.Drawing.Imaging.ImageCodecInfo myEncoder in myEncoders)
				if (myEncoder.MimeType == mimeType)
					return myEncoder;
			return null;
		}
		public static void addWatermarkText(string MarkText,int w,int h,ref Graphics picture)
		{
			int[] sizes = new int[]{16,14,12,10,8,6,4};
			Font crFont = null;
			SizeF crSize = new	SizeF();
			for (int i=0 ;i<7; i++)
			{
				crFont = new Font("arial", sizes[i]);
				crSize = picture.MeasureString(MarkText, crFont);
				if((ushort)crSize.Width < (ushort)w)
					break;
			}

			float xpos = 0;
			float ypos = 0;

			xpos = ((float)w * (float).01) + (crSize.Width / 2);
			ypos = ((float)h * (float).99) - crSize.Height;

			StringFormat StrFormat = new StringFormat();
			StrFormat.Alignment = StringAlignment.Center;

			SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0,0));
			picture.DrawString(MarkText, crFont, semiTransBrush2, xpos+1, ypos+1, StrFormat);

			SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));
			picture.DrawString(MarkText, crFont, semiTransBrush, xpos, ypos, StrFormat);


			semiTransBrush2.Dispose();
			semiTransBrush.Dispose();
		}		
	}
}

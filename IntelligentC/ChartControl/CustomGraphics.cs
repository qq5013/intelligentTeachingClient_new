#region Copyright �2005, Cristi Potlog - All Rights Reserved
/* -------------------------------------------------------------- *
 *      Copyright �2005, Cristi Potlog - All Rights reserved      *
 *                                                                *
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY *
 * OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT    *
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR    *
 * FITNESS FOR A PARTICULAR PURPOSE.                              *
 *                                                                *
 * THIS COPYRIGHT NOTICE MAY NOT BE REMOVED FROM THIS FILE.       *
 * -------------------------------------------------------------- */
#endregion Copyright �2005, Cristi Potlog - All Rights Reserved

#region References
using System;
using System.Drawing;
using System.Windows.Forms;
#endregion

namespace CristiPotlog.ChartControl
{
	/// <summary>
	/// Utility class for custom drawing procedures.
	/// </summary>
	public sealed class CustomGraphics
	{
		#region Constructor
		/// <summary>
		/// Not allow creation of instances of this class.
		/// </summary>
		private CustomGraphics()
		{
			// NOTE: remove this constructor when porting to .NET 2.0
			// this class must be declared "static sealed"
		}
		#endregion

		#region Methods
		/// <summary>
		/// Converts a HorizontalAlignment value to a StringFormat value.
		/// </summary>
		/// <param name="alignment">A HorizontalAlignment value to convert.</param>
		/// <returns>The StringFormat representation of the HorizontalAlignment value.</returns>
		public static StringFormat StringFormat(HorizontalAlignment alignment)
		{

			// Determine text format (based on TextAlignment)
			System.Drawing.StringFormat format = System.Drawing.StringFormat.GenericDefault;
			
			format.LineAlignment = StringAlignment.Center;

			switch (alignment)
			{
				case HorizontalAlignment.Left:
					format.Alignment = StringAlignment.Near;
					break;
				case HorizontalAlignment.Center:
					format.Alignment = StringAlignment.Center;
					break;
				case HorizontalAlignment.Right:
					format.Alignment = StringAlignment.Far;
					break;
			}

			return format;
		}

		/// <summary>
		/// Converts a HorizontalAlignment value to a StringFormat value.
		/// </summary>
		/// <param name="alignment">A HorizontalAlignment value to convert.</param>
		/// <returns>The StringFormat representation of the HorizontalAlignment value.</returns>
		public static StringFormat StringFormat(HorizontalAlignment alignment, bool multiline)
		{

			// Determine text format (based on TextAlignment)
			System.Drawing.StringFormat format = CustomGraphics.StringFormat(alignment);

			if (multiline)
			{
				format.LineAlignment = StringAlignment.Near;
			}

			return format;
		}

		/// <summary>
		/// Converts a ContentAlignment value to a StringFormat value.
		/// </summary>
		/// <param name="alignment">A ContentAlignment value to convert.</param>
		/// <returns>The StringFormat representation of the ContentAlignment value.</returns>
		public static StringFormat StringFormat(ContentAlignment alignment)
		{

			// Determine text format (based on TextAlignment)
			System.Drawing.StringFormat format = System.Drawing.StringFormat.GenericDefault;

			switch (alignment)
			{
				case ContentAlignment.BottomCenter:
					format.LineAlignment = StringAlignment.Far;
					format.Alignment = StringAlignment.Center;
					break;
				case ContentAlignment.BottomLeft:
					format.LineAlignment = StringAlignment.Far;
					format.Alignment = StringAlignment.Near;
					break;
				case ContentAlignment.BottomRight:
					format.LineAlignment = StringAlignment.Far;
					format.Alignment = StringAlignment.Far;
					break;						
				case ContentAlignment.MiddleCenter:
					format.LineAlignment = StringAlignment.Center;
					format.Alignment = StringAlignment.Center;
					break;
				case ContentAlignment.MiddleLeft:
					format.LineAlignment = StringAlignment.Center;
					format.Alignment = StringAlignment.Near;
					break;
				case ContentAlignment.MiddleRight:
					format.LineAlignment = StringAlignment.Center;
					format.Alignment = StringAlignment.Far;
					break;
				case ContentAlignment.TopCenter:
					format.LineAlignment = StringAlignment.Near;
					format.Alignment = StringAlignment.Center;
					break;
				case ContentAlignment.TopLeft:
					format.LineAlignment = StringAlignment.Near;
					format.Alignment = StringAlignment.Near;
					break;
				case ContentAlignment.TopRight:
					format.LineAlignment = StringAlignment.Near;
					format.Alignment = StringAlignment.Far;
					break;
			}

			format.FormatFlags |= StringFormatFlags.NoClip | StringFormatFlags.NoWrap;

			return format;
		}

		/// <summary>
		/// ...
		/// </summary>
		/// <param name="borderStyle"></param>
		/// <returns></returns>
		public static Size GetSystemBorderSize(BorderStyle borderStyle)
		{
			// offset the grid control's borders
			switch (borderStyle)
			{
				case BorderStyle.Fixed3D:
					return SystemInformation.Border3DSize;

				case BorderStyle.FixedSingle:
					return SystemInformation.BorderSize;

				default:
					return new Size(0, 0);
			}
		}

		/// <summary>
		/// ...
		/// </summary>
		/// <param name="borderStyle"></param>
		/// <returns></returns>
		public static Size GetBorderSize(BorderStyle borderStyle)
		{
			// offset the grid control's borders
			switch (borderStyle)
			{
				case BorderStyle.Fixed3D:
					return SystemInformation.Border3DSize;

				case BorderStyle.FixedSingle:
					return SystemInformation.BorderSize;

				default:
					return new Size(0, 0);
			}
		}

		/// <summary>
		/// ...
		/// </summary>
		/// <param name="borderStyle"></param>
		/// <returns></returns>
		public static Size GetBorder3DSize(Border3DStyle border3DStyle)
		{
			// offset the grid control's borders
			switch (border3DStyle)
			{
				case Border3DStyle.Bump:
				case Border3DStyle.Etched:
				case Border3DStyle.Raised:
				case Border3DStyle.Sunken:
					// get the dimensions, in pixels, of a three-dimensional (3-D) border.
					return SystemInformation.Border3DSize;

				case Border3DStyle.Flat:
				case Border3DStyle.RaisedInner:
				case Border3DStyle.RaisedOuter:
				case Border3DStyle.SunkenInner:
				case Border3DStyle.SunkenOuter:
					// get the width and height, in pixels, of a window border.  
					return SystemInformation.BorderSize;

				case Border3DStyle.Adjust:
					return new Size(0, 0);

				default:
					return new Size(0, 0);
			}
		}

		/// <summary>
		/// ...
		/// </summary>
		/// <param name="graphics"></param>
		/// <param name="rectangle"></param>
		/// <param name="borderStyle"></param>
		public static void DrawControlBorder(Graphics graphics, Rectangle rectangle, BorderStyle borderStyle)
		{
			CustomGraphics.DrawControlBorder(graphics, rectangle, borderStyle, SystemColors.WindowFrame);
		}

		/// <summary>
		/// ...
		/// </summary>
		/// <param name="graphics"></param>
		/// <param name="rectangle"></param>
		/// <param name="borderStyle"></param>
		/// <param name="borderColor"></param>
		public static void DrawControlBorder(Graphics graphics, Rectangle rectangle, BorderStyle borderStyle, Color borderColor)
		{
			switch (borderStyle)
			{
				case BorderStyle.Fixed3D:
					ControlPaint.DrawBorder3D(graphics, rectangle, Border3DStyle.Sunken, Border3DSide.All);
					break;

				case BorderStyle.FixedSingle:
					ControlPaint.DrawBorder(graphics, rectangle, borderColor, ButtonBorderStyle.Solid);
					break;
			}
		}


		public static void DrawControlBackgroundImage(Graphics graphics, Control control, Rectangle rectangle, Image backgroundImage, BackgroundImageStyle backgroundImageStyle)
		{
            if (backgroundImage == null)
            {
                // clear drawing area
                graphics.Clear(control.BackColor);
                return;
            }

			switch (backgroundImageStyle)
			{
				case BackgroundImageStyle.CenterImage:

                    // clear drawing area
                    graphics.Clear(control.BackColor);

					// perform the drawing for centered background image.
					Size imgSize = backgroundImage.Size;
					graphics.DrawImageUnscaled(backgroundImage,
													(rectangle.Width - imgSize.Width) / 2,
													(rectangle.Height - imgSize.Height) / 2);
					break;

				case BackgroundImageStyle.TileImage:

					// NOTE: don't clear drawing area
                    // (the backgrund is completely covered by the image)

					// perform the drawing for tiled background image.
					Brush textureBrush = new TextureBrush(backgroundImage);				
					try
					{
						graphics.FillRectangle(textureBrush, rectangle);
					}
					finally
					{
						textureBrush.Dispose();
					}
					break;

				case BackgroundImageStyle.StretchImage:

					// NOTE: don't clear drawing area
                    // (the backgrund is completely covered by the image)

					// perform the drawing for stretched background image.
					graphics.DrawImage(backgroundImage, rectangle);
					break;

				case BackgroundImageStyle.UnscalledImage:

                    // clear drawing area
                    graphics.Clear(control.BackColor);

					// perform the drawing for centered background image.
					graphics.DrawImageUnscaled(backgroundImage, rectangle);
					break;
			}
		}

		private static void ClearBackground(Graphics graphics, Control control, Rectangle rectangle)
		{
			Brush backBrush = new SolidBrush(control.BackColor);
			try
			{
				graphics.FillRectangle(backBrush, rectangle);
			}
			finally
			{
				backBrush.Dispose();
			}
		}

		#endregion
	}
}

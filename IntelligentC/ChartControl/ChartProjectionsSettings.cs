#region Copyright ©2005, Cristi Potlog - All Rights Reserved
/* -------------------------------------------------------------- *
 *      Copyright ©2005, Cristi Potlog - All Rights reserved      *
 *                                                                *
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY *
 * OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT    *
 * LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR    *
 * FITNESS FOR A PARTICULAR PURPOSE.                              *
 *                                                                *
 * THIS COPYRIGHT NOTICE MAY NOT BE REMOVED FROM THIS FILE.       *
 * -------------------------------------------------------------- */
#endregion Copyright ©2005, Cristi Potlog - All Rights Reserved

#region References
using System;
using System.ComponentModel;
using System.Windows.Forms;
#endregion

namespace CristiPotlog.ChartControl
{
	/// <summary>
	/// Represents the display setting for the projections of the chart control.
	/// </summary>
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ChartProjectionsSettings
	{
		#region Consts
		private const bool defaultVisible = false;
		#endregion

		#region Fields
		private ChartLineSettings line = null;
		private ChartLabelsSettings labels = null;
		private bool visible = ChartProjectionsSettings.defaultVisible;
		private Control owner = null;
		#endregion

		#region Constructor
		/// <summary>
		/// Creates a new instance of class ChartProjectionsSettings.
		/// </summary>
		/// <param name="owner">A Control object representing the owner chart.</param>
		internal ChartProjectionsSettings(Control owner)
		{
			this.owner = owner;
			// init fields
			this.line = new ChartLineSettings(owner);
			this.labels = new ChartLabelsSettings(owner);
		}

		#endregion

		#region Properties
		/// <summary>
		/// Determines the display settings for the line of the projection.
		/// </summary>
		[Description("Determines the display settings for the line of the projection.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ChartLineSettings Line
		{
			get
			{
				return this.line;
			}
		}

		/// <summary>
		/// Determines the display settings for the labels of the projection.
		/// </summary>
		[Description("Determines the display settings for the labels of the projection.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ChartLabelsSettings Labels
		{
			get
			{
				return this.labels;
			}
		}

		/// <summary>
		/// Indicates if the chart marks projections are shown.
		/// </summary>
		[DefaultValue(ChartProjectionsSettings.defaultVisible)]
		[Description("Indicates if the chart marks projections are shown.")]
		public bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				if (this.visible != value)
				{
					this.visible = value;
					this.owner.Invalidate();
				}
			}
		}

		#endregion

		#region Methods
		/// <summary>
		/// Returns a string that represents the current object.  
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			return "(" + this.GetType().Name + ")";
		}

		#endregion
	}
}
